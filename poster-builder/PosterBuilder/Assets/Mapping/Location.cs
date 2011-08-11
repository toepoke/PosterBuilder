using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PosterBuilder.Assets.Mapping
{
	/// <summary>
	/// Responsible for storing data about a location on a map 
	/// (whether the map itself or a marker on the map)
	/// </summary>
	public class Location {

		/// <summary>
		/// Default constructor
		/// </summary>
		public Location() {
			this.Latitude = null;
			this.Longitude = null;
			this.Address = "";
		}

		/// <summary>
		/// Sets the location point of a map based on an address, e.g. "Wembley, Middlesex HA9 0WS"
		/// </summary>
		/// <param name="address">Address to use as a map location</param>
		public Location(string address) : this() {
			this.Address = address;
		}

		/// <summary>
		/// Sets the location point on a map, based on the latitude/longitude co-ordinates.
		/// </summary>
		/// <param name="lat">Latitude of the location</param>
		/// <param name="lng">Longitude of the location</param>
		public Location(int lat, int lng) : this() {
			this.SetLatLng(lat, lng);
		}
			
		/// <summary>Gets/Sets the Latitude of the location</summary>
		public double? Latitude { get; set; }
				
		/// <summary>Gets/Sets the Longitude of the location</summary>
		public double? Longitude { get; set; }
		
		/// <summary>
		/// Gets/Sets the location point of a map based on an address, e.g. "Wembley, Middlesex HA9 0WS"
		/// </summary>
		public string  Address { get; set; }


		/// <summary>
		/// Sets the latitude/longitude location of the map point
		/// </summary>
		/// <param name="lat">Latitude</param>
		/// <param name="lng">Longitude</param>
		public void SetLatLng(double lat, double lng) {
			this.Address = "";
			this.Latitude = lat;
			this.Longitude = lng;
		}


		/// <summary>
		/// Sets the latitude/longitude location of the map point
		/// </summary>
		/// <param name="latLng">Latitude (index 0), Longitude (index 1)</param>
		public void SetLatLng(double[] latLng) {
			this.SetLatLng(latLng[0], latLng[1]);
		}


		/// <summary>
		/// Sets the latitude/longitude location of the map point
		/// </summary>
		/// <param name="latLngStr">Latitude/Longitude as a comma-separated string</param>
		public void SetLatLng(string latLngStr) {
			double[] latLng = Helpers.ConversionHelpers.StringToDoubleArray(latLngStr);
			this.SetLatLng(latLng);
		}


		/// <summary>
		/// Specifies that the location should be derived through an address search.
		/// </summary>
		/// <param name="address">Address to use when searching for the location.</param>
		public void SetAddress(string address) {
			this.Address = address;
			this.Latitude = null;
			this.Longitude = null;	
		}


		/// <summary>
		/// Establishes whether the latitude/longitude co-ordinates should be used when
		/// working out a location.
		/// </summary>
		/// <returns>
		/// Returns true if latitude/longitude should be used (both latitude and longitude are populated).
		/// Returns false if the address should be used (latitude and/or longitude are not populated).
		/// </returns>
		public bool UseLatLong() {
			if (this.Latitude.HasValue && this.Longitude.HasValue)
				return true;
			else 
				return false;
			
		} // UseLatLong

	} // Location

} // Mapping
