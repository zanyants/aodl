using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// All classes that will act as TextContent must implement
	/// this interface.
	/// </summary>
	public interface IText
	{
		/// <summary>
		/// Represent the xml node within the content.xml file of the open document.
		/// </summary>
		XmlNode Node {get; set;}
		/// <summary>
		/// The Text that will be displayed.
		/// </summary>
		string Text {get; set;}
		/// <summary>
		/// An style object that is referenced with this TextContent e.g a FormatedText object.
		/// </summary>
		IStyle Style {get; set;}
		/// <summary>
		/// A IText object must know his IContent object.
		/// </summary>
		IContent Content {get; set;}
		/// <summary>
		/// Returns the InnerXml value of the XmlNode.
		/// </summary>
		string Xml {get; }
	}
}
