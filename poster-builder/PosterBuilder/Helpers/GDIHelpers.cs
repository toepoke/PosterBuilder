using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Text;
using System.Drawing.Text;

namespace PosterBuilder.Helpers {

	public class GDI
	{
	
		/// <summary>
		/// Converts a hex colour into a brush
		/// </summary>
		/// <param name="hexColour">Hex colour to convert</param>
		/// <returns></returns>
		public static Brush HexColorToBrush(string hexColour)
		{
			Color c = ColorTranslator.FromHtml(hexColour);
			Brush b = new SolidBrush(c);
		
			return b;
		}


		/// <summary>
		/// Gets a list of Fonts that are installed on the server.  Note this is the _server_ not
		/// the Client.  The Client will have a completely different set of fonts installed and 
		/// we need to ones on the server (as we'll be drawing the image on the server).
		/// </summary>
		/// <returns></returns>
		public static List<string> GetFontNames() {
			InstalledFontCollection installedFonts = new InstalledFontCollection();
			FontFamily[] arrFamilies = installedFonts.Families;
			List<string> fonts = (from ff in arrFamilies orderby ff.Name select ff.Name).ToList<string>();

			return fonts;
		} // GetFontNames


		public static Bitmap GetThumbnail(Bitmap canvas, int newWidth, int newHeight) {
			return (Bitmap) canvas.GetThumbnailImage(newWidth, newHeight, GetThumbnailCB, IntPtr.Zero);
		}

		/// <summary>
		/// Apparently we have to supply a call-back to "GetThumbnailImage" even though it's not used!?!!
		/// </summary>
		protected static bool GetThumbnailCB() {
			return false;
		} // GetThumbnailCB


	} // GDI

} // Helpers
