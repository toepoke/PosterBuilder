using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Text;

namespace PosterBuilder {

	/// <summary>
	/// A class for taking an existing designed image and adding text dynamically onto the surface.
	/// So in essence you start with a template image and draw on details according to your user's 
	/// account.  The user can then download a version of your template image (with your branding) but
	/// configured with details applicable to the user's account.
	/// 
	/// For instance you may have a pretty invoice with your branding on it, you can then add the 
	/// order details of your user and provide them with a branded download invoice.
	/// </summary>
	public abstract partial class Builder: IDisposable {
	
		/// <summary>
		/// Constant for referring to the full sized image 
		/// (i.e. 100% of the size is the same size as the template)
		/// </summary>
		public const int FullSize = 100;
		
		/// <summary>
		/// Constructor, note that the input template filename is mandatory.
		/// </summary>
		/// <param name="filename">Template filename (including page) to use as a basis for the drawing</param>
		public Builder(string filename) {
			this.Filename = filename;
			this.PercentSize = 100;

			CreateGraphicsObjects();
			this.AreasOfInterest = new List<Assets.Position>();
		}


		/// <summary>
		/// Ensures resources used by the object are cleared up.
		/// </summary>
		public void Dispose()
		{
			if (this.GDI != null) 
				this.GDI.Dispose();
			if (this.Canvas != null) 
				this.Canvas.Dispose();

		} // Dispose


		/// <summary>Set of captions to draw (with co-ordinates, colours, fonts, etc to draw with)</summary>
		protected internal List<Assets.Position> AreasOfInterest { get; set; }

		/// <summary>Entry point for defining what and where should be drawn.</summary>
		protected abstract void RegisterAreasOfInterest();

		/// <summary>Filename to use as the source image to play with.</summary>
		public string Filename { get; private set; }


		/// <summary>
		/// Size of the rendered image (in relation to the original image) as a percentage 
		/// (so by default it's 100%, i.e. same size as the original).  Adjust the percentage as
		/// required (so to reduce to half the size use 50%, to double the size use 200%).
		/// </summary>
		public int PercentSize { get; set; }

		/// <summary>
		/// If true, draws a border where the rectangle in an Area is set 
		///		(useful for working out where your Captions will be placed).
		///	If false, no border is drawn
		/// </summary>
		public bool ShowGuides { get; set; }


		/// <summary>
		/// If true, draws the dimensions taken up by an area being drawn on, so the
		/// defined areas can be seen in context.
		/// If false, dimensions are not drawn
		/// </summary>
		public bool ShowDimensions { get; set; }


		/// <summary>For validating our inputs before we try and render a new image.</summary>
		public void Validate() {
			if (string.IsNullOrEmpty(this.Filename)) 
				throw new ArgumentException("No filename has been supplied.");
		
			// If we've got any Areas defined Validate them to
			// ... you don't have to have an area, you may just be re-sizing
			this.AreasOfInterest.ForEach( position => { position.Validate(); } );
			
		} // Validate


		private void CreateGraphicsObjects() {
			// Load the template image
			Bitmap srcImage = (Bitmap)Bitmap.FromFile( this.Filename );

			// Copy the template into a new Bitmap [canvas] for drawing on top of
			// ... if you don't copy you get random "An error occurred in the GDI+" error messages
			this.Canvas = new Bitmap(srcImage);
			this.GDI = Graphics.FromImage(this.Canvas);
			srcImage.Dispose();

		} // CreateGraphicsObjects


		/// <summary>Draws all the captions on the canvas.</summary>
		public Bitmap Render() {
					
			// Give the caller the chance to set-up their drawings
			this.RegisterAreasOfInterest();

			// Ensure the parameters we've been given are OK (including the defined Areas from above)
			this.Validate();

			// Add in all the defined assets 
			// ... (just loop over all of them and get each object to render itself onto the template)
			this.AreasOfInterest.ForEach( 
				position => {
					position.Render();
					if (this.ShowGuides) 
						position.DrawGuides(this.ShowDimensions);
				} 
			);

			if (this.PercentSize != Builder.FullSize) {
				// image needs adjustment
				this.ResizeImage();
			}

			return this.Canvas;

		} // Render


		/// <summary>
		/// Creates a filename for writing the user poster to the HttpResponse (i.e. the filename the user will see
		/// when prompted to download their poster).
		/// </summary>
		/// <param name="outputFilename">Start of the filename, e.g. "my-toepoke-poster"</param>
		/// <param name="outputFormat">The output format to be used, e.g. "jpeg" or "png"</param>
		/// <param name="avoidCaching">
		/// If true a hash is appended to the filename to reduce the chances of poster being cached 
		///		(so if the user makes another poster they will get a fresh version rather than a cached version)
		///	If false no hash is appended.
		/// </param>
		/// <returns>The build filename</returns>
		/// <remarks>
		/// You most probably want <paramref name="avoidCaching"/> to be true, otherwise the user will continually
		/// download the same poster, no matter how many changes they make.
		/// </remarks>
		public string BuildFilename(string outputFilename, ImgFormat.SupportedTypes outputFormat, bool avoidCaching = true) {
			string filename = outputFilename;
			string ext = ImgFormat.ToFileExtension(outputFormat);

			// add the correct file extension (to ensure the correct application opens the file on the Client's browser)
			filename = System.IO.Path.Combine(outputFilename, ext);

			return filename;

		} // BuildFilename


		/// <summary>
		/// Holds a GDI library object used when drawing onto the template (copy).
		/// </summary>
		/// <remarks>
		/// Note this is passed to all the assets (captions, maps, etc) so they all share
		/// the same graphics object
		/// </remarks>
		protected Graphics GDI { get; set; }

		/// <summary>
		/// Holds the actual bitmap copy of the template which we draw onto.
		/// </summary>
		protected Bitmap Canvas { get; set; }


		/// <summary>
		/// If required resizes the image using the <see cref="PercentSize"/> property.
		/// </summary>
		protected void ResizeImage() {
			if (this.PercentSize == Builder.FullSize)
				// no re-size required as the output is the same as what we already have
				return;

			float percent = (float)(this.PercentSize / 100.0f);
			int newWidth = (int)(this.Canvas.Width * percent);
			int newHeight = (int)(this.Canvas.Height * percent);

			//this.Canvas = (Bitmap)this.Canvas.GetThumbnailImage(newWidth, newHeight, GetThumbnailCB, IntPtr.Zero);
			this.Canvas = Helpers.GDI.GetThumbnail(this.Canvas, newWidth, newHeight);

		} // ResizeImage


		/// <summary>
		/// Apparently we have to supply a call-back to "GetThumbnailImage" even though it's not used!?!!
		/// </summary>
		protected bool GetThumbnailCB() {
			return false;
		} // GetThumbnailCB


	} // PosterBuilder

} // GDI