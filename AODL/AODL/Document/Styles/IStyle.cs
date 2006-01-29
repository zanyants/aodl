/*
 * $Id: IStyle.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Styles.Properties;

namespace AODL.Document.Styles
{
	/// <summary>
	/// All styles that can be used within an document 
	/// in the OpenDocument format have to implement
	/// this interface.
	/// </summary>
	public interface IStyle
	{
		/// <summary>
		/// The Xml node which represent the
		/// style
		/// </summary>
		XmlNode Node {get; set;}
		/// <summary>
		/// The style name
		/// </summary>
		string StyleName {get; set;}
		/// <summary>
		/// The OpenDocument document to which this style
		/// belongs.
		/// </summary>
		IDocument Document {get; set;}
		/// <summary>
		/// Collection of properties.
		/// </summary>
		IPropertyCollection PropertyCollection {get; set;}
	}
}

/*
 * $Log: IStyle.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */