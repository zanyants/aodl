/*
 * $Id: CellStyle.cs,v 1.2 2005/10/22 15:52:10 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style.Properties;


namespace AODL.TextDocument.Style
{
	/// <summary>
	/// Represent the CellStyle which is used by a Cell.
	/// </summary>
	public class CellStyle : IStyle
	{
		private Cell _cell;
		/// <summary>
		/// Gets or sets the cell.
		/// </summary>
		/// <value>The cell.</value>
		public Cell Cell
		{
			get { return this._cell; }
			set { this._cell = value; }
		}

		private CellProperties _cellproperties;
		/// <summary>
		/// Gets or sets the cell properties.
		/// </summary>
		/// <value>The cell properties.</value>
		public CellProperties CellProperties
		{
			get { return this._cellproperties; }
			set { this._cellproperties = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CellStyle"/> class.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="stylename">The stylename.</param>
		public CellStyle(Cell cell, string stylename)
		{
			this.Cell			= cell;
			this.Document		= cell.Document;
			this.CellProperties	= new CellProperties(this);
			this.NewXmlNode(stylename);
			this.Node.AppendChild(this.CellProperties.Node);
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
			xa.Value		= FamiliyStyles.TableCell;
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
 * $Log: CellStyle.cs,v $
 * Revision 1.2  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.1  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 */