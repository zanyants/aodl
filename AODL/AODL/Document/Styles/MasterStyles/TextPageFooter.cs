/*
 * $Id: TextPageFooter.cs,v 1.1 2007/02/13 17:58:55 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, 2007, Lars Behrmann, lb@OpenDocument4all.com
 * 
 */

using System;
using AODL.Document.Content;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;
using AODL.Document.Helper;
using System.Collections;
using System.Xml;


namespace AODL.Document.Styles.MasterStyles
{
	/// <summary>
	/// Summary for TextPageFooter.
	/// </summary>
	public class TextPageFooter : TextPageHeaderFooterBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TextPageFooter"/> class.
		/// </summary>
		public TextPageFooter()
		{
		}

		/// <summary>
		/// Create a new TextPageFooter object within the passed text master page.
		/// </summary>
		/// <param name="textMasterPage">The text master page.</param>
		public void New(TextMasterPage textMasterPage)
		{
			try
			{
				base.New(textMasterPage, "footer");
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
