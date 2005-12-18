using System;
using System.IO;
using System.Xml;
using NUnit.Framework;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style;
using AODL.TextDocument.Style.Properties;

namespace AODLTest
{
	[TestFixture]
	public class GraphicTest
	{
		//Must change this !!
		private string _imagefile		= @"D:\lb.png";

		[Test]
		public void GraphicsTest()
		{
			try
			{
				TextDocument textdocument		= new TextDocument();
				textdocument.New();

				Paragraph p						= new Paragraph(textdocument, ParentStyles.Standard.ToString());

				Frame frame						= new Frame(textdocument, "frame1",
					"graphic1", _imagefile);

				p.Content.Add(frame);

				textdocument.Content.Add(p);

				textdocument.SaveTo("grapic.odt");
				textdocument.Dispose();
			}
			catch(Exception ex)
			{
				//Console.Write(ex.Message);
			}
		}

		[Test]
		public void DrawTextBox()
		{
			//New TextDocument
			TextDocument textdocument		= new TextDocument();
			textdocument.New();
			//Standard Paragraph
			Paragraph paragraphOuter		= new Paragraph(textdocument, ParentStyles.Standard.ToString());
			//Create Frame for DrawTextBox
			Frame frameOuter				= new Frame(textdocument, "frame1");
			//Create DrawTextBox
			DrawTextBox drawTextBox			= new DrawTextBox(frameOuter);
			//Create a paragraph for the drawing frame
			Paragraph paragraphInner		= new Paragraph(textdocument, ParentStyles.Standard.ToString());
			//Create the frame with the Illustration resp. Graphic
			Frame frameIllustration			= new Frame(textdocument, "frame2", "graphic1", _imagefile);
			//Add Illustration frame to the inner Paragraph
			paragraphInner.Content.Add(frameIllustration);
			//Add inner Paragraph to the DrawTextBox
			drawTextBox.ContentCollection.Add(paragraphInner);
			//Add the DrawTextBox to the outer Frame
			frameOuter.ContentCollection.Add(drawTextBox);
			//Add the outer Frame to the outer Paragraph
			paragraphOuter.Content.Add(frameOuter);
			//Add the outer Paragraph to the TextDocument
			textdocument.Content.Add(paragraphOuter);
			//Save the document
			textdocument.SaveTo("DrawTextBox.odt");
		}

		[Test]
		public void create()
		{
//			XmlDocument xd = new XmlDocument();
//			xd.Load(@"D:\OpenDocument\AODL\AODLTest\bin\Debug\zip\fonts\content.xml");
//
//			XmlNamespaceManager xm = new XmlNamespaceManager(xd.NameTable);
//			xm.AddNamespace("style","urn:oasis:names:tc:opendocument:xmlns:style:1.0");
//			xm.AddNamespace("office",		"urn:oasis:names:tc:opendocument:xmlns:office:1.0");
//
//			XmlNodeList xnl = xd.SelectNodes("/office:document-content/office:font-face-decls/style:font-face", xm);
//			
//			foreach(XmlNode xn in xnl)
//			{
//				string stylename = xn.SelectSingleNode("@style:name", xm).InnerText;
//				
//				string x = "\t\t///<summary>\n\t\t///"+stylename+"\n\t\t///</summary>\n\t\tpublic static readonly string "+stylename.Replace(" ","")+" = \""+stylename+"\";";
				//Console.WriteLine(x);
//			}

			//Console.WriteLine(xnl.Count);
		}
	}
}
