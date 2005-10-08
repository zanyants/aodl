/*
 * $Id: IContent.cs,v 1.2 2005/10/08 08:19:25 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;
using AODL.TextDocument;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// All classes that will act as content within textdocument
	/// must implement this interface.
	/// </summary>
	public interface IContent
	{
		/// <summary>
		/// Every object (typeof(IContent)) have to know his TextDocument.
		/// </summary>
		TextDocument Document {get; set;}
		/// <summary>
		/// Represents the XmlNode within the content.xml from the odt file.
		/// </summary>
		XmlNode Node {get; set;}
		/// <summary>
		/// The stylename wihich is referenced with the content object.
		/// </summary>
		string Stylename {get; set;}
		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// </summary>
		IStyle Style {get; set;}
		/// <summary>
		/// All Content objects have a Text container. Which represents
		/// his Text this could be SimpleText, FormatedText or mixed.
		/// </summary>
		ITextCollection TextContent {get; set;}
	}
}

/*
 * $Log: IContent.cs,v $
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */