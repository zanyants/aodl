/*
 * $Id: DocumentStyles.cs,v 1.2 2006/02/05 20:03:32 larsbm Exp $
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
using System.Reflection;
using System.IO;

namespace AODL.Document.SpreadsheetDocuments
{
	/// <summary>
	/// DocumentStyles represent the styles.xml file of a file in the
	/// OpenDocument format.
	/// </summary>
	public class DocumentStyles : AODL.Document.TextDocuments.DocumentStyles
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentStyles"/> class.
		/// </summary>
		public DocumentStyles()
		{
		}

		/// <summary>
		/// Load the style from assmebly resource.
		/// </summary>
		public override void New()
		{
			try
			{
				Assembly ass		= Assembly.GetExecutingAssembly();
				Stream str			= ass.GetManifestResourceStream("AODL.Resources.OD.spreadsheetstyles.xml");
				this.Styles			= new XmlDocument();
				this.Styles.Load(str);
			}
			catch(Exception ex)
			{
				throw;
			}
		}	
	}
}

/*
 * $Log: DocumentStyles.cs,v $
 * Revision 1.2  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */