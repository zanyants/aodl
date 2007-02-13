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
		public iTextSharp.text.pdf.PdfPTable Convert(Table table)
		{
			try
			{
				iTextSharp.text.pdf.PdfPTable pdfTable;
				TableLayoutInfo tableLayout = new TableLayoutInfo();
				tableLayout.AnalyzeTableLayout(table);

				if(tableLayout.CellWidths != null)
				{
					pdfTable = new iTextSharp.text.pdf.PdfPTable(tableLayout.CellWidths);
				}
				else
				{
					pdfTable = new iTextSharp.text.pdf.PdfPTable(tableLayout.MaxCells);
				}

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
				
				//pdfTable = this.SetProperties(table, pdfTable, maxCells);

				return pdfTable;
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Converts the specified table.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="table">The table.</param>
		/// <returns>The PDF table with converted table properties.</returns>
		private iTextSharp.text.pdf.PdfPTable SetProperties(Table table, iTextSharp.text.pdf.PdfPTable pdfTable, int maxCells)
		{
			try
			{
				if(table.Style != null 
					&& table.Style is TableStyle
					&& ((TableStyle)table.Style).TableProperties != null)
				{
					string strWidth = ((TableStyle)table.Style).TableProperties.Width;
					if(strWidth != null)
					{
						double dWidth = AODL.Document.Helper.SizeConverter.GetDoubleFromAnOfficeSizeValue(strWidth);
						if(dWidth != 0)
						{
							if(AODL.Document.Helper.SizeConverter.IsCm(strWidth))
							{
								dWidth = AODL.ExternalExporter.PDF.Document.Helper.MeasurementHelper.CmToPoints(dWidth);
								pdfTable.LockedWidth = true;
								pdfTable.TotalWidth = (float)dWidth;
							}
							else if(AODL.Document.Helper.SizeConverter.IsInch(strWidth))
							{
								dWidth = AODL.ExternalExporter.PDF.Document.Helper.MeasurementHelper.CmToPoints(dWidth);
								pdfTable.LockedWidth = true;
								pdfTable.TotalWidth = (float)dWidth;
							}
						}
					}
					else
					{
						// assume that's a 100% table
					}
				}

				return pdfTable;
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Sets the cell properties.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="pdfCell">The PDF cell.</param>
		/// <returns>The PDF cell with converted odf cell properties.</returns>
		private iTextSharp.text.pdf.PdfPCell SetCellProperties(Cell cell, iTextSharp.text.pdf.PdfPCell pdfCell)
		{
			try
			{
				return null;

			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
