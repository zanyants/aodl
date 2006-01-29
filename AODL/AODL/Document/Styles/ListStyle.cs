/*
 * $Id: ListStyle.cs,v 1.2 2006/01/29 18:52:51 larsbm Exp $
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
using AODL.Document;
using AODL.Document.Styles.Properties;

namespace AODL.Document.Styles
{
	/// <summary>
	/// Represent the ListStyle for a List.
	/// </summary>
	public class ListStyle : IStyle
	{
		private ListLevelStyleCollection _listlevelcollection;
		/// <summary>
		/// The ListLevelStyles which belongs to this object.
		/// </summary>
		public ListLevelStyleCollection ListlevelStyles
		{
			get { return this._listlevelcollection; }
			set { this._listlevelcollection = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ListStyle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public ListStyle(IDocument document, XmlNode node)
		{
			this.Document				= document;
			this.Node					= node;
			this.ListlevelStyles		= new ListLevelStyleCollection();
		}

		/// <summary>
		/// Create a new ListStyle object.
		/// </summary>
		/// <param name="document">The docuemnt</param>
		/// <param name="styleName">The style name</param>
		public ListStyle(IDocument document, string styleName)
		{
			this.Document				= document;			
			this.NewXmlNode();
			this.InitStandards();
			this.ListlevelStyles		= new ListLevelStyleCollection();
			this.StyleName				= styleName;			
		}

		/// <summary>
		/// Add all possible ListLevelStyle objects automatically.
		/// Throws exception, if there are already ListLevelStyles
		/// which could'nt removed.
		/// </summary>
		/// <param name="typ">The Liststyle bullet, numbered, ..s</param>
		public void AutomaticAddListLevelStyles(ListStyles typ)
		{
			if(this.Node.ChildNodes.Count != 0)
			{
				try
				{
					foreach(XmlNode xn in this.Node.ChildNodes)
						this.Node.RemoveChild(xn);
				}
				catch(Exception ex)
				{
					throw;
				}
			}

			this.ListlevelStyles.Clear();

			for(int i = 1; i <= 10; i++)
			{
				ListLevelStyle style		= new ListLevelStyle(this.Document, this, typ, i);
				this.ListlevelStyles.Add(style);
				this.Document.Styles.Add(style);
			}

			foreach(ListLevelStyle lls in this.ListlevelStyles)
				this.Node.AppendChild(lls.Node);
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
		private void NewXmlNode()
		{			
			this.Node		= this.Document.CreateNode("list-style", "text");
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

	/// <summary>
	/// Represent the different kinds of lis styles.
	/// </summary>
	public enum ListStyles
	{
		/// <summary>
		/// Numbered list
		/// </summary>
		Number,
		/// <summary>
		/// Bullet list
		/// </summary>
		Bullet
	}
}

/*
 * $Log: ListStyle.cs,v $
 * Revision 1.2  2006/01/29 18:52:51  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.2  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.1  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 */