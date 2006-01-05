/*
 * $Id: SectionProperties.cs,v 1.1 2006/01/05 10:31:10 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Style.Properties
{
	/// <summary>
	/// Zusammenfassung für SectionProperties.
	/// </summary>
	public class SectionProperties : IProperty
	{
		private IStyle _style;
		/// <summary>
		/// Gets or sets the style.
		/// </summary>
		/// <value>The style.</value>
		public IStyle Style
		{
			get { return this._style; }
			set { this._style = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="SectionProperties"/> is editable.
		/// </summary>
		/// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
		public bool Editable
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:editable",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return Convert.ToBoolean(xn.InnerText);
				return false;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:editable",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("editable", value.ToString(), "style");
				this._node.SelectSingleNode("@style:editable",
					this.Style.Document.NamespaceManager).InnerText = value.ToString();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SectionProperties"/> class.
		/// </summary>
		/// <param name="style">The style.</param>
		public SectionProperties(IStyle style)
		{
			this.Style					= style;
			this.NewXmlNode();
		}

		/// <summary>
		/// Adds the standard column style.
		/// While creating new TableOfContent objects
		/// AODL will only support a TableOfContent
		/// which use the Header styles with outlining
		/// without table columns
		/// </summary>
		public void AddStandardColumnStyle()
		{
			XmlNode standardColStyle	= this.Style.Document.CreateNode("columns", "style");

			XmlAttribute xa				= this.Style.Document.CreateAttribute("column-count", "fo");
			xa.Value					= "0";
			standardColStyle.Attributes.Append(xa);

			xa							= this.Style.Document.CreateAttribute("column-gap", "fo");
			xa.Value					= "0cm";
			standardColStyle.Attributes.Append(xa);

			this.Node.AppendChild(standardColStyle);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		private void NewXmlNode()
		{			
			this.Node		= this.Style.Document.CreateNode("section-properties", "style");

			XmlAttribute xa = this.Style.Document.CreateAttribute("editable", "style");
			xa.Value		= "false";
			this.Node.Attributes.Append(xa);
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
		/// <summary>
		/// The XmlNode which represent the property element.
		/// </summary>
		/// <value></value>
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
 * $Log: SectionProperties.cs,v $
 * Revision 1.1  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 */