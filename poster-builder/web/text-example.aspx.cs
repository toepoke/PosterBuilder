using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace web
{
	public partial class TextExample : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			Preview.Click += new EventHandler(Preview_Click);
			Download.Click += new EventHandler(Download_Click);
		}


		void Download_Click(object sender, EventArgs e) {
			string templateFilename = this.Server.MapPath("./Poster-Templates/text-example-template.jpg");
			
			PosterDesigns.ExampleText poster = new PosterDesigns.ExampleText(templateFilename);

			poster.Frequency = Frequency.Text;
			poster.Venue = Venue.Text;
			poster.SignUpLink = string.Format("http://toepoke.co.uk/{0}.aspx", this.EventID.Text);
			poster.ShowGuides = false;
			poster.ShowDimensions = false;
			poster.PercentSize = 100;		// fullSize when downloading

			PosterBuilder.ImgFormat.SupportedTypes imgType = PosterBuilder.ImgFormat.FromString(PosterRendering.ImageTypes.SelectedValue);

			Helpers.SendPosterToBrowser(poster, this.Response, "my-poster", imgType);
		
		} // Download_Click


		void Preview_Click(object sender, EventArgs e)
		{
			string posterUrl = string.Format("PosterHandler.ashx?posterId=1&when={0}&where={1}&eventID={2}&",
				Frequency.Text,
				Venue.Text,
				EventID.Text
			);

			posterUrl += PosterRendering.ToQueryString();
			
			// Now we just set the image source on the right hand side, and let the HttpHandler do the rest
			Poster.ImageUrl = posterUrl;
			Poster.Visible = true;
		
		} // MakePoster_Click

	}

}
