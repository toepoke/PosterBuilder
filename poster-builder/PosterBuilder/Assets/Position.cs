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
	public class Position {

		/// <summary>
		/// Specifies what font combination (font name, size, etc) should be used when drawing
		/// the dimensions of a point or rectangle on the drawing area.
		/// </summary>
		/// <remarks>
		/// You can opt to have guides added to your template showing where
		/// you assets are being draw, which can help with positioning.  As part of these guides
		/// you can also output the position defined for the asset (caption, map, etc), in which case
		/// the position is drawn with the DIMENSIONS font.
		/// </remarks>
		public static Typeface DIMENSIONS = new Typeface("Courier New").FontSize(10);


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		public Position(Graphics gdi) {
			_ID = "";
			this.X = 0;
			this.Y = 0;
			this.Canvas = gdi;

			if (this.Canvas == null)
				throw new Exception("GDI graphics object cannot be null.");
		}


		/// <summary>
		/// Detailed constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		public Position(Graphics gdi, string id) : this(gdi) {
			_ID = id;
		}

		
		/// <summary>
		/// Detailed constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public Position(Graphics gdi, string id, int x, int y) : this(gdi) {
			_ID = id;
			this.TopLeft(x, y);
		}

		
		/// <summary>
		/// Detailed constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		/// <param name="coords"></param>
		public Position(Graphics gdi, string id, int[] coords) : this(gdi) {
			_ID = id;
			this.TopLeft(coords);
		}

		
		/// <summary>
		/// Copy constructor: Initialises this object from the copy
		/// </summary>
		/// <param name="copy">Object to make a copy from</param>
		public Position(Position copy) {
			this.Canvas = copy.Canvas;
			this._ID = copy._ID;
			this.X = copy.X;
			this.Y = copy.Y;
		}


		/// <summary>
		/// The <see cref="Graphics"/> object allocated to the poster for drawing on, this a 
		/// reference to that object for this object to draw onto.
		/// </summary>
		protected Graphics Canvas { get; private set; }
		
		/// <summary>ID of the caption being defined (optional, useful for debugging).</summary>
		protected internal string _ID { get; set; }
		
		/// <summary>X co-ordinate.</summary>
		public int X { get; set; }
		
		/// <summary>Y co-ordinate.</summary>
		public int Y { get; set; }

		
		/// <summary>
		/// An ID can be allocated to an asset that will be drawn on the template, which can be useful for positioning
		/// where your assets are drawn.
		/// </summary>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		public Position ID(string id) {
			_ID = id;
			return this;
		}


		/// <summary>
		/// Where on the template an asset should be drawn.
		/// </summary>
		/// <param name="x">X co-ordinate</param>
		/// <param name="y">Y co-ordinate</param>
		public Position TopLeft(int x, int y) {
			this.X = x;
			this.Y = y;
			return this;
		} // Rect


		/// <summary>
		/// Where on the template an asset should be drawn.
		/// </summary>
		/// <param name="coord">X co-ordinate (index 0) and Y co-ordinate (index 1)</param>
		public Position TopLeft(int[] coord) {
			if (coord.Length != 2)
				throw new ArgumentException("coord takes X and Y in the array", "coord");
			return this.TopLeft(coord[0], coord[1]);
		}


		/// <summary>
		/// Where on the template an asset should be drawn.
		/// </summary>
		/// <param name="coord">X,Y co-ordinate</param>
		public Position TopLeft(Position coord) {
			this._ID = coord._ID;
			this.X = coord.X;
			this.Y = coord.Y;
			return this;
		}

		/// <summary>
		/// Validates that the area properties are correctly defined.
		/// </summary>
		/// <remarks>
		/// The Position object has no validation rules as yet.
		/// </remarks>
		virtual protected internal void Validate() {
			// nothing to see here	
		} // Validate


		/// <summary>
		/// Method to be overridden instructing an asset object how to be drawn.
		/// </summary>
		virtual protected internal void Render() {
			// Nothing to see here
		} // Render


		/// <summary>
		/// Method to be overriden instructing the asset object how to draw guides.
		/// </summary>
		/// <param name="drawDimensions"></param>
		virtual public void DrawGuides(bool drawDimensions) {
			// Nothing to see here
		} // DrawGuides


		/// <summary>
		/// Converts the Position object into a GDI Point object used for the actual drawing.
		/// </summary>
		/// <returns>
		/// Converted GDI Point object
		/// </returns>
		public Point ToPoint() {
			return new Point(this.X, this.Y);
		}

	} // Area

} // PosterBuilder
