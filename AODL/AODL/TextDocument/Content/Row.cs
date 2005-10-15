/*
 * $Id: Row.cs,v 1.1 2005/10/15 11:40:31 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a row with used within a table.
	/// </summary>
	public class Row : IContent
	{
		private Table _table;
		/// <summary>
		/// Gets or sets the table.
		/// </summary>
		/// <value>The table.</value>
		public Table Table
		{
			get { return this._table; }
			set { this._table = value; }
		}

		private CellCollection _cells;
		/// <summary>
		/// Gets or sets the cells.
		/// </summary>
		/// <value>The cells.</value>
		public CellCollection Cells
		{
			get { return this._cells; }
			set { this._cells = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Row"/> class.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="stylename">The stylename.</param>
		public Row(Table table, string stylename)
		{
			this.Table			= table;
			this.Document		= table.Document;
			this.Style			= (RowStyle)new RowStyle(this, stylename);
			this.NewXmlNode(stylename);
			this.Init();
		}

		/// <summary>
		/// Inits the CellCollection instance.
		/// </summary>
		private void Init()
		{
			this.Cells			= new CellCollection();
			this.Cells.Inserted	+=new AODL.Collections.CollectionWithEvents.CollectionChange(Cells_Inserted);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="stylename">The stylename which should be referenced with this row.</param>
		private void NewXmlNode(string stylename)
		{			
			this.Node		= this.Document.CreateNode("table-row", "table");
			XmlAttribute xa = this.Document.CreateAttribute("style-name", "table");
			xa.Value		= stylename;

			this.Node.Attributes.Append(xa);
		}

		#region IContent Member

		private TextDocument _document;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his TextDocument.
		/// </summary>
		/// <value></value>
		public TextDocument Document
		{
			get
			{
				return this._document;
			}
			set
			{
				this._document = value;
			}
		}

		private XmlNode _node;
		/// <summary>
		/// Represents the XmlNode within the content.xml from the odt file.
		/// </summary>
		/// <value>The node</value>
		public System.Xml.XmlNode Node
		{
			get
			{
				return this._node;
			}
			set
			{
				this._node = value;
			}
		}

		/// <summary>
		/// The stylename wihich is referenced with the content object.
		/// </summary>
		/// <value>The name</value>
		public string Stylename
		{
			get
			{
				return this.Style.Name;
			}
			set
			{
				this.Style.Name = value;
				this._node.SelectSingleNode("@table:style-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// </summary>
		/// <value>The style</value>
		public AODL.TextDocument.Style.IStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this._style = value;
			}
		}

		/// <summary>
		/// Not implemented !!! Use the rows CellCollection
		/// to get the content.
		/// </summary>
		/// <value></value>
		public ITextCollection TextContent
		{
			get
			{
				return null;
			}
			set
			{
				
			}
		}

		#endregion

		private void Cells_Inserted(int index, object value)
		{
			this.Node.AppendChild(((Cell)value).Node);
		}
	}
}
