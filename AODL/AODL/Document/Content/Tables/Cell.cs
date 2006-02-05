/*
 * $Id: Cell.cs,v 1.3 2006/02/05 20:02:25 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, Lars Behrmann, lb@OpenDocument4all.com
 * 
 * Last changes:
 * 
 */

using System;
using System.Xml;
using AODL.Document.Styles;
using AODL.Document.Content;

namespace AODL.Document.Content.Tables
{
	/// <summary>
	/// Cell represent a table cell.
	/// </summary>
	public class Cell : IContent, IContentContainer, IHtml
	{
		/// <summary>
		/// Gets or sets the cell style.
		/// </summary>
		/// <value>The cell style.</value>
		public CellStyle CellStyle
		{
			get
			{
				return (CellStyle)this.Style;
			}
			set
			{
				this.StyleName	= value.StyleName;
				this.Style = value;
			}
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

		private Table _table;		
		/// <summary>
		/// Gets or sets the row.
		/// </summary>
		/// <value>The row.</value>
		public Table Table
		{
			get { return this._table; }
			set { this._table = value; }
		}

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
		/// Gets or sets the type of the office value.
		/// See class OfficeValueTypes for possible
		/// settings.
		/// </summary>
		/// <value>The type of the office value.</value>
		public string OfficeValueType
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@office:value-type",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@office:value-type",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("value-type", value, "office");
				this._node.SelectSingleNode("@office:value-type",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the office value.
		/// </summary>
		/// <value>The office value.</value>
		public string OfficeValue
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@office:value",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@office:value",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("value", value, "office");
				this._node.SelectSingleNode("@office:value",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or set a formula for this cell.
		/// </summary>
		/// <value>The formula.</value>
		public string Formula
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@table:formula",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@table:formula",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("formula", value, "table");
				this._node.SelectSingleNode("@table:formula",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Cell"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public Cell(IDocument document, XmlNode node)
		{
			this.Document			= document;
			this.Node				= node;
			this.InitStandards();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Cell"/> class.
		/// This will create an empty cell that use the default cell style
		/// </summary>
		/// <param name="table">The table.</param>
		public Cell(Table table)
		{
			this.Table				= table;
			this.Document			= table.Document;
			this.NewXmlNode(null);
			this.InitStandards();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Cell"/> class.
		/// Use this if you plan to add a custom style to this
		/// cell.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="styleName">Name of the style.</param>
		/// <param name="officeValueTyp">Name of the style.</param>
		public Cell(Table table, string styleName, string officeValueTyp)
		{
			this.Table				= table;
			this.Document			= table.Document;
			this.InitStandards();
			this.NewXmlNode(styleName);

			this.StyleName		= styleName;
			this.CellStyle		= new CellStyle(this.Document, styleName);
			this.Document.Styles.Add(this.CellStyle);

			if(officeValueTyp != null)
				this.OfficeValue	= officeValueTyp;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Cell"/> class.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="styleName">The style name.</param>
		public Cell(Table table, string styleName)
		{
			this.Table				= table;
			this.Document			= table.Document;
			this.NewXmlNode(null);
			this.InitStandards();

			if(styleName != null)
			{
				this.StyleName		= styleName;
				this.CellStyle		= new CellStyle(this.Document, styleName);
				this.Document.Styles.Add(this.CellStyle);
			}
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards()
		{
			this.Content			= new IContentCollection();
			this.Content.Inserted	+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(Content_Inserted);
			this.Content.Removed	+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(Content_Removed);
		}

		/// <summary>
		/// Create a new Xml node.
		/// </summary>
		/// <param name="styleName">Name of the style.</param>
		private void NewXmlNode(string styleName)
		{
			this.Node		= this.Document.CreateNode("table-cell", "table");
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
		/// <summary>
		/// Gets or sets the name of the style.
		/// </summary>
		/// <value>The name of the style.</value>
		public string StyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@table:style-name",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@table:style-name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("style-name", value, "table");
				this._node.SelectSingleNode("@table:style-name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		private IDocument _document;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his document.
		/// </summary>
		/// <value></value>
		public IDocument Document
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

		private IStyle _style;
		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this.StyleName	= value.StyleName;
				this._style = value;
			}
		}

		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node
		{
			get { return this._node; }
			set { this._node = value; }
		}

		#endregion

		#region IContentContainer Member

		private IContentCollection _content;
		/// <summary>
		/// Gets or sets the content.
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

		/// <summary>
		/// Content_s the removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IContent)value).Node);
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
				foreach(Cell cell in this.Row.CellCollection)
				{
					if(cell == this)
					{
						if(this.Row.Table.ColumnCollection != null)
							if(index <= this.Row.Table.ColumnCollection.Count)
							{
								Column column	= this.Row.Table.ColumnCollection[index];
								if(column != null)
									if(column.ColumnStyle.ColumnProperties.Width != null)
										return " width: "+column.ColumnStyle.ColumnProperties.Width.Replace(",",".")+"; ";

								
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

/*
 * $Log: Cell.cs,v $
 * Revision 1.3  2006/02/05 20:02:25  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */