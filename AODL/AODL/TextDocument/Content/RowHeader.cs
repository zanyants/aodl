/*
 * $Id: RowHeader.cs,v 1.1 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Zusammenfassung für RowHeader.
	/// </summary>
	public class RowHeader : IContent, IHtml
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

		private RowCollection _rowCollection;
		/// <summary>
		/// Gets or sets the row collection.
		/// </summary>
		/// <value>The row collection.</value>
		public RowCollection RowCollection
		{
			get { return this._rowCollection; }
			set { this._rowCollection = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RowHeader"/> class.
		/// </summary>
		/// <param name="table">The table.</param>
		public RowHeader(Table table)
		{
			this.Table				= table;
			this.Document			= table.Document;
			this.RowCollection		= new RowCollection();
			this.NewXmlNode();

			this.RowCollection.Inserted	+=new AODL.Collections.CollectionWithEvents.CollectionChange(RowCollection_Inserted);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		private void NewXmlNode()
		{			
			this.Node		= this.Document.CreateNode("table-header-rows", "table");
		}

		#region IContent Member

		private TextDocument _document;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his TextDocument.
		/// </summary>
		/// <value>The TextDocument</value>
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
		/// <value>The XmlNode</value>
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
		/// A RowHeader doesn't have a style
		/// </summary>
		/// <value>null</value>
		public string Stylename
		{
			get
			{
				// TODO:  Getter-Implementierung für RowHeader.Stylename hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für RowHeader.Stylename hinzufügen
			}
		}

		/// <summary>
		/// A RowHeader doesn't have a Style
		/// </summary>
		/// <value>null</value>
		public IStyle Style
		{
			get
			{
				// TODO:  Getter-Implementierung für RowHeader.Style hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für RowHeader.Style hinzufügen
			}
		}

		/// <summary>
		/// RowHeader doesn't act as TextContent Container
		/// </summary>
		/// <value>null</value>
		public ITextCollection TextContent
		{
			get
			{
				// TODO:  Getter-Implementierung für RowHeader.TextContent hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für RowHeader.TextContent hinzufügen
			}
		}

		#endregion

		/// <summary>
		/// Rows the collection_ inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void RowCollection_Inserted(int index, object value)
		{
			this.Node.AppendChild(((Row)value).Node);
		}

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string html			= "";

			foreach(Row	row in this.RowCollection)
				html		+= row.GetHtml()+"\n";

			return this.HtmlCleaner(html);
		}

		/// <summary>
		/// Table row header cleaner, this is needed,
		/// because in OD, the style of the table header
		/// row is used for to and bottom margin, but
		/// some brother use this from the text inside
		/// the cells. Which result in to large height
		/// settings.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>The cleaned text</returns>
		private string HtmlCleaner(string text)
		{
			try
			{
				string pat = @"margin-top: \d\.\d\d\w\w;";
				string pat1 = @"margin-bottom: \d\.\d\d\w\w;";
				Regex r = new Regex(pat, RegexOptions.IgnoreCase);
				text = r.Replace(text, "");
				r = new Regex(pat1, RegexOptions.IgnoreCase);
				text = r.Replace(text, "");
			}
			catch(Exception ex)
			{
			}
			return text;
		}

		#endregion
	}
}

/*
 * $Log: RowHeader.cs,v $
 * Revision 1.1  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 */