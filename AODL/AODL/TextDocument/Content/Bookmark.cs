/*
 * $Id: Bookmark.cs,v 1.2 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Xml;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a Bookmark.
	/// </summary>
	public class Bookmark : IText
	{
		
		/// <summary>
		/// Gets or sets the name of the bookmark.
		/// </summary>
		/// <value>The name of the bookmark.</value>
		public string BookmarkName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@text:name", 
					this.IContent.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@text:name",
					this.IContent.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("name", value, "text");
				this._node.SelectSingleNode("@text:name",
					this.IContent.Document.NamespaceManager).InnerText = value;
			}
		}

		private IContent _icontent;
		/// <summary>
		/// Gets or sets the paragraph.
		/// </summary>
		/// <value>The paragraph.</value>
		public IContent IContent
		{
			get { return this._icontent; }
			set { this._icontent = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Bookmark"/> class.
		/// </summary>
		/// <param name="content">The paragraph this Bookmark belong to.</param>
		/// <param name="type">The type end or start.</param>
		/// <param name="bookmarkname">The bookmark name.</param>
		public Bookmark(IContent content, BookmarkType type, string bookmarkname)
		{
			this.IContent		= content;
			this.NewXmlNode(content.Document, type, bookmarkname);
		}

		/// <summary>
		/// Create the XmlNode.
		/// </summary>
		/// <param name="document">The TextDocument.</param>
		/// <param name="type">The bookmark type.</param>
		/// <param name="bookmarkname">The bookmark name.</param>
		private void NewXmlNode(TextDocument document, BookmarkType type, string bookmarkname)
		{
			if(type == BookmarkType.Start)
				this.Node		= document.CreateNode("bookmark-start", "text");
			else if(type == BookmarkType.End)
				this.Node		= document.CreateNode("bookmark-end", "text");
			else
				this.Node		= document.CreateNode("bookmark", "text");

			XmlAttribute xa		= document.CreateAttribute("name", "text");
			xa.Value			= bookmarkname;

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
			XmlAttribute xa = this.Content.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		#region IText Member

		private XmlNode _node;
		/// <summary>
		/// Represent the xml node within the content.xml file of the open document.
		/// </summary>
		/// <value>The XmlNode</value>
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
		/// A Bookmark has no Text to display.
		/// It only mark a special textposition
		/// inside a paragraph.
		/// </summary>
		/// <value>null</value>
		public string Text
		{
			get
			{
				// TODO:  Getter-Implementierung für Bookmark.Text hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für Bookmark.Text hinzufügen
			}
		}

		/// <summary>
		/// A Bookmark has no Style to display.
		/// It only mark a special textposition
		/// inside a paragraph.
		/// </summary>
		/// <value>null</value>
		public AODL.TextDocument.Style.IStyle Style
		{
			get
			{
				// TODO:  Getter-Implementierung für Bookmark.Style hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für Bookmark.Style hinzufügen
			}
		}

		/// <summary>
		/// A Bookmark has no Style to display.
		/// It only mark a special textposition
		/// inside a paragraph.
		/// </summary>
		/// <value>null</value>
		public IContent Content
		{
			get
			{
				// TODO:  Getter-Implementierung für Bookmark.Content hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für Bookmark.Content hinzufügen
			}
		}

		/// <summary>
		/// Returns the OuterXml value of the XmlNode.
		/// </summary>
		/// <value></value>
		public string Xml
		{
			get
			{
				return this.Node.OuterXml;
			}
		}

		#endregion
	}

	/// <summary>
	/// Enum that represent the possible Bookmark types.
	/// </summary>
	public enum BookmarkType
	{
		/// <summary>
		/// A StartBookmark, requires also a EndBookmark!
		/// </summary>
		Start,
		/// <summary>
		/// Standard bookmark
		/// </summary>
		Standard,
		/// <summary>
		/// A EndBookmark
		/// </summary>
		End
	}
}

/*
 * $Log: Bookmark.cs,v $
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