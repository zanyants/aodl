/*
 * $Id: PDFExportTest.cs,v 1 2013/05/01 20:02:24 JbMOnGitHub 
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2013, JbMOnGitHub
 * 
 * Last changes:
 * 
 */

using System;
using System.IO;
using AODL.ExternalExporter.PDF;
using NUnit.Framework;
using AODL.Document.TextDocuments;
using AODL.Document.SpreadsheetDocuments;

namespace AODLTest
{
	[TestFixture]
	public class PDFExportTest
	{
		[Test]
		public void PDFExportTest1()
		{
			string file							= AARunMeFirstAndOnce.inPutFolder+@"complex_fr_odt.odt";
			FileInfo fInfo						= new FileInfo(file);
			//Load a text document 
			TextDocument textDocument			= new TextDocument();
			textDocument.Load(file);
			
			bool P10StyleExists = false;
			
			foreach ( AODL.Document.Styles.IStyle element in textDocument.Styles) {
				if (!string.IsNullOrEmpty(element.StyleName))
					P10StyleExists = element.StyleName == "P10";
					if (P10StyleExists)
						break;
			} 
			Assert.IsTrue(P10StyleExists, "P10 StyleName not found in " + fInfo.Name);
			
			//Save it back again
			new PDFExporter().Export(textDocument,AARunMeFirstAndOnce.outPutFolder+fInfo.Name+".pdf");
		}
	}
}

/*
 * $Log: PDFExportTest.cs,v $
 * Revision 1  2013/05/01 20:02:24  JbMOnGitHub
 * - Added complex export test
 */