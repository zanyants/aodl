using System;
using AODL.Document.Content.Tables;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;

namespace AODL.ExternalExporter.PDF.Document.ContentConverter
{
	/// <summary>
	/// Summary for TableConverter.
	/// </summary>
	public class TableConverter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TableConverter"/> class.
		/// </summary>
		public TableConverter()
		{
		}

		/// <summary>
		/// Converts the specified table.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <returns>The PDF table.</returns>
		public static iTextSharp.text.pdf.PdfPTable Convert(Table table)
		{
			try
			{
				int maxCells = 1;
				foreach(Row r in table.RowCollection)
				{
					if(r.CellCollection.Count > maxCells)
					{
						maxCells = r.CellCollection.Count;
					}
				}

				iTextSharp.text.pdf.PdfPTable pdfTable = new iTextSharp.text.pdf.PdfPTable(maxCells);
				if(table.Style != null 
					&& table.Style is TableStyle 
					&& ((TableStyle)table.Style).TableProperties != null)
				{
					//((TableStyle)table.Style).TableProperties.Width
				}

				foreach(Row row in table.RowCollection)
				{
					foreach(Cell cell in row.CellCollection)
					{
						iTextSharp.text.pdf.PdfPCell pdfCell = new iTextSharp.text.pdf.PdfPCell();
						
						if(cell.ColumnRepeating != null && Int32.Parse(cell.ColumnRepeating) > 0)
						{
							pdfCell.Colspan = Int32.Parse(cell.ColumnRepeating);							
						}

						foreach(iTextSharp.text.IElement pdfElement in MixedContentConverter.GetMixedPdfContent(cell.Content))
						{
							pdfCell.AddElement(pdfElement);
						}
						pdfTable.AddCell(pdfCell);		
					}
				}

				return pdfTable;
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
