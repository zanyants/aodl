/*
 * $Id: Paragraph.cs,v 1.2 2005/10/08 08:19:25 larsbm Exp $
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
			this.Document				= td;
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
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */