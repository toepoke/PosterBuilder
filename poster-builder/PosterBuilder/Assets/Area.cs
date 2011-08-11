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
	/// Class for defining how a piece of text is to be draw on the poster, so what the text will be, where on the
	/// page it should be drawn, etc.
	/// 
	/// Note a great deal of the properties in this class return the class itself.
	/// This is to employ a fluent interface to specifying the properties.
	/// </summary>
	public class Area: Position {

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		public Area(Graphics gdi) : base(gdi) {
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		public Area(Graphics gdi, string id) : base(gdi, id) {
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		/// <param name="x">X co-ordinate where the area is being placed on the template.</param>
		/// <param name="y">Y co-ordinate where the area is being placed on the template.</param>
		/// <param name="width">Width of the area on the the template.</param>
		/// <param name="height">Height of the area on the the template.</param>
		public Area(Graphics gdi, string id, int x, int y, int width, int height) : base(gdi, id) {
			this.Rect(x, y, width, height);
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		/// <param name="rect">
		/// Properties of the area, i.e. X (index 0), Y (index 1) co-ordinate of the area.
		/// Width (index 2) of the area and Height (index 3) of the area on the template.
		/// </param>
		public Area(Graphics gdi, string id, int[] rect) : base(gdi, id) {
			this.Rect(rect);
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		/// <param name="rect">Rectangle where the asset is to be placed on the template.</param>
		public Area(Graphics gdi, string id, Area rect) : base(gdi, id) {
			this._ID = id;
			this.Rect(rect);
		}


		/// <summary>
		/// Copy constructor: Initialises this object from the copy
		/// </summary>
		/// <param name="copy">Object to make a copy from</param>
		public Area(Area copy) : base(copy) {
			this.Width = copy.Width;
			this.Height = copy.Height;
		}

		/// <summary>Width of the area.</summary>
		public int Width { get; set; }

		/// <summary>Shortcut to the Height co-ordinate.</summary>
		public int Height { get; set; }


		/// <summary>ID of the caption being defined (optional, useful for debugging).</summary>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		new public Area ID(string id) {
			_ID = id;
			return this;
		}


		/// <summary>
		/// Specifies where the area is to be placed on the template.
		/// </summary>
		/// <param name="rect">Rectangle where the asset is to be placed on the template.</param>
		public Area Rect(int[] rect) {
			if (rect.Length != 4)
				throw new ArgumentException("Rectangle must have x, y, width and height arguments (in the array).");

			return this.Rect(rect[0], rect[1], rect[2], rect[3]);
		} // Rect


		/// <summary>
		/// Specifies where the area is to be placed on the template
		/// </summary>
		/// <param name="x">X co-ordinate on the template where the area should be placed.</param>
		/// <param name="y">Y co-ordinate on the template where the area should be placed.</param>
		/// <param name="width">Width of the area on the template</param>
		/// <param name="height">Height of the area on the template</param>
		public Area Rect(int x, int y, int width, int height) {
			base.X = x;
			base.Y = y;
			this.Width = width;
			this.Height = height;

			return this;
		} // Rect


		/// <summary>
		/// Specifies where the area is to be placed on the template.
		/// </summary>
		/// <param name="rect">Rectangle where the asset is to be placed on the template.</param>
		public Area Rect(Area rect) {
			this.Rect(rect.X, rect.Y, rect.Width, rect.Height);
			return this;
		}


		/// <summary>
		/// Converts the Area object into a GDI Rectangle object used for the actual drawing
		/// </summary>
		/// <returns>
		/// Converted GDI Rectangle object
		/// </returns>
		public Rectangle ToRectangle() {
			return new Rectangle(this.X, this.Y, this.Width, this.Height);
		}


		/// <summary>
		/// Draws a border around the defined area onto the template.  This is useful for being able 
		/// to visualise where a particular asset is being placed on the page.
		/// </summary>
		/// <param name="drawDimensions">
		/// Flags whether the dimensions of an area should also be shown (as well as the border)
		///		If true the dimensions of the area will be placed at the top-left position.
		///		If false only the border is drawn.
		/// </param>
		public override void DrawGuides(bool drawDimensions) {
			// add a border where the rectangle is to aid placement 
			// ... (fill with White and draw the dimensions in Black so we know we'll be able to read them)
			this.Canvas.DrawRectangle(Pens.Black, new Rectangle(X, Y, Width, Height) );

			if (drawDimensions) {
				// Write out the dimensions as well
				string dimensions = "";
				if (!string.IsNullOrEmpty(base._ID))
					dimensions = base._ID + ": ";

				dimensions += string.Format("x={0},y={1},w={2},h={3}", X, Y, Width, Height);

				// Fill the area where we put the dimensions with White so we know the Blank ink will be visible
				// ... (just where the writing goes, otherwise we'll overwrite the actual content of the box)
				Font dimsFont = Position.DIMENSIONS.GetFont();
				SizeF dimsSize = this.Canvas.MeasureString(dimensions, dimsFont);
				this.Canvas.FillRectangle(Brushes.White, X, Y, dimsSize.Width, dimsSize.Height );

				// ... add draw the dimensions (we add 2 onto the x/y so we don't draw on the border)
				Rectangle dimsRect = new Rectangle(X+2, Y+2, (int)Math.Ceiling(dimsSize.Width+2), (int)Math.Ceiling(dimsSize.Height+2));
				this.Canvas.DrawString( dimensions, dimsFont, Brushes.Black, X+2, Y+2);
			}

		} // DrawGuides

	} // Area

} // PosterBuilder
