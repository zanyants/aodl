/*
 * $Id: Cell.cs,v 1.3 2005/12/18 18:29:46 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a Cell which used within a table resp. a row.
	/// </summary>
	public class Cell : IContent, IContentContainer, IHtml
	{
		private Row _row;
		/// <summary>
		/// Gets or sets the row.
		/// </summary>
		/// <value>The row.</value>
		public Row Row
		{
			get { return this._row; }
			set { this._row = value; }
		}

		/// <summary>
		/// Use this to merge cells, the count of ColumnRepeating,
		/// need the same count of following CellSpan objects e.g. "2"
		/// </summary>
		/// <value>Count of Columns to be repeated</value>
		public string ColumnRepeating
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@table:number-columns-spanned",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@table:number-columns-spanned",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("number-columns-spanned", value, "table");
				this._node.SelectSingleNode("@table:number-columns-spanned",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Cell"/> class.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="stylename">The stylename.</param>
		public Cell(Row row, string stylename)
		{
			this.Row				= row;
			this.Document			= row.Document;
			this.Style				= (CellStyle)new CellStyle(this, stylename);
			this.NewXmlNode(stylename);
			this.Content			= new IContentCollection();
			this.Content.Inserted	+=new AODL.Collections.CollectionWithEvents.CollectionChange(Content_Inserted);
		}

		/// <summary>
		/// Inserts the text into cell. This text will be formated in the
		/// default paragraph style of the table to which this cell belong.
		/// </summary>
		/// <param name="text">The text.</param>
		public void InsertText(string text)
		{
			Paragraph p			= new Paragraph(this.Document, ParentStyles.Table , text);
			this.Content.Add(p);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="stylename">The stylename which should be referenced with this row.</param>
		private void NewXmlNode(string stylename)
		{			
			this.Node		= this.Document.CreateNode("table-cell", "table");

			XmlAttribute xa = this.Document.CreateAttribute("style-name", "table");
			xa.Value		= stylename;

			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("value-type", "office");
			xa.Value		= "string";

			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
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
		/// Not implemented !!! Use the cells IContentCollection
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

		#region IContentContainer Member

		private IContentCollection _content;
		/// <summary>
		/// Gets or sets the content collection.
		/// Here you can add every content object e.g. paragraphs, lists, ...
		/// </summary>
		/// <value>The content.</value>
		public IContentCollection Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		#endregion

		/// <summary>
		/// Content_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IContent)value).Node);
		}

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string html			= "<td ";

			if(this.ColumnRepeating != null)
				html			+= "colspan="+this.ColumnRepeating+" ";

			if(((CellStyle)this.Style).CellProperties != null)
				html			+= ((CellStyle)this.Style).CellProperties.GetHtmlStyle();

			string htmlwidth	= this.GetHtmlWidth();
			if(htmlwidth != null)
				if(html.IndexOf("style=") == -1)
					html		+= "style=\""+htmlwidth+"\"";
				else
					html		= html.Substring(0, html.Length-1)+htmlwidth+"\"";				

			html				+= ">\n";

			foreach(IContent content in this.Content)
				if(content is IHtml)
					html		+= ((IHtml)content).GetHtml();

			if(this.Content != null)
				if(this.Content.Count == 0)
					html		+= "&nbsp;";

			html				+= "\n</td>\n";

			return html;
		}

		/// <summary>
		/// Gets the width of the HTML.
		/// </summary>
		/// <returns></returns>
		private string GetHtmlWidth()
		{
			try
			{
				int index		= 0;
				foreach(Cell cell in this.Row.Cells)
				{
					if(cell == this)
					{
						if(this.Row.Table.Columns != null)
							if(index <= this.Row.Table.Columns.Count)
							{
								Column column	= this.Row.Table.Columns[index];
								if(column != null)
									if(((ColumnStyle)column.Style).Properties.Width != null)
										return " width: "+((ColumnStyle)column.Style).Properties.Width.Replace(",",".")+"; ";

								
							}
					}
					index++;
				}				
			}
			catch(Exception ex)
			{
				//Doesn't matter
			}
			return "";
		}

		#endregion
	}
}
