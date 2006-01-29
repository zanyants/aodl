/*
 * $Id: IHtml.cs,v 1.1 2006/01/29 11:29:46 larsbm Exp $
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

namespace AODL.Document.Content
{
	/// <summary>
	/// All content that could offer their text as
	/// HTML must implement this interface.
	/// </summary>
	public interface IHtml
	{
		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		string GetHtml();
	}
}

/*
 * $Log: IHtml.cs,v $
 * Revision 1.1  2006/01/29 11:29:46  larsbm
 * *** empty log message ***
 *
 * Revision 1.1  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 */