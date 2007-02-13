/*
 * $Id: ParagraphProperties.cs,v 1.4 2007/02/13 17:58:49 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, Lars Behrmann, lb@OpenDocument4all.com
 * 
 * Last changes:
 * 
 */

using System;
using System.Xml;
using AODL.Document.Styles;

namespace AODL.Document.Styles.Properties
{
	/// <summary>
	/// Represent access to the possible attributes of of an paragraph-propertie element.
	/// </summary>
	public class ParagraphProperties : IProperty, IHtmlStyle
	{
		/// <summary>
		/// The ParagraphStyle object to this object belongs.
		/// </summary>
		public ParagraphStyle Paragraphstyle
		{
			get { return (ParagraphStyle)this.Style; }
			set { this.Style = value; }
		}

		/// <summary>
		/// Margin left. in cm an object .MarginLeft = "1cm";
		/// </summary>
		public string MarginLeft
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:margin-left", 
					  this.Style.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:margin-left",
					  this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("margin-left", value, "fo");
				this._node.SelectSingleNode("@fo:margin-left",
					  this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the break before.
		/// e.g. set this to "page" if this paragraph
		/// should be start on the next page.
		/// </summary>
		/// <value>The break before.</value>
		public string BreakBefore
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:break-before", 
					this.Style.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:break-before",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("break-before", value, "fo");
				this._node.SelectSingleNode("@fo:break-before",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the break after.
		/// e.g. set this to "page" if after this paragraph
		/// should be added a new page.
		/// </summary>
		/// <value>The break before.</value>
		public string BreakAfter
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:break-after", 
					this.Style.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:break-after",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("break-after", value, "fo");
				this._node.SelectSingleNode("@fo:break-after",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the border.
		/// This could be e.g. 0.002cm solid #000000 (width, linestyle, color)
		/// or none
		/// </summary>
		/// <value>The border.</value>
		public string Border
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:border",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:border",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("border", value, "fo");
				this._node.SelectSingleNode("@fo:border",
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
		/// Gets or sets the padding. 
		/// Default 0.049cm 
		/// Use this in combination with the Border property
		/// </summary>
		/// <value>The padding.</value>
		public string Padding
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:padding",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:padding",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("padding", value, "fo");
				this._node.SelectSingleNode("@fo:padding",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the line height in %.
		/// 200% means double space
		/// </summary>
		/// <value>The line height.</value>
		public string LineSpacing
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:line-height",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:line-height",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("line-height", value, "fo");
				this._node.SelectSingleNode("@fo:line-height",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		private TabStopStyleCollection _tabstopstylecollection;
		/// <summary>
		/// Gets or sets the tab stop style collection.
		/// <b>Notice:</b> A TabStopStyleCollection will not work
		/// within a Standard Paragraph!
		/// </summary>
		/// <value>The tab stop style collection.</value>
		public TabStopStyleCollection TabStopStyleCollection
		{
			get { return this._tabstopstylecollection; }
			set 
			{ 
				if(this.Style.StyleName == "Standard")
					return;
				if(this._tabstopstylecollection != null)
				{
					//Remove node and reset the collection
					this.Node.RemoveChild(this._tabstopstylecollection.Node);
					this._tabstopstylecollection = null; 
				}
				
				this._tabstopstylecollection	= value;
				if(this.Node.SelectSingleNode("style:tab-stops", this.Style.Document.NamespaceManager) == null)
					this.Node.AppendChild(this._tabstopstylecollection.Node);
			}
		}

		/// <summary>
		/// Set paragraph alignment - object.Alignment = TextAlignments.right.ToString()
		/// </summary>
		public string Alignment
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:text-align",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:text-align",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("text-align", value, "fo");
				this._node.SelectSingleNode("@fo:text-align",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ParagraphProperties"/> class.
		/// </summary>
		/// <param name="style">The style.</param>
		public ParagraphProperties(IStyle style)
		{
			this.Style			= style;
			this.NewXmlNode();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ParagraphProperties"/> class.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="node">The node.</param>
		public ParagraphProperties(IStyle style, XmlNode node)
		{
			this.Style				= style;
			this.Node				= node;
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

		/// <summary>
		/// News the XML node.
		/// </summary>
		private void NewXmlNode()
		{
			this.Node		= this.Style.Document.CreateNode("paragraph-properties", "style");
		}

		#region IProperty Member
		private XmlNode _node;
		/// <summary>
		/// The XmlNode which represent the property element.
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

		private IStyle _style;
		/// <summary>
		/// The style object to which this property object belongs
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get { return this._style; }
			set { this._style = value; }
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

			if(this.Alignment != null)
				style	+= "text-align: "+this.Alignment+"; ";
			if(this.MarginLeft != null)
				style	+= "text-indent: "+this.MarginLeft+"; ";
			if(this.LineSpacing != null)
				style	+= "line-height: "+this.LineSpacing+"; ";
			if(this.Border != null && this.Padding == null)
				style	+= "border-width:1px; border-style:solid; padding: 0.5cm; ";
			if(this.Border != null && this.Padding != null)
				style	+= "border-width:1px; border-style:solid; padding:"+this.Padding+"; ";

			if(!style.EndsWith("; "))
				style	= "";
			else
				style	+= "\"";

			return style;
		}

		#endregion
	}

	/// <summary>
	/// Some helper constants for Paragraph properties
	/// </summary>
	public class ParagraphHelper
	{
		/// <summary>
		/// Line spacing 1.5 lines
		/// </summary>
		public static readonly string LineSpacing15	= "150%";
		/// <summary>
		/// Line spacing double
		/// </summary>
		public static readonly string LineDouble	= "200%";
		/// <summary>
		/// Line spacing three lines
		/// </summary>
		public static readonly string LineSpacing3	= "300%";
	}
}

/*
 * $Log: ParagraphProperties.cs,v $
 * Revision 1.4  2007/02/13 17:58:49  larsbm
 * - add first part of implementation of master style pages
 * - pdf exporter conversations for tables and images and added measurement helper
 *
 * Revision 1.3  2006/02/16 18:35:41  larsbm
 * - Add FrameBuilder class
 * - TextSequence implementation (Todo loading!)
 * - Free draing postioning via x and y coordinates
 * - Graphic will give access to it's full qualified path
 *   via the GraphicRealPath property
 * - Fixed Bug with CellSpan in Spreadsheetdocuments
 * - Fixed bug graphic of loaded files won't be deleted if they
 *   are removed from the content.
 * - Break-Before property for Paragraph properties for Page Break
 *
 * Revision 1.2  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.6  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.5  2005/11/23 19:18:17  larsbm
 * - New Textproperties
 * - New Paragraphproperties
 * - New Border Helper
 * - Textproprtie helper
 *
 * Revision 1.4  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.3  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */