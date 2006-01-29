/*
 * $Id: IndexTest.cs,v 1.2 2006/01/29 11:26:02 larsbm Exp $ 
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
using NUnit.Framework;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.TextDocuments;
using AODL.Document.Content;
using AODL.Document.Content.Text;
using AODL.Document.Content.Text.Indexes;

namespace AODLTest
{
	[TestFixture]
	public class IndexTest
	{
		[Test]
		public void TableOfContentsTest()
		{
		//Create new Document
		TextDocument textDocument		= new TextDocument();
		textDocument.New();
		//Create a new Table of contents
		TableOfContents tableOfContents	= new TableOfContents(
			textDocument, "Table_Of_Contents", true, true, "Table of contents");
		//Add the toc
		textDocument.Content.Add(tableOfContents);
		//Create a new heading, there's no need of the chapter number
		string sHeading					= "A first headline";
		//The corresponding text entry, here you need to set the
		//chapter number
		string sTocEntry				= "1. A first headline";
		Header header					= new Header(
			textDocument, Headings.Heading_20_1);
		header.OutLineLevel				= "1";
		header.TextContent.Add(new SimpleText(textDocument,  sHeading));
		//add the header to the content
		textDocument.Content.Add(header);
		//add the toc entry text as entry to the Table of contents
		tableOfContents.InsertEntry(sTocEntry, 1);
		//Add some text to this chapter
		Paragraph paragraph				= ParagraphBuilder.CreateStandardTextParagraph(textDocument);
		paragraph.TextContent.Add(new SimpleText(textDocument, "I'm the text for the first chapter!"));
		textDocument.Content.Add(paragraph);
		//Add a sub header to the first chapter
		//Create a new heading, there's no need of the chapter number
		sHeading						= "A first sub headline";
		//The corresponding text entry, here you need to set the
		//chapter number
		sTocEntry						= "1.1. A first sub headline";
		header							= new Header(
			textDocument, Headings.Heading_20_2);
		header.OutLineLevel				= "2";
		header.TextContent.Add(new SimpleText(textDocument,  sHeading));
		//add the header to the content
		textDocument.Content.Add(header);
		//add the toc entry text as entry to the Table of contents
		tableOfContents.InsertEntry(sTocEntry, 2);
		//Add some text to this sub chapter
			paragraph				= ParagraphBuilder.CreateStandardTextParagraph(textDocument);
			paragraph.TextContent.Add(new SimpleText(textDocument, "I'm the text for the subchapter chapter!"));
		textDocument.Content.Add(paragraph);
		//Save it
		textDocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+"toc.odt");
		}
	}
}

/*
 * $Log: IndexTest.cs,v $
 * Revision 1.2  2006/01/29 11:26:02  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.1  2006/01/05 10:28:06  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 */