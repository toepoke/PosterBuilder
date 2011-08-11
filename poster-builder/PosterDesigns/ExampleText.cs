using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using PosterBuilder;
using PosterBuilder.Assets;

namespace PosterDesigns
{

	/// <summary>
	/// Example poster showing where an imaginary game is going to take, where
	/// and how often.
	/// </summary>
	public class ExampleText: PosterBuilder.Builder
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="templateFilename">Filename of the image to use as a template to draw on</param>
		public ExampleText(string templateFilename)
			: base(templateFilename)
		{			
		}

		/// <summary>
		/// How often the user plays 5-a-side (this is the WHEN)
		/// </summary>
		public string Frequency { get; set; }

		/// <summary>
		/// Where the game of 5-a-side is played (this is the WHERE)
		/// </summary>
		public string Venue { get; set; }

		/// <summary>
		/// URL to sign-up for the football game (this is the HOW)
		/// </summary>
		public string SignUpLink { get; set; }

		/// <summary>
		/// Defines where the first caption is going to live.  For the other captions
		/// we'll be using similar points on the template image, so we'll just change
		/// what we need to for the other boxes (which will just be the Y starting point 
		/// and the height required for the rectangle)
		/// </summary>
		private int BaseX = 0;
		private int BaseY = 1200;
		private int BaseWidth = 2480;
		private int BaseHeight = 125;

		/// <summary>
		/// Specifies where we're going to draw on top of the template, what we're going to
		/// say and with which fonts, colours, decoration, etc.
		/// </summary>
		protected override void RegisterAreasOfInterest()
		{
			Typeface.DEFAULT_FONT_NAME = "Trebuchet MS";
			Typeface.DEFAULT_HEX_COLOUR = "#8f87ca";
			Typeface.DEFAULT_FONT_SIZE = 75f;

			// Define the font decorations we'll be using in the Titles and dynamic text
			Typeface titleFace = new Typeface().FontColour("#000000");
			Typeface textFace = new Typeface().Bold(true);
			Typeface urlFace = new Typeface().Bold(true).Underline(true);
			
			// Define where on the image we need to draw each of our captions
			// ... recall we're using the same X, width and height.  We only need modify the Y
			Caption whenTitle = new Caption(base.GDI, "#whenTitle", "WHEN?")
				.Typeface(titleFace)
				.HorizontalAlignment(StringAlignment.Center)
				.Rect(BaseX, BaseY, BaseWidth, BaseHeight)
			;

			Caption whenText = new Caption(base.GDI, "#whenText", string.Format("We play {0}.", this.Frequency) )
				.Typeface(textFace)
				.HorizontalAlignment(StringAlignment.Center)
				.Rect(BaseX, BaseY+150, BaseWidth, BaseHeight)
			;
			
			Caption whereTitle = new Caption(base.GDI, "#whereTitle", "WHERE?")
				.Typeface(titleFace)
				.HorizontalAlignment(StringAlignment.Center)
				.Rect(BaseX, BaseY+400, BaseWidth, BaseHeight)
			;

			Caption whereText = new Caption(base.GDI, "#whereText", this.Venue)
				.Typeface(textFace)
				.HorizontalAlignment(StringAlignment.Center)
				.Rect(BaseX, BaseY+550, BaseWidth, BaseHeight)
			;

			Caption howTitle = new Caption(base.GDI, "#howTitle", "HOW?")
				.Typeface(titleFace)
				.HorizontalAlignment(StringAlignment.Center)
				.Rect(BaseX, BaseY+800, BaseWidth, BaseHeight)
			;

			Caption howText = new Caption(base.GDI, "#howText", "Sign-up at:")
				.Typeface(textFace)
				.HorizontalAlignment(StringAlignment.Center)
				.Rect(BaseX, BaseY+950, BaseWidth, BaseHeight)
			;
			Caption howUrl = new Caption(base.GDI, "#howUrl", this.SignUpLink)
				.Typeface(urlFace)
				.HorizontalAlignment(StringAlignment.Center)
				.Rect(BaseX, BaseY+1100, BaseWidth, BaseHeight)
			;

			// And finally bring it all together and setup our rectangles, captions and fonts
			List<Position> areas = new List<Position>();
			areas.Add( whenTitle );
			areas.Add( whenText );
			areas.Add( whereTitle );
			areas.Add( whereText );
			areas.Add( howTitle );
			areas.Add( howText );
			areas.Add( howUrl );

			// Now we've defined all our instructions for what to draw, tell the PosterBuilder
			base.AreasOfInterest = areas;

		} // RegisterAreasOfInterest
		
	} // WhenWhereHow

} // PosterDesigns


