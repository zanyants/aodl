/*
 * $Id: TextDocumentBaseTests.cs,v 1.2 2006/01/29 18:52:14 larsbm Exp $
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
using System.Xml;
using AODL.Document.Content.Tables;
using AODL.Document.TextDocuments;
using AODL.Document.Styles;
using AODL.Document.Content.Text;

namespace AODLTest
{
	[TestFixture]
	public class TextDocumentBaseTests
	{
		[Test]
		public void EmptyDocument()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Save empty
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"empty.odt");
		}

		[Test]
		public void ParagraphSimpleText()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a standard paragraph using the ParagraphBuilder
			Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some simple text
			paragraph.TextContent.Add(new SimpleText(document, "Some simple text!"));
			//Add the paragraph to the document
			document.Content.Add(paragraph);
			//Save empty
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"simple.odt");
		}

		[Test]
		public void ParagraphFormatedText()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a standard paragraph using the ParagraphBuilder
			Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some formated text
			FormatedText formText					= new FormatedText(document, "T1", "Some formated text!");
			formText.TextStyle.TextProperties.Bold	= "bold";
			paragraph.TextContent.Add(formText);
			//Add the paragraph to the document
			document.Content.Add(paragraph);
			//Save empty
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"formated.odt");
		}

		[Test]
		public void NumberedListTest()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a numbered list
			List li				= new List(document, "L1", ListStyles.Number, "L1P1");
			Assert.IsNotNull(li.Node, "Node object must exist!");
			Assert.IsNotNull(li.Style, "Style object must exist!");
			Assert.IsNotNull(li.ListStyle.ListlevelStyles, "ListLevelStyleCollection must exist!");
			Assert.IsTrue(li.ListStyle.ListlevelStyles.Count == 10, "Must exist exactly 10 ListLevelStyle objects!");
			Assert.IsNotNull(li.ListStyle.ListlevelStyles[1].ListLevelProperties, "ListLevelProperties object must exist!");
		}

		[Test]
		public void BulletListTest()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a bullet list
			List li				= new List(document, "L1", ListStyles.Bullet, "L1P1");
			Assert.IsNotNull(li.Node, "Node object must exist!");
			Assert.IsNotNull(li.Style, "Style object must exist!");
			Assert.IsNotNull(li.ListStyle.ListlevelStyles, "ListLevelStyleCollection must exist!");
			Assert.IsTrue(li.ListStyle.ListlevelStyles.Count == 10, "Must exist exactly 10 ListLevelStyle objects!");
			Assert.IsNotNull(li.ListStyle.ListlevelStyles[1].ListLevelProperties, "ListLevelProperties object must exist!");
		}

		[Test]
		public void ListItemTest()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a numbered list
			List li									= new List(document, "L1", ListStyles.Bullet, "L1P1");
			//Create a new list item
			ListItem lit							= new ListItem(li);
			Assert.IsNotNull(lit.Content, "Content object must exist!");
			//Create a paragraph	
			Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some text
			paragraph.TextContent.Add(new SimpleText(document, "First item"));
			//Add paragraph to the list item
			lit.Content.Add(paragraph);
			//Add the list item
			li.Content.Add(lit);
			//Add the list
			document.Content.Add(li);
			//Save document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"list.odt");
		}

		[Test]
		public void HeadingsTest()
		{
			//Create a new text document
			TextDocument document		= new TextDocument();
			document.New();
			//Create a new Heading
			Header header				= new Header(document, Headings.Heading);
			//Add some header text
			header.TextContent.Add(new SimpleText(document, "I'm the first headline"));
			//Add header
			document.Content.Add(header);
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"Heading.odt");
		}

		[Test]
		public void HeadingFilledWithTextBuilder()
		{
			string headingText			= "Some    Heading with\n styles\t and more";
			//Create a new text document
			TextDocument document		= new TextDocument();
			document.New();
			//Create a new Heading
			Header header				= new Header(document, Headings.Heading);
			//Create a TextCollection from headingText using the TextBuilder
			ITextCollection textCol		= TextBuilder.BuildTextCollection(document, headingText);
			//Add text collection
			header.TextContent			= textCol;
			//Add header
			document.Content.Add(header);
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"HeadingWithControlCharacter.odt");
		}

		[Test]
		public void XLinkTest()
		{
			//Create new TextDocument
			TextDocument document		= new TextDocument();
			document.New();
			//Create a new Paragraph
			Paragraph para				= new Paragraph(document, "P1");
			//Create some simple text
			SimpleText stext			= new SimpleText(document, "Some simple text ");
			//Create a XLink
			XLink xlink					= new XLink(document, "http://www.sourceforge.net", "Sourceforge");
			//Add the textcontent
			para.TextContent.Add(stext);
			para.TextContent.Add(xlink);
			//Add paragraph to the document content
			document.Content.Add(para);
			//Save
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"XLink.odt");
		}

		[Test]
		public void FootnoteText()
		{
			//Create new TextDocument
			TextDocument document		= new TextDocument();
			document.New();
			//Create a new Paragph
			Paragraph para				= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Create some simple Text
			para.TextContent.Add(new SimpleText(document, "Some simple text. And I have a footnode"));
			//Create a Footnote
			para.TextContent.Add(new Footnote(document, "Footer Text", "1", FootnoteType.footnode));			
			//Add the paragraph
			document.Content.Add(para);
			//Save
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"footnote.odt");
		}

		[Test]
		public void IParagraphCollectionBuilderTest()
		{
			//some text e.g read from a TextBox
			string someText		= "Max Mustermann\nMustermann Str. 300\n22222 Hamburg\n\n\n\n"
									+"Heinz Willi\nDorfstr. 1\n22225 Hamburg\n\n\n\n"
									+"Offer for 200 Intel Pentium 4 CPU's\n\n\n\n"
									+"Dear Mr. Willi,\n\n\n\n"
									+"thank you for your request. We can offer you the 200 Intel Pentium IV 3 Ghz CPU's for a price of 79,80 € per unit."
									+"This special offer is valid to 31.10.2005. If you accept, we can deliver within 24 hours.\n\n\n\n"
									+"Best regards \nMax Mustermann";

			//Create new TextDocument
			TextDocument document				= new TextDocument();
			document.New();
			//Use the ParagraphBuilder to split the string into ParagraphCollection
			ParagraphCollection pCollection		= ParagraphBuilder.CreateParagraphCollection(
													document,
													someText,
													true,
													ParagraphBuilder.ParagraphSeperator);
			//Add the paragraph collection
			foreach(Paragraph paragraph in pCollection)
				document.Content.Add(paragraph);
			//save
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"Letter.odt");
		}

		/// <summary>
		/// Manipulate a common style.
		/// </summary>
		[Test]
		public void ManipulateACommonStyle()
		{
			TextDocument document				= new TextDocument();
			document.New();
			Assert.IsTrue(document.CommonStyles.Count > 0, "Common style resp. style templates must be loaded");
			//Find a Header template
			IStyle style						= document.CommonStyles.GetStyleByName("Heading_20_1");
			Assert.IsNotNull(style, "Style with name Heading_20_1 must exist");
			Assert.IsTrue(style is ParagraphStyle, "style must be a ParagraphStyle");
			((ParagraphStyle)style).TextProperties.FontName	= FontFamilies.BroadwayBT;
			//Create a header that use the standard style Heading_20_1
			Header header						= new Header(document, Headings.Heading_20_1);
			//Add some text
			header.TextContent.Add(new SimpleText(document, "I am the header text and my style template is modified :)"));
			//Add header to the document
			document.Content.Add(header);
			//save the document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"modifiedCommonStyle.odt");
		}
	}
}

/*
 * $Log: TextDocumentBaseTests.cs,v $
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:26:02  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */