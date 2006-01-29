/*
 * $Id: IContent.cs,v 1.1 2006/01/29 11:29:46 larsbm Exp $
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
using AODL.Document.Styles;
using AODL.Document;

namespace AODL.Document.Content
{
	/// <summary>
	/// All classes that will act as content document
	/// must implement this interface.
	/// </summary>
	public interface IContent
	{
		/// <summary>
		/// Every object (typeof(IContent)) have to know his document.
		/// </summary>
		IDocument Document {get; set;}
		/// <summary>
		/// Represents the XmlNode within the content.xml from the odt file.
		/// </summary>
		XmlNode Node {get; set;}
		/// <summary>
		/// The stylename wihich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		string StyleName {get; set;}
		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		IStyle Style {get; set;}
	}
}

/*
 * $Log: IContent.cs,v $
 * Revision 1.1  2006/01/29 11:29:46  larsbm
 * *** empty log message ***
 *
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */