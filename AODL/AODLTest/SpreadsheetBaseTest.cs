/*
 * $Id: SpreadsheetBaseTest.cs,v 1.1 2006/01/29 11:26:02 larsbm Exp $
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
using System.Xml;
using System.IO;
using NUnit.Framework;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Content.Tables;
using AODL.Document.TextDocuments;
using AODL.Document.Styles;
using AODL.Document.Content.Text;

namespace AODLTest
{
	/// <summary>
	/// SpreadsheetBase tests
	/// </summary>
	[TestFixture]
	public class SpreadsheetBaseTest
	{
		[Test]
		public void CreateNewSpreadsheet()
		{
			SpreadsheetDocument spreadsheetDocument		= new SpreadsheetDocument();
			spreadsheetDocument.New();
			Assert.IsNotNull(spreadsheetDocument.DocumentConfigurations2);
			Assert.IsNotNull(spreadsheetDocument.DocumentManifest);
			Assert.IsNotNull(spreadsheetDocument.DocumentPictures);
			Assert.IsNotNull(spreadsheetDocument.DocumentThumbnails);
			Assert.IsNotNull(spreadsheetDocument.DocumentSetting);
			Assert.IsNotNull(spreadsheetDocument.DocumentStyles);
			Assert.IsNotNull(spreadsheetDocument.TableCollection);
			spreadsheetDocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+"blank.ods");
			Assert.IsTrue(File.Exists(AARunMeFirstAndOnce.outPutFolder+"blank.ods"));
		}

		[Test]
		public void CreateSimpleTable()
		{
			//Create new spreadsheet document
			SpreadsheetDocument spreadsheetDocument		= new SpreadsheetDocument();
			spreadsheetDocument.New();
			//Create a new table
			Table table					= new Table(spreadsheetDocument, "First", "tablefirst");
			//Create a new cell, without any extra styles 
			Cell cell								= table.CreateCell("cell001");
			cell.OfficeValueType					= "string";
			//Set full border
			cell.CellStyle.CellProperties.Border	= Border.NormalSolid;			
			//Add a paragraph to this cell
			Paragraph paragraph						= ParagraphBuilder.CreateSpreadsheetParagraph(
				spreadsheetDocument);
			//Add some text content
			paragraph.TextContent.Add(new SimpleText(spreadsheetDocument, "Some text"));
			//Add paragraph to the cell
			cell.Content.Add(paragraph);
			//Insert the cell at row index 2 and column index 3
			//All need rows, columns and cells below the given
			//indexes will be build automatically.
			table.InsertCellAt(2, 3, cell);
			//Insert table into the spreadsheet document
			spreadsheetDocument.TableCollection.Add(table);
			spreadsheetDocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+"simple.ods");
		}

		[Test]
		public void CreateTableFormatedText()
		{
			//Create new spreadsheet document
			SpreadsheetDocument spreadsheetDocument		= new SpreadsheetDocument();
			spreadsheetDocument.New();
			//Create a new table
			Table table					= new Table(spreadsheetDocument, "First", "tablefirst");
			//Create a new cell, without any extra styles 
			Cell cell								= table.CreateCell();
			//cell.OfficeValueType					= "string";
			//Set full border
			//cell.CellStyle.CellProperties.Border	= Border.NormalSolid;			
			//Add a paragraph to this cell
			Paragraph paragraph						= ParagraphBuilder.CreateSpreadsheetParagraph(
				spreadsheetDocument);
			//Create some Formated text
			FormatedText fText						= new FormatedText(spreadsheetDocument, "T1", "Some Text");
			//fText.TextStyle.TextProperties.Bold		 = "bold";
			fText.TextStyle.TextProperties.Underline = LineStyles.dotted;
			//Add formated text
			paragraph.TextContent.Add(fText);
			//Add paragraph to the cell
			cell.Content.Add(paragraph);
			//Insert the cell at row index 2 and column index 3
			//All need rows, columns and cells below the given
			//indexes will be build automatically.
			table.InsertCellAt(2, 3, cell);
			//Insert table into the spreadsheet document
			spreadsheetDocument.TableCollection.Add(table);
			spreadsheetDocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+"formated.ods");
		}
	}
}

/*
 * $Log: SpreadsheetBaseTest.cs,v $
 * Revision 1.1  2006/01/29 11:26:02  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */