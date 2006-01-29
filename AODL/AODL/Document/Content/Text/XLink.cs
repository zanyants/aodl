/*
 * $Id: XLink.cs,v 1.1 2006/01/29 11:28:22 larsbm Exp $
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
using AODL.Document;
using AODL.Document.Styles;

namespace AODL.Document.Content.Text
{
	/// <summary>
	/// Represent a hyperlink, which could be 
	/// a web-, ftp- or telnet link
	/// </summary>
	public class XLink : IText, IHtml, ITextContainer
	{	
		/// <summary>
		/// Gets or sets the href. e.g http://www.sourceforge.net
		/// </summary>
		/// <value>The href.</value>
		public string Href
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:href", 
					this.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:href",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("href", value, "xlink");
				this._node.SelectSingleNode("@xlink:href",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the type of the X link.
		/// </summary>
		/// <value>The type of the X link.</value>
		public string XLinkType
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:type", 
					this.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:type",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("type", value, "xlink");
				this._node.SelectSingleNode("@xlink:type",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// This is not the display text! Gets or sets the office name 
		/// </summary>
		/// <value>The name of the office.</value>
		public string OfficeName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@office:name", 
					this.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@office:name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("name", value, "office");
				this._node.SelectSingleNode("@office:name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the target frame.
		/// e.g. _blank, _top
		/// </summary>
		/// <value>The name of the target frame.</value>
		public string TargetFrameName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@office:target-frame-name", 
					this.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@office:target-frame-name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("target-frame-name", value, "office");
				this._node.SelectSingleNode("@office:target-frame-name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the show.
		/// Standard value is <b>new</b>
		/// </summary>
		/// <value>The show.</value>
		public string Show
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:show", 
					this.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:show",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("show", value, "xlink");
				this._node.SelectSingleNode("@xlink:show",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XLink"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="href">The href.</param>
		/// <param name="name">The name.</param>
		public XLink(IDocument document, string href, string name)
		{
			this.Document			= document;
			this.NewXmlNode();
			this.InitStandards();

			this.Href				= href;
			this.TextContent.Add(new SimpleText(this.Document, name));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XLink"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		public XLink(IDocument document)
		{
			this.Document			= document;
			this.InitStandards();
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
		/// News the XML node.
		/// </summary>
		private void NewXmlNode()
		{
			this.Node		= this.Document.CreateNode("a", "text");
		}

		/// <summary>
		/// Creates the attribute.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="text">The text.</param>
		/// <param name="prefix">The prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// Append the xml from added IText object.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		private void TextContent_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IText)value).Node);

			if(((IText)value).Text != null)
			{
				try
				{
					if(this.Document is AODL.Document.TextDocuments.TextDocument)
					{
						string text		= ((IText)value).Text;
						this.Document.DocumentMetadata.CharacterCount	+= text.Length;
						string[] words	= text.Split(' ');
						this.Document.DocumentMetadata.WordCount		+= words.Length;
					}
				}
				catch(Exception ex)
				{
					//unhandled, only word and character count wouldn' be correct
				}
			}
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
		/// The text.
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
				this._style = value;
			}
		}

		private string _styleName;
		/// <summary>
		/// The style name which is used for the referenced style.
		/// This is null is no  style is available.
		/// </summary>
		/// <value></value>
		public string StyleName
		{
			get
			{
				return this._styleName;
			}
			set
			{
				this._styleName = value;
			}
		}

		#endregion

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string html		= "<a href=\"";

			if(this.Href != null)
				//html	+= this.Href+"\"";
				html	+= this.GetLink()+"\"";

			if(this.TargetFrameName != null)
				html	+= " target=\""+this.TargetFrameName+"\"";

			if(this.Href != null)
			{
				html	+= ">\n";
				html	+= this.Text;
				html	+= "</a>";
			}
			else
				html	= "";
				
			return html;
		}

		/// <summary>
		/// Gets the link.
		/// </summary>
		/// <returns></returns>
		private string GetLink()
		{
			string outline		= "|outline";
			string link			= this.Href;
//			if(this.Document.TableofContentsCount > 0
//				&& link.StartsWith("#") && link.EndsWith(outline))
//			{
//				link			= link.Replace(outline, "");
//				int lastWS		= link.IndexOf(" ");
//				if(lastWS != -1)
//					link		= link.Substring(lastWS);
//				foreach(AODL.Document.TextDocuments.Content.IContent content in this.Document.Content)
//					if(content is Header)
//						foreach(IText text in ((Header)content).TextContent)
//							if(text.Text.EndsWith(link))
//							{
//								return "#"+text.Text;
//							}
//			}
			return this.Href;
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
	}
}

/*
 * $Log: XLink.cs,v $
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.3  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 * Revision 1.2  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.1  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 */