/*
 * $Id: ColumnProperties.cs,v 1.2 2006/02/05 20:03:32 larsbm Exp $
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

namespace AODL.Document.Styles.Properties
{
	/// <summary>
	/// ColumnProperties represent the table column properties.
	/// </summary>
	public class ColumnProperties : IProperty
	{
		/// <summary>
		/// Set the column width -> table = 16.99cm -> column = 8.49cm
		/// </summary>
		public string Width
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:column-width",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:column-width",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("column-width", value, "style");
				this._node.SelectSingleNode("@style:column-width",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set the column relative width
		/// </summary>
		public string RelativeWidth
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:rel-column-width",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:rel-column-width",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("rel-column-width", value, "style");
				this._node.SelectSingleNode("@style:rel-column-width",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// The Constructor, create new instance of ColumnProperties
		/// </summary>
		/// <param name="style">The ColumnStyle</param>
		public ColumnProperties(IStyle style)
		{
			this.Style			= style;
			this.NewXmlNode();
		}
		
		/// <summary>
		/// Create the XmlNode which represent the propertie element.
		/// </summary>
		private void NewXmlNode()
		{
			this.Node		= this.Style.Document.CreateNode("table-column-properties", "style");
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Style.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		#region IProperty Member
		private XmlNode _node;
		/// <summary>
		/// The XmlNode which represent the property element.
		/// </summary>
		/// <value>The node</value>
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

		private IStyle _style;
		/// <summary>
		/// The style object to which this property object belongs
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get { return this._style; }
			set { this._style = value; }
		}
		#endregion
	}
}

/*
 * $Log: ColumnProperties.cs,v $
 * Revision 1.2  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.3  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.2  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 * Revision 1.1  2005/10/12 19:52:56  larsbm
 * - start table implementation
 * - added uml diagramm
 *
 */