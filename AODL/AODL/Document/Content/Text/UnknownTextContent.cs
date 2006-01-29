/*
 * $Id: UnknownTextContent.cs,v 1.1 2006/01/29 11:28:22 larsbm Exp $
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

namespace AODL.Document.Content.Text
{
	/// <summary>
	/// UnknownTextContent represent an unknown text element.
	/// </summary>
	public class UnknownTextContent : IContent
	{
		/// <summary>
		/// Gets the name of the get element.
		/// </summary>
		/// <value>The name of the get element.</value>
		public string GetElementName
		{
			get 
			{
				if(this.Node != null)
					return this.Node.Name;
				return null;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UnknownTextContent"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public UnknownTextContent(IDocument document, XmlNode node)
		{
			this.Document			= document;
			this.Node				= node;
		}

		#region IContent Member
		/// <summary>
		/// Return null, because the attribute name is unknown.
		/// </summary>
		/// <value></value>
		public string StyleName
		{
			get 
			{ 
				return null;
			}
			set
			{
			
			}
		}

		private IDocument _document;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his document.
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

		private IStyle _style;
		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this.StyleName	= value.StyleName;
				this._style = value;
			}
		}

		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node
		{
			get { return this._node; }
			set { this._node = value; }
		}

		#endregion
	}
}

/*
 * $Log: UnknownTextContent.cs,v $
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */