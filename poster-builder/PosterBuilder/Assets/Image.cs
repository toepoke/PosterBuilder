using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace PosterBuilder.Assets {

	/// <summary>
	/// Used for drawing an existing image onto the template image.  
	/// 
	/// Note a great deal of the properties in this class return the class itself.
	/// This is to employ a fluent interface to specifying the properties.
	/// </summary>
	public class Image: Position {
			
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		public Image(Graphics gdi) : base(gdi, "") {
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		public Image(Graphics gdi, string id) : base(gdi, id) {
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		/// <param name="imagePath">Path of the image to add onto the template</param>
		public Image(Graphics gdi, string id, string imagePath) : base(gdi, id) { 
			_ImagePath = imagePath;
			_ImageStream = null;
			_Drawing = null;
		}

		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		/// <param name="strm">(bitmap) Stream to add onto the template</param>
		public Image(Graphics gdi, string id, Stream strm) : base(gdi, id) {
			_ImageStream = strm;
			_ImagePath = "";
			_Drawing = null;
		}

		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		/// <param name="drawing">GDI Image object to add onto the template</param>
		public Image(Graphics gdi, string id, System.Drawing.Image drawing) : base(gdi, id) {
			_ImageStream = null;
			_ImagePath = "";
			_Drawing = drawing;
		}


		/// <summary>
		/// Copy constructor: Initialises this object from the copy
		/// </summary>
		/// <param name="copy">Object to make a copy from</param>
		public Image(Image copy) : base(copy) {
			_ImageStream = copy._ImageStream;
			_ImagePath = copy._ImagePath;
			_Drawing = copy._Drawing;
		}


		/// <summary>
		/// Path to an image to add onto the template
		/// </summary>
		protected internal string _ImagePath { get; set; }


		/// <summary>
		/// Image stream to add onto the template
		/// </summary>
		protected internal System.IO.Stream _ImageStream { get; set; }


		/// <summary>
		/// GDI Image object to add onto the template
		/// </summary>
		protected internal System.Drawing.Image _Drawing { get; set; }


		/// <summary>
		/// Path to an image to add onto the template
		/// </summary>
		/// <param name="imgPath">Path to the image</param>
		public Image ImagePath(string imgPath) {
			_ImagePath = imgPath;
			return this;
		}


		/// <summary>
		/// (Bitmap) Stream to add onto the template
		/// </summary>
		/// <param name="strm">Stream to add (typically a stream of another image loaded by other means)</param>
		/// <returns></returns>
		public Image ImageStream(Stream strm) {
			_ImageStream = strm;
			return this;
		}


		/// <summary>
		/// Adds a GDI drawing object onto the template
		/// </summary>
		/// <param name="drawing">GDI Image object to add</param>
		public Image GDIImage(System.Drawing.Image drawing) {
			_Drawing = drawing;
			return this;
		}


		/// <summary>ID of the caption being defined (optional, useful for debugging).</summary>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		new public Image ID(string id) {
			return (Image) base.ID(id);
		}
		

		/// <summary>
		/// Where on the template an asset should be drawn.
		/// </summary>
		/// <param name="x">X co-ordinate</param>
		/// <param name="y">Y co-ordinate</param>
		new public Image TopLeft(int x, int y) {
			base.TopLeft(x, y);
			return this;
		} 


		/// <summary>
		/// Where on the template an asset should be drawn.
		/// </summary>
		/// <param name="coord">X co-ordinate (index 0) and Y co-ordinate (index 1)</param>
		new public Image TopLeft(int[] coord) {
			base.TopLeft(coord);
			return this;
		}


		/// <summary>
		/// Where on the template an asset should be drawn.
		/// </summary>
		/// <param name="position">X,Y co-ordinate</param>
		new public Image TopLeft(Position position) {
			base.TopLeft(position);
			return this;
		}


		/// <summary>
		/// Creates a copy of the provided source image (either from the path, stream or GDI Image object)
		/// and returns it ready for adding onto the template.
		/// </summary>
		/// <returns>
		/// GDI Image object with a copy of the source image
		/// </returns>
		virtual protected internal System.Drawing.Image GetImage() {
			System.Drawing.Image bitmap = null;

			if (this.UseDrawing()) {
				// nothing to do, just pass it back as is
				bitmap = this._Drawing;
			} 
			else if (this.UseImagePath()) {
				// load the external file
				bitmap = new Bitmap(this._ImagePath);
			}
			else if (this.UseImageStream()) {
				// load through the stream
				bitmap = new Bitmap(this._ImageStream);
			} 

			return bitmap;
		}


		/// <summary>
		/// Responsible for drawing the image [copy] onto the canvas (i.e. the copy of the
		/// original template)
		/// </summary>
		protected internal override void Render() {
			System.Drawing.Image img = this.GetImage();

			this.Canvas.DrawImage(img, this.X, this.Y);

			img.Dispose();
		
		} // Render


		/// <summary>
		/// Ensures that the properties defined for the image are in a valid populated state.
		/// For example we need at least an image file, stream or GDI Image object to work with.
		/// </summary>
		new public void Validate() {
			base.Validate();

			if (this.UseImagePath() && this.UseImageStream())
				throw new ArgumentException("Both ImagePath and ImageStream are set, I don't know which one I should use!");

			if (!this.UseImagePath() && !this.UseImageStream())
				throw new ArgumentException("Neither ImagePath and ImageStream are set.");
		
		} // Validate


		/// <summary>
		/// Flags that we should render using an existing image on the file-system.
		/// </summary>
		/// <returns>
		/// Returns true if the file-system should be used for making an image copy.
		/// Returns false otherwise.
		/// </returns>
		protected bool UseImagePath() {
			return !string.IsNullOrEmpty(this._ImagePath);
		
		} // UseImagePath


		/// <summary>
		/// Flags that we should render using a (bitmap) stream.
		/// </summary>
		/// <returns>
		/// Returns true if the stream should be used for making an image copy.
		/// Returns false otherwise.
		/// </returns>
		protected bool UseImageStream() {
			return (this._ImageStream != null);
		
		} // UseImageStream


		/// <summary>
		/// Flags that we should render using a provided copy of a GDI Image object.
		/// </summary>
		/// <returns>
		/// Returns true if the GDI Image should be used for rendering.
		/// Returns false otherwise.
		/// </returns>
		protected bool UseDrawing() {
			return (this._Drawing != null);

		} // UseDrawing
		
	} // Caption

} // GDI.Assets