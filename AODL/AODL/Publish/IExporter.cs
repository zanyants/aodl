/*
 *  $Id: IExporter.cs,v 1.2 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using AODL.TextDocument;

namespace AODL.Export
{
	/// <summary>
	/// IExporter all classes that want to act as exporter
	/// have to implement this interface.
	/// </summary>
	public interface IExporter
	{
		//The export errórs as string		
		/// <summary>
		/// Gets the export erros.
		/// </summary>
		/// <value>The export erros.</value>
		System.Collections.ArrayList ExportErros {get; }
		//Export the document		
		/// <summary>
		/// Exports the specified document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="filename">The filename.</param>
		void Export(AODL.TextDocument.TextDocument document, string filename);
	}
}

/*
 * $Log: IExporter.cs,v $
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