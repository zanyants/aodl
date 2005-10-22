using System;
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
		private string _imagefile		= @"D:\lb.gif";

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
			}
			catch(Exception ex)
			{
				Console.Write(ex.Message);
			}
		}
	}
}
