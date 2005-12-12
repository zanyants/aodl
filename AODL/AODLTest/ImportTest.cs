using System;
using System.IO;
using NUnit.Framework;
using AODL.Import;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style;

namespace AODLTest
{
	[TestFixture]
	public class ImportTest
	{
		private string _testfile		=  Environment.CurrentDirectory+"\\OfferLongVersion.odt";
		private TextDocument _document	= null;

		[SetUp]
		public void Setup()
		{
			TestClass test		= new TestClass();
			test.LetterTestLongVersion();

			this._document		= new TextDocument();
			this._document.Load(this._testfile);
		}

		[Test]
		public void SimpleLoadTest()
		{	
			//File must exist to pass the test
			
			Assert.IsNotNull(this._document.DocumentConfigurations2, "DocumentConfigurations2 must exist!");
			Assert.IsNotNull(this._document.DocumentManifest, "DocumentManifest must exist!");
			Assert.IsNotNull(this._document.DocumentMetadata, "DocumentMetadat must exist!");
			Assert.IsNotNull(this._document.DocumentPictures, "DocumentPictures must exist!");
			Assert.IsNotNull(this._document.DocumentSetting, "DocumentSetting must exist!");
			Assert.IsNotNull(this._document.DocumentStyles, "DocumentStyles must exist!");
			Assert.IsNotNull(this._document.DocumentThumbnails, "DocumentThumbnails must exist!");

			File.Delete(this._testfile);
		}

		[Test]
		public void RealContentLoadTest()
		{
			Assert.IsNotNull(this._document.Content, "Content container must exist!");
			Assert.IsTrue(this._document.Content.Count > 0, "Must be content in their!");

			this._document.SaveTo("reloaded.odt");
			this._document.Dispose();
		}

		[Test]
		public void ReloadHeader()
		{
			TestClass test		= new TestClass();
			test.HeadingsTest();

			TextDocument doc	= new TextDocument();
			doc.Load("Heading.odt");
			doc.SaveTo("HeadingReloaded.odt");
			doc.Dispose();
		}

		[Test]
		public void ReloadXlink()
		{
			TestClass test		= new TestClass();
			test.XLinkTest();

			TextDocument doc	= new TextDocument();
			doc.Load("Xlink.odt");
			doc.SaveTo("XlinkReloaded.odt");
			doc.Dispose();
		}


		[Test]
		public void SaveAsHtml()
		{
			this._document.SaveTo("reloaded.html");
			this._document.Dispose();
		}

		[Test]
		public void SaveAsHtmlWithTable()
		{
			TextDocument document		= new TextDocument();
			document.Load("XLink.odt");
			document.SaveTo("xlink.html");
			document.Dispose();
		}

		[Test]
		public void RealStressTest()
		{
			TextDocument document		= new TextDocument();
			document.Load("AndrewMacroPart.odt");
			document.SaveTo("AndrewMacro.html");
//			document.Load("OfferLongVersion.odt");
//			document.SaveTo("OfferLongVersion.html");
			document.Dispose();
		}

		[Test]
		public void MegaStressTest()
		{
//			TextDocument document		= new TextDocument();
//			document.Load("AndrewMacro.odt");
//			document.SaveTo("AndrewMacroFull.html");
		}
	}
}
