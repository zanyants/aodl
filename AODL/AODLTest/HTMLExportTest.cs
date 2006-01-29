/*
 * $Id: HTMLExportTest.cs,v 1.1 2006/01/29 11:26:02 larsbm Exp $
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

namespace AODLTest
{
	[TestFixture]
	public class HTMLExportTest
	{
		[Test]
		public void HTMLExportTest1()
		{
			string file							= @"D:\OpenDocument\AODL\AODLTest\bin\Debug\Files\hallo.odt";
			FileInfo fInfo						= new FileInfo(file);
			//Load a text document 
			TextDocument textDocument			= new TextDocument();
			textDocument.Load(file);
			//Save it back again
			textDocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+fInfo.Name+".html");
		}

		[Test]
		public void HTMLExportTest2()
		{	
			string file							= @"D:\OpenDocument\AODL\AODLTest\bin\Debug\Files\OpenOffice.net.odt";
			FileInfo fInfo						= new FileInfo(file);
			//Load a text document 
			TextDocument textDocument			= new TextDocument();
			textDocument.Load(file);
			//Save it back again
			textDocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+fInfo.Name+".html");
		}

		[Test]
		public void HTMLExportTest3()
		{	
			string file							= @"D:\OpenDocument\AODL\AODLTest\bin\Debug\Files\simpleCalc.ods";
			FileInfo fInfo						= new FileInfo(file);
			//Load a spreadsheet document 
			SpreadsheetDocument document		= new SpreadsheetDocument();
			document.Load(file);
			//Save it back again
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+fInfo.Name+".html");
		}

		[Test]
		public void HTMLExportTestImageMap()
		{	
			string file							= @"D:\OpenDocument\AODL\AODLTest\bin\Debug\Files\imgmap.odt";
			FileInfo fInfo						= new FileInfo(file);
			//Load a text document 
			TextDocument textDocument			= new TextDocument();
			textDocument.Load(file);
			//Save it back again
			textDocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+fInfo.Name+".html");
		}
	}
}

/*
 * $Log: HTMLExportTest.cs,v $
 * Revision 1.1  2006/01/29 11:26:02  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */