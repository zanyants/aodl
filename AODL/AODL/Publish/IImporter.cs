/*
 * $Id: IImporter.cs,v 1.1 2005/11/06 14:55:25 larsbm Exp $
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
		TextDocument.TextDocument Import(string filename);
		//Must give access to not importable objects as string
		ArrayList ImportError {get; }
		//Must have a name
		string ImporterName {get; }
	}
}

/*
 * $Log: IImporter.cs,v $
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */