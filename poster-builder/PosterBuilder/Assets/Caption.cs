using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Text;

namespace PosterBuilder.Assets {

	/// <summary>
	/// Used for drawing text (in a given font, colour, size, etc) onto the template.
	/// 
	/// Note a great deal of the properties in this class return the class itself.
	/// This is to employ a fluent interface to specifying the properties.
	/// </summary>
	public class Caption: Area {
			
		/// <summary>
		/// Note setting this will affect _all_ usage (so any posters generated after yours will inherit this setting
		/// </summary>
		public static StringAlignment DEFAULT_HORIZONTAL_ALIGNMENT = StringAlignment.Near;

		/// <summary>
		/// Note setting this will affect _all_ usage (so any posters generated after yours will inherit this setting
		/// </summary>
		public static StringAlignment DEFAULT_VERTICAL_ALIGNMENT = StringAlignment.Near;


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		public Caption(Graphics gdi) : base(gdi, "") {
			_Text = "";
			_HorizontalAlignment = DEFAULT_HORIZONTAL_ALIGNMENT;
			_VerticalAlignment = DEFAULT_VERTICAL_ALIGNMENT;
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		/// <param name="text">
		/// Text to be drawn on the caption area.
		/// </param>
		public Caption(Graphics gdi, string text) : base(gdi, "") {
			_Text = text;
			_HorizontalAlignment = DEFAULT_HORIZONTAL_ALIGNMENT;
			_VerticalAlignment = DEFAULT_VERTICAL_ALIGNMENT;
		}
		

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		/// <param name="text">
		/// Text to be drawn on the caption area.
		/// </param>
		public Caption(Graphics gdi, string id, string text) : base(gdi, id) { 
			_Text = text; 
			_HorizontalAlignment = DEFAULT_HORIZONTAL_ALIGNMENT;
			_VerticalAlignment = DEFAULT_VERTICAL_ALIGNMENT;
		}

		
		/// <summary>
		/// Copy constructor: Initialises this object from the copy
		/// </summary>
		/// <param name="copy">Object to make a copy from</param>
		public Caption(Caption copy) : base(copy) {
			_Text = copy._Text;
			_HorizontalAlignment = copy._HorizontalAlignment;
			_VerticalAlignment = copy._VerticalAlignment;
			_Typeface = copy._Typeface;
		}

		/// <summary>Caption to draw in the given Rectangle.</summary>
		protected internal string _Text {get; set; }

		/// <summary>Font parameters to use when drawing the content</summary>
		protected internal Typeface _Typeface {get; set; }

		/// <summary>How the Caption should be aligned horizontally.</summary>
		protected internal StringAlignment _HorizontalAlignment { get; set; }

		/// <summary>How the Caption should be aligned vertically.</summary>
		protected internal StringAlignment _VerticalAlignment { get; set; }

		/// <summary>
		/// Sets the text to be drawn on the caption.
		/// </summary>
		/// <param name="text">Text to be drawn</param>
		public Caption Text(string text) {
			_Text = text;
			return this;
		}


		/// <summary>
		/// An ID can be allocated to an asset that will be drawn on the template, which can be useful for positioning
		/// where your assets are drawn.
		/// </summary>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		new public Caption ID(string id) {
			return (Caption) base.ID(id);
		}


		/// <summary>
		/// Where on the template an asset should be drawn.
		/// </summary>
		/// <param name="xy">X co-ordinate (index 0) and Y co-ordinate (index 1)</param>
		new public Caption TopLeft(int[] xy) {
			this.TopLeft(xy[0], xy[1]);
			return this;
		} // Rect


		/// <summary>
		/// Where on the template an asset should be drawn.
		/// </summary>
		/// <param name="x">X co-ordinate</param>
		/// <param name="y">Y co-ordinate</param>
		new public Caption TopLeft(int x, int y) {
			base.TopLeft(x, y);
			this.Width = 0;
			this.Height = 0;
			return this;
		} // Rect


		/// <summary>
		/// Where on the template an asset should be drawn.
		/// </summary>
		/// <param name="xy">X,Y co-ordinate</param>
		new public Caption TopLeft(Position xy) {
			this.TopLeft( xy.X, xy.Y );
			return this;
		} // Rect


		/// <summary>
		/// Specifies where the area is to be placed on the template.
		/// </summary>
		/// <param name="rect">Rectangle where the asset is to be placed on the template.</param>
		new public Caption Rect(int[] rect) {
			base.Rect(rect);
			return this;
		}


		/// <summary>
		/// Specifies where the area is to be placed on the template
		/// </summary>
		/// <param name="x">X co-ordinate on the template where the area should be placed.</param>
		/// <param name="y">Y co-ordinate on the template where the area should be placed.</param>
		/// <param name="width">Width of the area on the template</param>
		/// <param name="height">Height of the area on the template</param>
		new public Caption Rect(int x, int y, int width, int height) {
			base.Rect(x, y, width, height);
			return this;
		}


		/// <summary>
		/// Specifies where the area is to be placed on the template.
		/// </summary>
		/// <param name="rect">Rectangle where the asset is to be placed on the template.</param>
		new public Caption Rect(Area rect) {
			base.Rect(rect);
			return this;
		}


		/// <summary>
		/// Sets the typeface to be used when drawing the text.
		/// </summary>
		/// <param name="typeface">Typeface to use</param>
		public Caption Typeface(Typeface typeface) {
			_Typeface = typeface;
			return this;
		}


		/// <summary>
		/// Specifies how the text should be aligned horizontally.
		/// </summary>
		/// <param name="horizontal">How to align</param>
		/// <remarks>
		/// Note alignment is only relevant when width and height are also specified.
		/// </remarks>
		public Caption HorizontalAlignment(StringAlignment horizontal) {
			_HorizontalAlignment = horizontal;
			return this;
		}


		/// <summary>
		/// Specifies how the text should be aligned vertically.
		/// </summary>
		/// <param name="vertical">How to align</param>
		/// <remarks>
		/// Note alignment is only relevant when width and height are also specified.
		/// </remarks>
		public Caption VerticalAlignment(StringAlignment vertical) {
			_VerticalAlignment = vertical;
			return this;
		}


		/// <summary>
		/// Converts the horizontal and vertical alignment properties into 
		/// a GDI object for drawing with.
		/// </summary>
		/// <returns>
		/// GDI alignment object
		/// </returns>
		public StringFormat ToStringFormat() {
			return new StringFormat { 
				Alignment = _HorizontalAlignment,
				LineAlignment = _VerticalAlignment
			};
		}


		/// <summary>
		/// Specifies if the rectangle should be calculated, i.e. if the width/height isn't specified
		/// we calculate what it should be based on the size of the text being drawn.
		/// If the width/height is specified, then we need to create a rectangle on the supplied
		/// x, y, width and height.  
		/// 
		/// The reason for this difference is when you want to align objects left, centre or right because
		/// without the full co-ordinates it won't make any difference (i.e. if we only have x,y we'll
		/// calculate the minimum width required for the text so aligning vertically will make no difference).
		/// </summary>
		/// <returns>
		/// Returns true if no width/height has been specified (and the width/height needs calculating).
		/// Returns false if the width/height has been specified.
		/// </returns>
		protected internal bool CalculateSize() {
			return (this.Width == 0 && this.Height == 0);
		}


		/// <summary>
		/// Ensures the data in the caption object is valid
		/// </summary>
		new public void Validate() {
			base.Validate();

			if (string.IsNullOrEmpty(this._Text))
				throw new ArgumentException("No Text has been defined.");

		} // Validate


		/// <summary>
		/// Draws a caption in the correct location on the source image (as defined in the Area).
		/// </summary>
		protected internal override void Render() {
			StringFormat sf = null; Font f = null; Brush b = null; Pen p = null; Rectangle r;
			Typeface tf = this._Typeface;

			// Work out whether to use the font options from the area or the poster
			if (tf == null)
				// no typeface specified in the area, so just the default for the poster 
				// ... by newing up an instance and taking the defaults
				tf = new Typeface();

			// Set-up the parameters for the caption
			sf = this.ToStringFormat();

			// Set-up the required font
			f = tf.GetFont();

			// Set-up the area on the image where the Caption will be drawn
			if (this.CalculateSize()) {
				// Use the X,Y co-ordinates, and measuring the size of the string being drawn
				SizeF stringSize = base.Canvas.MeasureString(this._Text, f);
				r = new Rectangle(this.X, this.Y, (int)Math.Ceiling(stringSize.Width), (int)Math.Ceiling(stringSize.Height) );
			
			} else {
				// Use the dimensions provided
				r = new Rectangle(this.X, this.Y, this.Width, this.Height);

			}

			// Set-up the colour of the Caption
			b = tf.GetBrushColour(); 

			// Set-up the colour of the Pen
			p = new Pen(b);

			// and finally draw the Caption !
			this.Canvas.DrawString(this._Text, f, b, r, this.ToStringFormat());
			
			this.Canvas.ResetTransform();

			sf.Dispose();
			f.Dispose();
			b.Dispose();
			p.Dispose();

		} // Render
		
	} // Caption

} // GDI.Assets