/*
 * $Id: GraphicTest.cs,v 1.7 2006/01/29 19:30:24 larsbm Exp $
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
using System.Xml;
using NUnit.Framework;
using AODL.Document.TextDocuments;
using AODL.Document.Content.Text;
using AODL.Document.Content.Draw;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;

namespace AODLTest
{
	[TestFixture]
	public class GraphicTest
	{
		private string _imagefile		= AARunMeFirstAndOnce.inPutFolder+@"Eclipse_add_new_Class.jpg";

		[Test]
		public void GraphicsTest()
		{
				TextDocument textdocument		= new TextDocument();
				textdocument.New();
				Paragraph p						= ParagraphBuilder.CreateStandardTextParagraph(textdocument);
				Frame frame						= new Frame(textdocument, "frame1",
					"graphic1", _imagefile);
				p.Content.Add(frame);
				textdocument.Content.Add(p);
				textdocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+"grapic.odt");
		}

		[Test]
		public void DrawTextBoxTest()
		{
			TextDocument textdocument		= new TextDocument();
			textdocument.New();
			Paragraph pOuter				= ParagraphBuilder.CreateStandardTextParagraph(textdocument);
			DrawTextBox drawTextBox			= new DrawTextBox(textdocument);
			Frame frameTextBox				= new Frame(textdocument, "fr_txt_box");
			frameTextBox.DrawName			= "fr_txt_box";
			frameTextBox.ZIndex				= "0";
//			Paragraph pTextBox				= ParagraphBuilder.CreateStandardTextParagraph(textdocument);
//			pTextBox.StyleName				= "Illustration";
			Paragraph p						= ParagraphBuilder.CreateStandardTextParagraph(textdocument);
			p.StyleName						= "Illustration";
			Frame frame						= new Frame(textdocument, "frame1",
				"graphic1", _imagefile);
			frame.ZIndex					= "1";
			p.Content.Add(frame);
			p.TextContent.Add(new SimpleText(textdocument, "Illustration"));
			drawTextBox.Content.Add(p);
			
			frameTextBox.SvgWidth			= frame.SvgWidth;
			drawTextBox.MinWidth			= frame.SvgWidth;
			drawTextBox.MinHeight			= frame.SvgHeight;
			frameTextBox.Content.Add(drawTextBox);
			pOuter.Content.Add(frameTextBox);
			textdocument.Content.Add(pOuter);
			textdocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+"drawTextbox.odt");
		}

		/// <summary>
		/// Image map test. Desc: Create new Text document, create a new frame with
		/// graphic. Create a DrawAreaRectangle and a DrawAreaCircle and them
		/// as Image Map to the graphic.
		/// TODO: change imagePath!
		/// </summary>
		[Test]
		public void ImageMapTest()
		{
			string imagePath				= @"D:\OpenDocument\AODL\AODLTest\bin\Debug\Files\Eclipse_add_new_Class.jpg";
			TextDocument document			= new TextDocument();
			document.New();
			//Create standard paragraph
			Paragraph paragraphOuter		= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Create the frame with graphic
			Frame frame						= new Frame(document, "frame1", "graphic1", imagePath);
			//Create a Draw Area Rectangle
			DrawAreaRectangle drawAreaRec	= new DrawAreaRectangle(
				document, "0cm", "0cm", "1.5cm", "2.5cm", null);
			drawAreaRec.Href				= "http://OpenDocument4all.com";
			//Create a Draw Area Circle
			DrawAreaCircle drawAreaCircle	= new DrawAreaCircle(
				document, "4cm", "4cm", "1.5cm", null);
			drawAreaCircle.Href				= "http://AODL.OpenDocument4all.com";
			DrawArea[] drawArea				= new DrawArea[2] { drawAreaRec, drawAreaCircle };
			//Create a Image Map
			ImageMap imageMap				= new ImageMap(document, drawArea);
			//Add Image Map to the frame
			frame.Content.Add(imageMap);
			//Add frame to paragraph
			paragraphOuter.Content.Add(frame);
			//Add paragraph to document
			document.Content.Add(paragraphOuter);
			//Save the document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"simpleImageMap.odt");
		}

//		[Test]
//		public void DrawTextBox()
//		{
//			//New TextDocument
//			TextDocument textdocument		= new TextDocument();
//			textdocument.New();
//			//Standard Paragraph
//			Paragraph paragraphOuter		= new Paragraph(textdocument, ParentStyles.Standard.ToString());
//			//Create Frame for DrawTextBox
//			Frame frameOuter				= new Frame(textdocument, "frame1");
//			//Create DrawTextBox
//			DrawTextBox drawTextBox			= new DrawTextBox(frameOuter);
//			//Create a paragraph for the drawing frame
//			Paragraph paragraphInner		= new Paragraph(textdocument, ParentStyles.Standard.ToString());
//			//Create the frame with the Illustration resp. Graphic
//			Frame frameIllustration			= new Frame(textdocument, "frame2", "graphic1", _imagefile);
//			//Add Illustration frame to the inner Paragraph
//			paragraphInner.Content.Add(frameIllustration);
//			//Add inner Paragraph to the DrawTextBox
//			drawTextBox.ContentCollection.Add(paragraphInner);
//			//Add the DrawTextBox to the outer Frame
//			frameOuter.ContentCollection.Add(drawTextBox);
//			//Add the outer Frame to the outer Paragraph
//			paragraphOuter.Content.Add(frameOuter);
//			//Add the outer Paragraph to the TextDocument
//			textdocument.Content.Add(paragraphOuter);
//			//Save the document
//			textdocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+"DrawTextBox.odt");
//		}

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
