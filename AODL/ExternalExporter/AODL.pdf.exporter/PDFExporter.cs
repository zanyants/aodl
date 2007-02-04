using System;
using AODL.Document.Export;
using AODL.ExternalExporter.PDF.Document;

namespace AODL.ExternalExporter.PDF
{
	/// <summary>
	/// Summary for PDFExporter
	/// </summary>
	public class PDFExporter : IExporter
	{
		/// <summary>
		/// Invoked when the document has been exported.
		/// </summary>
		public delegate void ExportFinished();
		/// <summary>
		/// Invoked when the document has been exported.
		/// </summary>
		public event ExportFinished OnExportFinished;

		/// <summary>
		/// Initializes a new instance of the <see cref="PDFExporter"/> class.
		/// </summary>
		public PDFExporter()
		{
		}

		#region IExporter Member

		public System.Collections.ArrayList DocumentSupportInfos
		{
			get
			{
				// TODO:  Getter-Implementierung für PDFExporter.DocumentSupportInfos hinzufügen
				return null;
			}
		}

		public System.Collections.ArrayList ExportError
		{
			get
			{
				// TODO:  Getter-Implementierung für PDFExporter.ExportError hinzufügen
				return null;
			}
		}

		/// <summary>
		/// Exports the specified document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="filename">The filename.</param>
		public void Export(AODL.Document.IDocument document, string filename)
		{
			try
			{
				PDFDocument pdfDocument = new PDFDocument();
				pdfDocument.DoExport(document, filename);
				if(this.OnExportFinished != null)
				{
					this.OnExportFinished();
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		#endregion
	}
}
