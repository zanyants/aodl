/*
 * $Id: SectionStyle.cs,v 1.1 2006/01/05 10:31:10 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style.Properties;

namespace AODL.TextDocument.Style
{
	/// <summary>
	/// Zusammenfassung für SectionStyle.
	/// </summary>
	public class SectionStyle : IStyle
	{
		private IContent _content;
		/// <summary>
		/// Gets or sets the content to this
		/// section style belongs
		/// </summary>
		/// <value>The content.</value>
		public IContent Content
		{
			get { return this._content; }
			set { this._content = value; }
		}

		private SectionProperties _sectionProperties;
		/// <summary>
		/// Gets or sets the section properties.
		/// </summary>
		/// <value>The section properties.</value>
		public SectionProperties SectionProperties
		{
			get { return this._sectionProperties; }
			set { this._sectionProperties = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SectionStyle"/> class.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="styleName">Name of the style.</param>
		public SectionStyle(IContent content, string styleName)
		{
			this.Content			= content;
			this.Document			= content.Document;
			this.SectionProperties	= new SectionProperties(this);
			this.NewXmlNode(styleName);
			this.Node.AppendChild(this.SectionProperties.Node);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="stylename">The stylename which should be referenced with this table of contents.</param>
		private void NewXmlNode(string stylename)
		{			
			this.Node		= this.Document.CreateNode("style", "style");

			XmlAttribute xa = this.Document.CreateAttribute("name", "style");
			xa.Value		= stylename;
			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("family", "style");
			xa.Value		= "section";
			this.Node.Attributes.Append(xa);
		}

		#region IStyle Member

		private XmlNode _node;
		/// <summary>
		/// The XmlNode.
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
		public string Name
		{
			get
			{
				return  this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to this object belongs.
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

		#endregion
	}
}

/*
 * $Log: SectionStyle.cs,v $
 * Revision 1.1  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 */