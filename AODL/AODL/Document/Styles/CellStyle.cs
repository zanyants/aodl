/*
 * $Id: CellStyle.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
using AODL.Document.SpreadsheetDocuments;

namespace AODL.Document.Styles
{
	/// <summary>
	/// CellStyle represent the style that is
	/// used within a table cell.
	/// </summary>
	public class CellStyle : IStyle
	{

		/// <summary>
		/// Gets or sets the cell properties.
		/// </summary>
		/// <value>The cell properties.</value>
		public CellProperties CellProperties
		{
			get
			{
				foreach(IProperty property in this.PropertyCollection)
					if(property is CellProperties)
						return (CellProperties)property;
				CellProperties cellProperties	= new CellProperties(this);
				this.PropertyCollection.Add((IProperty)cellProperties);
				return cellProperties;
			}
			set
			{
				if(this.PropertyCollection.Contains((IProperty)value))
					this.PropertyCollection.Remove((IProperty)value);
				this.PropertyCollection.Add(value);
			}
		}

		/// <summary>
		/// Gets or sets the family style.
		/// </summary>
		/// <value>The family style.</value>
		public string FamilyStyle
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:family",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:family",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("family", value, "style");
				this._node.SelectSingleNode("@style:family",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the parent style.
		/// </summary>
		/// <value>The name of the parent style.</value>
		public string ParentStyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:parent-style-name",
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
		/// Initializes a new instance of the <see cref="CellStyle"/> class.
		/// </summary>
		/// <param name="document">The spreadsheet document.</param>
		public CellStyle(IDocument document)
		{
			this.Document		= document;
			this.InitStandards();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CellStyle"/> class.
		/// </summary>
		/// <param name="document">The spreadsheet document.</param>
		/// <param name="styleName">Name of the style.</param>
		public CellStyle(IDocument document, string styleName)
		{
			this.Document		= document;
			this.InitStandards();
			this.StyleName					= styleName;
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards()
		{
			this.NewXmlNode();
			this.PropertyCollection				= new IPropertyCollection();
			this.PropertyCollection.Inserted	+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(PropertyCollection_Inserted);
			this.PropertyCollection.Removed		+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(PropertyCollection_Removed);
			this.Document.Styles.Add(this);
		}

		/// <summary>
		/// Create a new Xml node.
		/// </summary>
		private void NewXmlNode()
		{
			this.Node				= this.Document.CreateNode("style", "style");
			this.FamilyStyle		= FamiliyStyles.TableCell;
			
			if(this.Document is SpreadsheetDocument)
				this.ParentStyleName	= "Default";
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
 * $Log: CellStyle.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */