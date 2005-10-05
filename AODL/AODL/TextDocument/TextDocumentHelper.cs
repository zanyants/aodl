using System;
using System.Xml;

namespace AODL.TextDocument
{
	/// <summary>
	/// Class with static helper methods for the TextDocument class.
	/// </summary>
	public class TextDocumentHelper
	{
		/// <summary>
		/// The XPath query for the offic:automatic-style element.
		/// </summary>
		public static string AutomaticStylePath = "/office:document-content/office:automatic-styles";
		/// <summary>
		/// The XPath query for the office:text element.
		/// </summary>
		public static string OfficeTextPath		= "/office:document-content/office:body/office:text";

		/// <summary>
		/// Getting a blank OpenDocument textdocument.
		/// </summary>
		/// <returns>The blank document. Which should be loaded for the TextDocument.</returns>
		public static string GetBlankDocument()
		{
			string header =
				"<office:document-content"
				+ " xmlns:office=\"urn:oasis:names:tc:opendocument:xmlns:office:1.0\""
				+ " xmlns:style=\"urn:oasis:names:tc:opendocument:xmlns:style:1.0\""
				+ " xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\""
				+ " xmlns:table=\"urn:oasis:names:tc:opendocument:xmlns:table:1.0\""
				+ " xmlns:draw=\"urn:oasis:names:tc:opendocument:xmlns:drawing:1.0\""
				+ " xmlns:fo=\"urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0\""
				+ " xmlns:xlink=\"http://www.w3.org/1999/xlink\""
				+ " xmlns:dc=\"http://purl.org/dc/elements/1.1/\""
				+ " xmlns:meta=\"urn:oasis:names:tc:opendocument:xmlns:meta:1.0\""
				+ " xmlns:number=\"urn:oasis:names:tc:opendocument:xmlns:datastyle:1.0\""
				+ " xmlns:svg=\"urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0\""
				+ " xmlns:chart=\"urn:oasis:names:tc:opendocument:xmlns:chart:1.0\"" 
				+ " xmlns:dr3d=\"urn:oasis:names:tc:opendocument:xmlns:dr3d:1.0\""
				+ " xmlns:math=\"http://www.w3.org/1998/Math/MathML\"" 
				+ " xmlns:form=\"urn:oasis:names:tc:opendocument:xmlns:form:1.0\""
				+ " xmlns:script=\"urn:oasis:names:tc:opendocument:xmlns:script:1.0\""
				+ " xmlns:ooo=\"http://openoffice.org/2004/office\"" 
				+ " xmlns:ooow=\"http://openoffice.org/2004/writer\""
				+ " xmlns:oooc=\"http://openoffice.org/2004/calc\""
				+ " xmlns:dom=\"http://www.w3.org/2001/xml-events\""
				+ " xmlns:xforms=\"http://www.w3.org/2002/xforms\""
				+ " xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\""
				+ " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\""
				+ " office:version=\"1.0\">"
				+ "<office:scripts />"
				+ "<office:font-face-decls>"
				+ "</office:font-face-decls>"
				+ "<office:automatic-styles>"
				+ "</office:automatic-styles>"
				+ "<office:body>"
				+ "<office:text>"
				+ "</office:text>"
				+ "</office:body>"
				+ "</office:document-content>";
			return header;
		}

		/// <summary>
		/// Create a new XmlNamespaceManager and fill it with all necessary namespaces and prefixes.
		/// </summary>
		/// <param name="nametable">The XmlNameTable of the TextDocument object XmlDocument.</param>
		/// <returns>The XmlNamespaceManager object.</returns>
		public static XmlNamespaceManager NameSpace(XmlNameTable nametable)
		{
			System.Xml.XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(nametable);
			xmlnsManager.AddNamespace("office",		"urn:oasis:names:tc:opendocument:xmlns:office:1.0");
			xmlnsManager.AddNamespace("style",		"urn:oasis:names:tc:opendocument:xmlns:style:1.0");
			xmlnsManager.AddNamespace("text",		"urn:oasis:names:tc:opendocument:xmlns:text:1.0");
			xmlnsManager.AddNamespace("table",		"urn:oasis:names:tc:opendocument:xmlns:table:1.0");
			xmlnsManager.AddNamespace("draw",		"urn:oasis:names:tc:opendocument:xmlns:drawing:1.0");
			xmlnsManager.AddNamespace("fo",			"urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0");
			xmlnsManager.AddNamespace("xlink",		"http://www.w3.org/1999/xlink");
			xmlnsManager.AddNamespace("dc",			"http://purl.org/dc/elements/1.1/");
			xmlnsManager.AddNamespace("meta",		"urn:oasis:names:tc:opendocument:xmlns:meta:1.0");
			xmlnsManager.AddNamespace("number",		"urn:oasis:names:tc:opendocument:xmlns:datastyle:1.0");
			xmlnsManager.AddNamespace("svg",		"urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0");
			xmlnsManager.AddNamespace("dr3d",		"urn:oasis:names:tc:opendocument:xmlns:chart:1.0");
			xmlnsManager.AddNamespace("math",		"http://www.w3.org/1998/Math/MathML");
			xmlnsManager.AddNamespace("form",		"urn:oasis:names:tc:opendocument:xmlns:form:1.0");
			xmlnsManager.AddNamespace("script",		"urn:oasis:names:tc:opendocument:xmlns:script:1.0");
			xmlnsManager.AddNamespace("ooo",		"http://openoffice.org/2004/office");
			xmlnsManager.AddNamespace("ooow",		"http://openoffice.org/2004/writer");
			xmlnsManager.AddNamespace("oooc",		"http://openoffice.org/2004/calc");
			xmlnsManager.AddNamespace("dom",		"http://www.w3.org/2001/xml-events");
			xmlnsManager.AddNamespace("xforms",		"http://www.w3.org/2002/xforms");
			xmlnsManager.AddNamespace("xsd",		"http://www.w3.org/2001/XMLSchema");
			xmlnsManager.AddNamespace("xsi",		"http://www.w3.org/2001/XMLSchema-instance");
			return xmlnsManager;
		}
	}
}
