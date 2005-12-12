/*
 * $Id: IHtml.cs,v 1.1 2005/12/12 19:39:17 larsbm Exp $
 */

using System;

namespace AODL.TextDocument.Content
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
 * Revision 1.1  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 */