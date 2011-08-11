using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PosterBuilder;
using PosterBuilder.Assets;

namespace web
{
	/// <summary>
	/// Summary description for PosterHandler
	/// </summary>
	public class PosterHandler : IHttpHandler
	{

		public void ProcessRequest(HttpContext context) {
			RenderPoster(context);
		} // ProcessRequest


		/// <summary>
		/// Establishes which poster is being drawn (based on the "posterId" parameter in the query string of the request)
		/// </summary>
		protected void RenderPoster(HttpContext ctx) {
			int posterID = 1;
			
			int.TryParse(ctx.Request["posterId"], out posterID);
			
			switch (posterID) {
				case 1: RenderTextPosterExample(ctx);
				break;

				case 2: RenderMapPosterExample(ctx);
				break;

				case 3: RenderVoucherExample(ctx);
				break;

				// haven't got any more yet!
			}
		
		} // RenderPoster


		/// <summary>
		/// Responsible for drawing (and downloading) the text example poster.  
		/// </summary>
		protected void RenderTextPosterExample(HttpContext ctx) {
			string templateFilename = ctx.Server.MapPath("./Poster-Templates/text-example-template.jpg");
			string when       = ctx.Request["when"] ?? "";
			string where      = ctx.Request["where"] ?? "";
			string id         = ctx.Request["eventId"] ?? "";			
			
			// And build the poster and send the response back to the browser
			PosterDesigns.ExampleText playFootball = new PosterDesigns.ExampleText(templateFilename);
			string url = string.Format("http://toepoke.co.uk/{0}.aspx", id);
			
			// Set the dynamic bits
			playFootball.Frequency = when;
			playFootball.Venue = where;
			playFootball.SignUpLink = url;

			// Show a border around the areas defined (helps debugging where the rectangles should go)
			playFootball.ShowGuides = GetShowGuidesParam(ctx);
			playFootball.ShowDimensions = GetShowGuidesParam(ctx);

			// Set the size we're after
			playFootball.PercentSize = GetSizeParam(ctx);

			// And send back to the browser
			// .. we're just going to hard-code PNG for the time being
			PosterBuilder.ImgFormat.SupportedTypes outFmt = GetImageType(ctx);
			
			Helpers.SendPosterToBrowser(playFootball, ctx.Response, "football-poster", outFmt);

		} // RenderTextPosterExample


		/// <summary>
		/// Responsible for drawing (and downloading) the map example poster.  
		/// </summary>
		protected void RenderMapPosterExample(HttpContext ctx) {
			string templateFilename = ctx.Server.MapPath("./Poster-Templates/map-example-template.jpg");
			string when          = GetParm(ctx, "when");
			string where         = GetParm(ctx, "where");
			string id            = GetParm(ctx, "eventId");
			string latLong       = GetParm(ctx, "lat-long");
			string address       = GetParm(ctx, "address");
			string mapType       = GetParm(ctx, "map-type");
			
			// And build the poster and send the response back to the browser
			PosterDesigns.ExampleMap mapPoster = new PosterDesigns.ExampleMap(templateFilename);
			string url = string.Format("http://toepoke.co.uk/{0}.aspx", id);

			// Set the dynamic bits
			mapPoster.Frequency = when;
			mapPoster.Venue = where;
			mapPoster.SignUpLink = url;

			mapPoster.VenueMap
				.Type( mapType )
			;

			if (!string.IsNullOrWhiteSpace(latLong)) 
				// use the Lan/Long in preference to the address (better accuracy)
				mapPoster.VenueMap.Centre(latLong);
			else 
				mapPoster.VenueMap.Centre(address);

			// Show a border around the areas defined (helps debugging where the rectangles should go)
			mapPoster.ShowGuides = GetShowGuidesParam(ctx);
			mapPoster.ShowDimensions = GetShowGuidesParam(ctx);

			// Set the size we're after
			mapPoster.PercentSize = GetSizeParam(ctx);

			// And send back to the browser
			// .. we're just going to hard-code PNG for the time being
			PosterBuilder.ImgFormat.SupportedTypes outFmt = GetImageType(ctx);
			
			Helpers.SendPosterToBrowser(mapPoster, ctx.Response, "football-poster", outFmt);

		} // RenderMapPosterExample


		/// <summary>
		/// Responsible for drawing (and downloading) the example voucher.  
		/// </summary>
		protected void RenderVoucherExample(HttpContext ctx) {
			string templateFilename = ctx.Server.MapPath("./Poster-Templates/voucher-example-template.jpg");
				
			// and build the voucher
			PosterDesigns.ExampleVoucher voucher = new PosterDesigns.ExampleVoucher(templateFilename);

			// Set the dynamic bits
			voucher.SpecialOffer = GetParm(ctx, "special-offer");
			voucher.OfferFor = GetParm(ctx, "offer-for");
			voucher.Birthday = DateTime.Parse(GetParm(ctx, "birthday"));

			voucher.ShowGuides = GetShowGuidesParam(ctx);
			voucher.ShowDimensions = GetShowGuidesParam(ctx);
			voucher.PercentSize = GetSizeParam(ctx);

			// And send back to the browser
			// .. we're just going to hard-code PNG for the time being
			PosterBuilder.ImgFormat.SupportedTypes outFmt = GetImageType(ctx);
			
			Helpers.SendPosterToBrowser(voucher, ctx.Response, "your-voucher", outFmt);

		} // RenderVoucherExample


		/// <summary>
		/// Convenience method to return a given query string value (decoded if it is present).
		/// </summary>
		/// <param name="name">Name of the query string parameter to return.</param>
		private string GetParm(HttpContext ctx, string name) {
			string value = ctx.Request[name] ?? "";

			if (!string.IsNullOrWhiteSpace(value))
				HttpUtility.UrlDecode(value);
			
			return value;

		} // GetParm


		/// <summary>
		/// Convenience method to return the "show-guides" tick box setting from the query string.
		/// </summary>
		private bool GetShowGuidesParam(HttpContext ctx) {
			bool showGuides = false;
				
			bool.TryParse(ctx.Request["show-guides"], out showGuides);

			return showGuides;
		
		} // GetShowGuidesParam


		/// <summary>
		/// Convenience method for establishing what the image output format was in the query string of the request.
		/// </summary>
		private PosterBuilder.ImgFormat.SupportedTypes GetImageType(HttpContext ctx) {
			string imgType = ctx.Request["type"] ?? "";

			return PosterBuilder.ImgFormat.FromString(imgType);

		} // GetImageType


		/// <summary>
		/// Convenience method for establishing what size of image is to be rendered (taken from the query string 
		/// in the request).
		/// </summary>
		private int GetSizeParam(HttpContext ctx) {
			int size = 100;	// default to 100%

			int.TryParse(ctx.Request["size"], out size);

			return size;

		} // GetSizeParam


		public bool IsReusable {
			get
			{
				return false;
			}
		}
	
	} // PosterHandler

} // web