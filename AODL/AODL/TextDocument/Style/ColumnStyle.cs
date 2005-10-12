/*
 * $Id: ColumnStyle.cs,v 1.1 2005/10/12 19:52:10 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style.Properties;

namespace AODL.TextDocument.Style
{
	/// <summary>
	/// The class ColumnStyle represent the style for a column.
	/// </summary>
	public class ColumnStyle : IStyle
	{
		private Column _column;
		/// <summary>
		/// The column to which this style belong
		/// </summary>
		public Column Column
		{
			get { return this._column; }
			set { this._column = value; }
		}

		private ColumnProperties _properties;
		/// <summary>
		/// The ColumnProperties which belong to this style.
		/// </summary>
		public ColumnProperties Properties
		{
			get { return this._properties; }
			set { this._properties = value; }
		}

		/// <summary>
		/// Constructor, create a new instance of Column.
		/// </summary>
		/// <param name="col">The Column.</param>
		/// <param name="stylename">The style name.</param>
		public ColumnStyle(Column col, string stylename)
		{
			this.Column			= col;
			this.Document		= col.Document;
			this.Properties		= new ColumnProperties(this);
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
			xa.Value		= "table-column";
			this.Node.Attributes.Append(xa);
		}

		#region IStyle Member

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
		/// The style name.
		/// </summary>
		public string Name
		{
			get
			{
				return  this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to which this style belong.
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
 * $Log: ColumnStyle.cs,v $
 * Revision 1.1  2005/10/12 19:52:10  larsbm
 * - start table implementation
 * - added uml diagramm
 *
 */