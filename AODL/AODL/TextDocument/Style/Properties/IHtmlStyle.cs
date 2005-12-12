/*
 * $Id: IHtmlStyle.cs,v 1.1 2005/12/12 19:39:17 larsbm Exp $
 */

using System;

namespace AODL.TextDocument.Style.Properties
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
 * Revision 1.1  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 */