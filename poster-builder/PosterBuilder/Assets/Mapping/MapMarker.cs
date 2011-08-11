using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PosterBuilder.Assets.Mapping
{

	/// <summary>
	/// Used for adding markers to a map, for full details, see 
	/// http://code.google.com/apis/maps/documentation/staticmaps/#Markers
	/// </summary>
	public class MapMarker {

		/// <summary>
		/// Size of the marker to be drawn on the map.
		/// </summary>
		public enum eSize {
			Normal,
			Tiny,
			Mid,
			Small
		}


		/// <summary>
		/// Default constructor
		/// </summary>
		public MapMarker() {
			this.Size = eSize.Normal;
			this.Colour = "#ffffcc";
			this.Label = "";
			this.Local = new Location();
		}


		/// <summary>
		/// Detailed constructor specifying the location the marker should be placed at
		/// </summary>
		/// <param name="location">Location of the where the marker should be placed on the map (latitude/longitude or address)</param>
		public MapMarker(Location location) {
			this.Local = location;
		}


		/// <summary>
		/// The colour of the marker this can be either a named colour of a hex colour.
		/// </summary>
		/// <remarks>
		/// See http://code.google.com/apis/maps/documentation/staticmaps/#MarkerStyles for details.
		/// </remarks>
		public string Colour = "";


		/// <summary>
		/// Size of the marker on the map.
		/// </summary>
		public eSize Size { get; set; }


		/// <summary>
		/// Sets a label to apply to the marker.  There are certain rules when this is, or is not applied,
		/// see http://code.google.com/apis/maps/documentation/staticmaps/#MarkerStyles for full details.
		/// </summary>
		public string Label { get; set; }


		/// <summary>
		/// Gets/Sets the location of the marker on the map.
		/// </summary>
		public Location Local { get; set; }
	
	} // MapMarker

} // Mapping
