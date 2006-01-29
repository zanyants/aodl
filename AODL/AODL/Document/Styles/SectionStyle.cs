/*
 * $Id: SectionStyle.cs,v 1.2 2006/01/29 18:52:51 larsbm Exp $
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
using AODL.Document.TextDocuments;
using AODL.Document.Content.Text;
using AODL.Document.Styles.Properties;
using AODL.Document.Content;

namespace AODL.Document.Styles
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

		/// <summary>
		/// Gets or sets the section properties.
		/// </summary>
		/// <value>The section properties.</value>
		public SectionProperties SectionProperties
		{
			get
			{
				foreach(IProperty property in this.PropertyCollection)
					if(property is SectionProperties)
						return (SectionProperties)property;
				SectionProperties sectionProperties	= new SectionProperties(this);
				this.PropertyCollection.Add((IProperty)sectionProperties);
				return sectionProperties;
			}
			set
			{
				if(this.PropertyCollection.Contains((IProperty)value))
					this.PropertyCollection.Remove((IProperty)value);
				this.PropertyCollection.Add(value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SectionStyle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public SectionStyle(IDocument document, XmlNode node)
		{
			this.Document			= document;
			this.Node				= node;
			this.InitStandards();
			this.SectionProperties	= new SectionProperties(this);
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
			this.NewXmlNode(styleName);
			this.InitStandards();
			this.SectionProperties	= new SectionProperties(this);
//			this.Document.Styles.Add(this);
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards()
		{
			this.PropertyCollection				= new IPropertyCollection();
			this.PropertyCollection.Inserted	+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(PropertyCollection_Inserted);
			this.PropertyCollection.Removed		+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(PropertyCollection_Removed);
			this.Document.Styles.Add(this);
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
		/// Properties the collection_ inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void PropertyCollection_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IProperty)value).Node);
		}

		/// <summary>
		/// Properties the collection_ removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void PropertyCollection_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IProperty)value).Node);
		}

		#region IStyle Member

		private XmlNode _node;
		/// <summary>
		/// The Xml node which represent the
		/// style
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
		/// The style name
		/// </summary>
		/// <value></value>
		public string StyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:name",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("name", value, "style");
				this._node.SelectSingleNode("@style:name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		private IDocument _document;
		/// <summary>
		/// The document to which this style
		/// belongs.
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

		private IPropertyCollection _propertyCollection;
		/// <summary>
		/// Collection of properties.
		/// </summary>
		/// <value></value>
		public IPropertyCollection PropertyCollection
		{
			get { return this._propertyCollection; }
			set { this._propertyCollection = value; }
		}
		#endregion
	}
}

/*
 * $Log: SectionStyle.cs,v $
 * Revision 1.2  2006/01/29 18:52:51  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.1  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 */