/*
 * $Id: Footnote.cs,v 1.2 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Xml;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a Footnote which could be 
	/// a Foot- or a Endnote
	/// </summary>
	public class Footnote : IText
	{
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		public string Id
		{
			get 
			{
				XmlNode xn = this._node.SelectSingleNode("//@text:note-citation", 
					this.Content.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{
				XmlNode xn = this._node.SelectSingleNode("//@text:note-citation",
					this.Content.Document.NamespaceManager);
				if(xn != null)
				{
					this._node.SelectSingleNode("//@text:note-citation",
						this.Content.Document.NamespaceManager).InnerText = value;
					this._node.SelectSingleNode("//@text:id",
						this.Content.Document.NamespaceManager).InnerText = "ftn"+value;
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Footnote"/> class.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="notetext">The notetext.</param>
		/// <param name="id">The id.</param>
		/// <param name="type">The type.</param>
		public Footnote(IContent content, string notetext, string id, FootnoteType type)
		{
			this.NewXmlNode(content.Document, id, notetext, type);

			this.Content		= content;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Footnote"/> class.
		/// </summary>
		/// <param name="content">The content.</param>
		internal Footnote(IContent content)
		{
			this.Content		= content;
		}

		/// <summary>
		/// News the XML node.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		/// <param name="id">The id.</param>
		/// <param name="notetext">The notetext.</param>
		/// <param name="type">The type.</param>
		private void NewXmlNode(TextDocument td, string id, string notetext, FootnoteType type)
		{			
			this.Node		= td.CreateNode("note", "text");
			
			XmlAttribute xa = td.CreateAttribute("id", "text");
			xa.Value		= "ftn"+id;
			this.Node.Attributes.Append(xa);

			xa				 = td.CreateAttribute("note-class", "text");
			xa.Value		= type.ToString();
			this.Node.Attributes.Append(xa);

			//Node citation
			XmlNode node	 = td.CreateNode("not-citation", "text");
			node.InnerText	 = id;

			this._node.AppendChild(node);

			//Node Footnode body
			XmlNode nodebody = td.CreateNode("note-body", "text");

			//Node Footnode text
			node			 = td.CreateNode("p", "text");
			node.InnerXml	 = TextContentSpecialCharacter.ReplaceSpecialCharacter(notetext);

			xa				 = td.CreateAttribute("style-name", "text");
			xa.Value		 = (type == FootnoteType.footnode)?"Footnote":"Endnote";
			node.Attributes.Append(xa);

			nodebody.AppendChild(node);

			this._node.AppendChild(nodebody);
		}

		#region IText Member

		private XmlNode _node;
		/// <summary>
		/// Represent the xml node within the content.xml file of the open document.
		/// </summary>
		/// <value>The node</value>
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
		/// The Text that will be displayed.
		/// </summary>
		/// <value>The Text</value>
		public string Text
		{
			get 
			{
				XmlNode xn = this._node.SelectSingleNode("//@text:p", 
					this.Content.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{
				XmlNode xn = this._node.SelectSingleNode("//@text:p",
					this.Content.Document.NamespaceManager);
				if(xn != null)
				{
					this._node.SelectSingleNode("//@text:p",
						this.Content.Document.NamespaceManager).InnerText = value;
				}
			}
		}

		/// <summary>
		/// An Footnode has no local style. 
		/// </summary>
		/// <value>null</value>
		public AODL.TextDocument.Style.IStyle Style
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		private IContent _content;
		/// <summary>
		/// A IText object must know his IContent object.
		/// </summary>
		/// <value></value>
		public IContent Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
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
				return this._node.OuterXml;
			}
		}

		#endregion
	}

	/// <summary>
	/// Represent the possible footnodes
	/// </summary>
	public enum FootnoteType
	{
		/// <summary>
		/// A footnode
		/// </summary>
		footnode,
		/// <summary>
		/// A endnote
		/// </summary>
		endnote
	}
}

/*
 * $LoG$
 */