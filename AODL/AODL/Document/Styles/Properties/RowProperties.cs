/*
 * $Id: RowProperties.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
	/// Zusammenfassung für RowProperties.
	/// </summary>
	public class RowProperties : IProperty
	{		
		/// <summary>
		/// Gets or sets the color of the background.
		/// </summary>
		/// <value>The color of the background.</value>
		public string BackgroundColor
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:background-color",
					this.Style.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:background-color",
					this.Style.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("background-color", value, "fo");
				this._node.SelectSingleNode("@fo:background-color",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RowProperties"/> class.
		/// </summary>
		/// <param name="style">The rowstyle.</param>
		public RowProperties(IStyle style)
		{
			this.Style				= style;
			this.NewXmlNode();
		}

		/// <summary>
		/// Create the XmlNode which represent the propertie element.
		/// </summary>
		private void NewXmlNode()
		{
			this.Node		= this.Style.Document.CreateNode("table-row-properties", "style");
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

		#region IHtmlStyle Member

		/// <summary>
		/// Gets the HTML style.
		/// </summary>
		/// <returns></returns>
		public string GetHtmlStyle()
		{
			string style		= "style=\"";

			if(this.BackgroundColor != null)
				style	+= "background-color: "+this.BackgroundColor+"; ";

			if(!style.EndsWith("; "))
				style	= "";
			else
				style	+= "\"";

			return style;
		}

		#endregion
	}
}

/*
 * $Log: RowProperties.cs,v $
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
 * Revision 1.1  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 */