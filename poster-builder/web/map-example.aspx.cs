using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


namespace web
{
	public partial class MapExample : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			Preview.Click += new EventHandler(Preview_Click);
			Download.Click += new EventHandler(Download_Click);
		}

		
		void Download_Click(object sender, EventArgs e) {
			string templateFilename = this.Server.MapPath("./Poster-Templates/map-example-template.jpg");
			
			PosterDesigns.ExampleMap poster = new PosterDesigns.ExampleMap(templateFilename);

			poster.Frequency = Frequency.Text;
			poster.Venue = Venue.Text;
			poster.SignUpLink = string.Format("http://toepoke.co.uk/{0}.aspx", this.EventID.Text);
			poster.ShowGuides = false;
			poster.ShowDimensions = false;
			poster.PercentSize = 100;		// fullSize when downloading
			poster.VenueMap.Type( this.MapType.SelectedValue );
			if (!string.IsNullOrWhiteSpace(this.LatLong.Text))
				poster.VenueMap.Centre( this.LatLong.Text );
			else 
				poster.VenueMap.Centre( this.Address.Text );

			PosterBuilder.ImgFormat.SupportedTypes imgType = PosterBuilder.ImgFormat.FromString(PosterRendering.ImageTypes.SelectedValue);

			Helpers.SendPosterToBrowser(poster, this.Response, "my-poster", imgType);
		
		} // Download_Click



		void Preview_Click(object sender, EventArgs e) {
			// Now we just set the image source on the right hand side, and let the HttpHandler do the rest
			Poster.ImageUrl = GetPosterURL();
			Poster.Visible = true;
		
		} // MakePoster_Click



		/// <summary>
		/// Converts the parameters expressed on the page into a URL which will call the HttpHandler and thereby 
		/// construct the image (according to the parameters given in the query string).
		/// </summary>
		private string GetPosterURL() {
			const int PosterID = 2;
			StringBuilder posterUrl = new StringBuilder();

			posterUrl.Append("PosterHandler.ashx?");
			posterUrl.AppendFormat("posterId={0}", PosterID); 
			posterUrl.AppendFormat("&when={0}", HttpUtility.UrlEncode(Frequency.Text) );
			posterUrl.AppendFormat("&where={0}", HttpUtility.UrlEncode(Venue.Text) );
			posterUrl.AppendFormat("&eventID={0}", int.Parse(EventID.Text) );
			posterUrl.AppendFormat("&lat-long={0}", HttpUtility.UrlEncode(LatLong.Text) );
			posterUrl.AppendFormat("&address={0}", HttpUtility.UrlEncode(Address.Text) );
			posterUrl.AppendFormat("&map-type={0}", HttpUtility.UrlEncode(MapType.SelectedValue) );
			posterUrl.Append("&");
			posterUrl.Append( PosterRendering.ToQueryString() );

			return posterUrl.ToString();
		}

	}
}
