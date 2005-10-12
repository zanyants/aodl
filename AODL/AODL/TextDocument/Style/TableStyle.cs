/*
 * $Id: TableStyle.cs,v 1.1 2005/10/12 19:52:10 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style.Properties;

namespace AODL.TextDocument.Style
{
	/// <summary>
	/// The class TableStyle represent the style for a Table.
	/// </summary>
	public class TableStyle : IStyle
	{
		private Table _table;
		/// <summary>
		/// The Table object to this sytle belongs.
		/// </summary>
		public Table Table
		{
			get { return this._table; }
			set { this._table = value; }
		}

		private TableProperties _properties;
		/// <summary>
		/// The TableProperties object which belong to this style.
		/// </summary>
		public TableProperties Properties
		{
			get { return this._properties; }
			set { this._properties = value; }
		}

		/// <summary>
		/// Constructor, create a new instance.
		/// </summary>
		/// <param name="table">The Table object</param>
		/// <param name="stylename">The style name</param>
		public TableStyle(Table table, string stylename)
		{
			this.Table			= table;
			this.Document		= table.Document;
			this.Properties		= new TableProperties(this);
			this.NewXmlNode(stylename);
		}

		/// <summary>
		/// Create the XmlNode that represent this element.
		/// </summary>
		/// <param name="name">The style name.</param>
		private void NewXmlNode(string name)
		{			
			this.Node		= this.Document.CreateNode("style", "style");

			XmlAttribute xa = this.Document.CreateAttribute("name", "style");
			xa.Value		= name;
			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("family", "style");
			xa.Value		= FamiliyStyles.table.ToString();;
			this.Node.Attributes.Append(xa);
		}

		#region IStyle Member

		private XmlNode _node;
		/// <summary>
		/// The XmlNode
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
		/// The style name
		/// </summary>
		public string Name
		{
			get
			{
				return this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to this style belongs.
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

		#endregion
	}
}

/*
 * $Log: TableStyle.cs,v $
 * Revision 1.1  2005/10/12 19:52:10  larsbm
 * - start table implementation
 * - added uml diagramm
 *
 */