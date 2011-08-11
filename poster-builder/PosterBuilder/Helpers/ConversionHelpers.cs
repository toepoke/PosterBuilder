using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace PosterBuilder.Helpers
{
	public class ConversionHelpers
	{

		public static double[] StringToDoubleArray(string param) {
			List<string> strCoords = param.Split(',').ToList<string>();
			List<double> dblCoords = new List<double>();

			strCoords.ForEach( c => { dblCoords.Add(double.Parse(c)); } );

			return dblCoords.ToArray();
		}

		public static string GetHashValue(string inStr)
		{
			SHA1 algorithm = null;
			ASCIIEncoding encoder = null;
			Byte[] combined = null;
			string hash = "";

			algorithm = SHA1.Create();
			encoder = new ASCIIEncoding();
			combined = encoder.GetBytes(inStr);
			algorithm.ComputeHash(combined);
			hash = Convert.ToBase64String(algorithm.Hash);

			return hash;
		}

	}


}
