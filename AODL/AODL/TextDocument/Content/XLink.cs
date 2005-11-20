/*
 * $Id: XLink.cs,v 1.1 2005/11/20 17:31:20 larsbm Exp $
 */
using System;
using System.Xml;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a hyperlink, which could be 
	/// a web-, ftp- or telnet link
	/// </summary>
	public class XLink : IText
	{
		private Paragraph _paragraph;
		/// <summary>
		/// Gets or sets the paragraph.
		/// </summary>
		/// <value>The paragraph.</value>
		public Paragraph Paragraph
		{
			get { return this._paragraph; }
			set { this._paragraph = value; }
		}

		/// <summary>
		/// Gets or sets the href. e.g http://www.sourceforge.net
		/// </summary>
		/// <value>The href.</value>
		public string Href
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:href", 
					this.Paragraph.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:href",
					this.Paragraph.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("href", value, "xlink");
				this._node.SelectSingleNode("@xlink:href",
					this.Paragraph.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the type of the X link.
		/// </summary>
		/// <value>The type of the X link.</value>
		public string XLinkType
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:type", 
					this.Paragraph.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:type",
					this.Paragraph.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("type", value, "xlink");
				this._node.SelectSingleNode("@xlink:type",
					this.Paragraph.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// This is not the display text! Gets or sets the office name 
		/// </summary>
		/// <value>The name of the office.</value>
		public string OfficeName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@office:name", 
					this.Paragraph.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@office:name",
					this.Paragraph.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("name", value, "office");
				this._node.SelectSingleNode("@office:name",
					this.Paragraph.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the target frame.
		/// e.g. _blank, _top
		/// </summary>
		/// <value>The name of the target frame.</value>
		public string TargetFrameName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@office:target-frame-name", 
					this.Paragraph.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@office:target-frame-name",
					this.Paragraph.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("target-frame-name", value, "office");
				this._node.SelectSingleNode("@office:target-frame-name",
					this.Paragraph.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the show.
		/// Standard value is <b>new</b>
		/// </summary>
		/// <value>The show.</value>
		public string Show
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:show", 
					this.Paragraph.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:show",
					this.Paragraph.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("show", value, "xlink");
				this._node.SelectSingleNode("@xlink:show",
					this.Paragraph.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XLink"/> class.
		/// </summary>
		/// <param name="paragraph">The paragraph.</param>
		/// <param name="href">The href.</param>
		/// <param name="name">The name.</param>
		public XLink(Paragraph paragraph, string href, string name)
		{
			this.Paragraph		= paragraph;
			this.NewXmlNode(paragraph.Document, href, name);
			this.Node.InnerText	= name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XLink"/> class.
		/// </summary>
		/// <param name="paragraph">The paragraph.</param>
		internal XLink(Paragraph paragraph)
		{
			this.Paragraph		= paragraph;
		}

		/// <summary>
		/// News the XML node.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="href">The href.</param>
		/// <param name="name">The name.</param>
		private void NewXmlNode(TextDocument document, string href, string name)
		{
			this.Node		= document.CreateNode("a", "text");

			XmlAttribute xa		= document.CreateAttribute("href", "xlink");
			xa.Value			= href;

			this.Node.Attributes.Append(xa);

			xa					= document.CreateAttribute("type", "xlink");
			xa.Value			= "simple"; //Fixed up to now

			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// Creates the attribute.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="text">The text.</param>
		/// <param name="prefix">The prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Paragraph.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		#region IText Member

		private XmlNode _node;
		/// <summary>
		/// Represent the xml node within the content.xml file of the open document.
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
		/// The Text that will be displayed.
		/// </summary>
		/// <value></value>
		public string Text
		{
			get
			{
				return this.Node.InnerText;
			}
			set
			{
				this.Node.InnerText	= value;
			}
		}

		/// <summary>
		/// A Xlink object doesn't have a style object.
		/// </summary>
		/// <value>null</value>
		public AODL.TextDocument.Style.IStyle Style
		{
			get
			{
				// TODO:  Getter-Implementierung für XLink.Style hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für XLink.Style hinzufügen
			}
		}

		/// <summary>
		/// A XLink object doesn't have a content container.
		/// </summary>
		/// <value>null</value>
		public IContent Content
		{
			get
			{
				// TODO:  Getter-Implementierung für XLink.Content hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für XLink.Content hinzufügen
			}
		}

		/// <summary>
		/// Returns the InnerXml value of the XmlNode.
		/// </summary>
		/// <value>Outer Xml of the node.</value>
		public string Xml
		{
			get
			{
				return this._node.OuterXml;
			}
		}

		#endregion
	}
}

/*
 * $Log: XLink.cs,v $
 * Revision 1.1  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 */