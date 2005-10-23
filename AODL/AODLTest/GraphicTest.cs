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
			}
			catch(Exception ex)
			{
				Console.Write(ex.Message);
			}
		}

		[Test]
		public void create()
		{
			XmlDocument xd = new XmlDocument();
			xd.Load(@"D:\OpenDocument\AODL\AODLTest\bin\Debug\zip\fonts\content.xml");

			XmlNamespaceManager xm = new XmlNamespaceManager(xd.NameTable);
			xm.AddNamespace("style","urn:oasis:names:tc:opendocument:xmlns:style:1.0");
			xm.AddNamespace("office",		"urn:oasis:names:tc:opendocument:xmlns:office:1.0");

			XmlNodeList xnl = xd.SelectNodes("/office:document-content/office:font-face-decls/style:font-face", xm);
			
			foreach(XmlNode xn in xnl)
			{
				string stylename = xn.SelectSingleNode("@style:name", xm).InnerText;
				
				string x = "\t\t///<summary>\n\t\t///"+stylename+"\n\t\t///</summary>\n\t\tpublic static readonly string "+stylename.Replace(" ","")+" = \""+stylename+"\";";
				Console.WriteLine(x);
			}

			Console.WriteLine(xnl.Count);
		}
	}
}
