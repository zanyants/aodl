using System;
using System.Xml;

namespace AODL.TextDocument.Style.Properties
{
	/// <summary>
	/// All classes that should act as a Property class must implement this
	/// interface.
	/// </summary>
	public interface IProperty
	{
		/// <summary>
		/// The XmlNode which represent the property element.
		/// </summary>
		XmlNode Node {get; set;}
	}
}
