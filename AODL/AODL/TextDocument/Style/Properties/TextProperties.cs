
/*
 * $Id: TextProperties.cs,v 1.5 2005/10/22 15:52:10 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Style.Properties
{
	/// <summary>
	/// Represent access to the possible attributes of of an text-propertie element.
	/// </summary>
	public class TextProperties : IProperty
	{
		private IStyle _style;
		/// <summary>
		/// The TextStyle object to this object belongs.
		/// </summary>
		public IStyle Style
		{
			get { return this._style; }
			set { this._style = value; }
		}

		/// <summary>
		/// Set font-weight bold object.Bold = "bold";
		/// </summary>
		public string Bold
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:font-weight",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:font-weight",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("font-weight", value, "fo");
				this._node.SelectSingleNode("@fo:font-weight",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set font-style italic object.Italic = "italic";
		/// </summary>
		public string Italic
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:font-style",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:font-style",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("font-style", value, "fo");
				this._node.SelectSingleNode("@fo:font-style",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set text-underline-style Underline object.Underline = "dotted";
		/// </summary>
		public string Underline
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:text-underline-style",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:text-underline-style",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("text-underline-style", value, "style");
				this._node.SelectSingleNode("@style:text-underline-style",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set text-underline-style Underline object.Underline = "dotted";
		/// </summary>
		public string UnderlineWidth
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:text-underline-width",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:text-underline-width",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("text-underline-width", value, "style");
				this._node.SelectSingleNode("@style:text-underline-width",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set text-underline-color - object.UnderlineColor = "font-color";
		/// </summary>
		public string UnderlineColor
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:text-underline-color",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:text-underline-color",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("text-underline-color", value, "style");
				this._node.SelectSingleNode("@style:text-underline-color",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set font-name you will find all available fonts in class FontFamilies
		/// </summary>
		public string FontName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:font-name",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				this.Style.Document.AddFont(value);
				XmlNode xn = this._node.SelectSingleNode("@style:font-name",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("font-name", value, "style");
				this._node.SelectSingleNode("@style:font-name",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set font-name - object.FontSize = "10pt";
		/// </summary>
		public string FontSize
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:font-size",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:font-size",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("font-size", value, "fo");
				this._node.SelectSingleNode("@fo:font-size",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Create a new TextProperties object that belongs to the given TextStyle object.
		/// </summary>
		/// <param name="style">The TextStyle object.</param>
		public TextProperties(IStyle style)
		{
			this.Style	= style;
			this.NewXmlNode(style.Document);
		}

		/// <summary>
		/// Use to set all possible underline styles. You can use this method
		/// instead to set all necessary properties by hand.
		/// </summary>
		/// <param name="style">The style. Sess enum LineStyles.</param>
		/// <param name="width">The width. Sess enum LineWidths.</param>
		/// <param name="color">The color. "font-color" or as hex #[0-9a-fA-F]{6}</param>
		public void SetUnderlineStyles(string style, string width, string color)
		{
			this.Underline		= style;
			this.UnderlineWidth	= width;
			this.UnderlineColor	= color;
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
		/// <summary>
		/// Represent the XmlNode for the propertie element.
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

		#endregion
	}
}

/*
 * $Log: TextProperties.cs,v $
 * Revision 1.5  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.4  2005/10/08 09:01:15  larsbm
 * --- uncommented ---
 *
 * Revision 1.3  2005/10/08 08:07:45  larsbm
 * - added cvs tags
 *
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */
