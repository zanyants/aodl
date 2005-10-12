/*
 * $Id: ColumnProperties.cs,v 1.1 2005/10/12 19:52:56 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Style.Properties
{
	/// <summary>
	/// Zusammenfassung für ColumnProperties.
	/// </summary>
	public class ColumnProperties : IProperty
	{
		private ColumnStyle _style;
		/// <summary>
		/// The style to this object belong.
		/// </summary>
		public ColumnStyle Style
		{
			get { return this._style; }
			set { this._style = value; }
		}

		/// <summary>
		/// Set the column width -> table = 16.99cm -> column = 8.49cm
		/// </summary>
		public string Width
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:column-width",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:column-width",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("column-width", value, "style");
				this._node.SelectSingleNode("@style:column-width",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set the column relative width
		/// </summary>
		public string RelativeWidth
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:rel-column-width",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:rel-column-width",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("rel-column-width", value, "style");
				this._node.SelectSingleNode("@style:rel-column-width",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// The Constructor, create new instance of ColumnProperties
		/// </summary>
		/// <param name="style">The ColumnStyle</param>
		public ColumnProperties(ColumnStyle style)
		{
			this.Style			= style;
			this.NewXmlNode(style.Document);
		}

		
		/// <summary>
		/// Create the XmlNode which represent the propertie element.
		/// </summary>
		/// <param name="td">The TextDocument</param>
		private void NewXmlNode(TextDocument td)
		{
			this.Node		= td.CreateNode("text-properties", "style");
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Style.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		#region IProperty Member

		private XmlNode _node;
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
 * $Log: ColumnProperties.cs,v $
 * Revision 1.1  2005/10/12 19:52:56  larsbm
 * - start table implementation
 * - added uml diagramm
 *
 */