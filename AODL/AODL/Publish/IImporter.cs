/*
 * $Id: IImporter.cs,v 1.2 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using AODL.TextDocument;
using System.Collections;

namespace AODL.Import
{
	/// <summary>
	/// All classes that want to act as an importer have to
	/// to implement this interface.
	/// </summary>
	public interface IImporter
	{
		//A Importer class have to return a TextDocument object
		/// <summary>
		/// Imports the specified document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="filename">The filename.</param>
		void Import(TextDocument.TextDocument document,string filename);
		//Must give access to not importable objects as string
		/// <summary>
		/// Gets the import error.
		/// </summary>
		/// <value>The import error.</value>
		ArrayList ImportError {get; }
		//Must have a name
		/// <summary>
		/// Gets the name of the importer.
		/// </summary>
		/// <value>The name of the importer.</value>
		string ImporterName {get; }
	}
}

/*
 * $Log: IImporter.cs,v $
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