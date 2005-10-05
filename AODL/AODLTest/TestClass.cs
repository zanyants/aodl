using System;
using NUnit.Framework;
using AODL.TextDocument.Style;
using AODL.TextDocument.Style.Properties;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.Publish;
using System.Xml;
using System.IO;

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
			Console.WriteLine("Doc: {0}", td.XmlDoc.OuterXml);
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
			//Add the Paragraph
			td.Content.Add((IContent)p);			
			Console.WriteLine("Document: {0}", td.XmlDoc.OuterXml);
		}

		[Test]
		public void ParagraphContentTest()
		{
			TextDocument td		= new TextDocument();
			td.New();

			Paragraph p			= new Paragraph(td, "P1");
			((ParagraphProperties)p.Style.Properties).Alignment = TextAlignments.end.ToString();
			//Add some content
			p.TextContent.Add( new SimpleText((IContent)p, "Hallo i'm simple text!"));
			Assert.IsTrue(p.TextContent.Count > 0, "Must be greater than zero!");
			FormatedText ft = new FormatedText((IContent)p, "T1", " And i'm formated text!");
			((TextProperties)ft.Style.Properties).Bold = "bold";
			((TextProperties)ft.Style.Properties).Italic = "italic";
			((TextProperties)ft.Style.Properties).SetUnderlineStyles( 
				LineStyles.wave.ToString(), LineWidths.bold.ToString(), "#A1B1C2");
//			((TextProperties)ft.Style.Properties).Underline = LineStyles.dotted.ToString();
//			((TextProperties)ft.Style.Properties).UnderlineColor = "font-color";
//			((TextProperties)ft.Style.Properties).UnderlineWidth = LineWidths.auto.ToString();
			p.TextContent.Add(ft);			
			Assert.IsTrue(p.TextContent.Count == 2, "Must be two!");
			//Add as document content 
			td.Content.Add(p);
			Console.WriteLine(td.XmlDoc.OuterXml);
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
			((ParagraphProperties)p.Style.Properties).Alignment	= TextAlignments.justify.ToString();
			//Set complet paragraph to italic
			((ParagraphStyle)p.Style).Textproperties.Italic		= "italic";
			((ParagraphStyle)p.Style).Textproperties.FontName	= "Arial";
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
			Console.WriteLine("Blue : {0}", Colors.GetColor(System.Drawing.Color.Blue));
		}

		[Test]
		public void LetterTest()
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
			{
				Paragraph p		= new Paragraph(td, "Pa"+i.ToString());				
				p.TextContent.Add(new SimpleText(p, address[i]));
				td.Content.Add(p);
			}

			Paragraph pnone		= new Paragraph(td, "none1");
			pnone.TextContent.Add(new SimpleText(pnone, @"\n"));
			td.Content.Add(pnone);

			for(int i=0; i < recipient.Length; i++)
			{
				Paragraph p		= new Paragraph(td, "Pr"+i.ToString());				
				p.TextContent.Add(new SimpleText(p, recipient[i]));
				td.Content.Add(p);
			}

			pnone		= new Paragraph(td, "none21");
			pnone.TextContent.Add(new SimpleText(pnone, @"\n"));
			td.Content.Add(pnone);

			Paragraph pb		= new Paragraph(td, "Pb");
			pnone.TextContent.Add(new SimpleText(pb, betreff));
			td.Content.Add(pb);

			pnone		= new Paragraph(td, "none2");
			pnone.TextContent.Add(new SimpleText(pnone, "\n"));
			td.Content.Add(pnone);

			pb		= new Paragraph(td, "Pan");
			pnone.TextContent.Add(new SimpleText(pb, bodyheader));
			td.Content.Add(pb);

			for(int i=0; i < bodytext.Length; i++)
			{
				Paragraph p		= new Paragraph(td, "Pr"+i.ToString());
				((ParagraphProperties)p.Style.Properties).Alignment = TextAlignments.justify.ToString();
				p.TextContent.Add(new SimpleText(p, bodytext[i]));
				td.Content.Add(p);
			}

			pnone		= new Paragraph(td, "none4");
			pnone.TextContent.Add(new SimpleText(pnone, "\n"));
			td.Content.Add(pnone);

			pb		= new Paragraph(td, "PReg");
			pnone.TextContent.Add(new SimpleText(pb, regards));
			td.Content.Add(pb);

			td.SaveTo("Offer.odt");
		}
	}
}
