/*
 * $Id: DocumentImportTest.cs,v 1.2 2006/01/29 18:52:14 larsbm Exp $
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
using NUnit.Framework;
using AODL.Document.TextDocuments;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Content;
using AODL.Document.Content.Draw;
using AODL.Document.Content.Text;

namespace AODLTest
{
	[TestFixture]
	public class DocumentImportTest
	{

		[Test]
		public void SimpleLoadTest()
		{	
			string file							= @"D:\OpenDocument\AODL\AODLTest\bin\Debug\Files\hallo.odt";
			FileInfo fInfo						= new FileInfo(file);
			//Load a text document 
			TextDocument textDocument			= new TextDocument();
			textDocument.Load(file);
			Assert.IsTrue(textDocument.CommonStyles.Count > 0, "Common Styles must be read!");
			Console.WriteLine("Common styles: {0}", textDocument.CommonStyles.Count);
			//Save it back again
			textDocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+fInfo.Name+".rel.odt");
		}

		[Test]
		public void SimpleLoad1Test()
		{	
			string file							= @"D:\OpenDocument\AODL\AODLTest\bin\Debug\Files\OpenOffice.net.odt";
			FileInfo fInfo						= new FileInfo(file);
			//Load a text document 
			TextDocument textDocument			= new TextDocument();
			textDocument.Load(file);
			Assert.IsTrue(textDocument.CommonStyles.Count > 0, "Common Styles must be read!");
			Console.WriteLine("Common styles: {0}", textDocument.CommonStyles.Count);
			//Save it back again
			textDocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+fInfo.Name+".rel.odt");
		}

		[Test]
		public void SimpleCalcLoadTest()
		{	
			string file							= @"D:\OpenDocument\AODL\AODLTest\bin\Debug\Files\simpleCalc.ods";
			FileInfo fInfo						= new FileInfo(file);
			//Load a spreadsheet document 
			SpreadsheetDocument document		= new SpreadsheetDocument();
			document.Load(file);
			Assert.IsTrue(document.CommonStyles.Count > 0, "Common Styles must be read!");
			Console.WriteLine("Common styles: {0}", document.CommonStyles.Count);
			//Save it back again
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+fInfo.Name+".rel.ods");
		}

		[Test]
		public void TextDocumentWithImgMapReload()
		{
			string file							= @"D:\OpenDocument\AODL\AODLTest\bin\Debug\Files\imgmap.odt";
			FileInfo fInfo						= new FileInfo(file);
			//Load the text document 
			TextDocument document				= new TextDocument();
			document.Load(file);
			IContent iContent					= document.Content[2];
			Assert.IsNotNull(iContent, "Must exist!");
			Assert.IsTrue(iContent is Paragraph, "iContent have to be a paragraph! But is :"+iContent.GetType().Name);
			Assert.IsTrue(((Paragraph)iContent).Content[0] is Frame, "Must be a frame! But is :"+((Paragraph)iContent).Content[0].GetType().Name);
			Frame frame							= ((Paragraph)iContent).Content[0] as Frame;
			Assert.IsTrue(frame.Content[1] is ImageMap, "Must be a ImageMap! But is :"+frame.Content[1].GetType().Name);
			ImageMap imageMap					= frame.Content[1] as ImageMap;
			Assert.IsTrue(imageMap.Content[0] is DrawAreaRectangle, "Must be a DrawAreaRectangle! But is :"
				+imageMap.Content[0].GetType().Name);
			//Save it back again
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+fInfo.Name+".rel.odt");
		}
//
//		[Test]
//		public void RealContentLoadTest()
//		{
//			Assert.IsNotNull(this._document.Content, "Content container must exist!");
//			Assert.IsTrue(this._document.Content.Count > 0, "Must be content in their!");
//
//			this._document.SaveTo("reloaded.odt");
//			this._document.Dispose();
//		}
//
//		[Test]
//		public void ReloadHeader()
//		{
//			TestClass test		= new TestClass();
//			test.HeadingsTest();
//
//			TextDocument doc	= new TextDocument();
//			doc.Load("Heading.odt");
//			doc.SaveTo("HeadingReloaded.odt");
//			doc.Dispose();
//		}
//
//		[Test]
//		public void ReloadXlink()
//		{
//			TestClass test		= new TestClass();
//			test.XLinkTest();
//
//			TextDocument doc	= new TextDocument();
//			doc.Load("Xlink.odt");
//			doc.SaveTo("XlinkReloaded.odt");
//			doc.Dispose();
//		}
//
//		[Test]
//		public void ReloadTableOfContents()
//		{
//			TextDocument doc	= new TextDocument();
//			doc.Load("OpenOffice.net.odt");
//			doc.SaveTo("OpenOffice.net.Reloaded.odt");
//			doc.Dispose();
//		}
//
//		[Test]
//		public void TableOfContentsHtmlExport()
//		{
//			TextDocument doc	= new TextDocument();
//			doc.Load("OpenOffice.net.odt");
//			doc.SaveTo("OpenOffice.net.html");
//			doc.Dispose();
//		}
//
//		[Test]
//		public void SaveAsHtml()
//		{
//			this._document.SaveTo("reloaded.html");
//			this._document.Dispose();
//		}
//
//		[Test]
//		public void SaveAsHtmlWithTable()
//		{
//			TextDocument document		= new TextDocument();
//			document.Load("tablewithList.odt");
//			document.SaveTo("tablewithList.html");
//			document.Dispose();
//		}
//
//		[Test]
//		public void ProgrammaticControl()
//		{
//			TextDocument document		= new TextDocument();
//			document.Load("ProgrammaticControlOfMenuAndToolbarItems.odt");
//			document.SaveTo("ProgrammaticControlOfMenuAndToolbarItems.html");
////			document.Load("AndrewMacroPart.odt");
////			document.SaveTo("AndrewMacro.html");
////			document.Load("OfferLongVersion.odt");
////			document.SaveTo("OfferLongVersion.html");
//			document.Dispose();
//		}
//
//		[Test]
//		public void Howto_special_char()
//		{
//			TextDocument document		= new TextDocument();
//			document.Load("Howto_special_char.odt");
//			document.SaveTo("Howto_special_char.html");
//			document.Dispose();
//		}
//
//		[Test]
//		public void Howto_special_charInch()
//		{
//			TextDocument document		= new TextDocument();
//			document.Load(@"F:\odtFiles\Howto_special_char.odt");
//			document.SaveTo(@"F:\odtFiles\Howto_special_char.html");
//			document.Dispose();
//		}
//
//		[Test]
//		public void ComplexTable()
//		{
//			TableTest tableTest	= new TableTest();
//			tableTest.MergeCellsTest();
//
//			TextDocument doc	= new TextDocument();
//			doc.Load("tablemergedcell.odt");
//			doc.SaveTo("tablemergedcellReloaded.odt");
//			doc.Dispose();
//		}
//
//		[Test]
//		public void MegaStressTest()
//		{
////			TextDocument document		= new TextDocument();
////			document.Load("AndrewMacro.odt");
////			document.SaveTo("AndrewMacroFull.html");
//		}
	}
}

/*
 * $Log: DocumentImportTest.cs,v $
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:26:02  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */