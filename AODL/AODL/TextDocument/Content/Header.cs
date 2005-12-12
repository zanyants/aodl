/*
 * $Id: Header.cs,v 1.1 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Zusammenfassung für Header.
	/// </summary>
	public class Header : IContent, IHtml
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
		/// <param name="simpletext">The simpletext.</param>
		public Header(TextDocument document, Headings heading, string simpletext)
		{
			this.Document		= document;
			this.NewXmlNode(document, this.GetHeading(heading));
			this.Init();
			this.TextContent.Add(new SimpleText(this, simpletext));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Header"/> class.
		/// </summary>
		/// <param name="headernode">The headernode.</param>
		/// <param name="document">The document.</param>
		internal Header(XmlNode headernode, TextDocument document)
		{
			this.Document		= document;
			this.Node			= headernode;
			this.Init();
		}

		/// <summary>
		/// Inits the standards for this instance.
		/// </summary>
		private void Init()
		{
			this.TextContent	= new ITextCollection();

			this.TextContent.Inserted	+=new AODL.Collections.CollectionWithEvents.CollectionChange(TextContent_Inserted);
			this.TextContent.Removed	+=new AODL.Collections.CollectionWithEvents.CollectionChange(TextContent_Removed);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		/// <param name="stylename">The stylename which should be referenced with this paragraph.</param>
		private void NewXmlNode(TextDocument td, string stylename)
		{			
			this.Node		= td.CreateNode("h", "text");
			XmlAttribute xa = td.CreateAttribute("style-name", "text");
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

		/// <summary>
		/// Gets the heading.
		/// </summary>
		/// <param name="heading">The heading.</param>
		/// <returns>The heading stylename</returns>
		private string GetHeading(Headings heading)
		{
			if(heading == Headings.Heading)
				return "Heading";
			else if(heading == Headings.Heading1)
				return "Heading_20_1";
			else if(heading == Headings.Heading2)
				return "Heading_20_2";
			else if(heading == Headings.Heading3)
				return "Heading_20_3";
			else if(heading == Headings.Heading4)
				return "Heading_20_4";
			else if(heading == Headings.Heading5)
				return "Heading_20_5";
			else if(heading == Headings.Heading6)
				return "Heading_20_6";
			else if(heading == Headings.Heading7)
				return "Heading_20_7";
			else if(heading == Headings.Heading8)
				return "Heading_20_8";
			else if(heading == Headings.Heading9)
				return "Heading_20_9";
			else if(heading == Headings.Heading10)
				return "Heading_20_10";
			else
				return "Heading";
		}

		#region IContent Member

		private TextDocument _document;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his TextDocument.
		/// </summary>
		/// <value></value>
		public TextDocument Document
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

		private XmlNode _node;
		/// <summary>
		/// Represents the XmlNode within the content.xml from the odt file.
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
		/// The style name.
		/// </summary>
		/// <value></value>
		public string Stylename
		{
			get
			{
				return  this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// A Heading is referenced with a fixed style.
		/// return null
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get
			{
				// TODO:  Getter-Implementierung für Header.Style hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für Header.Style hinzufügen
			}
		}

		private ITextCollection _textcontent;
		/// <summary>
		/// All Content objects have a Text container. Which represents
		/// his Text this could be SimpleText, FormatedText or mixed.
		/// </summary>
		/// <value></value>
		public ITextCollection TextContent
		{
			get
			{
				return this._textcontent;
			}
			set
			{
				this._textcontent = value;
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
			this.Node.InnerXml += ((IText)value).Xml;
		}

		/// <summary>
		/// Texts the content_ removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void TextContent_Removed(int index, object value)
		{
			string inner	= this.Node.InnerXml;
			string replace	= ((IText)value).Xml;
			inner			= inner.Replace(replace, "");
			this.Node.InnerXml	= inner;
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
				string html		= "<p ";
				string style	= this.GetHtmlStyle(this.Stylename);
				
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

				XmlNode stylenode		= this.Document.DocumentStyles.Styles.SelectSingleNode("//style:style[@style:name='"+headingname+"']",
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
			catch(Exception ex)
			{
				string exs = ex.Message;
			}

			return "";
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
		Heading1,
		/// <summary>
		/// Heading 2
		/// </summary>
		Heading2,
		/// <summary>
		/// Heading 3
		/// </summary>
		Heading3,
		/// <summary>
		/// Heading 4
		/// </summary>
		Heading4,
		/// <summary>
		/// Heading 5
		/// </summary>
		Heading5,
		/// <summary>
		/// Heading 6
		/// </summary>
		Heading6,
		/// <summary>
		/// Heading 7
		/// </summary>
		Heading7,
		/// <summary>
		/// Heading 8
		/// </summary>
		Heading8,
		/// <summary>
		/// Heading 9
		/// </summary>
		Heading9,
		/// <summary>
		/// Heading 10
		/// </summary>
		Heading10
	}
}

/*
 * $Log: Header.cs,v $
 * Revision 1.1  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 */