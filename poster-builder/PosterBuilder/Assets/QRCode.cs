using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace PosterBuilder.Assets {

	/// <summary>
	/// Used for drawing a QRCode on the template.  A QR (or quick response code) is a bit like a barcode 
	/// (see http://en.wikipedia.org/wiki/QR_code).  A third-party library is used here to produce the
	/// codes.  For full details on the library (which is GPL licensed for non-commercial use), please see
	/// http://twit88.com/platform/projects/show/mt-qrcode.
	/// 
	/// Note a great deal of the properties in this class return the class itself.
	/// This is to employ a fluent interface to specifying the properties.
	/// </summary>
	public class QRCode: Area {
			
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		public QRCode(Graphics gdi) : base(gdi, "") {
			this.Reset();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		/// <param name="data"></param>
		public QRCode(Graphics gdi, string id, string data) : base(gdi, id) { 
			this.Reset();
			this._Data = data;
		}


		/// <summary>
		/// Resets the object back to a known state.
		/// </summary>
		protected internal void Reset() {
			_Data = "";
			_Scale = 15;
			_EncodingType = Helpers.QRCoder.EncodingType.Alphanumeric;
			_ErrorCorrectionLevel = Helpers.QRCoder.ErrorCorrectionLevel.H;
			_Version = 3;
		}


		/// <summary>
		/// Gets/Sets the data to be converted to a quick response code.
		/// </summary>
		protected internal string _Data { get; set; }
		
		/// <summary>
		/// Gets/Sets the encoding type
		/// </summary>
		/// <remarks>
		/// See http://en.wikipedia.org/wiki/QR_code#Data_capacity for details.
		/// </remarks>
		protected internal Helpers.QRCoder.EncodingType _EncodingType { get; set; }
		
		/// <summary>
		/// Gets/Sets the level of error correction employed.
		/// </summary>
		/// <remarks>
		/// See http://en.wikipedia.org/wiki/QR_code#Error_correction for details.
		/// </remarks>
		protected internal Helpers.QRCoder.ErrorCorrectionLevel _ErrorCorrectionLevel { get; set; }
		
		/// <summary>
		/// Specifies the scaling employed (the Version specifies the size of the resulting code, scale
		/// makes it bigger by the given factor, making it more readable and useable on a printed version).
		/// </summary>
		/// <remarks>
		/// This is _kind of_ the size of the resulting image, but this seems to be a 
		/// combination of the Scale and the Version.  The Version stipulates the size
		/// of the resulting QR code (see http://en.wikipedia.org/wiki/QR_code#Data_capacity), so I think
		/// the scale basically makes it bigger to be more viewable (so if we're using version 1 which
		/// is 21x21 and set the Scale to 2 we'd actually get a 42x42 image).
		/// </remarks>
		protected internal int _Scale;

		
		/// <summary>
		/// Specifies what QR code version to employ, see http://en.wikipedia.org/wiki/QR_code#Data_capacity
		/// for versions available (not necessarily supported by the third-party API).
		/// </summary>
		protected internal int _Version;


		/// <summary>
		/// Sets the data that is to be encoded into the QR code image.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public QRCode Data(string data) {
			_Data = data;
			return this;
		}


		/// <summary>
		/// Sets the encoding type to be used
		/// </summary>
		/// <param name="encodingType">Encoding type to be used.</param>
		/// <remarks>
		/// See http://en.wikipedia.org/wiki/QR_code#Data_capacity for details.
		/// </remarks>
		public QRCode EncodingType(Helpers.QRCoder.EncodingType encodingType) {
			_EncodingType = encodingType;
			return this;
		}


		/// <summary>
		/// Sets the level of error correction employed.
		/// </summary>
		/// <param name="errorCorrectionLevel">Error correction level to be used when creating the image.</param>
		/// <remarks>
		/// See http://en.wikipedia.org/wiki/QR_code#Error_correction for details.
		/// </remarks>
		public QRCode ErrorCorrectionLevel(Helpers.QRCoder.ErrorCorrectionLevel errorCorrectionLevel) {
			_ErrorCorrectionLevel = errorCorrectionLevel;
			return this;
		}


		/// <summary>
		/// Specifies the scaling employed (the Version specifies the size of the resulting code, scale
		/// makes it bigger by the given factor, making it more readable and useable on a printed version).
		/// </summary>
		/// <param name="scale">Amount to scale the resulting image up by</param>
		/// <remarks>
		/// This is _kind of_ the size of the resulting image, but this seems to be a 
		/// combination of the Scale and the Version.  The Version stipulates the size
		/// of the resulting QR code (see http://en.wikipedia.org/wiki/QR_code#Data_capacity), so I think
		/// the scale basically makes it bigger to be more viewable (so if we're using version 1 which
		/// is 21x21 and set the Scale to 2 we'd actually get a 42x42 image).
		/// </remarks>
		public QRCode Scale(int scale) {
			_Scale = scale;
			return this;
		}


		/// <summary>
		/// Specifies what QR code version to employ, see http://en.wikipedia.org/wiki/QR_code#Data_capacity
		/// for versions available (not necessarily supported by the third-party API).
		/// </summary>
		/// <param name="version">QR code version to use in the resulting image.</param>
		/// <returns></returns>
		public QRCode Version(int version) {
			_Version = version;
			return this;
		}


		/// <summary>ID of the caption being defined (optional, useful for debugging).</summary>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		/// <returns></returns>
		new public QRCode ID(string id) {
			return (QRCode) base.ID(id);
		}

		
		/// <summary>
		/// Where on the template an asset should be drawn.
		/// </summary>
		/// <param name="x">X co-ordinate</param>
		/// <param name="y">Y co-ordinate</param>
		new public QRCode TopLeft(int x, int y) {
			base.TopLeft(x, y);
			return this;
		} // Rect


		/// <summary>
		/// Where on the template an asset should be drawn.
		/// </summary>
		/// <param name="coord">X co-ordinate (index 0) and Y co-ordinate (index 1)</param>
		new public QRCode TopLeft(int[] coord) {
			base.TopLeft(coord);
			return this;
		}


		/// <summary>
		/// Where on the template an asset should be drawn.
		/// </summary>
		/// <param name="coord">X,Y co-ordinate</param>
		new public QRCode TopLeft(Position coord) {
			base.TopLeft(coord);
			return this;
		}


		/// <summary>
		/// Responsible for creating a GDI image with the resulting QR code on it 
		/// (which is in turn built by the third-party API)
		/// </summary>
		/// <returns>
		/// GDI Image object with the QR code rendered on it (ready to be added onto the template).
		/// </returns>
		public System.Drawing.Image GetQRCodeImage() {
			System.Drawing.Image bitmap = null;
		
			Helpers.QRCoder encoder = new Helpers.QRCoder();
			encoder.EncoderType = this._EncodingType;	
			encoder.ErrorCorrection = this._ErrorCorrectionLevel;
			encoder.Size = this._Scale;
			encoder.Version = this._Version;

			// and pass over to the 3rd party API for encoding as an image
			bitmap = encoder.GenerateImage(_Data);
			
			encoder = null;

			return bitmap;
		}


		/// <summary>
		/// Draws the QR code (based on the provided data) at the defined position on the template.
		/// </summary>
		protected internal override void Render() {
			System.Drawing.Image img = this.GetQRCodeImage();

			this.Canvas.DrawImage(img, this.X, this.Y);

			img.Dispose();
		}


		/// <summary>
		/// Ensures the data in the QRCode object is valid
		/// </summary>
		new public void Validate() {
			base.Validate();

			if (string.IsNullOrEmpty(_Data))
				throw new ArgumentException("No Data has been specified to create a Quick Response Code from.");

		} // Validate
		
	} // Caption

} // GDI.Assets