/*
 * $Id: IImporter.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
using AODL.Document.TextDocuments;
using System.Collections;
using AODL.Document;

namespace AODL.Document.Import
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
		void Import(IDocument document,string filename);
		//Must give access to not importable objects as string
		/// <summary>
		/// Gets the import error.
		/// </summary>
		/// <value>The import error.</value>
		ArrayList ImportError {get; }
		/// <summary>
		/// ArrayList of DocumentSupportInfo objects
		/// </summary>
		/// <value>ArrayList of DocumentSupportInfo objects.</value>
		ArrayList DocumentSupportInfos {get; }
	}
}

/*
 * $Log: IImporter.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
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