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
		/// Class to define what rendering attributes to use when drawing a piece of text on the
		/// poster, e.g. the font, colour, size, bold, underlined, etc.
		/// </summary>
		public class Typeface {
		
			/// <summary>
			/// Name of the font to use when drawing text on _ALL_ assets.  Therefore if you change this
			/// any subsequent assets will use the new value (unless a specific font is allocated to the asset).
			/// </summary>
			public static string DEFAULT_FONT_NAME = "Trebuchet MS";

			/// <summary>
			/// Size of the font to use when drawing text on _ALL_ assets.  Therefore if you changes this
			/// any subsequent assets will use the new value (unless a specific font size is allocated to the asset).
			/// </summary>
			public static float  DEFAULT_FONT_SIZE = 50f;

			/// <summary>
			/// Colour of the font to use when drawing text on _ALL_ assets.  Therefore if you changes this
			/// any subsequent assets will use the new value (unless a specific font colour is allocated to the asset).
			/// </summary>
			public static string DEFAULT_HEX_COLOUR = "#000000";


			/// <summary>
			/// Default constructor, adopts the defaults outlined above.
			/// </summary>
			public Typeface() {
				_FontName = DEFAULT_FONT_NAME;
				_FontSize = DEFAULT_FONT_SIZE;
				_FontColour = DEFAULT_HEX_COLOUR;
			}


			/// <summary>
			/// Detailed constructor, adopts the defaults above except for the given font name.
			/// </summary>
			/// <param name="fontName">Name of the font to use.</param>
			public Typeface(string fontName) : this() {
				_FontName = fontName;
			}


			/// <summary>
			/// Copy constructor: Initialises this object from the copy
			/// </summary>
			/// <param name="copy">Object to make a copy from</param>
			public Typeface(Typeface copy) : this() {
				_FontName = copy._FontName;
				_FontColour = copy._FontColour;
				_FontStyle = copy._FontStyle;
				_FontSize = copy._FontSize;
			}

			///// <summary>Font to use when drawing the text additions.</summary>
			protected internal string _FontName = DEFAULT_FONT_NAME;

			///// <summary>Size of the font to render (this is in "em" measurements).</summary>
			protected internal float _FontSize = DEFAULT_FONT_SIZE;

			/// <summary>Hex colour of the font to use.</summary>
			protected internal string _FontColour = DEFAULT_HEX_COLOUR;

			/// <summary>Font styles to apply: bold, underline, etc.  By default this is Regular (no decorations)</summary>
			protected internal FontStyle _FontStyle = FontStyle.Regular; 

			
			/// <summary>
			/// Sets the FontName (Times New Roman [urg!], Arial, Verdana, etc)
			/// </summary>
			/// <param name="fontName">Name of the font to use</param>
			/// <returns>this, i.e. using a fluent method for defining properties (for details see: http://en.wikipedia.org/wiki/Fluent_interface) </returns>
			public Typeface FontName(string fontName) {
				_FontName = fontName;
				return this;
			}

			/// <summary>
			/// Sets the size of the Font to draw with (in ems)
			/// </summary>
			/// <param name="fontSizeInEms">Size of Font to use</param>
			/// <returns>this, i.e. using a fluent method for defining properties (for details see: http://en.wikipedia.org/wiki/Fluent_interface) </returns>
			public Typeface FontSize(float fontSizeInEms) {
				_FontSize = fontSizeInEms;
				return this;
			}


			/// <summary>
			/// Sets the colour (in hex notation) to draw the text in.
			/// </summary>
			/// <param name="fontColor">Hex colour to draw with</param>
			/// <returns></returns>
			public Typeface FontColour(string fontColor) {
				_FontColour = fontColor;
				return this;
			}


			/// <summary>
			/// Turns embolden decoration on/off when drawing the caption.
			/// </summary>
			/// <param name="boldOn">
			/// true: Drawing the caption in bold is turned on
			/// false: Drawing the caption in bold is turned off
			/// </param>
			/// <returns>this, i.e. using a fluent method for defining properties (for details see: http://en.wikipedia.org/wiki/Fluent_interface) </returns>
			public Typeface Bold(bool boldOn) {
				if (boldOn) 
					_FontStyle |= FontStyle.Bold;
				else
					_FontStyle ^= FontStyle.Bold;

				return this;
			}


			/// <summary>
			/// Turns underline decoration on/off when drawing the caption.
			/// </summary>
			/// <param name="underlineOn">
			/// true: Drawing the caption in underlining is turned on
			/// false: Drawing the caption in underlining is turned off
			/// </param>
			/// <returns>this, i.e. using a fluent method for defining properties (for details see: http://en.wikipedia.org/wiki/Fluent_interface) </returns>
			public Typeface Underline(bool underlineOn) {
				if (underlineOn)
					_FontStyle |= FontStyle.Underline;
				else
					_FontStyle ^= FontStyle.Underline;

				return this;
			}


			/// <summary>
			/// Turns italics on/off when drawing the caption.
			/// </summary>
			/// <param name="italicOn">
			/// true: Drawing the caption in italics is turned on
			/// false: Drawing the caption in italics is turned off
			/// </param>
			/// <returns>this, i.e. using a fluent method for defining properties (for details see: http://en.wikipedia.org/wiki/Fluent_interface) </returns>
			public Typeface Italic(bool italicOn) {
				if (italicOn) 
					_FontStyle |= FontStyle.Italic;
				else 
					_FontStyle ^= FontStyle.Italic;

				return this;
			}


			/// <summary>
			/// Turns strikeout decoration on/off when drawing the caption.
			/// </summary>
			/// <param name="strikeoutOn">
			/// true: Drawing the caption in strikethrough is turned on
			/// false: Drawing the caption in strikethrough is turned off
			/// </param>
			/// <returns>this, i.e. using a fluent method for defining properties (for details see: http://en.wikipedia.org/wiki/Fluent_interface) </returns>
			public Typeface Strikeout(bool strikeoutOn) {
				if (strikeoutOn) 
					_FontStyle |= FontStyle.Strikeout;
				else
					_FontStyle ^= FontStyle.Strikeout;

				return this;
			}

			/// <summary>
			/// Builds up a Font object according to the properties setup on the object.
			/// </summary>
			public Font GetFont() {
				Font f = new Font(this._FontName, this._FontSize, _FontStyle );

				return f;
			} // ToFont


			/// <summary>
			/// Helper method to convert the Font Colour into a Brush (brushes are used for drawing).
			/// </summary>
			public Brush GetBrushColour() {
				return Helpers.GDI.HexColorToBrush(this._FontColour);
			} // GetBrushColour

		} // Typeface

} // PosterBuilder

