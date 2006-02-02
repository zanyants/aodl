/*
 * $Id: FormatedText.cs,v 1.3 2006/02/02 21:55:59 larsbm Exp $
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
using AODL.Document.Styles.Properties;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Content;
using AODL.Document;

namespace AODL.Document.Content.Text
{
	/// <summary>
	/// Represent a formated Text e.g bold, italic, underline etc.
	/// </summary>
	public class FormatedText : IHtml, IText, ITextContainer, ICloneable
	{
		/// <summary>
		/// Gets or sets the text style.
		/// </summary>
		/// <value>The text style.</value>
		public TextStyle TextStyle
		{
			get { return (TextStyle)this.Style; }
			set { this.Style = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FormatedText"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public FormatedText(IDocument document, XmlNode node)
		{
			this.Document				= document;
			this.Node					= node;
			this.InitStandards();
		}

		/// <summary>
		/// Overloaded constructor.
		/// </summary>
		/// <param name="document">The content object to which the formated text belongs to.</param>
		/// <param name="name">The stylename which should be referenced with this FormatedText object.</param>
		/// <param name="text">The Displaytext.</param>
		public FormatedText(IDocument document, string name, string text)
		{
			this.Document				= document;
			this.NewXmlNode(name);
			this.InitStandards();

			this.Text					= text;
			this.Style					= (IStyle)new TextStyle(this.Document, name);
			this.Document.Styles.Add(this.Style);
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards()
		{
			this.TextContent			= new ITextCollection();
			this.TextContent.Inserted	+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(TextContent_Inserted);
			this.TextContent.Removed	+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(TextContent_Removed);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="stylename">The stylename which should be referenced with this FormatedText.</param>
		private void NewXmlNode( string stylename)
		{			
			this.Node		= this.Document.CreateNode("span", "text");
			XmlAttribute xa = this.Document.CreateAttribute("style-name", "text");
			xa.Value		= stylename;

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
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string style	= ((TextStyle)this.Style).TextProperties.GetHtmlStyle();
			string html		= "<span ";
			string text		= this.GetTextWithHtmlControl();

			if(style.Length > 0)
				html		= html + style + ">\n";
			else
				html		+= ">\n";

			if(text.Length > 0)
				html		+= text;

			html			+= "</span>\n";

			html			= this.GetSubOrSupStartTag()+html+this.GetSubOrSupEndTag();

			return html;
		}

		/// <summary>
		/// Gets the text with HTML controls
		/// as Tab as &nbsp; and line-break as br tag
		/// </summary>
		/// <returns>The string</returns>
		private string GetTextWithHtmlControl()
		{
			string text		= "";

			foreach(XmlNode node in this.Node)
			{
				if(node.LocalName == "tab")
					text	+= "&nbsp;&nbsp;&nbsp;";
				else if(node.LocalName == "line-break")
					text	+= "<br>";
//				else if(node.LocalName == "s")
//					text	+= WhiteSpace.GetWhiteSpaceHtml(node.OuterXml);
				else if(node.InnerText.Length > 0)
					text	+= node.InnerText;
			}

			return text;
		}

		/// <summary>
		/// Gets the sub or sup start tag.
		/// </summary>
		/// <returns></returns>
		private string GetSubOrSupStartTag()
		{
			if(((TextStyle)this.Style).TextProperties.Position != null)
				if(((TextStyle)this.Style).TextProperties.Position.Length > 0)
					if(((TextStyle)this.Style).TextProperties.Position.ToLower().StartsWith("sub"))
						return "<sub>";
					else
						return "<sup>";
			
			return "";
		}

		/// <summary>
		/// Gets the sub or sup end tag.
		/// </summary>
		/// <returns></returns>
		private string GetSubOrSupEndTag()
		{
			if(((TextStyle)this.Style).TextProperties.Position != null)
				if(((TextStyle)this.Style).TextProperties.Position.Length > 0)
					if(((TextStyle)this.Style).TextProperties.Position.ToLower().StartsWith("sub"))
						return "</sub>";
					else
						return "</sup>";

			return "";
		}

		#endregion

		#region IText Member

		private XmlNode _node;
		/// <summary>
		/// The node that represent the text content.
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

		/// <summary>
		/// Use this if use text without control character,
		/// otherwise use the the TextColllection TextContent. 
		/// </summary>
		/// <value></value>
		public string Text
		{
			get
			{
				return this.Node.InnerText;
			}
			set
			{
				this.Node.InnerText = value;
			}
		}

		private IDocument _document;
		/// <summary>
		/// The document to which this text content belongs to.
		/// </summary>
		/// <value></value>
		public IDocument Document
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

		private IStyle _style;
		/// <summary>
		/// The style which is referenced with this text object.
		/// This is null if no style is available.
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this.StyleName	= value.StyleName;
				this._style = value;
			}
		}

		/// <summary>
		/// The style name which is used for the referenced style.
		/// This is null is no  style is available.
		/// </summary>
		/// <value></value>
		public string StyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@text:style-name",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@text:style-name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("style-name", value, "text");
				this._node.SelectSingleNode("@text:style-name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		#endregion

		#region ITextContainer Member

		private ITextCollection _textContent;
		/// <summary>
		/// All Content objects have a Text container. Which represents
		/// his Text this could be SimpleText, FormatedText or mixed.
		/// </summary>
		/// <value></value>
		public ITextCollection TextContent
		{
			get
			{
				return this._textContent;
			}
			set
			{
				if(this._textContent != null)
					foreach(IText text in this._textContent)
						this.Node.RemoveChild(text.Node);

				this._textContent = value;
				
				if(this._textContent != null)
					foreach(IText text in this._textContent)
						this.Node.AppendChild(text.Node);
			}
		}

		#endregion

		/// <summary>
		/// Texts the content_ inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void TextContent_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IText)value).Node);
		}

		/// <summary>
		/// Texts the content_ removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void TextContent_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IText)value).Node);
		}

		#region ICloneable Member
		/// <summary>
		/// Create a deep clone of this FormatedText object.
		/// </summary>
		/// <remarks>A possible Attached Style wouldn't be cloned!</remarks>
		/// <returns>
		/// A clone of this object.
		/// </returns>
		public object Clone()
		{
			FormatedText formatedTextClone		= null;

			if(this.Document != null && this.Node != null)
			{
				TextContentProcessor tcp		= new TextContentProcessor();
				formatedTextClone				= tcp.CreateFormatedText(
					this.Document, this.Node.CloneNode(true));
			}

			return formatedTextClone;
		}

		#endregion
	}
}

/*
 * $Log: FormatedText.cs,v $
 * Revision 1.3  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.4  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.3  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */