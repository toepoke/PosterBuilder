using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;

namespace PosterBuilder.Helpers
{
	public class XmlSerializerSectionHandler: IConfigurationSectionHandler
	{
		public object Create(object parent, object configContext, XmlNode section)
		{
			XPathNavigator nav = section.CreateNavigator();
			string typeName = (string)nav.Evaluate("string(@type)");
			Type t = Type.GetType(typeName);
			XmlSerializer ser = new XmlSerializer(t);
			XmlNodeReader xnr = new XmlNodeReader(section);
			object deSerialised = null;
			
			deSerialised = ser.Deserialize(xnr);

			return deSerialised;
		} // Create
	}

}
