using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using PosterBuilder;
using PosterBuilder.Assets;
using PosterBuilder.Assets.Mapping;

namespace PosterDesigns
{

	/// <summary>
	/// Example poster showing where an imaginary game is going to take on a map, 
	/// and a QR code which navigates to a URL in order to join a game.
	/// </summary>
	public class ExampleMap: PosterBuilder.Builder
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="templateFilename">Filename of the image to use as a template to draw on</param>
		public ExampleMap(string templateFilename)
			: base(templateFilename)
		{			
			this.Frequency = "";
			this.Venue = "";
			this.SignUpLink = "";
			this.VenueMap = new Map(base.GDI, "#venueMap")
				.TopLeft(1715, 850)
				.Zoom(18)
				.Width(640).Height(640)
				.Type(Map.MapType.Road)
			;
		}

		/// <summary>
		/// How often the user plays 5-a-side (this is the WHEN).
		/// </summary>
		public string Frequency { get; set; }

		/// <summary>
		/// Where the game of 5-a-side is played (this is the WHERE).
		/// </summary>
		public string Venue { get; set; }

		/// <summary>
		/// URL to sign-up for the football game (this is the HOW).
		/// </summary>
		public string SignUpLink { get; set; }

		/// <summary>
		/// Map object which defines where the map location is, what markers, etc.
		/// </summary>
		public Map VenueMap { get; set; }

		/// <summary>
		/// Specifies where we're going to draw on top of the template, what we're going to
		/// say and with which fonts, colours, decoration, etc.
		/// </summary>
		protected override void RegisterAreasOfInterest()
		{
			// Setup the position of each asset (if they've not been specified outside the class)
			Area mapCaptionRect = new Area(this.GDI, "#mapCaption", 1715, 1520, 650, 120);
			Position qrCodePosition = new Position(this.GDI,"#qrCode", 1704, 1800);
			Position qRCaptionPosition = new Position(this.GDI,"#qrCaption", 1704, 2000);

			// Add caption on to the polaroid
			Typeface mapCaptionFont = new Typeface("Bradley Hand ITC")
				.Bold(true)
				.Italic(true)
				.FontColour("#000000")
				.FontSize(35)
			;

			Typeface gameDetailFont = new Typeface()
				.FontColour("#252525")
				.FontSize(50)
			;
			Typeface gameLinkFont = new Typeface(gameDetailFont)
				.Underline(true)
				.FontColour("#cc0000")
			;

			Caption mapCaption = 
				new Caption(base.GDI, "#mapCaption", this.Venue)
				.Typeface(mapCaptionFont)
				.HorizontalAlignment(StringAlignment.Near)
				.Rect(mapCaptionRect)
			;


			// Add the Quick Response code
			QRCode qrCode = 
				new QRCode(base.GDI, "#qrCode", this.SignUpLink)
				.TopLeft(qrCodePosition)
				.Data(this.SignUpLink)
				.Scale(20)
				.Version(4)
			;


			// add instructions
			Caption where = new Caption(base.GDI, "#where")
				.Text(this.Venue)
				.Rect(200, 1000, 1400, 300)
				.Typeface(gameDetailFont)
			;
			Caption when = new Caption(base.GDI, "#when")
				.Text(this.Frequency)
				.Rect(200, 1500, 1400, 300)
				.Typeface(gameDetailFont)
			;
			Caption howSignUp = new Caption(base.GDI, "#how")
				.Text("Join the game with the QR code, or at the website below:")
				.Rect(200, 2000, 1400, 300)
				.Typeface(gameDetailFont)
			;
			Caption howLink = new Caption(base.GDI, "#how")
				.Text(this.SignUpLink)
				.Rect(200, 2200, 1400, 300)
				.Typeface(gameLinkFont)
			;
			
			List<Position> areas = new List<Position>();

			areas.Add( where );
			areas.Add( when );
			areas.Add( howSignUp );
			areas.Add( howLink );
			
			areas.Add( this.VenueMap );
			areas.Add( mapCaption );
			areas.Add( qrCode );

			// Now we've defined all our instructions for what to draw, tell the PosterBuilder
			base.AreasOfInterest = areas;

		} // RegisterAreasOfInterest
		
	} // WhenWhereHow_Mapped

} // PosterDesigns
