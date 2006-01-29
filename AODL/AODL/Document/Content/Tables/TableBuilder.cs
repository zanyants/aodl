/*
 * $Id: TableBuilder.cs,v 1.1 2006/01/29 11:28:22 larsbm Exp $
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
using AODL.Document;
using AODL.Document.Styles;

namespace AODL.Document.Content.Tables
{
	/// <summary>
	/// TableBuilder offer static methode to build table for different
	/// document types.
	/// </summary>
	public class TableBuilder
	{
		/// <summary>
		/// Create a spreadsheet table.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="styleName">Name of the style.</param>
		/// <returns></returns>
		public static Table CreateSpreadsheetTable(AODL.Document.SpreadsheetDocuments.SpreadsheetDocument document, string tableName, string styleName)
		{
			return new Table(document, tableName, styleName);
		}
		
		/// <summary>
		/// Creates the text document table.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="styleName">Name of the style.</param>
		/// <param name="rows">The rows.</param>
		/// <param name="columns">The columns.</param>
		/// <param name="width">The width.</param>
		/// <param name="useTableRowHeader">if set to <c>true</c> [use table row header].</param>
		/// <param name="useBorder">The useBorder.</param>
		/// <returns></returns>
		public static Table CreateTextDocumentTable(AODL.Document.TextDocuments.TextDocument document, string tableName, string styleName, int rows, int columns, double width, bool useTableRowHeader, bool useBorder)
		{
			string tableCnt							= document.DocumentMetadata.TableCount.ToString();
			Table table								= new Table(document, tableName, styleName);
			table.TableStyle.TableProperties.Width	= width.ToString().Replace(",",".")+"cm";

			for(int i=0; i<columns; i++)
			{
				Column column						= new Column(table, "co"+tableCnt+i.ToString());
				column.ColumnStyle.ColumnProperties.Width = GetColumnCellWidth(columns, width);
				table.ColumnCollection.Add(column);
			}

			if(useTableRowHeader)
			{
				rows--;
				RowHeader rowHeader					= new RowHeader(table);
				Row row								= new Row(table, "roh1"+tableCnt);
				for(int i=0; i<columns; i++)
				{
					Cell cell						= new Cell(table, "rohce"+tableCnt+i.ToString());
					if(useBorder)
						cell.CellStyle.CellProperties.Border = Border.NormalSolid;
					row.CellCollection.Add(cell);
				}
				rowHeader.RowCollection.Add(row);
				table.RowHeader						= rowHeader;
			}

			for(int ir=0; ir<rows; ir++)
			{
				Row row								= new Row(table, "ro"+tableCnt+ir.ToString());
				
				for(int ic=0; ic<columns; ic++)
				{
					Cell cell						= new Cell(table, "ce"+tableCnt+ir.ToString()+ic.ToString());
					if(useBorder)
						cell.CellStyle.CellProperties.Border = Border.NormalSolid;
					row.CellCollection.Add(cell);
				}

				table.RowCollection.Add(row);
			}

			return table;
		}

		/// <summary>
		/// Gets the width of the column cell.
		/// </summary>
		/// <param name="columns">The columns.</param>
		/// <param name="tableWith">The table with.</param>
		/// <returns></returns>
		private static string GetColumnCellWidth(int columns, double tableWith)
		{
			double ccWidth							= (double)((tableWith/(double)columns));
			return ccWidth.ToString("F2").Replace(",",".");
		}
	}
}
