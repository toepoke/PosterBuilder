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
using MessagingToolkit.QRCode.Codec;

namespace PosterBuilder.Helpers {

	public class QRCoder
	{
		public enum EncodingType: byte {
			Byte = 0,
			Alphanumeric = 1,
			Numeric = 2
		};

		public enum ErrorCorrectionLevel: byte {
			L = 76,
			M = 77,
			Q = 81,
			H = 72
		};

		public QRCoder() {
			this.EncoderType = QRCoder.EncodingType.Alphanumeric;
			this.ErrorCorrection = ErrorCorrectionLevel.H; 
			this.Size = 3;
			this.Version = 7;
		}

		public int Size { get; set; }
		public int Version { get; set; }
		public EncodingType EncoderType { get; set; }
		public ErrorCorrectionLevel ErrorCorrection { get; set; }
		
		public System.Drawing.Image GenerateImage(string data) {
			
			QRCodeEncoder encoder = new QRCodeEncoder();

			switch (this.EncoderType) {
				case EncodingType.Byte: 
					encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
				break;
				case EncodingType.Alphanumeric: 
					encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
				break;
				case EncodingType.Numeric: 
					encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
				break;
				default:
					encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
				break;
			}

			switch (this.ErrorCorrection) {
				case ErrorCorrectionLevel.H: 
					encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
				break;
				case ErrorCorrectionLevel.Q: 
					encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
				break;
				case ErrorCorrectionLevel.L: 
					encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
				break;
				case ErrorCorrectionLevel.M: 
					encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
				break;
				default: 
					encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
				break;
			}

			encoder.QRCodeScale = this.Size;
			encoder.QRCodeVersion = this.Version;

			encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
			encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;

			System.Drawing.Image qrCodeImage = encoder.Encode(data);

			return qrCodeImage;

		} // GenerateImage

	} // QRCode

} // Helpers
