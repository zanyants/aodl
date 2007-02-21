/*************************************************************************
 *
 *  OpenOffice.org - a multi-platform office productivity suite
 *
 *  $RCSfile: DrawArea.cs,v $
 *
 *  $Revision: 1.4 $
 *
 *  last change: $Author: larsbm $ $Date: 2007/02/21 20:45:46 $
 * 
 *  Initial Author:
 *  Copyright 2006, Kristy Saunders, ksaunders@eduworks.com
 *
 *  The Contents of this file are made available subject to
 *  the terms of GNU Lesser General Public License Version 2.1.
 *
 *
 *    GNU Lesser General Public License Version 2.1
 *    =============================================
 *    Copyright 2005 by Sun Microsystems, Inc.
 *    901 San Antonio Road, Palo Alto, CA 94303, USA
 *
 *    This library is free software; you can redistribute it and/or
 *    modify it under the terms of the GNU Lesser General Public
 *    License version 2.1, as published by the Free Software Foundation.
 *
 *    This library is distributed in the hope that it will be useful,
 *    but WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *    Lesser General Public License for more details.
 *
 *    You should have received a copy of the GNU Lesser General Public
 *    License along with this library; if not, write to the Free Software
 *    Foundation, Inc., 59 Temple Place, Suite 330, Boston,
 *    MA  02111-1307  USA
 *
 ************************************************************************/

using System;
using System.IO;
using System.Drawing;
using System.Xml;
using AODL.Document.Styles;
using AODL.Document.Content.Text;
using AODL.Document.Content;
using AODL.Document.Content.OfficeEvents;
using AODL.Document;

namespace AODL.Document.Content.Draw
{
	// @@@@ Add EventListeners to DrawArea
	// @@@@ draw:area-circle attributes: svg:cx, svg:cy, svg:r
	// @@@@ draw:area-polygon:  omit
	// 

	/// <summary>
	/// DrawAreaRectangle represent draw area rectangle which
	/// could be used within a ImageMap.
	/// </summary>
	public class DrawAreaRectangle : DrawArea
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DrawAreaRectangle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public DrawAreaRectangle(IDocument document, XmlNode node) : base(document)
		{
			this.Node			= node;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DrawAreaRectangle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		public DrawAreaRectangle(IDocument document,
			string x, string y, string width, string height) : base(document) 
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DrawAreaRectangle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="listeners">The listeners.</param>
		public DrawAreaRectangle(IDocument document,
			string x, string y, string width, string height,
			EventListeners listeners)
			: base(document)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;

			if (listeners != null)
			{
				this.Content.Add(listeners);
			}
		}

		/// <summary>
		/// Gets or sets the x-position.
		/// </summary>
		/// <value>The description of the area-position.</value>
		public string X
		{
			get
			{
				XmlNode xn = this.Node.SelectSingleNode("@svg:x",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@svg:x",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("x", value, "svg");
				this.Node.SelectSingleNode("@svg:x",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the y-position.
		/// </summary>
		/// <value>The y-position of the area-rectangle.</value>
		public string Y
		{
			get
			{
				XmlNode xn = this.Node.SelectSingleNode("@svg:y",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@svg:y",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("y", value, "svg");
				this.Node.SelectSingleNode("@svg:y",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width of the area-rectangle.</value>
		public string Width
		{
			get
			{
				XmlNode xn = this.Node.SelectSingleNode("@svg:width",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@svg:width",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("width", value, "svg");
				this.Node.SelectSingleNode("@svg:width",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height of the area-rectangle.</value>
		public string Height
		{
			get
			{
				XmlNode xn = this.Node.SelectSingleNode("@svg:height",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@svg:height",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("height", value, "svg");
				this.Node.SelectSingleNode("@svg:height",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		override protected void NewXmlNode()
		{			
			this.Node		= this.Document.CreateNode("area-rectangle", "draw");
		}
	}

	/// <summary>
	/// Summary for DrawAreaRectangle.
	/// </summary>
	public class DrawAreaCircle : DrawArea
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DrawAreaCircle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public DrawAreaCircle(IDocument document, XmlNode node) : base(document)
		{
			this.Node			= node;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DrawAreaCircle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="cx">The cx.</param>
		/// <param name="cy">The cy.</param>
		/// <param name="radius">The radius.</param>
		public DrawAreaCircle(IDocument document,
			string cx, string cy, string radius)
			: base(document)
		{
			this.CX = cx;
			this.CY = cy;
			this.Radius = radius;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DrawAreaCircle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="cx">The cx.</param>
		/// <param name="cy">The cy.</param>
		/// <param name="radius">The radius.</param>
		/// <param name="listeners">The listeners.</param>
		public DrawAreaCircle(IDocument document,
			string cx, string cy, string radius, EventListeners listeners)
			: base(document)
		{
			this.CX = cx;
			this.CY = cy;
			this.Radius = radius;

			if (listeners != null)
			{
				this.Content.Add(listeners);
			}
		}

		/// <summary>
		/// Gets or sets the cx-position.
		/// </summary>
		/// <value>The center of the area-circle.</value>
		public string CX
		{
			get
			{
				XmlNode xn = this.Node.SelectSingleNode("@svg:cx",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				if (value == "")
					return;
				XmlNode xn = this.Node.SelectSingleNode("@svg:cx",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("cx", value, "svg");
				this.Node.SelectSingleNode("@svg:cx",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the cy-position.
		/// </summary>
		/// <value>The center position of the area-cicle.</value>
		public string CY
		{
			get
			{
				XmlNode xn = this.Node.SelectSingleNode("@svg:cy",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				if (value == "")
					return;
				XmlNode xn = this.Node.SelectSingleNode("@svg:cy",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("cy", value, "svg");
				this.Node.SelectSingleNode("@svg:cy",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the radius.
		/// </summary>
		/// <value>The radius of the area-circle.</value>
		public string Radius
		{
			get
			{
				XmlNode xn = this.Node.SelectSingleNode("@svg:r",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				if (value == "")
					return;
				XmlNode xn = this.Node.SelectSingleNode("@svg:r",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("r", value, "svg");
				this.Node.SelectSingleNode("@svg:r",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		override protected void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("area-circle", "draw");
		}
	}

	/// <summary>
	/// Summary for DrawArea.
	/// </summary>
	abstract public class DrawArea: IContent, IContentContainer
	{
		/// <summary>
		/// Gets or sets the href. e.g http://www.sourceforge.net
		/// </summary>
		/// <value>The href.</value>
		public string Href
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@xlink:href",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@xlink:href",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("href", value, "xlink");
				this._node.SelectSingleNode("@xlink:href",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the type of the X link.
		/// </summary>
		/// <value>The type of the X link.</value>
		public string XLinkType
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@xlink:type",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@xlink:type",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("type", value, "xlink");
				this._node.SelectSingleNode("@xlink:type",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description of the draw area.</value>
		public string Description
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:desc",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:desc",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("desc", value, "draw");
				this._node.SelectSingleNode("@draw:desc",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		protected void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value = text;
			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DrawArea"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		protected DrawArea(IDocument document)
		{
			this.Document = document;
			this.InitStandards();
			this.NewXmlNode();
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards()
		{
			this.Content				= new IContentCollection();
			this.Content.Inserted		+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(Content_Inserted);
			this.Content.Removed		+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(Content_Removed);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DrawAreaRectangle"/> class.
		/// </summary>
		abstract protected void NewXmlNode();

		/// <summary>
		/// Content_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		protected void Content_Inserted(int index, object value)
		{
			if(this.Node != null)
				this.Node.AppendChild(((IContent)value).Node);
		}

		/// <summary>
		/// Content_s the removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		protected void Content_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IContent)value).Node);
		}

		#region IContent Member		

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

		/// <summary>
		/// A draw:area-rectangle doesn't have a style-name.
		/// </summary>
		/// <value>The name</value>
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

		/// <summary>
		/// A draw:area-rectangle doesn't have a style.
		/// </summary>
		public IStyle Style
		{
			get
			{
				return null;
			}
			set
			{
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

		#region IContentCollection Member

		private IContentCollection _content;
		/// <summary>
		/// Gets or sets the content collection.
		/// </summary>
		/// <value>The content collection.</value>
		public IContentCollection Content
		{
			get { return this._content; }
			set { this._content = value; }
		}

		#endregion

		#region IContent Member old

//		private IDocument _document;
//		/// <summary>
//		/// The IDocument to which this draw-area is bound.
//		/// </summary>
//		public IDocument Document
//		{
//			get
//			{
//				return this._document;
//			}
//			set
//			{
//				this._document = value;
//			}
//		}
//
//		private XmlNode _node;
//		/// <summary>
//		/// The XmlNode.
//		/// </summary>
//		public XmlNode Node
//		{
//			get
//			{
//				return this._node;
//			}
//			set
//			{
//				this._node = value;
//			}
//		}
//
//		/// <summary>
//		/// A draw:area-rectangle doesn't have a style-name.
//		/// </summary>
//		/// <value>The name</value>
//		public string Stylename
//		{
//			get
//			{
//				return null;
//			}
//			set
//			{
//			}
//		}
//
//		/// <summary>
//		/// A draw:area-rectangle doesn't have a style.
//		/// </summary>
//		public IStyle Style
//		{
//			get
//			{
//				return null;
//			}
//			set
//			{
//			}
//		}
//
//		/// <summary>
//		/// A draw:area-rectangle doesn't contain text.
//		/// </summary>
//		public ITextCollection TextContent
//		{
//			get
//			{
//				return null;
//			}
//			set
//			{
//			}
//		}

		#endregion
	
	}
}
