/*
 * $Id: Column.cs,v 1.2 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// The class Column represent a column which is used within a table.
	/// </summary>
	public class Column : IContent
	{
		private Table _table;
		/// <summary>
		/// The table to this column belong.
		/// </summary>
		public Table Table
		{
			get { return this._table; }
			set { this._table = value; }
		}		

		/// <summary>
		/// Constructor, create a new instance of Column.
		/// </summary>
		/// <param name="table">The Table.</param>
		/// <param name="stylename">The style name.</param>
		public Column(Table table, string stylename)
		{
			this.Table			= table;
			this.Document		= table.Document;
			this.Style			= (ColumnStyle)new ColumnStyle(this, stylename);
			this.NewXmlNode(stylename);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="stylename">The stylename which should be referenced with this table.</param>
		private void NewXmlNode(string stylename)
		{			
			this.Node		= this.Document.CreateNode("table-column", "table");
			XmlAttribute xa = this.Document.CreateAttribute("style-name", "table");
			xa.Value		= stylename;

			this.Node.Attributes.Append(xa);
		}


		#region IContent Member

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to which this column.
		/// </summary>
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
		/// The XmlNode.
		/// </summary>
		public XmlNode Node
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
		/// The ColumnStyle which is referenced with this column.
		/// </summary>
		public IStyle Style
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
		/// Not implemented!!! The text is added to Cell's only!
		/// </summary>
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
	}
}

/*
 * $Log: Column.cs,v $
 * Revision 1.2  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.1  2005/10/12 19:52:10  larsbm
 * - start table implementation
 * - added uml diagramm
 *
 */