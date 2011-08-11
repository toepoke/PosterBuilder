using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Text;

namespace PosterBuilder {

		/// <summary>
		/// Specifies what output image formats are supported by the builder.
		/// </summary>
		/// <remarks>
		/// Note that the <see cref="SupportedTypes"/> enum is a subset of those image formats
		/// supported by MS.  These are abstracted out so we only see what's useful to us.
		/// </remarks>
		public class ImgFormat {

			/// <summary>
			/// Set of supported output types when rendering the poster.
			/// </summary>
			public enum SupportedTypes: int {
				Bitmap = 1,
				Gif = 2,
				Jpeg = 3,
				Png = 4
			} // SupportedTypes


			/// <summary>
			/// Converts a string into a supported image type.
			/// </summary>
			/// <param name="type">String to convert</param>
			/// <returns></returns>
			public static SupportedTypes FromString(string type) {
				SupportedTypes retVal = SupportedTypes.Jpeg;

				if (Enum.IsDefined(typeof(SupportedTypes), type))
					retVal = (SupportedTypes) Enum.Parse(typeof(SupportedTypes), type, true);

				return retVal;
			} // FromString


			/// <summary>
			/// Helper method to convert the supported output format to the enum .NET expects
			/// </summary>
			/// <param name="type">Output format to be converted</param>
			public static ImageFormat ToImageFormat(SupportedTypes type) {

				switch (type) {
					case SupportedTypes.Bitmap: return ImageFormat.Bmp;
					case SupportedTypes.Gif: return ImageFormat.Gif;
					case SupportedTypes.Jpeg: return ImageFormat.Jpeg;
					case SupportedTypes.Png: return ImageFormat.Png;
					default: throw new ArgumentException(string.Format("{0} is not a supported output format.", type.ToString()) );
				}

			} // ToImageFormat


			/// <summary>
			/// Helper method to get correct MIME type for the supported type.
			/// </summary>
			/// <param name="type">Output format to get the MIME type of</param>
			/// <remarks>
			/// For more information on MIME types, see http://en.wikipedia.org/wiki/MIME
			/// </remarks>
			public static string ToMimeType(SupportedTypes type) {

				switch (type) {
					case SupportedTypes.Bitmap: return "image/bmp";
					case SupportedTypes.Gif: return "image/gif";
					case SupportedTypes.Jpeg: return "image/jpg";
					case SupportedTypes.Png: return "image/png";
					default: throw new ArgumentException(string.Format("{0} is not a supported output format.", type.ToString()) );
				}

			} // ToContentType


			/// <summary>
			/// Helper method to get the correct file extension for the supported type 
			/// (ensuring the correct application opens the poster once downloaded to the Client's browser).
			/// </summary>
			/// <param name="type">Output format to get the extension of</param>
			/// <returns></returns>
			public static string ToFileExtension(SupportedTypes type) {

				switch (type) {
					case SupportedTypes.Bitmap: return "bmp";
					case SupportedTypes.Gif: return "gif";
					case SupportedTypes.Jpeg: return "jpg";
					case SupportedTypes.Png: return "png";
					default: throw new ArgumentException(string.Format("{0} is not a supported output format.", type.ToString()) );
				}

			} // ToContentType

		} // ImgFormat

} // PosterBuilder

