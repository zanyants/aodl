/*
 * $Id: TextStyle.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document;

namespace AODL.Document.Styles
{
	/// <summary>
	/// Represent the style for a FormatedText object.
	/// </summary>
	public class TextStyle : IStyle
	{
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
		/// The family tyle to this object belongs.
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
		/// Initializes a new instance of the <see cref="TextStyle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public TextStyle(IDocument document, XmlNode node)
		{
			this.Document						= document;
			this.Node							= node;
			this.InitStandards();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextStyle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="styleName">Name of the style.</param>
		public TextStyle(IDocument document, string styleName)
		{
			this.Document						= document;
			this.NewXmlNode(styleName);
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
			this.Document.Styles.Add(this);
		}

		/// <summary>
		/// Create the XmlNode that represent the Style element.
		/// </summary>
		/// <param name="name">The style name.</param>
		private void NewXmlNode(string name)
		{			
			this.Node		= this.Document.CreateNode("style", "style");
			
			XmlAttribute xa = this.Document.CreateAttribute("name", "style");
			xa.Value		= name;
			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("family", "style");
			xa.Value		= FamiliyStyles.Text;
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
 * $Log: TextStyle.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
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