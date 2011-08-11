using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Web.UI.WebControls;

namespace web
{
	public class Helpers
	{
		public static void SendPosterToBrowser( PosterBuilder.Builder pb, HttpResponse resp, string posterFilename, PosterBuilder.ImgFormat.SupportedTypes outputFormat) {
			string filename = BuildFilename(posterFilename, outputFormat);
			
			resp.Clear();
			// Ensure caching is off naturally
			resp.CacheControl = "no-cache";				

			resp.ContentType = PosterBuilder.ImgFormat.ToMimeType(outputFormat);
			resp.AddHeader("Content-Disposition", "attachment;filename=" + filename);

			// Call our image with our amendments and have it save to the response so we can send it back
			Bitmap bmp = pb.Render();

			// Have the Poster build our new image and save the result to the outgoing response
			// ... have to do all this with MemoryStreams as PNG doesn't like being saved directly to HttpResponse.OutputStream
			// ... may as well do the same for all image types and be consistent
			using (Bitmap bitmap = pb.Render()) {

				using (MemoryStream ms = new MemoryStream()) {
					ImageFormat outFmt = PosterBuilder.ImgFormat.ToImageFormat(outputFormat);
				
					bmp.Save(ms, outFmt);

					ms.WriteTo(resp.OutputStream);
				} // using ms

			} // using pb

			// And of course, clear up after ourselves
			pb.Dispose();
			resp.End();			
		}

		public static string BuildFilename(string filenamePrefix, PosterBuilder.ImgFormat.SupportedTypes imgFormat) {
			string newFilename = ""; string extension = "";

			extension = PosterBuilder.ImgFormat.ToFileExtension(imgFormat);
			newFilename = System.IO.Path.ChangeExtension(filenamePrefix, extension);

			return newFilename;
		}

		/// <summary>
		/// Gets a list of fonts that are installed on the "server".  As we're drawing on
		/// an image on the server they are the only fonts we have available.  Not the ones on the
		/// browser side.
		/// </summary>
		private void LoadFontsDDL(DropDownList ddlFonts) {
		  ddlFonts.DataSource = PosterBuilder.Helpers.GDI.GetFontNames();
		  ddlFonts.DataBind();

		  // See if there's a nice (IMHO) font to use, failing that just pick the 
		  // first in the list
		  ListItem font = ddlFonts.Items.FindByText("Trebuchet MS");
		  if (font != null)
		    ddlFonts.SelectedValue = font.Value;
		  else 
		    // just pick the first one then
		    ddlFonts.SelectedIndex = 0;
		}

	}
}