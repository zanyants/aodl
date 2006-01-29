/*
 * $Id: DocumentManifest.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
	/// DocumentManifest represent the spreadsheet manifest file
	/// </summary>
	public class DocumentManifest : AODL.Document.TextDocuments.DocumentManifest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentManifest"/> class.
		/// </summary>
		public DocumentManifest()
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
				Stream str			= ass.GetManifestResourceStream("AODL.Resources.OD.spreadsheetmanifest.xml");
				this.Manifest		= new XmlDocument();
				this.Manifest.Load(str);
			}
			catch(Exception ex)
			{
				throw;
			}
		}

	}
}

/*
 * $Log: DocumentManifest.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */