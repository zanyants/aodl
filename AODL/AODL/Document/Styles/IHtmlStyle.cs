/*
 * $Id: IHtmlStyle.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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

namespace AODL.Document.Styles
{
	/// <summary>
	/// All IProperty implementations that
	/// offer a css style must implement this
	/// interface.
	/// </summary>
	public interface IHtmlStyle
	{
		/// <summary>
		/// Get the css style fragement
		/// </summary>
		/// <returns>The css style attribute</returns>
		string GetHtmlStyle();
	}
}

/*
 * $Log: IHtmlStyle.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.1  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 */