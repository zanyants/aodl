/*
 * $Id: OpenDocumentTextImporter.cs,v 1.1 2005/11/06 14:55:25 larsbm Exp $
 */

using System;
using System.Collections;
using AODL.TextDocument;

namespace AODL.Import
{
	/// <summary>
	/// OpenDocumentTextImporter - Importer for OpenDocuments in the Text format.
	/// </summary>
	public class OpenDocumentTextImporter : IImporter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OpenDocumentTextImporter"/> class.
		/// </summary>
		public OpenDocumentTextImporter()
		{
			this._importError		= new ArrayList();
		}

		#region IImporter Member

		/// <summary>
		/// Imports the specified filename.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <returns>The created TextDocument</returns>
		public AODL.TextDocument.TextDocument Import(string filename)
		{
			// TODO:  Implementierung von OpenDocumentTextImporter.Import hinzufügen
			return null;
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

		private string _openDocumentTextImporter	= "OpenDocumentText";
		/// <summary>
		/// Gets the name of the importer.
		/// </summary>
		/// <value>The name of the importer.</value>
		public string ImporterName
		{
			get
			{
				return this._openDocumentTextImporter;
			}
		}

		#endregion
	}
}

/*
 * $Log: OpenDocumentTextImporter.cs,v $
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */