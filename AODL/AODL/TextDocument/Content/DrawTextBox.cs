/*
 * $Id: DrawTextBox.cs,v 1.1 2005/12/18 18:29:46 larsbm Exp $
 */

using System;
using System.Xml;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// DrawTextBox represent drawed textbox which
	/// could be used to host Illustrations.
	/// </summary>
	public class DrawTextBox : IContent, IHtml
	{
		private IContentCollection _contentCollection;
		/// <summary>
		/// Gets or sets the content collection.
		/// </summary>
		/// <value>The content collection.</value>
		public IContentCollection ContentCollection
		{
			get { return this._contentCollection; }
			set { this._contentCollection = value; }
		}

		/// <summary>
		/// Gets or sets the height of the min. e.g. 7.80cm
		/// </summary>
		/// <value>The height of the min.</value>
		public string MinHeight
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:min-height",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:min-height",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("min-height", value, "fo");
				this._node.SelectSingleNode("@fo:min-height",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the height of the max. e.g. 8.00cm
		/// </summary>
		/// <value>The height of the max.</value>
		public string MaxHeight
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:max-height",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:max-height",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("max-height", value, "fo");
				this._node.SelectSingleNode("@fo:max-height",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the width of the min. e.g. 8.00cm
		/// </summary>
		/// <value>The width of the min.</value>
		public string MinWidth
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:min-width",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:min-width",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("min-width", value, "fo");
				this._node.SelectSingleNode("@fo:min-width",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the width of the max. e.g. 8.00cm
		/// </summary>
		/// <value>The width of the max.</value>
		public string MaxWidth
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:max-width",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:max-widtht",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("max-width", value, "fo");
				this._node.SelectSingleNode("@fo:max-width",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DrawTextBox"/> class.
		/// </summary>
		/// <param name="content">The content.</param>
		public DrawTextBox(IContent content)
		{
			this.Document			= content.Document;
			this.ContentCollection	= new IContentCollection();
			this.NewXmlNode();

			this.ContentCollection.Inserted	+=new AODL.Collections.CollectionWithEvents.CollectionChange(ContentCollection_Inserted);
		}

		/// <summary>
		/// Create new XML node.
		/// </summary>
		private void NewXmlNode()
		{			
			this.Node		= this.Document.CreateNode("text-box", "draw");
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

		/// <summary>
		/// Contents the collection_ inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void ContentCollection_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IContent)value).Node);
		}

		#region IContent Member

		private TextDocument _document;
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
		/// Represents the XmlNode within the content.xml from the odt file.
		/// </summary>
		/// <value>The Xml node.</value>
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
		/// Return null. Not supported by this object.
		/// </summary>
		/// <value></value>
		public string Stylename
		{
			get
			{
				// TODO:  Getter-Implementierung für DrawTextBox.Stylename hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für DrawTextBox.Stylename hinzufügen
			}
		}

		/// <summary>
		/// Returns null not supported by this object.
		/// </summary>
		/// <value></value>
		public AODL.TextDocument.Style.IStyle Style
		{
			get
			{
				// TODO:  Getter-Implementierung für DrawTextBox.Style hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für DrawTextBox.Style hinzufügen
			}
		}

		/// <summary>
		/// Returns null. Not supported by this Object.
		/// </summary>
		/// <value></value>
		public ITextCollection TextContent
		{
			get
			{
				// TODO:  Getter-Implementierung für DrawTextBox.TextContent hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für DrawTextBox.TextContent hinzufügen
			}
		}

		#endregion

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string html			= "<p style=\"border-width:1px; border-style:solid; padding: 0.5cm;\">\n";

			foreach(IContent content in this.ContentCollection)
				if(content is IHtml)
					html		+= ((IHtml)content).GetHtml();

			html				+= "</p>\n";

			return html;
		}

		#endregion
	}
}

/*
 * $Log: DrawTextBox.cs,v $
 * Revision 1.1  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 */