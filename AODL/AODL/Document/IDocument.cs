/*
 * $Id: IDocument.cs,v 1.2 2006/01/29 18:52:14 larsbm Exp $
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
using System.Collections;
using System.Xml;
using AODL.Document.TextDocuments;
using AODL.Document.Content;
using AODL.Document.Styles;

namespace AODL.Document
{
	/// <summary>
	/// IDocument.
	/// </summary>
	public interface IDocument
	{
		/// <summary>
		/// Every document must have a XmlNamespaceManager
		/// </summary>
		XmlNamespaceManager NamespaceManager {get; set;}
		/// <summary>
		/// Every document must have a XmlDocument that
		/// represent the content.
		/// </summary>
		XmlDocument XmlDoc {get; set;}
		/// <summary>
		/// Every document must give access to his meta data
		/// </summary>
		DocumentMetadata DocumentMetadata {get; set;}
		/// <summary>
		/// Every document must give access to his document configurations
		/// </summary>
		DocumentConfiguration2 DocumentConfigurations2 {get; set; }
		/// <summary>
		/// Every document must give access to his pictures
		/// </summary>
		DocumentPictureCollection DocumentPictures {get; set;}
		/// <summary>
		/// Every document must give access to his thumbnails
		/// </summary>
		DocumentPictureCollection DocumentThumbnails {get; set;}
		/// <summary>
		/// The font list
		/// </summary>
		ArrayList FontList {get; set;}
		/// <summary>
		/// Graphics used within the document.
		/// </summary>
		ArrayList Graphics {get;}
		/// <summary>
		/// Collection of local styles used with this document.
		/// </summary>
		IStyleCollection Styles {get; set;}
		/// <summary>
		/// Collection of common styles used with this document.
		/// </summary>
		IStyleCollection CommonStyles {get; set;}
		/// <summary>
		/// Collection of contents used by this document.
		/// </summary>
		IContentCollection Content {get; set;}
		/// <summary>
		/// Every document must offer CreateNode for creating
		/// new nodes
		/// </summary>
		/// <param name="name">The name of the node</param>
		/// <param name="prefix">The prefix of the node</param>
		/// <returns>The created node</returns>
		XmlNode CreateNode(string name, string prefix);
		/// <summary>
		/// Every document must offer CreateAttribute for creating
		/// new attributes
		/// </summary>
		/// <param name="name">The name of the attribute</param>
		/// <param name="prefix">The prefix of the attribute</param>
		/// <returns>The created attribute</returns>
		XmlAttribute CreateAttribute(string name, string prefix);
		/// <summary>
		/// If this file was loaded
		/// </summary>
		bool IsLoadedFile {get;}
		/// <summary>
		/// Load the given file.
		/// </summary>
		/// <param name="file"></param>
		void Load(string file);
	}

	/// <summary>
	/// By AODL supported OpenDocument document types.
	/// </summary>
	public enum DocumentTypes
	{
		/// <summary>
		/// OpenDocument Text document
		/// </summary>
		TextDocument,
		/// <summary>
		/// OpenDocument Spreadsheet document
		/// </summary>
		SpreadsheetDocument
	}
}

/*
 * $Log: IDocument.cs,v $
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */