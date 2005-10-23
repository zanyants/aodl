using System;
using System.Xml;
using System.Drawing;
using AODL.Collections;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style;
using AODL.TextDocument.Style.Properties;
using NUnit.Framework;

namespace AODLTest
{
	[TestFixture]
	public class TableTest
	{
		[Test]
		public void TableTest1()
		{
			TextDocument td			= new TextDocument();
			td.New();

			Table t					= new Table(td, "table1");

			Assert.IsNotNull(t.Node, "Node must exist");

			Assert.IsNotNull(t.Style, "Style must exist");

			Assert.AreEqual("table1", t.Style.Name, "Name must be table1");

			Assert.IsNotNull(((TableStyle)t.Style).Properties, "Must exist!");

			Assert.AreEqual("16.99cm", ((TableStyle)t.Style).Properties.Width, "Must be the default 16.99cm");

			t.Init(2, 2, 16.99);

			Assert.IsNotNull(t.Columns, "Columncollection must exist!");

			Assert.IsTrue(t.Columns.Count == 2, "Must be 2 columns!");

			Assert.AreEqual("table1.A", t.Columns[0].Style.Name, "Must be table1.A!");

			Assert.AreEqual("table1.B", t.Columns[1].Style.Name, "Must be table1.B!");

			Assert.IsNotNull(t.Rows, "RowCollection must exist!");

			Assert.IsTrue(t.Rows.Count == 2, "Must be 2 rows");

			Assert.AreEqual("table1.1", t.Rows[0].Stylename, "Must be table1.1");

			Assert.AreEqual("table1.2", t.Rows[1].Stylename, "Must be table1.2");

			Assert.IsTrue(t.Rows[0].Cells.Count == 2, "Must be 2 Cells wihtin this row!");

			Assert.AreEqual("table1.A11", t.Rows[0].Cells[0].Stylename, "Must be table1.A1");

			foreach(Row r in t.Rows)
				foreach(Cell c in r.Cells)
					c.InsertText("Hallo");

			foreach(Row r in t.Rows)
				foreach(Cell c in r.Cells)
					Assert.IsTrue(c.Content.Count == 1, "Must be all 1");

			td.Content.Add(t);

			td.SaveTo("table1.odt");
		}

		[Test]
		public void CellParagraphTest()
		{
			TextDocument doc		= new TextDocument();
			doc.New();

			Table table				= new Table(doc, "table1");
			table.Init(5, 3, 16.99);

			foreach(Row r in table.Rows)
				foreach(Cell c in r.Cells)
					c.InsertText("Hello");

			Paragraph p				= new Paragraph(doc, "P1");

			FormatedText ft			= new FormatedText(p, "T1", "Hello World");

			((TextStyle)ft.Style).Properties.Italic = "italic";

			p.TextContent.Add(ft);

			table.Rows[0].Cells[0].Content.Add(p);

			doc.Content.Add(table);

			doc.SaveTo("tablewithstyles.odt");
		}

		[Test]
		public void LongTableTest()
		{
			TextDocument doc		= new TextDocument();
			doc.New();

			Table table				= new Table(doc, "table1");
			table.Init(150, 5, 16.99);

			foreach(Row r in table.Rows)
				foreach(Cell c in r.Cells)
					c.InsertText("Hello");

			doc.Content.Add(table);

			doc.SaveTo("tablelong.odt");
		}

		[Test]
		public void CellWithListTest()
		{
			TextDocument doc		= new TextDocument();
			doc.New();

			Table table				= new Table(doc, "table1");
			table.Init(5, 3, 16.99);

			((CellStyle)table.Rows[0].Cells[0].Style).CellProperties.BackgroundColor = Colors.GetColor(Color.Tomato);
			
			List li				= new List(doc, "L1", ListStyles.Bullet, "L1P1");
			ListItem lit		= new ListItem(li);
			lit.Paragraph.TextContent.Add(new SimpleText(lit, "Hello"));
			li.Content.Add(lit);
			
			//The ListItem will become a inner list !!
			lit					= new ListItem(li);
			lit.Paragraph.TextContent.Add(new SimpleText(lit, "Hello Again"));

			//Inner List - see the constrctor usage !
			List liinner		= new List(doc, li);

			Assert.IsNull(liinner.Style, "Style must be null! The inner list inherited his style from the outer list!");

			ListItem litinner	= new ListItem(liinner);
			litinner.Paragraph.TextContent.Add(new SimpleText(lit, "Hello i'm in the inner list"));
			liinner.Content.Add(litinner);

			//Add the inner list to ListItem lit
			lit.Content.Add(liinner);

			
			//Add the ListItem with inner list inside
			li.Content.Add(lit);

			table.Rows[0].Cells[0].Content.Add(li);

			doc.Content.Add(table);

			doc.SaveTo("tablewithList.odt");
		}
	}
}
