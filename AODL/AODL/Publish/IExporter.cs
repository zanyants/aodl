/*
 *  $Id: IExporter.cs,v 1.1 2005/11/06 14:55:25 larsbm Exp $
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
		System.Collections.ArrayList ExportErros {get; }
		//Export the document
		void Export(AODL.TextDocument.TextDocument document, string filename);
	}
}

/*
 * $Log: IExporter.cs,v $
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */