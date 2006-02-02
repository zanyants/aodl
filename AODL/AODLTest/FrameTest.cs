/*
 * $Id: FrameTest.cs,v 1.1 2006/02/02 21:55:59 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, Kristy Saunders, ksaunders@eduworks.com
 * 
 * Last changes:
 * 2006-02-02 Extend EventListenersTest with Clone() method Lars Behrmann lb@opendocument4all.com
 */

using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using AODL.Document.TextDocuments;
using AODL.Document.Content.Text;
using AODL.Document.Content.Draw;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.Content.OfficeEvents;

namespace AODLTest
{
	[TestFixture]
	public class FrameTest
	{
		private string _imagefile = AARunMeFirstAndOnce.inPutFolder + @"Eclipse_add_new_Class.jpg";
		private readonly string _framefile = AARunMeFirstAndOnce.outPutFolder + @"frame.odt";
		private readonly string _framefileSave = AARunMeFirstAndOnce.outPutFolder + @"frameSave.odt";
		private readonly string _framefile2 = AARunMeFirstAndOnce.outPutFolder + @"frame2.odt";
		private readonly string _framefile3 = AARunMeFirstAndOnce.outPutFolder + @"frame3.odt";

		[TestFixtureSetUp]
		public void Initialize()
		{
			// Used when running this text fixture alone.
			if (Directory.Exists(AARunMeFirstAndOnce.outPutFolder))
				Directory.Delete(AARunMeFirstAndOnce.outPutFolder, true);
			Directory.CreateDirectory(AARunMeFirstAndOnce.outPutFolder);
		}

		[Test(Description="Write an image with image map to a frame")]
		public void FrameWriteTest()
		{
			try
			{
				TextDocument textdocument = new TextDocument();
				textdocument.New();

				// Create a frame (GraphicName == name property of frame)
				Frame frame = new Frame(textdocument, "frame1");
				frame.DrawName = "img1";

				Graphic graphic = new Graphic(textdocument, frame, _imagefile);

				// Add the graphic to the frame collection
				frame.Content.Add(graphic);
				
				// Create some event listeners (using OpenOffice friendly syntax).
				EventListener script1 = new EventListener(textdocument, 
					"dom:mouseover", "javascript", 
					"vnd.sun.star.script:HelloWorld.helloworld.js?language=JavaScript&location=share");
				EventListener script2 = new EventListener(textdocument,
					"dom:mouseout", "javascript",
					"vnd.sun.star.script:HelloWorld.helloworld.js?language=JavaScript&location=share");
				EventListeners listeners = new EventListeners(textdocument, new EventListener[] { script1, script2 });
				
				// Create and add some area rectangles
				DrawAreaRectangle[] rects = new DrawAreaRectangle[2];
				rects[0] = new DrawAreaRectangle(textdocument, "4cm", "4cm", "2cm", "2cm");
				rects[0].Href = @"http://www.eduworks.com";
				rects[1] = new DrawAreaRectangle(textdocument, "1cm", "1cm", "2cm", "2cm", listeners);

				// Create and add an image map, referencing the area rectangles
				ImageMap map = new ImageMap(textdocument, rects);
				frame.Content.Add(map);

				// Add the frame to the text document
				textdocument.Content.Add(frame);

				// Save the document
				textdocument.SaveTo(_framefile3);
				textdocument.Dispose();
			}
			catch (Exception ex)
			{
				//Console.Write(ex.Message);
			}
		}

		[Test(Description="Read back elements written by FrameWriteTest")]
		public void FrameTestRead()
		{
			try
			{
				TextDocument document = new TextDocument();
				document.Load(_framefile);
				Assert.IsTrue(document.Content[0].GetType() == typeof(Frame));
				Frame frame = (Frame)document.Content[0];
				Assert.IsNotNull(frame);
				document.SaveTo(_framefileSave);
			}
			catch (Exception e)
			{
			}
		}

		[Test(Description="Write an image with image map; reuse event listeners")]
		public void EventListenerTest()
		{
			try
			{
				TextDocument textdocument = new TextDocument();
				textdocument.New();

				// Create a frame (GraphicName == name property of frame)
				Frame frame						= new Frame(textdocument, "frame1", "img1", _imagefile);

				// Create some event listeners (using OpenOffice friendly syntax).
				EventListener script1 = new EventListener(textdocument,
					"dom:mouseover", "javascript",
					"vnd.sun.star.script:HelloWorld.helloworld.js?language=JavaScript&location=share");
				EventListener script2 = new EventListener(textdocument,
					"dom:mouseout", "javascript",
					"vnd.sun.star.script:HelloWorld.helloworld.js?language=JavaScript&location=share");
				EventListeners listeners = new EventListeners(textdocument, new EventListener[] { script1, script2 });

				// Create and add some area rectangles; reuse event listeners
				DrawAreaRectangle[] rects = new DrawAreaRectangle[2];
				rects[0] = new DrawAreaRectangle(textdocument, "4cm", "4cm", "2cm", "2cm", listeners);
				//Reuse a clone of the EventListener
				rects[1] = new DrawAreaRectangle(textdocument, "1cm", "1cm", "2cm", "2cm", (EventListeners)listeners.Clone());

				// Create and add an image map, referencing the area rectangles
				ImageMap map = new ImageMap(textdocument, rects);
				frame.Content.Add(map);

				// Add the frame to the text document
				textdocument.Content.Add(frame);

				// Save the document
				textdocument.SaveTo(_framefile);
				textdocument.Dispose();
			}
			catch (Exception ex)
			{
				//Console.Write(ex.Message);
			}
		}

		[Test]
		public void DrawTextBox()
		{
			//New TextDocument
			TextDocument textdocument = new TextDocument();
			textdocument.New();
			//Standard Paragraph
			Paragraph paragraphOuter = new Paragraph(textdocument, ParentStyles.Standard.ToString());
			//Create Frame for DrawTextBox
			Frame frameOuter = new Frame(textdocument, "frame1");
			//Create DrawTextBox
			DrawTextBox drawTextBox = new DrawTextBox(textdocument);
			//Create a paragraph for the drawing frame
			Paragraph paragraphInner = new Paragraph(textdocument, ParentStyles.Standard.ToString());
			//Create the frame with the Illustration resp. Graphic
			Frame frameIllustration = new Frame(textdocument, "frame2", "graphic1", _imagefile);
			//Add Illustration frame to the inner Paragraph
			paragraphInner.Content.Add(frameIllustration);
			//Add inner Paragraph to the DrawTextBox
			drawTextBox.Content.Add(paragraphInner);
			//Add the DrawTextBox to the outer Frame
			frameOuter.Content.Add(drawTextBox);
			//Add the outer Frame to the outer Paragraph
			paragraphOuter.Content.Add(frameOuter);
			//Add the outer Paragraph to the TextDocument
			textdocument.Content.Add(paragraphOuter);
			//Save the document
			textdocument.SaveTo(_framefile2);
		}
	}
}

/*
 * $Log: FrameTest.cs,v $
 * Revision 1.1  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 */
