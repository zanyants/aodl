/*
 * $Id: IProperty.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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

namespace AODL.Document.Styles.Properties
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
		/// <summary>
		/// The style object to which this property object belongs
		/// </summary>
		IStyle Style {get; set;}
	}
}

/*
 * $Log: IProperty.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */