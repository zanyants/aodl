/*
 * $Id: RowStyle.cs,v 1.3 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style.Properties;

namespace AODL.TextDocument.Style
{
	/// <summary>
	/// Zusammenfassung für RowStyle.
	/// </summary>
	public class RowStyle : IStyle
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

		private RowProperties _rowproperties;
		/// <summary>
		/// Gets or sets the row properties.
		/// </summary>
		/// <value>The row properties.</value>
		public RowProperties RowProperties
		{
			get { return this._rowproperties; }
			set { this._rowproperties = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RowStyle"/> class.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="stylename">The stylename.</param>
		public RowStyle(Row row, string stylename)
		{
			this.Row			= row;
			this.Document		= row.Document;
			this.RowProperties	= new RowProperties(this);
			this.NewXmlNode(stylename);
			this.Node.AppendChild(this.RowProperties.Node);
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
			xa.Value		= FamiliyStyles.TableRow;
			this.Node.Attributes.Append(xa);
		}

		#region IStyle Member

		private XmlNode _node;
		/// <summary>
		/// The XmlNode.
		/// </summary>
		/// <value>The Node</value>
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
		/// The style name.
		/// </summary>
		/// <value></value>
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
		/// The TextDocument to this object belongs.
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

		#endregion
	}
}

/*
 * $Log: RowStyle.cs,v $
 * Revision 1.3  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.2  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.1  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 */