/*
 * $Id: RowProperties.cs,v 1.1 2005/10/15 11:40:31 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Style.Properties
{
	/// <summary>
	/// Zusammenfassung für RowProperties.
	/// </summary>
	public class RowProperties : IProperty
	{
		private RowStyle _rowstyle;
		/// <summary>
		/// Gets or sets the row style.
		/// </summary>
		/// <value>The row style.</value>
		public RowStyle RowStyle
		{
			get { return this._rowstyle; }
			set { this._rowstyle = value; }
		}
		
		/// <summary>
		/// Gets or sets the color of the background.
		/// </summary>
		/// <value>The color of the background.</value>
		public string BackgroundColor
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:background-color",
					this.RowStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:background-color",
					this.RowStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("background-color", value, "fo");
				this._node.SelectSingleNode("@fo:background-color",
					this.RowStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RowProperties"/> class.
		/// </summary>
		/// <param name="rowstyle">The rowstyle.</param>
		public RowProperties(RowStyle rowstyle)
		{
			this.RowStyle			= rowstyle;
			this.NewXmlNode(rowstyle.Document);
		}

		/// <summary>
		/// Create the XmlNode which represent the propertie element.
		/// </summary>
		/// <param name="td">The TextDocument</param>
		private void NewXmlNode(TextDocument td)
		{
			this.Node		= td.CreateNode("table-row-properties", "style");
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.RowStyle.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		#region IProperty Member

		private XmlNode _node;
		/// <summary>
		/// The XmlNode which represent the property element.
		/// </summary>
		/// <value>The node</value>
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

		#endregion
	}
}

/*
 * $Log: RowProperties.cs,v $
 * Revision 1.1  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 */