/*
 * $Id: ParagraphStyle.cs,v 1.5 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style.Properties;
using AODL.TextDocument;
using AODL.TextDocument.Content;

namespace AODL.TextDocument.Style
{
	/// <summary>
	/// Represent the style for a Paragraph object.
	/// </summary>
	public class ParagraphStyle : IStyle, IFamilyStyle
	{
//		private Paragraph _paragraph;
//		/// <summary>
//		/// The Paragraph object to this object belongs.
//		/// </summary>
//		public Paragraph Paragraph
//		{
//			get { return this._paragraph; }
//			set { this._paragraph = value; }
//		}

		private IContent _content;
		/// <summary>
		/// The IContent object to which this ParagraphStyle belongs.
		/// </summary>
		public IContent Content
		{
			get { return this._content; }
			set { this._content = value; }
		}

		private ParagraphProperties _properties;
		/// <summary>
		/// The IProperties object which is linked with this object.
		/// </summary>
		public ParagraphProperties Properties
		{
			get { return this._properties; }
			set { this._properties = value; }
		}

//		private string _parentStyle;
		/// <summary>
		/// The parent style of this object.
		/// </summary>
		public string ParentStyle
		{
			get
			{	
				return this._node.SelectSingleNode("@style:parent-style-name", 
					this.Content.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:parent-style-name", 
					this.Content.Document.NamespaceManager).InnerText = value.ToString();
			}
		}

		/// <summary>
		/// The parent style of this object.
		/// </summary>
		public string ListStyleName
		{
			get
			{	
				XmlNode xn = this._node.SelectSingleNode("@style:list-style-name", 
					this.Content.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:list-style-name", 
					this.Content.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("list-style-name", value, "style");
				this._node.SelectSingleNode("@style:list-style-name", 
					this.Content.Document.NamespaceManager).InnerText = value;
			}
		}


		private TextProperties _textproperties;
		/// <summary>
		/// The TextProperties object to this object belongs.
		/// </summary>
		/// <remarks>
		/// This is optional. Only if used, the Node will attached to the
		/// ParagraphStyle node.
		/// </remarks>
		public TextProperties Textproperties
		{
			get
			{
				if(this._textproperties == null)
				{
					this._textproperties	= new TextProperties(this);
					this.Node.AppendChild(this.Textproperties.Node);
				}
				return this._textproperties;
			}
			set { this._textproperties = value; }
		}

		/// <summary>
		/// Create a new ParagraphStyle object.
		/// </summary>
		/// <param name="c">The Paragraph object to this object belongs.</param>
		/// <param name="name">The style name.</param>
		public ParagraphStyle(IContent c, string name)
		{
			this.Content	= c;
			this.Document	= c.Document;
			this.Properties = new ParagraphProperties(this);
			this.NewXmlNode(c.Document, name);
			this.Node.AppendChild(this.Properties.Node);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ParagraphStyle"/> class.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="node">The node.</param>
		public ParagraphStyle(IContent content, XmlNode node)
		{
			this.Content	= content;
			this.Document	= content.Document;
			this.Node		= node;
		}

		/// <summary>
		/// Create the XmlNode that represent this element.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		/// <param name="name">The style name.</param>
		private void NewXmlNode(TextDocument td, string name)
		{			
			this.Node		= td.CreateNode("style", "style");
			XmlAttribute xa = td.CreateAttribute("name", "style");
			xa.Value		= name;
			this.Node.Attributes.Append(xa);
			xa				= td.CreateAttribute("family", "style");
			xa.Value		= FamiliyStyles.Paragraph;
			this.Node.Attributes.Append(xa);
			xa				= td.CreateAttribute("parent-style-name", "style");
			xa.Value		= ParentStyles.Standard.ToString();
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

		#region IStyle Member

		private XmlNode _node;
		/// <summary>
		/// The XmlNode
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

		/// <summary>
		/// The style name.
		/// </summary>
		public string Name
		{
			get
			{	
				return this._node.SelectSingleNode("@style:name", 
					this.Content.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:name", 
					this.Content.Document.NamespaceManager).InnerText = value.ToString();
			}
		}

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to this object belongs.
		/// </summary>
		public TextDocument Document
		{
			get { return this._document; }
			set { this._document = value; }
		}

		#endregion

		#region IFamilyStyle Member
		/// <summary>
		/// The family style.
		/// </summary>
		public string Family
		{
			get
			{	
				return this._node.SelectSingleNode("@style:family", 
					this.Content.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:family", 
					this.Content.Document.NamespaceManager).InnerText = value.ToString();
			}
		}

		#endregion
	}
}

/*
 * $Log: ParagraphStyle.cs,v $
 * Revision 1.5  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.4  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.3  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */