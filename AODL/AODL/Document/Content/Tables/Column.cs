/*
 * $Id: Column.cs,v 1.1 2006/01/29 11:28:22 larsbm Exp $
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
using AODL.Document.SpreadsheetDocuments;

namespace AODL.Document.Content.Tables
{
	/// <summary>
	/// Zusammenfassung für Column.
	/// </summary>
	public class Column : IContent
	{
		/// <summary>
		/// Gets or sets the name of the parent cell style.
		/// </summary>
		/// <value>The name of the parent cell style.</value>
		public string ParentCellStyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@table:default-cell-style-name",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@table:default-cell-style-name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("default-cell-style-name", value, "table");
				this._node.SelectSingleNode("@table:default-cell-style-name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		private Table _table;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public Table Table
		{
			get { return this._table; }
			set { this._table = value; }
		}

		/// <summary>
		/// Gets or sets the column style.
		/// </summary>
		/// <value>The column style.</value>
		public ColumnStyle ColumnStyle
		{
			get { return (ColumnStyle)this.Style; }
			set 
			{ 
				this.StyleName		= ((ColumnStyle)value).StyleName;
				this.Style			= value; 
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Column"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public Column(IDocument document, XmlNode node)
		{
			this.Document			= document;
			this.Node				= node;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Column"/> class.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="styleName">Name of the style.</param>
		public Column(Table table, string styleName)
		{
			this.Table				= table;
			this.Document			= table.Document;
			this.NewXmlNode(styleName);
			this.ColumnStyle		= new ColumnStyle(this.Document, styleName);			
		}

		/// <summary>
		/// Create a new Xml node.
		/// </summary>
		/// <param name="styleName">Name of the style.</param>
		private void NewXmlNode(string styleName)
		{
			this.Node		= this.Document.CreateNode("table-column", "table");

			XmlAttribute xa = this.Document.CreateAttribute("style-name", "table");
			xa.Value		= styleName;
			this.Node.Attributes.Append(xa);

			if(this.Document is SpreadsheetDocument)
				this.ParentCellStyleName = "Default";
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
	}
}

/*
 * $Log: Column.cs,v $
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */