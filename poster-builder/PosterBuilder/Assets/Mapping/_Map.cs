using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Drawing;

namespace PosterBuilder.Assets.Mapping
{
	/// <summary>
	/// Used for drawing a map onto the template image.  
	/// 
	/// Note a great deal of the properties in this class return the class itself.
	/// This is to employ a fluent interface to specifying the properties.
	/// </summary>
	public class Map: Image
	{
		/// <summary>
		/// URL to use when querying Google Maps for a map.
		/// (note you no longer need a key for the API, hence no entry point).
		/// </summary>
		public static string GOOGLE_STATIC_MAPS_URL = "http://maps.googleapis.com/maps/api/staticmap";


		/// <summary>
		/// Defines the types of rendered maps that are supported.
		/// </summary>
		/// <remarks>
		/// For further details, see http://code.google.com/apis/maps/documentation/staticmaps/#MapTypes
		/// </remarks>
		public enum MapType {
			Road = 1,
			Satellite = 2,
			Hybrid = 3
		}

		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		public Map(Graphics gdi) : base(gdi, "") {
			this._Markers = new List<MapMarker>();
			this._Location = new Location();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gdi">Graphics object used to draw the template, shared between all drawing objects</param>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		public Map(Graphics gdi, string id) : base(gdi, id) {
			this._Markers = new List<MapMarker>();
			this._Location = new Location();
		}


		/// <summary>
		/// Copy constructor: Initialises this object from the copy
		/// </summary>
		/// <param name="copy">Object to make a copy from</param>
		public Map(Map copy) : base((Image)copy) {
			_Location = copy._Location;
			_MapType = copy._MapType;
			_ZoomLevel = copy._ZoomLevel;
			_Width = copy._Width;
			_Height = copy._Height;
			_Markers = new List<MapMarker>(copy._Markers);
		}
		

		/// <summary>
		/// Where the map should be centred.
		/// </summary>
		protected Location _Location = null;
		
		
		/// <summary>
		/// Type of map that should be rendered (road or hybrid, etc).
		/// </summary>
		/// <remarks>
		/// See http://code.google.com/apis/maps/documentation/staticmaps/#MapTypes for details
		/// </remarks>
		protected internal MapType _MapType = MapType.Road;


		/// <summary>
		/// Zoom level to use when drawing the map
		/// </summary>
		/// <remarks>
		/// See http://code.google.com/apis/maps/documentation/staticmaps/#Zoomlevels for details
		/// </remarks>
		protected internal int _ZoomLevel = 16;
		
		/// <summary>
		/// Width of the rendered map.
		/// </summary>
		/// <remarks>
		/// See http://code.google.com/apis/maps/documentation/staticmaps/#Imagesizes for details
		/// </remarks>
		protected internal int _Width = 640;

		/// <summary>
		/// Height of the rendered map.
		/// </summary>
		/// <remarks>
		/// See http://code.google.com/apis/maps/documentation/staticmaps/#Imagesizes for details
		/// </remarks>
		protected internal int _Height = 640;
		

		/// <summary>
		/// Specifies a set of markers that can be added to the map
		/// </summary>
		protected internal List<MapMarker> _Markers = null;


		/// <summary>
		/// Sets the location of the centre of the map when it is rendered.
		/// </summary>
		/// <param name="lat">Latitude of the centre</param>
		/// <param name="lon">Longitude of the centre</param>
		/// <remarks>
		/// See http://code.google.com/apis/maps/documentation/staticmaps/#Locations for details
		/// </remarks>
		public Map Centre(double lat, double lon) {
			this._Location.SetLatLng(lat, lon);

			return this;
		}


		/// <summary>
		/// Sets the location of the centre of the map when it is rendered.
		/// </summary>
		/// <param name="latLon">Lat/Long of the centre (index 0 being the Latitude, index 1 being the Longitude</param>
		/// <remarks>
		/// See http://code.google.com/apis/maps/documentation/staticmaps/#Locations for details
		/// </remarks>
		public Map Centre(double[] latLon) {
			this._Location.SetLatLng(latLon);
			return (Map)this;
		}


		/// <summary>
		/// Sets the location of the centre of the map when it is rendered.
		/// </summary>
		/// <param name="centre">Lat/Long of the centre, but in a comma-delimited format 
		/// (useful for a method of easy input).</param>
		/// <remarks>
		/// See http://code.google.com/apis/maps/documentation/staticmaps/#Locations for details
		/// </remarks>
		public Map Centre(string centre) {
			this._Location.SetAddress(centre);
			return (Map)this;
		}


		/// <summary>
		/// Sets the latitude of the centre of the map.
		/// </summary>
		/// <param name="lat">Latitude of the centre of the map.</param>
		public Map Latitude(double lat) {
			this._Location.Latitude = lat;
			return (Map)this;
		}


		/// <summary>
		/// Sets the longitude of the centre of the map.
		/// </summary>
		/// <param name="lon">Longitude of the centre of the map</param>
		public Map Longitude(double lon) {
			this._Location.Longitude = lon;
			return (Map)this;
		}


		/// <summary>
		/// Type of map to be rendered
		/// </summary>
		/// <param name="mapType">Road, hybrid, etc</param>
		public Map Type(MapType mapType) {
			this._MapType = mapType;
			return this;
		}


		/// <summary>
		/// Type of map to be rendered
		/// </summary>
		/// <param name="mapType">Road, Hybrid or Satellite</param>
		public Map Type(string mapType) {
			this._MapType = (MapType) Enum.Parse(typeof(MapType), mapType, false);

			return this;
		}


		/// <summary>
		/// Zoom level to be used when drawing the map
		/// </summary>
		/// <param name="zoomLevel">Map zoom level</param>
		/// <remarks>
		/// See http://code.google.com/apis/maps/documentation/staticmaps/#Zoomlevels for details
		/// </remarks>
		public Map Zoom(int zoomLevel) {
			this._ZoomLevel = zoomLevel;
			return this;
		}


		/// <summary>
		/// Width (in pixels) of the rendered map.
		/// </summary>
		/// <param name="width">Width of the map (pixels)</param>
		/// <remarks>
		/// See http://code.google.com/apis/maps/documentation/staticmaps/#Imagesizes for details
		/// </remarks>
		public Map Width(int width) {
			this._Width = width;
			return this;
		}


		/// <summary>
		/// Height (in pixels) of the rendered map.
		/// </summary>
		/// <param name="height">Height of the map (pixels)</param>
		/// <remarks>
		/// See http://code.google.com/apis/maps/documentation/staticmaps/#Imagesizes for details
		/// </remarks>
		public Map Height(int height) {
			this._Height = height;
			return this;
		}


		/// <summary>
		/// A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </summary>
		/// <param name="id">A unique reference to the object being constructed.  This isn't really required, 
		/// but useful when debugging as the id is output when the guides are active so you can see which id corresponds
		/// to which box being drawn.
		/// </param>
		new public Map ID(string id) {
			return (Map) base.ID(id);			
		}


		/// <summary>
		/// Sets the X,Y location of where the map should be draw on the template.
		/// </summary>
		/// <param name="x">X co-ordinate</param>
		/// <param name="y">Y co-ordinate</param>
		/// <returns></returns>
		new public Map TopLeft(int x, int y) {
			base.TopLeft(x, y);
			return this;
		} 


		/// <summary>
		/// Sets the X,Y location of where the map should be draw on the template.
		/// </summary>
		/// <param name="coord">Sets the X (index 0) and Y (index 1) co-ordinates.</param>
		/// <returns></returns>
		new public Map TopLeft(int[] coord) {
			base.TopLeft(coord);
			return this;
		}


		/// <summary>
		/// Sets the X,Y location of where the map should be draw on the template.
		/// </summary>
		/// <param name="position">Position to render the map at.</param>
		/// <returns></returns>
		new public Map TopLeft(Position position) {
			base.TopLeft(position);
			return this;
		}


		/// <summary>
		/// Responsible for retrieving the map from the Google servers ready for
		/// drawing on the canvas
		/// </summary>
		/// <returns>
		/// Returns an GDI Image object with the map drawn on it.
		/// </returns>
		protected internal override System.Drawing.Image GetImage()
		{
			string url = GetMapLink();
			WebClient wc = new WebClient();
			byte[] imgBytes = wc.DownloadData(url);
			MemoryStream imgStrm = new MemoryStream(imgBytes);
			return System.Drawing.Image.FromStream(imgStrm);
		}


		/// <summary>
		/// Responsible for drawing the map onto the canvas 
		/// (i.e. the copy of our original template)
		/// </summary>
		protected internal override void Render() {
			System.Drawing.Image imgMap = this.GetImage();
			
			this.Canvas.DrawImage(imgMap, this.X, this.Y);

			imgMap.Dispose();

		} // Render


		/// <summary>
		/// Responsible for constructing a query (which it just a normal URL call) 
		/// to the Google servers to render the map (with any markers that have been defined).
		/// The URL holds the instructions to the mapping servers of how to draw the map.
		/// </summary>
		/// <returns>
		/// URL to use when calling the map servers.
		/// </returns>
		protected string GetMapLink() {
			StringBuilder mapLink = new StringBuilder(GOOGLE_STATIC_MAPS_URL);

			if (this._Location.UseLatLong())
				mapLink.AppendFormat("?center={0},{1}", this._Location.Latitude, this._Location.Longitude);
			else 
				mapLink.AppendFormat("?center={0}", HttpUtility.UrlEncode(this._Location.Address));
			mapLink.AppendFormat("&zoom={0}", this._ZoomLevel);
			mapLink.AppendFormat("&size={0}x{0}", this._Width, this._Height);
			mapLink.AppendFormat("&maptype={0}",  HttpUtility.UrlEncode(this.GetMapType()) );
			mapLink.Append("&sensor=false");

			// Add a marker to the centre of the map
			this._Markers.Add(new MapMarker(this._Location));

			AddMapMarkers(mapLink);

			return mapLink.ToString();
		}


		/// <summary>
		/// Appends all the markers defines to the URL already constructed (which is sent to the
		/// mapping servers).
		/// </summary>
		/// <param name="sb"></param>
		protected void AddMapMarkers(StringBuilder sb) {
			if (this._Markers == null || !this._Markers.Any()) 
				// nothing to see here!
				return;		
				
			foreach (MapMarker marker in 	this._Markers) {
				sb.AppendFormat("&markers=");
				sb.AppendFormat("color:{0}", HttpUtility.UrlEncode(marker.Colour + "|"));
				if (!string.IsNullOrEmpty(marker.Label))
					sb.AppendFormat("label:{0}", HttpUtility.UrlEncode(marker.Label + "|"));
				sb.AppendFormat("size:{0}", marker.Size + "|");
				if (this._Location.UseLatLong())
					sb.AppendFormat("{0},{1}", this._Location.Latitude, this._Location.Longitude);
				else 
					sb.AppendFormat("{0}", HttpUtility.UrlEncode(this._Location.Address));
			}

		} // AddMapMarkers


		/// <summary>
		/// Converts the internal enumeration representation of the mapping type to use, to what
		/// the mapping servers expect.
		/// </summary>
		/// <returns>
		/// Returns string representation of the map type.
		/// </returns>
		protected string GetMapType() {
			switch (this._MapType) {
				case MapType.Road: return "road";
				case MapType.Hybrid: return "hybrid";
				case MapType.Satellite: return "satellite";
				default: throw new ArgumentException(string.Format("{0} is not a known map type.", this._MapType));
			}		
		} // GetMapType

	} // Map

} // Mapping
