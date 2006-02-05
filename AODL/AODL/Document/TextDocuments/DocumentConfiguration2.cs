/*
 * $Id: DocumentConfiguration2.cs,v 1.2 2006/02/05 20:03:32 larsbm Exp $
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
using System.IO;

namespace AODL.Document.TextDocuments
{
	/// <summary>
	/// DocumentConfiguration2 represent the Configuration2 file.
	/// </summary>
	public class DocumentConfiguration2
	{
		/// <summary>
		/// The folder name.
		/// </summary>
		public static readonly string FolderName = "Configurations2";

		private string _filename;
		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName
		{
			get { return this._filename; }
			set { this._filename = value; }
		}

		private string _configurations2Content;
		/// <summary>
		/// Gets or sets the content of the configurations2.
		/// </summary>
		/// <value>The content of the configurations2.</value>
		public string Configurations2Content
		{
			get { return this._configurations2Content; }
			set { this._configurations2Content = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentConfiguration2"/> class.
		/// </summary>
		public DocumentConfiguration2()
		{
		}

		/// <summary>
		/// Loads the specified filename.
		/// </summary>
		/// <param name="filename">The filename.</param>
		public void Load(string filename)
		{
			try
			{
				StreamReader sr		= new StreamReader(filename);
				string line			= null;
				while((line = sr.ReadLine()) != null)
				{
					this.Configurations2Content	+= line;
				}
				sr.Close();
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}

/*
 * $Log: DocumentConfiguration2.cs,v $
 * Revision 1.2  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:30  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.3  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.2  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */