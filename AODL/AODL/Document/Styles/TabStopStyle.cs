/*
 * $Id: TabStopStyle.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;

namespace AODL.Document.Styles
{
	/// <summary>
	/// Class represent a TabStopStyle.
	/// </summary>
	public class TabStopStyle : IStyle
	{
		/// <summary>
		/// Position e.g = "4.98cm";
		/// </summary>
		public string Position
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:position", 
					this.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:position",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("postion", value, "style");
				this._node.SelectSingleNode("@style:position",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// A Tabstoptype e.g center
		/// </summary>
		public string Type
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:type", 
					this.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:type",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("type", value, "style");
				this._node.SelectSingleNode("@style:type",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// The Tabstop LeaderStyle e.g dotted
		/// </summary>
		public string LeaderStyle
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:leader-style", 
					this.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:leader-style",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("leader-style", value, "style");
				this._node.SelectSingleNode("@style:leader-style",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// The Tabstop Leader text e.g. "."
		/// Use this if you use the LeaderStyle property
		/// </summary>
		public string LeaderText
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:leader-text", 
					this.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:leader-text",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("leader-text", value, "style");
				this._node.SelectSingleNode("@style:leader-text",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TabStopStyle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="position">The position.</param>
		public TabStopStyle(IDocument document, double position)
		{
			this.Document		= document;
			this.NewXmlNode(position);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TabStopStyle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public TabStopStyle(IDocument document, XmlNode node)
		{
			this.Document		= document;
			this.Node			= node;
		}

		/// <summary>
		/// Create the XmlNode that represent this element.
		/// </summary>
		/// <param name="position">The position.</param>
		private void NewXmlNode(double position)
		{			
			this.Node		= this.Document.CreateNode("tab-stop", "style");

			XmlAttribute xa = this.Document.CreateAttribute("position", "style");
			xa.Value		= position.ToString().Replace(",",".")+"cm";
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

		/// <summary>
		/// Not supported. Returns null.
		/// </summary>
		/// <value></value>
		public IPropertyCollection PropertyCollection
		{
			get { return null; }
			set { }
		}
		#endregion
	}
}

/*
 * $Log: TabStopStyle.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.1  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 */