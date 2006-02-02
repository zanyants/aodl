/*
 * $Id: CsvImporter.cs,v 1.1 2006/02/02 21:55:59 larsbm Exp $
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
using System.Collections;
using System.Diagnostics;
using AODL.Document;
using AODL.Document.Import;
using AODL.Document.Export;
using AODL.Document.Exceptions;
using AODL.Document.Content;
using AODL.Document.Content.Text;
using AODL.Document.Content.Tables;
using AODL.Document.SpreadsheetDocuments;

namespace AODL.Document.Import.PlainText
{
	/// <summary>
	/// CsvImporter, a class for importing csv files into
	/// OpenDocument spreadsheet documents.
	/// </summary>
	public class CsvImporter : IImporter, IPublisherInfo
	{
		/// <summary>
		/// The document to fill with content.
		/// </summary>
		internal IDocument _document;

		/// <summary>
		/// Initializes a new instance of the <see cref="CsvImporter"/> class.
		/// </summary>
		public CsvImporter()
		{
			this._importError					= new ArrayList();
			
			this._supportedExtensions			= new ArrayList();
			this._supportedExtensions.Add(new DocumentSupportInfo(".csv", DocumentTypes.SpreadsheetDocument));

			this._author						= "Lars Behrmann, lb@OpenDocument4all.com";
			this._infoUrl						= "http://AODL.OpenDocument4all.com";
			this._description					= "This the standard importer for comma seperated text files of the OpenDocument library AODL.";
		}

		#region IExporter Member

		private ArrayList _supportedExtensions;
		/// <summary>
		/// Gets the document support infos.
		/// </summary>
		/// <value>The document support infos.</value>
		public ArrayList DocumentSupportInfos
		{
			get { return this._supportedExtensions; }
		}

		/// <summary>
		/// Imports the specified filename.
		/// </summary>
		/// <param name="document">The TextDocument to fill.</param>
		/// <param name="filename">The filename.</param>		
		/// <returns>The created TextDocument</returns>
		public void Import(IDocument document, string filename)
		{
			try
			{
				this._document			= document;
				ArrayList lines			= this.GetFileContent(filename);
					
				if(lines.Count > 0)
					this.CreateTables(lines);
				else
				{
					AODLWarning warning	= new AODLWarning("Empty file. ["+filename+"]");
					this.ImportError.Add(warning);
				}
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		private ArrayList _importError;
		/// <summary>
		/// Gets the import errors as ArrayList of strings.
		/// </summary>
		/// <value>The import errors.</value>
		public System.Collections.ArrayList ImportError
		{
			get
			{
				return this._importError;
			}
		}

		/// <summary>
		/// If the import file format isn't any OpenDocument
		/// format you have to return true and AODL will
		/// create a new one.
		/// </summary>
		/// <value></value>
		public bool NeedNewOpenDocument
		{
			get { return true; }
		}

		#endregion

		#region IPublisherInfo Member

		private string _author;
		/// <summary>
		/// The name the Author
		/// </summary>
		/// <value></value>
		public string Author
		{
			get
			{
				return this._author;
			}
		}

		private string _infoUrl;
		/// <summary>
		/// Url to a info site
		/// </summary>
		/// <value></value>
		public string InfoUrl
		{
			get
			{
				return this._infoUrl;
			}
		}

		private string _description;
		/// <summary>
		/// Description about the exporter resp. importer
		/// </summary>
		/// <value></value>
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		#endregion

		/// <summary>
		/// Creates the tables.
		/// </summary>
		/// <param name="lines">The lines.</param>
		private void CreateTables(ArrayList lines)
		{
			string unicodeDelimiter				= "\u00BF"; // turned question mark

			if(lines != null)
			{
				Table table						= TableBuilder.CreateSpreadsheetTable(
					(SpreadsheetDocument)this._document, "Table1", "table1");
				//First line must specify the used delimiter
				string delimiter				= lines[0] as string;
				lines.RemoveAt(0);

				try
				{
					//Perform lines
					foreach(string line in lines)
					{
						string lineContent			= line.Replace(delimiter, unicodeDelimiter);
						string[] cellContents		= lineContent.Split(unicodeDelimiter.ToCharArray());
						Row row						= new Row(table);
						foreach(string cellContent in cellContents)
						{
							Cell cell				= new Cell(table);
							Paragraph paragraph		= ParagraphBuilder.CreateSpreadsheetParagraph(this._document);
							paragraph.TextContent.Add(new SimpleText(this._document, cellContent));
							cell.Content.Add(paragraph);
							row.InsertCellAt(row.CellCollection.Count, cell);
						}
						table.RowCollection.Add(row);
					}
				}
				catch(Exception ex)
				{
					AODLException aodlExeception		= new AODLException("Error while proccessing the csv file.");
					aodlExeception.InMethod				= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
					aodlExeception.OriginalException	= ex;

					throw aodlExeception;
				}

				this._document.Content.Add(table);
			}
		}

		/// <summary>
		/// Gets the content of the file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns>All text lines as an ArrayList of strings.</returns>
		private ArrayList GetFileContent(string fileName)
		{
			ArrayList lines						= new ArrayList();

			try
			{
				StreamReader sReader	= File.OpenText(fileName);
				string currentLine		= null;

				while((currentLine = sReader.ReadLine()) != null)
				{
					lines.Add(currentLine);
				}
				sReader.Close();
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return lines;
		}
	}
}

/*
 * $Log: CsvImporter.cs,v $
 * Revision 1.1  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 */