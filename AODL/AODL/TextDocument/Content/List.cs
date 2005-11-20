/*
 * $Id: List.cs,v 1.2 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument;
using AODL.TextDocument.Style;
using AODL.TextDocument.Style.Properties;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a list which could be a numbered or bullet style list.
	/// </summary>
	public class List : IContent, IContentContainer
	{
		private ParagraphStyle _paragraphstyle;
		/// <summary>
		/// The ParagraphStyle to which this List belongs.
		/// There is only one ParagraphStyle per List and
		/// its ListItems.
		/// </summary>
		public ParagraphStyle ParagraphStyle
		{
			get { return this._paragraphstyle; }
			set { this._paragraphstyle = value; }
		}

		private IContentCollection _content;
		/// <summary>
		/// The ContentCollection. A List don't have
		/// the TextContentCollection, because the
		/// List acts as another ContentContainer.
		/// </summary>
		public IContentCollection Content
		{
			get { return this._content; }
			set { this._content = value; }
		}

		/// <summary>
		/// Create a new List object
		/// </summary>
		/// <param name="td">The TextDocument</param>
		/// <param name="stylename">The style name</param>
		/// <param name="typ">The list typ bullet, ..</param>
		/// <param name="paragraphstylename">The style name for the ParagraphStyle.</param>
		public List(TextDocument td, string stylename, ListStyles typ, string paragraphstylename)
		{
			this.Document						= td;
			this.Content						= new IContentCollection();
			this.Style							= (IStyle)new ListStyle(this, stylename);
			this.ParagraphStyle					= new ParagraphStyle(this, paragraphstylename);
			this.ParagraphStyle.ListStyleName	= stylename;
			((ListStyle)this.Style).AutomaticAddListLevelStyles(typ);

			this.NewXmlNode(td, stylename);

			this.Content.Inserted +=new AODL.Collections.CollectionWithEvents.CollectionChange(Content_Inserted);
		}

		/// <summary>
		/// Create a new List which is used to represent a inner list.
		/// </summary>
		/// <param name="td">The TextDocument</param>
		/// <param name="outerlist">The List to which this List belongs.</param>
		public List(TextDocument td, List outerlist)
		{
			this.Document		= td;
			this.ParagraphStyle	= outerlist.ParagraphStyle;			
			this.Content		= new IContentCollection();
			//Create an inner list node, don't need a style
			//use the parents list style
			this.NewXmlNode(td, null);

			this.Content.Inserted +=new AODL.Collections.CollectionWithEvents.CollectionChange(Content_Inserted);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		/// <param name="stylename">The stylename which should be referenced with this List.</param>
		private void NewXmlNode(TextDocument td, string stylename)
		{			
			this.Node		= td.CreateNode("list", "text");

			if(stylename != null)
			{
				XmlAttribute xa = td.CreateAttribute("style-name", "text");
				xa.Value		= stylename;

				this.Node.Attributes.Append(xa);
			}
		}

		#region IContent Member

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to this object belongs.
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
		/// The XmlNode.
		/// </summary>
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

		private string _stylename;
		/// <summary>
		/// The stylename.
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
		/// The style object which belongs to this list.
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

		/// <summary>
		/// The textcontentcollection is not supported !
		/// The list instead offer a ContentCollection
		/// of ListItems or inner Lists
		/// </summary>
		public ITextCollection TextContent
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

		private void Content_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IContent)value).Node);
		}
	}
}
