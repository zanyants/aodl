/*
 * $Id: DocumentConfiguration2.cs,v 1.2 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using System.IO;

namespace AODL.TextDocument
{
	/// <summary>
	/// Zusammenfassung für DocumentConfiguration2.
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
				throw ex;
			}
		}
	}
}

/*
 * $Log: DocumentConfiguration2.cs,v $
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