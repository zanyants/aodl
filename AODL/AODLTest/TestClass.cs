/*
 * $Id: TestClass.cs,v 1.13 2005/12/18 18:29:46 larsbm Exp $ 
 */

using System;
using NUnit.Framework;
using AODL.TextDocument.Style;
using AODL.TextDocument.Style.Properties;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.Publish;
using System.Xml;
using System.IO;
using System.Reflection;

namespace AODLTest
{
	/// <summary>
	/// Zusammenfassung für Class1.
	/// </summary>
	[TestFixture]
	public class TestClass
	{

		[SetUp]
		public void SetUp()
		{
		}

		[Test]
		public void NewTextDocument()
		{
			TextDocument td = new TextDocument();
			td.New();
			Assert.IsNotNull(td.XmlDoc, "Must exist!");
			//Console.WriteLine("Doc: {0}", td.XmlDoc.OuterXml);
		}

		[Test]
		public void ParagraphTest()
		{
			TextDocument td = new TextDocument();
			td.New();
			Paragraph p = new Paragraph(td, "P1");
			Assert.IsNotNull(p.Style, "Style object must exist!");
			Assert.AreEqual(p.Style.GetType().Name, "ParagraphStyle", "IStyle object must be type of ParagraphStyle");
			Assert.IsNotNull(((ParagraphStyle)p.Style).Properties, "Properties object must exist!");
			//add text
			p.TextContent.Add(new SimpleText(p, "HallO"));
			//Add the Paragraph
			td.Content.Add((IContent)p);
			//Blank para
			td.Content.Add(new Paragraph(td, ParentStyles.Standard.ToString()));
			// new para
			p = new Paragraph(td, "P2");
			p.TextContent.Add(new SimpleText(p, "Hallo"));
			td.Content.Add(p);
			td.SaveTo("parablank.odt");
			//Console.WriteLine("Document: {0}", td.XmlDoc.OuterXml);
		}

		[Test]
		public void ParagraphAddRemoveTest()
		{
			TextDocument td = new TextDocument();
			td.New();
			Paragraph p = new Paragraph(td, "P1");
			Assert.IsNotNull(p.Style, "Style object must exist!");
			Assert.AreEqual(p.Style.GetType().Name, "ParagraphStyle", "IStyle object must be type of ParagraphStyle");
			Assert.IsNotNull(((ParagraphStyle)p.Style).Properties, "Properties object must exist!");
			//add text
			p.TextContent.Add(new SimpleText(p, "Hello"));
			IText itext		= p.TextContent[0];
			p.TextContent.Remove(itext);
			//Console.Write(p.Node.Value);
			Assert.IsTrue(p.Node.InnerXml.IndexOf("Hello") == -1, "Must be removed!");
			//Add the Paragraph
			td.Content.Add((IContent)p);
			//Blank para
			td.Content.Add(new Paragraph(td, ParentStyles.Standard.ToString()));
			// new para
			p = new Paragraph(td, "P2");
			p.TextContent.Add(new SimpleText(p, "Hello i'm still here"));
			td.Content.Add(p);
			td.SaveTo("pararemoved.odt");
			//Console.WriteLine("Document: {0}", td.XmlDoc.OuterXml);
		}

		[Test]
		public void ParagraphContentTest()
		{
			TextDocument td		= new TextDocument();
			td.New();

			Paragraph p			= new Paragraph(td, "P1");
			((ParagraphStyle)p.Style).Properties.Alignment = TextAlignments.end.ToString();
			//Add some content
			p.TextContent.Add( new SimpleText((IContent)p, "Hallo i'm simple text!"));
			Assert.IsTrue(p.TextContent.Count > 0, "Must be greater than zero!");
			FormatedText ft = new FormatedText((IContent)p, "T1", " \"And < > &     i'm formated text!");
			((TextStyle)ft.Style).Properties.Bold = "bold";
			((TextStyle)ft.Style).Properties.Italic = "italic";
			((TextStyle)ft.Style).Properties.SetUnderlineStyles( 
				LineStyles.wave, LineWidths.bold.ToString(), "#A1B1C2");
//			((TextStyle)ft.Style).Properties.Underline = LineStyles.dotted;
//			((TextStyle)ft.Style).Properties.UnderlineColor = "font-color";
//			((TextStyle)ft.Style).Properties.UnderlineWidth = LineWidths.auto.ToString();
			p.TextContent.Add(ft);			
			Assert.IsTrue(p.TextContent.Count == 2, "Must be two!");
			//Add as document content 
			td.Content.Add(p);
			//Console.WriteLine(td.XmlDoc.OuterXml);
			//Generate and save
			td.SaveTo("ParagraphTest.odt");
		}

		[Test]
		public void ParagraphTextPropertiesTest()
		{
			TextDocument td						= new TextDocument();
			td.New();

			Paragraph p							= new Paragraph(td, "P1");
			//justify
			((ParagraphStyle)p.Style).Properties.Alignment	= TextAlignments.justify.ToString();
			//Set complet paragraph to italic
			((ParagraphStyle)p.Style).Textproperties.Italic		= "italic";
			((ParagraphStyle)p.Style).Textproperties.FontName	= FontFamilies.BroadwayBT;
			((ParagraphStyle)p.Style).Textproperties.FontSize	= "18pt";

			//add simple text
			// use \n to insert a linebreak !!
			p.TextContent.Add( new SimpleText(p, @"A \nloooooooooooooooooooooooooooooooooooooooong texxxxxxxxxxxxxxxt. A reaaaaaaaaaaalllly looooooooong teeeeeeeeeexxxxxxt"));

			//add paragraph
			td.Content.Add(p);

			//Blank paragraph
			Paragraph p2						= new Paragraph(td, "P2");
			p2.TextContent.Add(new SimpleText(p2, ""));
			td.Content.Add(p2);

			Paragraph p3						= new Paragraph(td, "P3");
			p3.TextContent.Add(new SimpleText(p3, "Hello"));
			td.Content.Add(p3);

			td.SaveTo("ParagraphTextProp.odt");
		}

		[Test]
		public void ColorTest()
		{
			Assert.AreEqual("#0000ff", Colors.GetColor(System.Drawing.Color.Blue),
				"Must be equal");
			//Console.WriteLine("Blue : {0}", Colors.GetColor(System.Drawing.Color.Blue));
		}

		[Test]
		public void LetterTestLongVersion()
		{
			string[] address		= new string[] {"Max Mustermann","Mustermann Str. 300","22222 Hamburg"};
			string[] recipient  = new string[] {"Heinz Willi", "Dorfstr. 1", @"22225 Hamburg\n\n"};
			string betreff		= @"Offer for 200 Intel Pentium 4 CPU's\n\n";
			string bodyheader	= "Dear Mr. Willi,";
			string[] bodytext	= new string[] {
									  @"thank you for your request. We can offer you the 200 Intel Pentium IV 3 Ghz CPU's for a price of 79,80 € per unit.",
									  @"This special offer < & >; is valid to 31.10.2005. If you accept, we can deliver within 24 hours.\n\n\n"
								  };
			string regards		= @"Best regards \nMax Mustermann";

			TextDocument td		= new TextDocument();
			td.New();

			for(int i=0; i < address.Length; i++)
			{
				Paragraph p		= new Paragraph(td, "Pa"+i.ToString());				
				p.TextContent.Add(new SimpleText(p, address[i]));
				td.Content.Add(p);
			}

			td.Content.Add(new Paragraph(td, ParentStyles.Standard.ToString()));

			for(int i=0; i < recipient.Length; i++)
			{
				Paragraph p		= new Paragraph(td, "Pr"+i.ToString());				
				p.TextContent.Add(new SimpleText(p, recipient[i]));
				td.Content.Add(p);
			}

			td.Content.Add(new Paragraph(td, ParentStyles.Standard.ToString()));

			Paragraph pb		= new Paragraph(td, "Pb");
			pb.TextContent.Add(new SimpleText(pb, betreff));
			td.Content.Add(pb);

			pb		= new Paragraph(td, "Pan");
			pb.TextContent.Add(new SimpleText(pb, bodyheader));
			td.Content.Add(pb);

			for(int i=0; i < bodytext.Length; i++)
			{
				Paragraph p		= new Paragraph(td, "Pr"+i.ToString());
				((ParagraphStyle)p.Style).Properties.Alignment = TextAlignments.justify.ToString();
				p.TextContent.Add(new SimpleText(p, bodytext[i]));
				td.Content.Add(p);
			}

			td.Content.Add(new Paragraph(td, ParentStyles.Standard.ToString()));

			pb		= new Paragraph(td, "PReg");
			pb.TextContent.Add(new SimpleText(pb, regards));
			td.Content.Add(pb);

			td.SaveTo("OfferLongVersion.odt");

			MetaData metaData			= new MetaData();
			metaData.DisplyMetaData(td);
		}

		[Test]
		public void LetterTestShortVersion()
		{
			string[] address		= new string[] {"Max Mustermann","Mustermann Str. 300","22222 Hamburg"};
			string[] recipient  = new string[] {"Heinz Willi", "Dorfstr. 1", @"22225 Hamburg\n\n"};
			string betreff		= @"Offer for 200 Intel Pentium 4 CPU's\n\n";
			string bodyheader	= "Dear Mr. Willi,";
			string[] bodytext	= new string[] {
												   @"thank you for your request. We can offer you the 200 Intel Pentium IV 3 Ghz CPU's for a price of 79,80 € per unit.",
												   @"This special offer is valid to 31.10.2005. If you accept, we can deliver within 24 hours.\n\n\n"
											   };
			string regards		= @"Best regards \nMax Mustermann";

			TextDocument td		= new TextDocument();
			td.New();

			for(int i=0; i < address.Length; i++)
				td.Content.Add(new Paragraph(td, ParentStyles.Standard, address[i]));

			td.Content.Add(new Paragraph(td, ParentStyles.Standard.ToString()));

			for(int i=0; i < recipient.Length; i++)
				td.Content.Add(new Paragraph(td, ParentStyles.Standard, recipient[i]));

			td.Content.Add(new Paragraph(td, ParentStyles.Standard.ToString()));
			td.Content.Add(new Paragraph(td, ParentStyles.Standard, betreff));
			td.Content.Add(new Paragraph(td, ParentStyles.Standard, bodyheader));
			
			for(int i=0; i < bodytext.Length; i++)
			{
				Paragraph p		= new Paragraph(td, "Pb"+i.ToString());
				((ParagraphStyle)p.Style).Properties.Alignment = TextAlignments.justify.ToString();
				p.TextContent.Add(new SimpleText(p, bodytext[i]));
				td.Content.Add(p);
			}

			td.Content.Add(new Paragraph(td, ParentStyles.Standard.ToString()));
			td.Content.Add(new Paragraph(td, ParentStyles.Standard, regards));

			td.SaveTo("OfferShortVersion.odt");
		}

		[Test]
		public void AllFonts()
		{
			Type fontfamiliestype	= typeof(FontFamilies);
			foreach(MemberInfo mi in fontfamiliestype.GetMembers())
				Console.WriteLine(mi.Name+"<br>");
		}

		[Test]
		public void TabstopTest()
		{
			//Create new document
			TextDocument document		= new TextDocument();
			document.New();
			//Create new paragraph
			Paragraph par				= new Paragraph(document, "P1");
			//Create a new TabStopStyle collection
			TabStopStyleCollection tsc	= new TabStopStyleCollection(document);
			//Create TabStopStyles
			TabStopStyle ts				= new TabStopStyle(document, 4.98);
			ts.LeaderStyle				= TabStopLeaderStyles.Dotted;
			ts.LeaderText				= ".";
			ts.Type						= TabStopTypes.Center;
			//Add the tabstop
			tsc.Add(ts);
			//Append the TabStopStyleCollection
			((ParagraphStyle)par.Style).Properties.TabStopStyleCollection = tsc;
			//Add some text, use @ qualifier when ever you use control chars!
			string mytebstoptext		= @"Hello\tHello again";
			SimpleText stext			= new SimpleText(par, mytebstoptext);
			//the simple text
			par.TextContent.Add(stext);
			//Add the paragraph to the content container
			document.Content.Add(par);
			//Save
			document.SaveTo("tabstop.odt");
		}

		[Test]
		public void BookmarkTest()
		{
			//Create a new TextDocument
			TextDocument document		= new TextDocument();
			document.New();
			//Create a new Paragraph
			Paragraph para				= new Paragraph(document, "P1");
			//Create simple text
			SimpleText stext			= new SimpleText(para, "Some text here");
			//add the simple text
			para.TextContent.Add(stext);
			//add the paragraph
			document.Content.Add(para);
			//add a blank paragraph
			document.Content.Add(new Paragraph(document, ParentStyles.Standard.ToString()));
			//Create a new Paragraph
			para						= new Paragraph(document, "P2");
			//add some simple text
			para.TextContent.Add(new SimpleText(para, "some simple text"));
			//create a new standard bookmark
			Bookmark bookmark			= new Bookmark(para, BookmarkType.Standard, "address");
			//add bookmark to the paragraph
			para.TextContent.Add(bookmark);
			//add the paragraph to the document
			document.Content.Add(para);

			//create a paragraph, with a bookmark range Start -> End
			para						= new Paragraph(document, "P3");
			bookmark					= new Bookmark(para, BookmarkType.Start, "address2");
			//add bookmark to the paragraph
			para.TextContent.Add(bookmark);
			//add some simple text
			para.TextContent.Add(new SimpleText(para, "more text"));
			//create a new end bookmark
			bookmark			= new Bookmark(para, BookmarkType.End, "address2");
			//add bookmark to the paragraph, so the encapsulated bookmark text is "more text"
			para.TextContent.Add(bookmark);
			//add the paragraph to the document content
			document.Content.Add(para);
			//save
			document.SaveTo("bookmark.odt");
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
			SimpleText stext			= new SimpleText(para, "Some simple text");
			//Create a XLink
			XLink xlink					= new XLink(para, "http://www.sourceforge.net", "Sourceforge");
			//Add the textcontent
			para.TextContent.Add(stext);
			para.TextContent.Add(xlink);
			//Add paragraph to the document content
			document.Content.Add(para);
			//Save
			document.SaveTo("XLink.odt");
		}

		[Test]
		public void FootnoteText()
		{
			//Create new TextDocument
			TextDocument document		= new TextDocument();
			document.New();
			//Create a new Paragph
			Paragraph para				= new Paragraph(document, "P1");
			//Create some simple Text
			SimpleText stext			= new SimpleText(para, "Some simple text. And I have a footnode");
			//Create a Footnote
			Footnote fnote				= new Footnote(para, "Footer \"     Text", "1", FootnoteType.footnode);
			//Add the text content
			para.TextContent.Add(stext);
			para.TextContent.Add(fnote);
			//Add the paragraph
			document.Content.Add(para);
			//Save
			document.SaveTo("footnote.odt");
		}

		[Test]
		public void FooterAndHeader()
		{
			//Create a new document
			TextDocument td		= new TextDocument();
			td.New();
			//Create a new paragraph
			Paragraph p			= new Paragraph(td, "P1");
			//Create some formated text
			FormatedText ftext	= new FormatedText(p, "F1", "Im the Header");
			((TextStyle)ftext.Style).Properties.Italic = "italic";
			//add the text
			p.TextContent.Add(ftext);
			//Insert paragraph as header
			td.InsertHeader(p);
			//New paragraph 
			p					= new Paragraph(td, ParentStyles.Standard,
				"I'm the Footer");
			//Insert paragraph as footer
			td.InsertFooter(p);
			//Save
			td.SaveTo("Header_Footer.odt");
		}

		[Test]
		public void NewTextAndParagraphProps()
		{
			//Create a new TextDocument
			TextDocument document		= new TextDocument();
			document.New();
			//Create new Paragraph
			Paragraph paragraph			= new Paragraph(document, "P1");
			//Create paragraph border
			((ParagraphStyle)paragraph.Style).Properties.Border =
				Border.MiddleSolid;
			//Set line spacing to double
			((ParagraphStyle)paragraph.Style).Properties.LineSpacing =
				ParagraphHelper.LineDouble;
			//Create some formated text
			FormatedText ftext			= new FormatedText(paragraph, "T1", @"Outline\n");
			//Set text as Outline
			((TextStyle)ftext.Style).Properties.Outline		= "true";
			//Add to paragraph
			paragraph.TextContent.Add(ftext);
			//Create some formated text
			ftext						= new FormatedText(paragraph, "T2", @"Colored\n");
			//Set font color
			((TextStyle)ftext.Style).Properties.FontColor	= Colors.GetColor(System.Drawing.Color.Cyan);
			//Add to paragraph
			paragraph.TextContent.Add(ftext);
			//Create some formated text
			ftext						= new FormatedText(paragraph, "T3", @"Backcolor\n");
			//Set background color
			((TextStyle)ftext.Style).Properties.BackgroundColor	= Colors.GetColor(System.Drawing.Color.Cyan);
			//Add to paragraph
			paragraph.TextContent.Add(ftext);
			//Create some formated text
			ftext						= new FormatedText(paragraph, "T4", @"Shadow\n");
			//Set shadow
			((TextStyle)ftext.Style).Properties.Shadow		= TextPropertieHelper.Shadowmidlle;
			//Add to paragraph
			paragraph.TextContent.Add(ftext);
			//Create some formated text
			ftext						= new FormatedText(paragraph, "T5", @"Subscript\n");
			//Set subscript
			((TextStyle)ftext.Style).Properties.Position	= TextPropertieHelper.Subscript;
			//Add to paragraph
			paragraph.TextContent.Add(ftext);
			//Create some formated text
			ftext						= new FormatedText(paragraph, "T6", @"Superscript\n");
			//Set superscript
			((TextStyle)ftext.Style).Properties.Position	= TextPropertieHelper.Superscript;
			//Add to paragraph
			paragraph.TextContent.Add(ftext);
			//Create some formated text
			ftext						= new FormatedText(paragraph, "T7", @"Strice through\n");
			//Set superscript
			((TextStyle)ftext.Style).Properties.TextLineThrough = LineStyles.solid;
			//Add to paragraph
			paragraph.TextContent.Add(ftext);
			//finally add paragraph to the document
			document.Content.Add(paragraph);
			//Save the document
			document.SaveTo("NewProperties.odt");
		}

		[Test]
		public void HeadingsTest()
		{
			//Create a new text document
			TextDocument document		= new TextDocument();
			document.New();
			//Create a new Heading
			Header header				= new Header(document, Headings.Heading, "\"I'm the\n first   Headline!\"");
			//Add header
			document.Content.Add(header);
			document.SaveTo("Heading.odt");
		}

		[Test]
		public void WhiteSpaceTest()
		{
			//Create new TextDocument
			TextDocument document		= new TextDocument();
			document.New();
			//Create a new Paragraph
			Paragraph para				= new Paragraph(document, "P1");
			//Create some simple text with whitespaces
			SimpleText stext			= new SimpleText(para, "Some simple text add 12 whitespace         \"and go on.");			
			//Add the textcontent
			para.TextContent.Add(stext);
			//Add paragraph to the document content
			document.Content.Add(para);
			//Save
			document.SaveTo("Whitespaces.odt");
		}

	}
}

/*
 * $Log: TestClass.cs,v $
 * Revision 1.13  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.12  2005/12/12 19:39:16  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.11  2005/11/23 19:18:17  larsbm
 * - New Textproperties
 * - New Paragraphproperties
 * - New Border Helper
 * - Textproprtie helper
 *
 * Revision 1.10  2005/11/22 21:09:19  larsbm
 * - Add simple header and footer support
 *
 * Revision 1.9  2005/11/20 19:30:23  larsbm
 * - Added Foot- and Endnote support
 *
 * Revision 1.8  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.7  2005/10/23 16:47:48  larsbm
 * - Bugfix ListItem throws IStyleInterface not implemented exeption
 * - now. build the document after call saveto instead prepare the do at runtime
 * - add remove support for IText objects in the paragraph class
 *
 * Revision 1.6  2005/10/23 09:17:20  larsbm
 * - Release 1.0.3.0
 *
 * Revision 1.5  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.4  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 * Revision 1.3  2005/10/08 12:31:33  larsbm
 * - better usabilty of paragraph handling
 * - create paragraphs with text and blank paragraphs with one line of code
 *
 * Revision 1.2  2005/10/08 07:50:15  larsbm
 * - added cvs tags
 *
 */