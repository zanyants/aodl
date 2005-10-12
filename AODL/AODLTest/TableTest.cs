using System;
using System.Xml;
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
		}
	}
}
