/*
 * $Id: IndexTest.cs,v 1.1 2006/01/05 10:28:06 larsbm Exp $ 
 */

using System;
using NUnit.Framework;
using AODL.TextDocument.Style;
using AODL.TextDocument.Style.Properties;
using AODL.TextDocument;
using AODL.TextDocument.Content;

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
			textDocument, Headings.Heading1, sHeading);
		//add the header to the content
		textDocument.Content.Add(header);
		//add the toc entry text as entry to the Table of contents
		tableOfContents.InsertEntry(sTocEntry, 1);
		//Add some text to this chapter
		Paragraph paragraph				= new Paragraph(textDocument, 
			ParentStyles.Standard, "I'm the text for the first chapter!");
		textDocument.Content.Add(paragraph);
		//Add a sub header to the first chapter
		//Create a new heading, there's no need of the chapter number
		sHeading						= "A first sub headline";
		//The corresponding text entry, here you need to set the
		//chapter number
		sTocEntry						= "1.1. A first sub headline";
		header							= new Header(
			textDocument, Headings.Heading2, sHeading);
		//add the header to the content
		textDocument.Content.Add(header);
		//add the toc entry text as entry to the Table of contents
		tableOfContents.InsertEntry(sTocEntry, 2);
		//Add some text to this sub chapter
		paragraph						= new Paragraph(textDocument, 
			ParentStyles.Standard, "I'm the text for the first sub chapter!");
		textDocument.Content.Add(paragraph);
		//Save it
		textDocument.SaveTo("toc.odt");
		}
	}
}

/*
 * $Log: IndexTest.cs,v $
 * Revision 1.1  2006/01/05 10:28:06  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 */