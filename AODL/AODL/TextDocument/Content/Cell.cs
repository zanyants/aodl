/*
 * $Id: Cell.cs,v 1.1 2005/10/15 11:40:31 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a Cell which used within a table resp. a row.
	/// </summary>
	public class Cell : IContent, IContentContainer
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
	}
}
