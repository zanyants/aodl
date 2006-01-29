/*
 * $Id: UnknownStyle.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
	/// UnknownStyle represent an unknown style element.
	/// </summary>
	public class UnknownStyle : IStyle
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UnknownStyle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="unknownNode">The unknown node.</param>
		public UnknownStyle(IDocument document, XmlNode unknownNode)
		{
			this.Document				= document;
			this.Node					= unknownNode;
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
		/// The style name, if no name is found it return null.
		/// Notice: Set isn't available.
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
		/// A collection of properties isn't available
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
 * $Log: UnknownStyle.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */