/*
 * $Id: Frame.cs,v 1.1 2005/10/17 19:32:47 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Zusammenfassung für Frame.
	/// </summary>
	public class Frame : IContent
	{
		/// <summary>
		/// Gets or sets the name of the graphic.
		/// </summary>
		/// <value>The name of the graphic.</value>
		public string GraphicName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:name",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("name", value, "draw");
				this._node.SelectSingleNode("@draw:name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Frame"/> class.
		/// </summary>
		/// <param name="textdocument">The textdocument.</param>
		/// <param name="stylename">The stylename.</param>
		public Frame(TextDocument textdocument, string stylename)
		{
			this.Document		= textdocument;
			this.NewXmlNode(stylename);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Frame"/> class.
		/// </summary>
		/// <param name="textdocument">The textdocument.</param>
		/// <param name="stylename">The stylename.</param>
		/// <param name="graphicname">The graphicname.</param>
		/// <param name="graphicfile">The graphicfile.</param>
		public Frame(TextDocument textdocument, string stylename, string graphicname, string graphicfile)
		{
			this.Document		= textdocument;
			this.NewXmlNode(stylename);
			this.GraphicName	= graphicname;
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="stylename">The stylename which should be referenced with this frame.</param>
		private void NewXmlNode(string stylename)
		{			
			this.Node		= this.Document.CreateNode("frame", "draw");

			XmlAttribute xa = this.Document.CreateAttribute("style-name", "text");
			xa.Value		= stylename;

			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("anchor-type", "text");
			xa.Value		= "paragraph"; //TODO: static implementation

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

		#region IContent Member

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to which this column.
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

		public string Stylename
		{
			get
			{
				return this.Style.Name;
			}
			set
			{
				this.Style.Name = value;
				this._node.SelectSingleNode("@table:style-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// The ColumnStyle which is referenced with this column.
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
		/// Not implemented!!! The text is added to Cell's only!
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
	}
}

/*
 * $Log: Frame.cs,v $
 * Revision 1.1  2005/10/17 19:32:47  larsbm
 * - start vers. 1.0.3.0
 * - add frame, framestyle, graphic, graphicproperties
 *
 */