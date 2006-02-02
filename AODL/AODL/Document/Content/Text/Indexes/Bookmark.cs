/*
 * $Id: Bookmark.cs,v 1.2 2006/02/02 21:55:59 larsbm Exp $
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
using AODL.Document.Content.Text;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;

namespace AODL.Document.Content.Text.Indexes
{
	/// <summary>
	/// Represent a Bookmark.
	/// </summary>
	public class Bookmark : IText, ICloneable
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
					this.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@text:name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("name", value, "text");
				this._node.SelectSingleNode("@text:name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets the type of the bookmark.
		/// </summary>
		/// <value>The type of the bookmark.</value>
		public BookmarkType BookmarkType
		{
			get 
			{
				if(this.Node.Name == "text:bookmark-start")
					return BookmarkType.Start;
				else if(this.Node.Name == "text:bookmark-end")
					return BookmarkType.End;
				else
					return BookmarkType.Standard;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Bookmark"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="type">The type.</param>
		/// <param name="bookmarkname">The bookmarkname.</param>
		public Bookmark(IDocument document, BookmarkType type, string bookmarkname)
		{
			this.Document				= document;
			this.NewXmlNode(type, bookmarkname);
		}

		/// <summary>
		/// Create the XmlNode.
		/// </summary>
		/// <param name="type">The bookmark type.</param>
		/// <param name="bookmarkname">The bookmark name.</param>
		private void NewXmlNode(BookmarkType type, string bookmarkname)
		{
			if(type == BookmarkType.Start)
				this.Node		= this.Document.CreateNode("bookmark-start", "text");
			else if(type == BookmarkType.End)
				this.Node		= this.Document.CreateNode("bookmark-end", "text");
			else
				this.Node		= this.Document.CreateNode("bookmark", "text");

			XmlAttribute xa		= this.Document.CreateAttribute("name", "text");
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
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
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
				return this.BookmarkName;
			}
			set
			{
				this.BookmarkName	= value;
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

		/// <summary>
		/// Is null no style is available.
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get { return null; }
			set { }
		}

		/// <summary>
		/// No style name available
		/// </summary>
		/// <value></value>
		public string StyleName
		{
			get 
			{ 
				return null;
			}
			set
			{
			}
		}

		#endregion

		#region ICloneable Member
		/// <summary>
		/// Create a deep clone of this Bookmark object.
		/// </summary>
		/// <remarks>A possible Attached Style wouldn't be cloned!</remarks>
		/// <returns>
		/// A clone of this object.
		/// </returns>
		public object Clone()
		{
			Bookmark bookmarkClone			= null;

			if(this.Document != null && this.Node != null)
			{
				TextContentProcessor tcp	= new TextContentProcessor();
				bookmarkClone				= tcp.CreateBookmark(
					this.Document, this.Node.CloneNode(true), this.BookmarkType);
			}

			return bookmarkClone;
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
 * Revision 1.2  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
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