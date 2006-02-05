/*
 * $Id: Header.cs,v 1.3 2006/02/05 20:02:25 larsbm Exp $
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
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document;

namespace AODL.Document.Content.Text
{
	/// <summary>
	/// Header represent a header.
	/// </summary>
	public class Header : IContent, IHtml, ITextContainer, ICloneable
	{
		/// <summary>
		/// Gets or sets the out line level.
		/// e.g
		/// start header = "1"
		/// some paragraphs here
		/// subheader	 = "2"
		/// some paragraphs here
		/// start header = "1"
		/// will result in:
		/// 1. a header
		/// some text
		/// 1.1 a subheader
		/// some text
		/// 2. a header
		/// </summary>
		/// <value>The out line level.</value>
		public string OutLineLevel
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@text:outline-level",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@text:outline-level",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("outline-level", value, "text");
				this._node.SelectSingleNode("@text:outline-level",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Header"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="heading">The heading.</param>
		public Header(IDocument document, Headings heading)
		{
			this.Document		= document;
			this.NewXmlNode();
			this.StyleName		= this.GetHeading(heading);
			this.InitStandards();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Header"/> class.
		/// </summary>
		/// <param name="headernode">The headernode.</param>
		/// <param name="document">The document.</param>
		internal Header(XmlNode headernode, IDocument document)
		{
			this.Document				= document;
			this.Node					= headernode;
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
		/// Create a new XmlNode.
		/// </summary>
		private void NewXmlNode()
		{			
			this.Node		= this.Document.CreateNode("h", "text");
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

		/// <summary>
		/// Gets the heading.
		/// </summary>
		/// <param name="heading">The heading.</param>
		/// <returns>The heading stylename</returns>
		private string GetHeading(Headings heading)
		{
			if(heading == Headings.Heading)
				return "Heading";
			else if(heading == Headings.Heading_20_1)
				return "Heading_20_1";
			else if(heading == Headings.Heading_20_2)
				return "Heading_20_2";
			else if(heading == Headings.Heading_20_3)
				return "Heading_20_3";
			else if(heading == Headings.Heading_20_4)
				return "Heading_20_4";
			else if(heading == Headings.Heading_20_5)
				return "Heading_20_5";
			else if(heading == Headings.Heading_20_6)
				return "Heading_20_6";
			else if(heading == Headings.Heading_20_7)
				return "Heading_20_7";
			else if(heading == Headings.Heading_20_8)
				return "Heading_20_8";
			else if(heading == Headings.Heading_20_9)
				return "Heading_20_9";
			else if(heading == Headings.Heading_20_10)
				return "Heading_20_10";
			else
				return "Heading";
		}

		#region IContent Member
		/// <summary>
		/// Gets or sets the name of the style.
		/// </summary>
		/// <value>The name of the style.</value>
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

		private IDocument _document;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his document.
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
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
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

		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node
		{
			get { return this._node; }
			set { this._node = value; }
		}

		#endregion

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


		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			try
			{
				string html		= this.GetAnchor()+"<p ";
				string style	= this.GetHtmlStyle(this.StyleName);
				
				if(style.Length > 0)
					html		+= style;
				html			+= ">\n";

				//Check numbering
				string number	= this.GetHeadingNumber();
				if(number.Length > 0)
					html		+= number+"&nbsp;&nbsp;";

				foreach(IText itext in this.TextContent)
					if(itext is IHtml)
						html	+= ((IHtml)itext).GetHtml();

				html			+= "</p>\n";

				return html;
			}
			catch(Exception ex)
			{
			}

			return "";
		}

		/// <summary>
		/// Gets the HTML style.
		/// </summary>
		/// <param name="headingname">The headingname.</param>
		/// <returns>The css style element</returns>
		private string GetHtmlStyle(string headingname)
		{
			try
			{
				string style			= "style=\"margin-left: 0.5cm; margin-top: 0.5cm; margin-bottom: 0.5cm; ";
				string fontname			= "";
				string fontsize			= "";
				string bold				= "";
				string italic			= "";

				if(this.Document is AODL.Document.TextDocuments.TextDocument)
				{
					AODL.Document.TextDocuments.TextDocument doc = (AODL.Document.TextDocuments.TextDocument)this.Document;
					XmlNode stylenode		= doc.DocumentStyles.Styles.SelectSingleNode("//style:style[@style:name='"+headingname+"']",
						this.Document.NamespaceManager);
					XmlNode propertynode	= null;

					if(stylenode != null)
						propertynode		= stylenode.SelectSingleNode("style:text-properties", this.Document.NamespaceManager);

					if(propertynode != null)
					{
						XmlNode attribute	= propertynode.SelectSingleNode("@fo:font-name", this.Document.NamespaceManager);
						if(attribute != null)
							fontname		= "font-family:"+attribute.InnerText+"; ";
					
						attribute	= propertynode.SelectSingleNode("@fo:font-size", this.Document.NamespaceManager);
						if(attribute != null)
							fontsize		= "font-size:"+attribute.InnerText+"; ";
					
						if(propertynode.OuterXml.IndexOf("bold") != -1)
							bold			= "font-weight: bold; ";

						if(propertynode.OuterXml.IndexOf("italic") != -1)
							italic			= "font-style: italic; ";
					}
				
					if(fontname.Length > 0)
						style		+= fontname;
					if(fontsize.Length > 0)
						style		+= fontsize;
					if(bold.Length > 0)
						style		+= bold;
					if(italic.Length > 0)
						style		+= italic;

					if(style.EndsWith(" "))
						style		+= "\"";
					else
						style		= "";

					return style;
				}
			}
			catch(Exception ex)
			{
				string exs = ex.Message;
			}
		
			return "";
		}

		/// <summary>
		/// Gets the html anchor, if the document use a table of contents
		/// </summary>
		/// <returns></returns>
		private string GetAnchor()
		{
			string anchor		= "";

//			try
//			{
//				if(this.Document.TableofContentsCount > 0)
//				{
//					anchor		+= "<a name=\"";
//					anchor		+= this.Node.InnerText;
//					anchor		+= "\"> </a>\n";
//				}
//			}
//			catch(Exception ex)
//			{
//			}

			return anchor;
		}

		/// <summary>
		/// Gets the heading number, if used via Outline-Level.
		/// Support for outline numbering up to deepth of 6, yet.
		/// </summary>
		/// <returns>The number string.</returns>
		private string GetHeadingNumber()
		{
			try
			{
				int outline1		= 0;
				int outline2		= 0;
				int outline3		= 0;
				int outline4		= 0;
				int outline5		= 0;
				int outline6		= 0;

				foreach(IContent content in this.Document.Content)
					if(content is Header)
						if(((Header)content).OutLineLevel != null)
						{
							int no	= Convert.ToInt32(((Header)content).OutLineLevel);
							if(no == 1)
							{
								outline1++;
								outline2	= 0;
								outline3	= 0;
								outline4	= 0;
								outline5	= 0;
								outline6	= 0;
							}
							else if(no == 2)
								outline2++;
							else if(no == 3)
								outline3++;
							else if(no == 4)
								outline4++;
							else if(no == 5)
								outline5++;
							else if(no == 6)
								outline6++;

							if(content == this)
							{
								string sNumber		= outline1.ToString()+".";
								string sNumber1		= "";
								if(outline6 != 0)
									sNumber1		= "."+outline6.ToString()+".";
								if(outline5 != 0)
									sNumber1		= sNumber1+"."+outline5.ToString()+".";
								if(outline4 != 0)
									sNumber1		= sNumber1+"."+outline4.ToString()+".";
								if(outline3 != 0)
									sNumber1		= sNumber1+"."+outline3.ToString()+".";
								if(outline2 != 0)
									sNumber1		= sNumber1+"."+outline2.ToString()+".";
								
								sNumber				+= sNumber1;

								return sNumber.Replace("..",".");
							}
						}
			}
			catch(Exception ex)
			{
				//unhandled, only the numbering will maybe incorrect
			}
			return "";
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

		#region ICloneable Member

		/// <summary>
		/// Create a deep clone of this Header object.
		/// </summary>
		/// <remarks>A possible Attached Style wouldn't be cloned!</remarks>
		/// <returns>
		/// A clone of this object.
		/// </returns>
		public object Clone()
		{
			Header headerClone			= null;

			if(this.Document != null && this.Node != null)
			{
				MainContentProcessor mcp	= new MainContentProcessor(this.Document);
				headerClone					= mcp.CreateHeader(this.Node.CloneNode(true));
			}

			return headerClone;
		}

		#endregion
	}

	/// <summary>
	/// All possible Standard Headings
	/// </summary>
	public enum Headings
	{
		/// <summary>
		/// Standard Heading
		/// </summary>
		Heading,
		/// <summary>
		/// Heading 1
		/// </summary>
		Heading_20_1,
		/// <summary>
		/// Heading 2
		/// </summary>
		Heading_20_2,
		/// <summary>
		/// Heading 3
		/// </summary>
		Heading_20_3,
		/// <summary>
		/// Heading 4
		/// </summary>
		Heading_20_4,
		/// <summary>
		/// Heading 5
		/// </summary>
		Heading_20_5,
		/// <summary>
		/// Heading 6
		/// </summary>
		Heading_20_6,
		/// <summary>
		/// Heading 7
		/// </summary>
		Heading_20_7,
		/// <summary>
		/// Heading 8
		/// </summary>
		Heading_20_8,
		/// <summary>
		/// Heading 9
		/// </summary>
		Heading_20_9,
		/// <summary>
		/// Heading 10
		/// </summary>
		Heading_20_10
	}
}

/*
 * $Log: Header.cs,v $
 * Revision 1.3  2006/02/05 20:02:25  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.2  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.2  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 * Revision 1.1  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 */