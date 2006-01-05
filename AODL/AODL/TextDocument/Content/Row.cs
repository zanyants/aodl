/*
 * $Id: Row.cs,v 1.3 2006/01/05 10:31:10 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a row with used within a table.
	/// </summary>
	public class Row : IContent, IHtml
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

		private CellSpanCollection _cellSpans;
		/// <summary>
		/// Gets or sets the cells.
		/// </summary>
		/// <value>The cells.</value>
		public CellSpanCollection CellSpans
		{
			get { return this._cellSpans; }
			set { this._cellSpans = value; }
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
		/// Merge cells
		/// </summary>
		/// <param name="cellStartIndex">Start index of the cell.</param>
		/// <param name="mergeCells">The count of cells to merge incl. the starting cell.</param>
		/// <param name="mergeContent">if set to <c>true</c> [merge content].</param>
		public void MergeCells(int cellStartIndex, int mergeCells, bool mergeContent)
		{
			try
			{
				this.Cells[cellStartIndex].ColumnRepeating		= mergeCells.ToString();
				
				if(mergeContent)
				{
					for(int i=cellStartIndex+1; i<cellStartIndex+mergeCells; i++)
					{
						foreach(IContent content in this.Cells[i].Content)
							this.Cells[cellStartIndex].Content.Add(content);
					}
				}

				for(int i=cellStartIndex+mergeCells-1; i>cellStartIndex; i--)
				{
					this.Cells.RemoveAt(i);
					this.CellSpans.Add(new CellSpan(this));
				}
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Inits the CellCollection instance.
		/// </summary>
		private void Init()
		{
			this.Cells				= new CellCollection();
			this.Cells.Inserted		+=new AODL.Collections.CollectionWithEvents.CollectionChange(Cells_Inserted);
			this.Cells.Removed		+=new AODL.Collections.CollectionWithEvents.CollectionChange(Cells_Removed);

			this.CellSpans			= new CellSpanCollection();
			this.CellSpans.Inserted	+=new AODL.Collections.CollectionWithEvents.CollectionChange(CellSpans_Inserted);
			this.CellSpans.Removed	+=new AODL.Collections.CollectionWithEvents.CollectionChange(CellSpans_Removed);
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

		/// <summary>
		/// Cells_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Cells_Inserted(int index, object value)
		{
			this.Node.AppendChild(((Cell)value).Node);
		}

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string html		= "<tr ";

			if(((RowStyle)this.Style).RowProperties != null)
				html		+= ((RowStyle)this.Style).RowProperties.GetHtmlStyle();

			html			+= ">\n";

			foreach(Cell cell in this.Cells)
				if(cell is IHtml)
					html	+= cell.GetHtml()+"\n";

			html			+= "</tr>";

			return html;
		}

		#endregion

		/// <summary>
		/// Cells the spans_ inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void CellSpans_Inserted(int index, object value)
		{
			this.Node.AppendChild(((CellSpan)value).Node);
		}

		/// <summary>
		/// Cells_s the removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Cells_Removed(int index, object value)
		{
			this.Node.RemoveChild(((Cell)value).Node);
		}

		/// <summary>
		/// Cells the spans_ removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void CellSpans_Removed(int index, object value)
		{
			this.Node.RemoveChild(((CellSpan)value).Node);
		}
	}
}
