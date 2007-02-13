/*
 * $Id: TextPageHeaderFooterBase.cs,v 1.1 2007/02/13 17:58:55 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, 2007, Lars Behrmann, lb@OpenDocument4all.com
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
	/// Summary for TextPageHeaderFooterBase.
	/// </summary>
	public class TextPageHeaderFooterBase
	{
		private TextMasterPage _textMasterPage;
		/// <summary>
		/// Gets or sets the owner text master page.
		/// </summary>
		/// <value>The text master page.</value>
		public TextMasterPage TextMasterPage
		{
			get { return this._textMasterPage; }
			set { this._textMasterPage = value; }
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
		/// Gets or sets the property node.
		/// </summary>
		/// <value>The property node.</value>
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
		
		private XmlNode _contentNode;
		/// <summary>
		/// Gets or sets the content node.
		/// </summary>
		/// <value>The content node.</value>
		public System.Xml.XmlNode ContentNode
		{
			get
			{
				return this._contentNode;
			}
			set
			{
				this._contentNode = value;
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
		/// Gets or sets the min height. e.g. 0cm 
		/// </summary>
		/// <value>The height of the min.</value>
		public string MinHeight
		{
			get 
			{ 
				XmlAttribute xmlAttr = this._propertyNode.Attributes["min-height", "fo"];
				if(xmlAttr != null)
					return xmlAttr.InnerText;
				return null;
			}
			set
			{
				XmlAttribute xmlAttr = this._propertyNode.Attributes["min-height", "fo"];
				if(xmlAttr == null)
					this.CreateAttribute("min-height", value, "fo");
			}
		}

		/// <summary>
		/// Gets or sets the margin right. e.g. 0cm
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

		/// <summary>
		/// Gets or sets the margin bottom. e.g. 0.499cm
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
		/// Gets or sets the margin top. e.g. 0.499cm
		/// </summary>
		/// <value>The margin bottom.</value>
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
		/// Gets or sets the margin left. e.g. 0cm
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

		private IContentCollection _contentCollection;
		/// <summary>
		/// Gets or sets the content collection.
		/// </summary>
		/// <value>The content collection.</value>
		/// <remarks>This is the content which will be displayed within the footer or the header.</remarks>
		public IContentCollection ContentCollection
		{
			get { return this._contentCollection; }
			set { this._contentCollection = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextPageFooter"/> class.
		/// </summary>
		protected TextPageHeaderFooterBase()
		{
			this._contentCollection = new IContentCollection();
		}

		/// <summary>
		/// Create a new TextPageFooter object.
		/// !!NOTICE: The TextPageLayout of the TextMasterPage object must exist!
		/// </summary>
		/// <param name="textMasterPage">The text master page.</param>
		/// <param name="typeName">Name of the type to create header or footer.</param>
		/// <remarks>The TextPageLayout of the TextMasterPage object must exist!</remarks>
		protected void New(TextMasterPage textMasterPage, string typeName)
		{
			try
			{
				this._textMasterPage = textMasterPage;
				this._textDocument = textMasterPage.TextDocument;
				this._styleNode = this.TextDocument.DocumentStyles.CreateNode(
					"footer-style", "style", this.TextDocument);
				this._textMasterPage.TextPageLayout.StyleNode.AppendChild(this._styleNode);
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// This method call will activate resp. create the header or footer 
		/// for a master page. There is no need of call it directly so its
		/// internal. Activation for footer and header will be done through
		/// the TextMasterPage object. 
		/// </summary>
		internal void Activate()
		{
			string typeName = (this is TextPageHeader) ? "header" : "footer";

			// only if the content node doesn't exist
			if(this._contentNode == null)
			{
				this._contentNode = this.TextDocument.DocumentStyles.CreateNode(
					typeName, "style", this.TextDocument);
				this._textMasterPage.Node.AppendChild(this._contentNode);
			}

			// only if the property node doesn't exist
			if(this._propertyNode == null)
			{
				this._propertyNode = this.TextDocument.DocumentStyles.CreateNode(
					"header-footer-properties", "style", this.TextDocument);
				// Set defaults
				this.MarginLeft = "0cm";
				this.MarginRight = "0cm";
				this.MinHeight = "0cm";
				this.MarginBottom = (typeName.Equals("header")) ? "0.499cm" : "0cm";
				this.MarginTop = (typeName.Equals("footer")) ? "0.499cm" : "0cm";
				this._styleNode.AppendChild(this._propertyNode);
			}
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
	}
}
