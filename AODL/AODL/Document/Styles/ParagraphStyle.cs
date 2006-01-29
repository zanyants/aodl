/*
 * $Id: ParagraphStyle.cs,v 1.2 2006/01/29 18:52:51 larsbm Exp $
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
using AODL.Document.Styles.Properties;
using AODL.Document;
using AODL.Document.Content;

namespace AODL.Document.Styles
{
	/// <summary>
	/// Represent the style for a Paragraph object.
	/// </summary>
	public class ParagraphStyle : IStyle 
	{
		/// <summary>
		/// Gets or sets the paragraph properties.
		/// </summary>
		/// <value>The paragraph properties.</value>
		public ParagraphProperties ParagraphProperties
		{
			get
			{
				foreach(IProperty property in this.PropertyCollection)
					if(property is ParagraphProperties)
						return (ParagraphProperties)property;
				ParagraphProperties parProperties	= new ParagraphProperties(this);
				this.PropertyCollection.Add((IProperty)parProperties);
				return parProperties;
			}
			set
			{
				if(this.PropertyCollection.Contains((IProperty)value))
					this.PropertyCollection.Remove((IProperty)value);
				this.PropertyCollection.Add(value);
			}
		}

		/// <summary>
		/// Gets or sets the text properties.
		/// </summary>
		/// <value>The text properties.</value>
		public TextProperties TextProperties
		{
			get
			{
				foreach(IProperty property in this.PropertyCollection)
					if(property is TextProperties)
						return (TextProperties)property;
				TextProperties textProperties	= new TextProperties(this);
				this.PropertyCollection.Add((IProperty)textProperties);
				return textProperties;
			}
			set
			{
				if(this.PropertyCollection.Contains((IProperty)value))
					this.PropertyCollection.Remove((IProperty)value);
				this.PropertyCollection.Add(value);
			}
		}

		/// <summary>
		/// The parent style of this object.
		/// </summary>
		public string ParentStyle
		{
			get
			{	
				XmlNode xn	= this._node.SelectSingleNode("@style:parent-style-name", 
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:parent-style-name", 
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("parent-style-name", value, "style");
				this._node.SelectSingleNode("@style:parent-style-name", 
					this.Document.NamespaceManager).InnerText = value;
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
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:list-style-name", 
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("list-style-name", value, "style");
				this._node.SelectSingleNode("@style:list-style-name", 
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ParagraphStyle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="styleName">Name of the style.</param>
		public ParagraphStyle(IDocument document, string styleName)
		{
			this.Document	= document;
			this.NewXmlNode(styleName);
			this.InitStandards();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ParagraphStyle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public ParagraphStyle(IDocument document, XmlNode node)
		{
			this.Document	= document;
			this.Node		= node;
			this.InitStandards();
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards()
		{
			this.PropertyCollection				= new IPropertyCollection();
			this.PropertyCollection.Inserted	+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(PropertyCollection_Inserted);
			this.PropertyCollection.Removed		+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(PropertyCollection_Removed);
//			this.Document.Styles.Add(this);
		}

		/// <summary>
		/// Create the XmlNode that represent this element.
		/// </summary>
		/// <param name="name">The style name.</param>
		private void NewXmlNode(string name)
		{			
			this.Node		= this.Document.CreateNode("style", "style");
			XmlAttribute xa = this.Document.CreateAttribute("name", "style");
			xa.Value		= name;
			this.Node.Attributes.Append(xa);
			xa				= this.Document.CreateAttribute("family", "style");
			xa.Value		= FamiliyStyles.Paragraph;
			this.Node.Attributes.Append(xa);
			xa				= this.Document.CreateAttribute("parent-style-name", "style");
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

		/// <summary>
		/// The family style.
		/// </summary>
		public string Family
		{
			get
			{	
				return this._node.SelectSingleNode("@style:family", 
					this.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:family", 
					this.Document.NamespaceManager).InnerText = value.ToString();
			}
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
	}
}

/*
 * $Log: ParagraphStyle.cs,v $
 * Revision 1.2  2006/01/29 18:52:51  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.6  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
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