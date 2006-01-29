/*
 * $Id: ImportHandler.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
using AODL.Document.Exceptions;
using AODL.Document.Export;
using AODL.Document.Import.OpenDocument;

namespace AODL.Document.Import
{
	/// <summary>
	/// ImportHandler class to get the right IImporter implementations
	/// for the document to import.
	/// </summary>
	public class ImportHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ImportHandler"/> class.
		/// </summary>
		public ImportHandler()
		{
		
		}

		/// <summary>
		/// Gets the first importer that match the parameter criteria.
		/// </summary>
		/// <param name="documentType">Type of the document.</param>
		/// <param name="loadPath">The save path.</param>
		/// <returns></returns>
		public IImporter GetFirstImporter(DocumentTypes documentType, string loadPath)
		{
			string targetExtension			= ExportHandler.GetExtension(loadPath);

			foreach(IImporter iImporter in this.LoadImporter())
			{
				foreach(DocumentSupportInfo documentSupportInfo in iImporter.DocumentSupportInfos)
					if(documentSupportInfo.Extension.ToLower().Equals(targetExtension.ToLower()))
						if(documentSupportInfo.DocumentType == documentType)
							return iImporter;
			}
//			foreach(IImporter iImporter in this.LoadImporter())
//			{
//				foreach(string extension in iImporter.SupportedExtensions.Keys)
//					if(extension.ToLower().Equals(targetExtension.ToLower()))
//						if(documentType.ToString() == iImporter.SupportedExtensions[extension].ToString())
//							return iImporter;
//			}

			AODLException exception		= new AODLException("No importer available for type "+documentType.ToString()+" and extension "+targetExtension);
			exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
			throw exception;
		}

		/// <summary>
		/// Load internal and external importer.
		/// </summary>
		/// <returns></returns>
		private ArrayList LoadImporter()
		{
			try
			{
				ArrayList alImporter			= new ArrayList();				
				alImporter.Add(new OpenDocumentImporter());

				return alImporter;
			}
			catch(Exception ex)
			{	
				AODLException exception		= new AODLException("Error while trying to load the importer.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;
				throw exception;			
			}
		}
	}
}

/*
 * $Log: ImportHandler.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */