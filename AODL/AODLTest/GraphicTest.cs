/*
 * $Id: GraphicTest.cs,v 1.8 2006/02/16 18:35:40 larsbm Exp $
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
using AODL.Document.Content.Text.Indexes;
using AODL.Document.Content.Draw;
using AODL.Document.Content;
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

		/// <summary>
		/// Create a Illustration manuel.
		/// </summary>
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

		[Test]
		public void CreateIllustrationUsingTheFrameBuilder()
		{
			TextDocument document			= new TextDocument();
			document.New();
			//Create a standard pargraph for the illustration
			Paragraph paragraphStandard		= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Create Illustration Frame using the FrameBuilder
			Frame frameIllustration			= FrameBuilder.BuildIllustrationFrame(
				document,
				"illustration_frame_1",
				"graphic1",
				_imagefile,
				"This is a Illustration",
				1);
			//Add the Illustration Frame to the Paragraph
			paragraphStandard.Content.Add(frameIllustration);
			Assert.IsTrue(frameIllustration.Content[0] is DrawTextBox, "Must be a DrawTextBox!");
			Assert.IsTrue(((DrawTextBox)frameIllustration.Content[0]).Content[0] is Paragraph, "Must be a Paragraph!");
			Paragraph paragraph				= ((DrawTextBox)frameIllustration.Content[0]).Content[0] as Paragraph;
			Assert.IsTrue(paragraph.TextContent[1] is TextSequence, "Must be a TextSequence!");
			//Add Paragraph to the document
			document.Content.Add(paragraphStandard);
			//Save the document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"illustration.odt");
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

		[Test]
		public void LoadGraphicAccessGraphic()
		{
			TextDocument document			= new TextDocument();
			document.Load(AARunMeFirstAndOnce.inPutFolder+"hallo.odt");
			foreach(IContent iContent in document.Content)
			{
				if(iContent is Paragraph && ((Paragraph)iContent).Content.Count > 0
					&& ((Paragraph)iContent).Content[0] is Frame)
				{
					Frame frame				= ((Paragraph)iContent).Content[0] as Frame;
					Assert.IsTrue(frame.Content[0] is Graphic, "Must be a graphic!");
					Graphic graphic			= frame.Content[0] as Graphic;
					//now access the full qualified graphic path
					Assert.IsNotNull(graphic.GraphicRealPath, "The graphic real path must exist!");
					Assert.IsTrue(File.Exists(graphic.GraphicRealPath));
				}
			}
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"hallo_rel_graphic_touch.odt");
		}

		[Test]
		public void LoadFileDeleteGraphic()
		{
			string fileName					= "hallo_rem_graphic.odt";
			string graphicFile				= "";
			TextDocument document			= new TextDocument();
			document.Load(AARunMeFirstAndOnce.inPutFolder+"hallo.odt");
			foreach(IContent iContent in document.Content)
			{
				if(iContent is Paragraph && ((Paragraph)iContent).Content.Count > 0
					&& ((Paragraph)iContent).Content[0] is Frame)
				{
					Frame frame				= ((Paragraph)iContent).Content[0] as Frame;
					Assert.IsTrue(frame.Content[0] is Graphic, "Must be a graphic!");
					Graphic graphic			= frame.Content[0] as Graphic;
					//now access the full qualified graphic path
					Assert.IsNotNull(graphic.GraphicRealPath, "The graphic real path must exist!");
					Assert.IsTrue(File.Exists(graphic.GraphicRealPath));
					graphicFile				= graphic.GraphicRealPath;
					//Delete the graphic
					frame.Content.Remove(graphic);
				}
			}
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+fileName);
			//Special case, only for this test neccessary
			document.Dispose();
			//Load file again
			TextDocument documentRel		= new TextDocument();
			documentRel.Load(AARunMeFirstAndOnce.outPutFolder+fileName);
			Assert.IsFalse(File.Exists(graphicFile), "This file must be removed from this file!");
		}

		[Test]
		public void CreateFreePositionGraphic()
		{
			TextDocument textdocument		= new TextDocument();
			textdocument.New();
			Paragraph p						= ParagraphBuilder.CreateStandardTextParagraph(textdocument);
			Frame frame						= FrameBuilder.BuildStandardGraphicFrame(textdocument, "frame1",
				"graphic1", _imagefile);
			//Setps to set a graphic free with x and y
			frame.SvgX						= "1.75cm";
			frame.SvgY						= "1.75cm";
			((FrameStyle)frame.Style).GraphicProperties.HorizontalPosition		= "from-left";
			((FrameStyle)frame.Style).GraphicProperties.VerticalPosition		= "from-top";
			((FrameStyle)frame.Style).GraphicProperties.HorizontalRelative		= "paragraph";
			((FrameStyle)frame.Style).GraphicProperties.VerticalRelative		= "paragraph";
			p.Content.Add(frame);
			textdocument.Content.Add(p);
			textdocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+"grapic_free_xy.odt");
		}
	}
}
