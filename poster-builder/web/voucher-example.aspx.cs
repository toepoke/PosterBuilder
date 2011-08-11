using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


namespace web
{
	public partial class VoucherExample : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			Preview.Click += new EventHandler(Preview_Click);

			if (!IsPostBack)
				this.PosterRendering.Size.SelectedValue = "100";			
			Download.Click += new EventHandler(Download_Click);
		}


		void Preview_Click(object sender, EventArgs e) {
			// Now we just set the image source on the right hand side, and let the HttpHandler do the rest
			Poster.ImageUrl = GetPosterURL();
			Poster.Visible = true;

		} // MakePoster_Click


		void Download_Click(object sender, EventArgs e) {
			string templateFilename = this.Server.MapPath("./Poster-Templates/voucher-example-template.jpg");
			
			PosterDesigns.ExampleVoucher voucher = new PosterDesigns.ExampleVoucher(templateFilename);

			voucher.SpecialOffer = SpecialOffer.Text;
			voucher.OfferFor = OfferFor.Text;
			voucher.Birthday = DateTime.Parse(Birthday.Text);
			voucher.ShowGuides = false;
			voucher.ShowDimensions = false;
			voucher.PercentSize = 100;		// fullSize when downloading
			
			PosterBuilder.ImgFormat.SupportedTypes imgType = PosterBuilder.ImgFormat.FromString(PosterRendering.ImageTypes.SelectedValue);

			Helpers.SendPosterToBrowser(voucher, this.Response, "my-voucher", imgType);
		
		} // Download_Click

		
		/// <summary>
		/// Converts the parameters expressed on the page into a URL which will call the HttpHandler and thereby 
		/// construct the image (according to the parameters given in the query string).
		/// </summary>
		private string GetPosterURL() {
			const int PosterID = 3;
			StringBuilder posterUrl = new StringBuilder();

			posterUrl.Append("PosterHandler.ashx?");
			posterUrl.AppendFormat("posterId={0}", PosterID); 
			posterUrl.AppendFormat("&special-offer={0}", HttpUtility.UrlEncode(SpecialOffer.Text) );
			posterUrl.AppendFormat("&offer-for={0}", HttpUtility.UrlEncode(OfferFor.Text) );
			posterUrl.AppendFormat("&birthday={0}", HttpUtility.UrlEncode( Birthday.Text ) );
			posterUrl.Append("&");
			posterUrl.Append(PosterRendering.ToQueryString());

			return posterUrl.ToString();

		} // GetPosterURL

	}
}
