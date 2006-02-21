/*
 * $Id: TextDocumentHelper.cs,v 1.2 2006/02/21 19:34:56 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, Lars Behrmann, lb@OpenDocument4all.com
 * 
 * Last changes:
 * 
 */

using System;
using System.Xml;

namespace AODL.Document.TextDocuments
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
				"<?xml version=\"1.0\" encoding=\"UTF-8\" ?> <office:document-content"
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
				+ "<style:font-face style:name=\"StarSymbol\" svg:font-family=\"StarSymbol\" style:font-charset=\"x-symbol\" />"
				+ "<style:font-face style:name=\"Tahoma1\" svg:font-family=\"Tahoma\" /> "
				+ "<style:font-face style:name=\"Lucida Sans Unicode\" svg:font-family=\"'Lucida Sans Unicode'\" style:font-pitch=\"variable\" />"
				+ "<style:font-face style:name=\"Tahoma\" svg:font-family=\"Tahoma\" style:font-pitch=\"variable\" />" 
				+ "<style:font-face style:name=\"Times New Roman\" svg:font-family=\"'Times New Roman'\" style:font-family-generic=\"roman\" style:font-pitch=\"variable\" />"
			    + "</office:font-face-decls>"
				+ "<office:automatic-styles>"
				+ "</office:automatic-styles>"
				+ "<office:body>"
				+ "<office:text>"
				+ "<office:forms form:automatic-focus=\"false\" form:apply-design-mode=\"false\" /> "
				+ "<text:sequence-decls>"
				+ "<text:sequence-decl text:display-outline-level=\"0\" text:name=\"Illustration\" />" 
				+ "<text:sequence-decl text:display-outline-level=\"0\" text:name=\"Table\" /> "
				+ "<text:sequence-decl text:display-outline-level=\"0\" text:name=\"Text\" />"
				+ "<text:sequence-decl text:display-outline-level=\"0\" text:name=\"Drawing\" />"
				+ "</text:sequence-decls>"
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

/*
 * $Log: TextDocumentHelper.cs,v $
 * Revision 1.2  2006/02/21 19:34:56  larsbm
 * - Fixed Bug text that contains a xml tag will be imported  as UnknowText and not correct displayed if document is exported  as HTML.
 * - Fixed Bug [ 1436080 ] Common styles
 *
 * Revision 1.1  2006/01/29 11:28:30  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.4  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.3  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 * Revision 1.2  2005/10/08 07:50:15  larsbm
 * - added cvs tags
 *
 */