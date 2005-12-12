using System;
using System.Xml;
using NUnit.Framework;
using AODL.TextDocument;
using AODL.TextDocument.Style;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style.Properties;

namespace AODLTest
{
	[TestFixture]
	public class ListTests
	{
		private TextDocument _td;

		[SetUp]
		public void Init()
		{
			this._td	= new TextDocument();
			this._td.New();
		}

		[Test]
		public void NumberedListTest()
		{
			List li				= new List(this._td, "L1", ListStyles.Number, "L1P1");
			Assert.IsNotNull(li.Node, "Node object must exist!");

			//Console.WriteLine(li.Node.OuterXml);

			Assert.IsNotNull(li.Style, "Style object must exist!");

			Assert.AreEqual(li.Style.GetType().Name, "ListStyle", "Must be a ListStyle type");

			//Console.WriteLine(li.Style.Node.OuterXml);

			Assert.IsNotNull(((ListStyle)li.Style).ListlevelStyles, "ListLevelStyleCollection must exist!");

			Assert.IsTrue(((ListStyle)li.Style).ListlevelStyles.Count == 10, "Must exist exactly 10 ListLevelStyle objects!");

			Assert.IsNotNull(((ListStyle)li.Style).ListlevelStyles[1].ListlevelProperties, "ListLevelProperties object must exist!");
		}

		[Test]
		public void BulletListTest()
		{
			List li				= new List(this._td, "L1", ListStyles.Bullet, "L1P1");

			Assert.IsNotNull(li.Node, "Node must exist!");

			Assert.IsNotNull(li.Style, "Style must exist!");

			Assert.IsTrue(((ListStyle)li.Style).ListlevelStyles.Count == 10, "Must exist exactly 10 ListLevelStyle objects!");

			Assert.IsNotNull(((ListStyle)li.Style).ListlevelStyles[1].TextProperties, "TextProperties object must exist, this is an bullet list which needs this propertie!");
		}

		[Test]
		public void ListItemTest()
		{
			List li				= new List(this._td, "L1", ListStyles.Bullet, "L1P1");

			Assert.IsNotNull(li.ParagraphStyle, "ParagraphStyle object must exist!");

			ListItem lit		= new ListItem(li);

			Assert.IsNotNull(lit.Paragraph, "Paragraph object must exist!");

			Assert.AreEqual(lit.Paragraph.Stylename, "L1P1", "Must be equal all ListItems reference to the same ParagraphStyle, to this one of their List!");

			lit.Paragraph.TextContent.Add(new SimpleText(lit, "Hello"));

			Assert.IsTrue(lit.Paragraph.TextContent.Count == 1, "Must be one!");

			li.Content.Add(lit);

			this._td.Content.Add(li);

			this._td.SaveTo("list.odt");
		}

		[Test]
		public void InnerListTest()
		{
			List li				= new List(this._td, "L1", ListStyles.Bullet, "L1P1");
			ListItem lit		= new ListItem(li);
			lit.Paragraph.TextContent.Add(new SimpleText(lit, "Hello"));
			li.Content.Add(lit);
			
			//The ListItem will become a inner list !!
			lit					= new ListItem(li);
			lit.Paragraph.TextContent.Add(new SimpleText(lit, "Hello Again"));

			//Inner List - see the constrctor usage !
			List liinner		= new List(this._td, li);

			Assert.IsNull(liinner.Style, "Style must be null! The inner list inherited his style from the outer list!");

			ListItem litinner	= new ListItem(liinner);
			litinner.Paragraph.TextContent.Add(new SimpleText(lit, "Hello i'm in the inner list"));
			liinner.Content.Add(litinner);

			//Add the inner list to ListItem lit
			lit.Content.Add(liinner);

			//Add the ListItem with inner list inside
			li.Content.Add(lit);

			//Add the whole list to the document
			this._td.Content.Add(li);

			this._td.SaveTo("innerlist.odt");			
		}
	}
}
