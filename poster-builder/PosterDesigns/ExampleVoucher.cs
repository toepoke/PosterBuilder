using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PosterBuilder;
using PosterBuilder.Assets;
using PosterBuilder.Assets.Mapping;

namespace PosterDesigns
{

	/// <summary>
	/// Example voucher showing an imaginary special offer to a customer.
	/// </summary>
	public class ExampleVoucher: PosterBuilder.Builder
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="templateFilename">Filename of the image to use as a template to draw on</param>
		public ExampleVoucher(string templateFilename)
			: base(templateFilename)
		{
			this.SpecialOffer = "";
			this.OfferFor = "";
			this.Birthday = DateTime.MinValue;
		}

		/// <summary>
		/// Details what the special offer is.
		/// </summary>
		/// <remarks>(this is printed on the voucher)</remarks>
		public string SpecialOffer { get; set; }

		/// <summary>
		/// Who the voucher is being made out to
		/// </summary>
		/// <remarks>(this is printed on the voucher)</remarks>
		public string OfferFor { get; set; }

		/// <summary>
		/// Birthday the voucher is valid for
		/// </summary>
		public DateTime Birthday { get; set; }

		/// <summary>
		/// The content of the QR code that has been generated
		/// </summary>
		/// <remarks>
		/// This is a semi-random string which you would store in your database and make 
		/// available at the point-of-sale so the offer can be verified against the generated code.
		/// </remarks>
		public string QRCodeData { get; set; }

		/// <summary>
		/// Specifies where we're going to draw on top of the template, what we're going to
		/// say and with which fonts, colours, decoration, etc.
		/// </summary>
		protected override void RegisterAreasOfInterest()
		{
			Typeface.DEFAULT_FONT_NAME = "Bradley Hand ITC";
			Typeface.DEFAULT_HEX_COLOUR = "#A56BDB";
			Typeface.DEFAULT_FONT_SIZE = 11;

			// roughly number of years since they started playing 
			int years = (int)Math.Ceiling((DateTime.Now - this.Birthday).TotalDays / 365d);

			string birthdayMsg = 
				string.Format("On {0} it's your pitch birthday.  You've been playing football with us for {1} years!",
					this.Birthday.ToString("MMM dd"), years
				);

			Caption hello = 
				new Caption(base.GDI, "#hello", "Hi " + this.OfferFor + ",")
					.TopLeft(5, 65)
				;

			Caption noticedYourBithday = 
				new Caption(base.GDI, "#birthday", birthdayMsg)
					.Rect(5, 90, 350, 100)
				;

			Caption offer = 
				new Caption(base.GDI, "#offer", this.SpecialOffer)
					.Rect(5, 135, 350, 100)
				;

			QRCode qrCode = 
				new QRCode(base.GDI, "#qrCode", this.GenerateQRCodeData())
					.TopLeft(350, 75)
					.Scale(2)
					.Version(4)
			;

			this.AreasOfInterest.Add(hello);
			this.AreasOfInterest.Add(noticedYourBithday);
			this.AreasOfInterest.Add(offer);
			this.AreasOfInterest.Add(qrCode);
		}

		/// <summary>
		/// Combines the message into a unique hash value so the code
		/// can be verified at retailer (avoids the user adding their
		/// own "special offer").
		/// </summary>
		public string GenerateQRCodeData() {
			string salt = DateTime.Now.Ticks.ToString();
			string srcCode = this.OfferFor + salt;
			string hash = PosterBuilder.Helpers.ConversionHelpers.GetHashValue(srcCode);

			this.QRCodeData = hash.Substring(0, 6);

			return this.QRCodeData;
		}

	} // Voucher

} // PosterDesigns
