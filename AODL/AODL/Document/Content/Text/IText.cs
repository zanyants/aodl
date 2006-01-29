/*
 * $Id: IText.cs,v 1.1 2006/01/29 11:28:22 larsbm Exp $
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
using AODL.Document;
using AODL.Document.Styles;

namespace AODL.Document.Content.Text
{
	/// <summary>
	/// IText all content that is text content must implement
	/// this interface.
	/// </summary>
	public interface IText
	{
		/// <summary>
		/// The node that represent the text content.
		/// </summary>
		XmlNode Node {get; set;}
		/// <summary>
		/// The text.
		/// </summary>
		string Text {get; set;}
		/// <summary>
		/// The document to which this text content belongs to.
		/// </summary>
		IDocument Document {get; set;}
		/// <summary>
		/// The style which is referenced with this text object.
		/// This is null if no style is available.
		/// </summary>
		IStyle Style {get; set;}
		/// <summary>
		/// The style name which is used for the referenced style.
		/// This is null is no  style is available.
		/// </summary>
		string StyleName {get; set;}
	}
}

/*
 * $Log: IText.cs,v $
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */