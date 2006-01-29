/*
 * $Id: ExportHandler.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
using AODL.Document.Export.Html;
using AODL.Document.Export.OpenDocument;

namespace AODL.Document.Export
{
	/// <summary>
	/// ExportHandler class to get the right IExporter implementations
	/// for the document to export.
	/// </summary>
	public class ExportHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExportHandler"/> class.
		/// </summary>
		public ExportHandler()
		{
		
		}

		/// <summary>
		/// Gets the first exporter that match the parameter criteria.
		/// </summary>
		/// <param name="documentType">Type of the document.</param>
		/// <param name="savePath">The save path.</param>
		/// <returns></returns>
		public IExporter GetFirstExporter(DocumentTypes documentType, string savePath)
		{
			string targetExtension			= GetExtension(savePath);

			foreach(IExporter iExporter in this.LoadExporter())
			{
				foreach(DocumentSupportInfo documentSupportInfo in iExporter.DocumentSupportInfos)
					if(documentSupportInfo.Extension.ToLower().Equals(targetExtension.ToLower()))
						if(documentSupportInfo.DocumentType == documentType)
							return iExporter;
			}

			AODLException exception		= new AODLException("No exporter available for type "+documentType.ToString()+" and extension "+targetExtension);
			exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
			throw exception;
		}

		/// <summary>
		/// Load internal and external exporter.
		/// </summary>
		/// <returns></returns>
		private ArrayList LoadExporter()
		{
			try
			{
				ArrayList alExporter			= new ArrayList();
				
				alExporter.Add(new OpenDocumentTextExporter());
				alExporter.Add(new OpenDocumentHtmlExporter());

				return alExporter;
			}
			catch(Exception ex)
			{	
				AODLException exception		= new AODLException("Error while trying to load the exporter.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;
				throw exception;			
			}
		}

		/// <summary>
		/// Gets the extension.
		/// </summary>
		/// <param name="aFullPathOrFileName">Name of a full path or file.</param>
		/// <returns></returns>
		public static string GetExtension(string aFullPathOrFileName)
		{
			int point				= aFullPathOrFileName.LastIndexOf(".");

			return aFullPathOrFileName.Substring(point);
		}
	}
}

/*
 * $Log: ExportHandler.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */