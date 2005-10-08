/*
 * $Id: Paragraph.cs,v 1.3 2005/10/08 12:31:33 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a paragraph within a opendocument textdocument.
	/// </summary>
	public class Paragraph : IContent
	{

		/// <summary>
		/// Create a new Paragraph object.
		/// </summary>
		/// <param name="td">The Textdocument.</param>
		/// <param name="stylename">The stylename which should be referenced with this paragraph.</param>
		public Paragraph(TextDocument td, string stylename)
		{
			this.Init(td, stylename);
//			this.Document				= td;
//			if(stylename != "Standard")
//				this.Style					= (IStyle)new ParagraphStyle(this, stylename);
//			this.TextContent			= new ITextCollection();
//			this.NewXmlNode(td, stylename);
//
//			this.TextContent.Inserted	+=new AODL.Collections.CollectionWithEvents.CollectionChange(TextContent_Inserted);
		}

		/// <summary>
		/// Overloaded constructor.
		/// Use this to create a standard paragraph with the given text from
		/// string simpletext. Notice, the text will be styled as standard.
		/// You won't be able to style it bold, underline, etc. this will only
		/// occur if standard style attributes of the textdocument are set to
		/// this.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		/// <param name="style">The only accepted ParentStyle is Standard! All other styles will be ignored!</param>
		/// <param name="simpletext">The text which should be append within this paragraph.</param>
		public Paragraph(TextDocument td, ParentStyles style, string simpletext)
		{
			this.Init(td, ParentStyles.Standard.ToString());
			//Attach simple text withhin the paragraph
			this.TextContent.Add(new SimpleText(this, simpletext));
		}

		/// <summary>
		/// Create the Paragraph.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		/// <param name="stylename">The style name.</param>
		private void Init(TextDocument td, string stylename)
		{
			this.Document				= td;
			if(stylename != "Standard")
				this.Style					= (IStyle)new ParagraphStyle(this, stylename);
			this.TextContent			= new ITextCollection();
			this.NewXmlNode(td, stylename);

			this.TextContent.Inserted	+=new AODL.Collections.CollectionWithEvents.CollectionChange(TextContent_Inserted);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		/// <param name="stylename">The stylename which should be referenced with this paragraph.</param>
		private void NewXmlNode(TextDocument td, string stylename)
		{			
			this.Node		= td.CreateNode("p", "text");
			XmlAttribute xa = td.CreateAttribute("style-name", "text");
			xa.Value		= stylename;

			this.Node.Attributes.Append(xa);
		}

		#region IContent Member

		private TextDocument _document;
		/// <summary>
		/// The TextDocument.
		/// </summary>
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
		/// The XmlNode which represent this paragraph.
		/// </summary>
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

		private string _stylename;
		/// <summary>
		/// The stylename which is referenced with this paragraph.
		/// </summary>
		public string Stylename
		{
			get
			{
				return this._stylename;
			}
			set
			{
				this._stylename = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// The IStyle object which is referenced whith this paragraph.
		/// </summary>
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

		private ITextCollection _textcontent;
		/// <summary>
		/// Represent the textcontent of this paragraph.
		/// </summary>
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
		/// Append the xml from added IText object.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		private void TextContent_Inserted(int index, object value)
		{
			this.Node.InnerXml += ((IText)value).Xml;
		}
	}
}

/*
 * $Log: Paragraph.cs,v $
 * Revision 1.3  2005/10/08 12:31:33  larsbm
 * - better usabilty of paragraph handling
 * - create paragraphs with text and blank paragraphs with one line of code
 *
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */