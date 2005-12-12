
/*
 * $Id: TextProperties.cs,v 1.8 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Style.Properties
{
	/// <summary>
	/// Represent access to the possible attributes of of an text-propertie element.
	/// </summary>
	public class TextProperties : IProperty, IHtmlStyle
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
		/// Set text position, could be sub- ("sub 58%") or superscript ("super 58%")
		/// See TextPropertieHelper for possible settings
		/// </summary>
		public string Position
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:text-position",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:text-position",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("text-position", value, "style");
				this._node.SelectSingleNode("@style:text-position",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set text shadow e.g. "1pt 1pt"
		/// See TextPropertieHelper for possible settings
		/// </summary>
		public string Shadow
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:text-shadow",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:text-shadow",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("text-shadow", value, "style");
				this._node.SelectSingleNode("@style:text-shadow",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set text outline e.g. "true"
		/// </summary>
		public string Outline
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:text-outline",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:text-outline",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("text-outline", value, "style");
				this._node.SelectSingleNode("@style:text-outline",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set text to line through
		/// See LineStyles for possible settings
		/// </summary>
		public string TextLineThrough
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:text-line-through-style",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:text-line-through-style",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("text-line-through-style", value, "style");
				this._node.SelectSingleNode("@style:text-line-through-style",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set the font color
		/// Use Colors.GetColor(Color color) to set 
		/// on of the available .net colors
		/// </summary>
		public string FontColor
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:color",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:color",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("color", value, "fo");
				this._node.SelectSingleNode("@fo:color",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set the background color
		/// Use Colors.GetColor(Color color) to set 
		/// on of the available .net colors
		/// </summary>
		public string BackgroundColor
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:background-color",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:background-color",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("background-color", value, "fo");
				this._node.SelectSingleNode("@fo:background-color",
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
				this.Style.Document.FontList.Add(value);
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

		#region IHtmlStyle Member

		/// <summary>
		/// Get the css style fragement
		/// </summary>
		/// <returns>The css style attribute</returns>
		public string GetHtmlStyle()
		{
			string style		= "style=\"";

			if(this.Italic != null)
				style	+= "font-style: italic; ";
			if(this.Bold != null)
				style	+= "font-weight: "+this.Bold+"; ";
			if(this.Underline != null)
				style	+= "text-decoration: underline; ";
			if(this.TextLineThrough != null)
				style	+= "text-decoration: line-through; ";
			if(this.FontName != null)
				style	+= "font-family: "+FontFamilies.HtmlFont(this.FontName)+"; ";
			if(this.FontSize != null)
				style	+= "font-size: "+FontFamilies.PtToPx(this.FontSize)+"; ";
			if(this.FontColor != null)
				style	+= "color: "+this.FontColor+"; ";
			if(this.BackgroundColor != null)
				style	+= "background-color: "+this.BackgroundColor+"; ";

			if(!style.EndsWith("; "))
				style	= "";
			else
				style	+= "\"";

			return style;
		}

		#endregion
	}

	/// <summary>
	/// Propertie Helpers
	/// </summary>
	public class TextPropertieHelper
	{
		/// <summary>
		/// Subscript use within Postion
		/// </summary>
		public static readonly string Subscript		= "sub 58%";
		/// <summary>
		/// Superscript use within Postion
		/// </summary>
		public static readonly string Superscript	= "super 58%";
		/// <summary>
		/// Light shadow use within Shadow
		/// </summary>
		public static readonly string Shadowlight	= "1pt 1pt";
		/// <summary>
		/// Middle shadow use within Shadow
		/// </summary>
		public static readonly string Shadowmidlle	= "3pt 3pt";
		/// <summary>
		/// Heavy shadow use within Shadow
		/// </summary>
		public static readonly string Shadowheavy	= "6pt 6pt";
	}
}

/*
 * $Log: TextProperties.cs,v $
 * Revision 1.8  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.7  2005/11/23 19:18:17  larsbm
 * - New Textproperties
 * - New Paragraphproperties
 * - New Border Helper
 * - Textproprtie helper
 *
 * Revision 1.6  2005/10/23 16:47:48  larsbm
 * - Bugfix ListItem throws IStyleInterface not implemented exeption
 * - now. build the document after call saveto instead prepare the do at runtime
 * - add remove support for IText objects in the paragraph class
 *
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
