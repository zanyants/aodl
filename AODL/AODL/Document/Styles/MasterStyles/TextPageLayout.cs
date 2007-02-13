/*
 * $Id: TextPageLayout.cs,v 1.1 2007/02/13 17:58:55 larsbm Exp $
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
 */

using System;
using AODL.Document.Content;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;
using AODL.Document.Helper;
using System.Collections;
using System.Xml;

namespace AODL.Document.Styles.MasterStyles
{
	/// <summary>
	/// Summary for TextPageLayout.
	/// </summary>
	public class TextPageLayout
	{
		/// <summary>
		/// Gets or sets the width of the page.
		/// e.g. 20.99 cm (A4)
		/// </summary>
		/// <value>The width of the page.</value>
		public string PageWidth
		{
			get 
			{ 
				XmlNode xn = this._propertyNode.SelectSingleNode("@fo:page-width",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._propertyNode.SelectSingleNode("@fo:page-width",
					this.TextDocument.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("page-width", value, "fo");
				this._propertyNode.SelectSingleNode("@fo:page-width",
					this.TextDocument.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the height of the page.
		/// e.g 29,699cm (A4)
		/// </summary>
		/// <value>The height of the page.</value>
		public string PageHeight
		{
			get 
			{ 
				XmlNode xn = this._propertyNode.SelectSingleNode("@fo:page-height",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._propertyNode.SelectSingleNode("@fo:page-height",
					this.TextDocument.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("page-height", value, "fo");
				this._propertyNode.SelectSingleNode("@fo:page-height",
					this.TextDocument.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the print orientation.
		/// e.g portrait, landscape
		/// </summary>
		/// <value>The print orientation.</value>
		public string PrintOrientation
		{
			get 
			{ 
				XmlNode xn = this._propertyNode.SelectSingleNode("@style:print-orientation",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._propertyNode.SelectSingleNode("@style:print-orientation",
					this.TextDocument.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("print-orientation", value, "style");
				this._propertyNode.SelectSingleNode("@style:print-orientation",
					this.TextDocument.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the height of the footnote max. e.g. 0cm == without limit
		/// </summary>
		/// <value>The height of the footnote max.</value>
		public string FootnoteMaxHeight
		{
			get 
			{ 
				XmlNode xn = this._propertyNode.SelectSingleNode("@style:footnote-max-height",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._propertyNode.SelectSingleNode("@style:footnote-max-height",
					this.TextDocument.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("footnote-max-height", value, "style");
				this._propertyNode.SelectSingleNode("@style:footnote-max-height",
					this.TextDocument.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the writing mode. e.g. lr-tab
		/// </summary>
		/// <value>The writing mode.</value>
		public string WritingMode
		{
			get 
			{ 
				XmlNode xn = this._propertyNode.SelectSingleNode("@style:writing-mode",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._propertyNode.SelectSingleNode("@style:writing-mode",
					this.TextDocument.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("writing-mode", value, "style");
				this._propertyNode.SelectSingleNode("@style:writing-mode",
					this.TextDocument.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the num format. e.g. 1
		/// </summary>
		/// <value>The num format.</value>
		public string NumFormat
		{
			get 
			{ 
				XmlNode xn = this._propertyNode.SelectSingleNode("@style:num-format",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._propertyNode.SelectSingleNode("@style:num-format",
					this.TextDocument.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("num-format", value, "style");
				this._propertyNode.SelectSingleNode("@style:num-format",
					this.TextDocument.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the margin top.
		/// e.g. 2cm
		/// </summary>
		/// <value>The margin top.</value>
		public string MarginTop
		{
			get 
			{ 
				XmlAttribute xmlAttr = this._propertyNode.Attributes["margin-top", "fo"];
				if(xmlAttr != null)
					return xmlAttr.InnerText;
				return null;
			}
			set
			{
				XmlAttribute xmlAttr = this._propertyNode.Attributes["margin-top", "fo"];
				if(xmlAttr == null)
					this.CreateAttribute("margin-top", value, "fo");
			}
		}

		/// <summary>
		/// Gets or sets the margin bottom. e.g. 2cm
		/// </summary>
		/// <value>The margin bottom.</value>
		public string MarginBottom
		{
			get 
			{ 
				XmlAttribute xmlAttr = this._propertyNode.Attributes["margin-bottom", "fo"];
				if(xmlAttr != null)
					return xmlAttr.InnerText;
				return null;
			}
			set
			{
				XmlAttribute xmlAttr = this._propertyNode.Attributes["margin-bottom", "fo"];
				if(xmlAttr == null)
					this.CreateAttribute("margin-bottom", value, "fo");
			}
		}

		/// <summary>
		/// Gets or sets the margin left. e.g. 2cm
		/// </summary>
		/// <value>The margin left.</value>
		public string MarginLeft
		{
			get 
			{ 
				XmlAttribute xmlAttr = this._propertyNode.Attributes["margin-left", "fo"];
				if(xmlAttr != null)
					return xmlAttr.InnerText;
				return null;
			}
			set
			{
				XmlAttribute xmlAttr = this._propertyNode.Attributes["margin-left", "fo"];
				if(xmlAttr == null)
					this.CreateAttribute("margin-left", value, "fo");
			}
		}

		/// <summary>
		/// Gets or sets the margin right. e.g. 2cm
		/// </summary>
		/// <value>The margin right.</value>
		public string MarginRight
		{
			get 
			{ 
				XmlAttribute xmlAttr = this._propertyNode.Attributes["margin-right", "fo"];
				if(xmlAttr != null)
					return xmlAttr.InnerText;
				return null;
			}
			set
			{
				XmlAttribute xmlAttr = this._propertyNode.Attributes["margin-right", "fo"];
				if(xmlAttr == null)
					this.CreateAttribute("margin-right", value, "fo");
			}
		}

		private TextDocument _textDocument;
		/// <summary>
		/// Gets or sets the text document.
		/// </summary>
		/// <value>The text document.</value>
		public TextDocument TextDocument
		{
			get { return this._textDocument; }
			set { this._textDocument = value; }
		}
		
		private XmlNode _propertyNode;
		/// <summary>
		/// The XmlNode which represent the page layout property element.
		/// </summary>
		/// <value>The node</value>
		public System.Xml.XmlNode PropertyNode
		{
			get
			{
				return this._propertyNode;
			}
			set
			{
				this._propertyNode = value;
			}
		}

		private XmlNode _styleNode;
		/// <summary>
		/// The XmlNode which represent the page layout style element.
		/// </summary>
		/// <value>The node</value>
		public System.Xml.XmlNode StyleNode
		{
			get
			{
				return this._styleNode;
			}
			set
			{
				this._styleNode = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextPageLayout"/> class.
		/// </summary>
		public TextPageLayout()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextPageLayout"/> class.
		/// </summary>
		/// <param name="ownerDocument">The owner document.</param>
		/// <param name="styleNode">The style node.</param>
		/// <param name="propertyNode">The property node.</param>
		public TextPageLayout(TextDocument ownerDocument, XmlNode styleNode, XmlNode propertyNode)
		{
			this.TextDocument = ownerDocument;
			this.StyleNode = styleNode;
			this.PropertyNode = propertyNode;
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			string nuri = this.TextDocument.GetNamespaceUri(prefix);
			XmlAttribute xa = this.TextDocument.DocumentStyles.Styles.CreateAttribute(prefix, name, nuri);
			xa.Value		= text;
			this.PropertyNode.Attributes.Append(xa);
		}

		/// <summary>
		/// Gets the width of the content.
		/// </summary>
		/// <value>The width of the content area as double value without the left and right margins. 
		/// Notice, that you have to call GetLayoutMeasurement() to find out if 
		/// the the document use cm or inch.</value>
		/// <remarks>Will return 0 if the width couldn't be calculated.</remarks>
		public double GetContentWidth()
		{
			try
			{
				if(this.PageWidth != null 
					&& SizeConverter.GetDoubleFromAnOfficeSizeValue(this.PageWidth) > 0)
				{
					return SizeConverter.GetDoubleFromAnOfficeSizeValue(this.PageWidth)
						- SizeConverter.GetDoubleFromAnOfficeSizeValue(this.MarginLeft)
						- SizeConverter.GetDoubleFromAnOfficeSizeValue(this.MarginRight);
				}
				return 0;
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Gets the layout measurement.
		/// </summary>
		/// <returns>True if it's in cm and false if it's in in.</returns>
		public bool GetLayoutMeasurement()
		{
			try
			{
				if(this.PageWidth != null)
				{
					return this.PageWidth.EndsWith("cm");
				}
				return true;
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
