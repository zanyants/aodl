/*
 * $Id: Graphic.cs,v 1.2 2006/02/05 20:02:25 larsbm Exp $
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
using System.Drawing;
using System.Xml;
using AODL.Document.Styles;
using AODL.Document.Content;
using AODL.Document;

namespace AODL.Document.Content.Draw
{
	/// <summary>
	/// Graphic represent a graphic resp. image.
	/// </summary>
	public class Graphic : IContent, IContentContainer
	{
		/// <summary>
		/// Gets or sets the H ref.
		/// </summary>
		/// <value>The H ref.</value>
		public string HRef
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:href",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@xlink:href",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("href", value, "xlink");
				this._node.SelectSingleNode("@xlink:href",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the actuate.
		/// e.g. onLoad
		/// </summary>
		/// <value>The actuate.</value>
		public string Actuate
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:actuate",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@xlink:actuate",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("actuate", value, "xlink");
				this._node.SelectSingleNode("@xlink:actuate",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the type of the Xlink.
		/// e.g. simple, standard, ..
		/// </summary>
		/// <value>The type of the X link.</value>
		public string XLinkType
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:type",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@xlink:type",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("type", value, "xlink");
				this._node.SelectSingleNode("@xlink:type",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the show.
		/// e.g. embed
		/// </summary>
		/// <value>The show.</value>
		public string Show
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@xlink:show",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@xlink:show",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("show", value, "xlink");
				this._node.SelectSingleNode("@xlink:show",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		private Frame _frame;
		/// <summary>
		/// Gets or sets the frame.
		/// </summary>
		/// <value>The frame.</value>
		public Frame Frame
		{
			get { return this._frame; }
			set { this._frame = value; }
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Graphic"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="frame">The frame.</param>
		/// <param name="graphiclink">The graphiclink.</param>
		public Graphic(IDocument document, Frame frame, string graphiclink)
		{
			this.Frame			= frame;
			this.Document		= document;
			this.NewXmlNode("Pictures/"+graphiclink);
			this.InitStandards();
			this.Document.Graphics.Add(this);
			this.Document.DocumentMetadata.ImageCount	+= 1;
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
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="graphiclink">The stylename which should be referenced with this frame.</param>
		private void NewXmlNode(string graphiclink)
		{			
			this.Node		= this.Document.CreateNode("image", "draw");

			XmlAttribute xa = this.Document.CreateAttribute("href", "xlink");
			xa.Value		= graphiclink;

			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("type", "xlink");
			xa.Value		= "standard"; 

			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("show", "xlink");
			xa.Value		= "embed"; 

			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("actuate", "xlink");
			xa.Value		= "onLoad"; 

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

		/// <summary>
		/// Content_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IContent)value).Node);
		}

		/// <summary>
		/// Content_s the removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IContent)value).Node);
		}

		#region IContent Member
		/// <summary>
		/// Gets or sets the name of the style.
		/// </summary>
		/// <value>The name of the style.</value>
		public string StyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@text:style-name",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@text:style-name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("style-name", value, "text");
				this._node.SelectSingleNode("@text:style-name",
					this.Document.NamespaceManager).InnerText = value;
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

//		#region IHtml Member
//
//		/// <summary>
//		/// Return the content as Html string
//		/// </summary>
//		/// <returns>The html string</returns>
//		public string GetHtml()
//		{
//			string align	= "<span align=\"#align#\">\n";
//			string html		= "<img src=\""+this.GetHtmlImgFolder()+"\" hspace=\"14\" vspace=\"14\""; //>\n";
//
//			Size size		= this.GetSizeInPix();
//			html			+= " width=\""+size.Width+"\" height=\""+size.Height+"\">\n";
//
//			if(this.Frame.FrameStyle.GraphicProperties != null)
//				if(this.Frame.FrameStyle.GraphicProperties.HorizontalPosition != null)
//				{
//					align	= align.Replace("#align#", 
//						this.Frame.FrameStyle.GraphicProperties.HorizontalPosition);
//					html	= align + html + "</span>\n";
//				}
//
//			return html;
//		}
//
//		/// <summary>
//		/// Gets the HTML img folder.
//		/// </summary>
//		/// <returns></returns>
//		private string GetHtmlImgFolder()
//		{
//			try
//			{
//				if(this.Frame.RealGraphicName != null)
//				{
//					return "temphtmlimg/Pictures/"+this.Frame.RealGraphicName;
//				}
//			}
//			catch(Exception ex)
//			{
//			}
//			return "";
//		}
//
//		/// <summary>
//		/// Gets the size in pix. As it is set in the frame.
//		/// This is needed, because the size of the graphic
//		/// could be another.
//		/// </summary>
//		/// <returns>The size in pixel</returns>
//		private Size GetSizeInPix()
//		{
//			try
//			{
//				double pxtocm		= 37.7928;
//				double intocm		= 2.41;
//				double height		= 0.0;
//				double width		= 0.0;
//
//				if(this.Frame.GraphicHeight.IndexOf("cm") > 0)
//				{
//					height		= Convert.ToDouble(this.Frame.GraphicHeight.Replace("cm",""), 
//						System.Globalization.NumberFormatInfo.InvariantInfo);
//					width		= Convert.ToDouble(this.Frame.GraphicWidth.Replace("cm",""),
//						System.Globalization.NumberFormatInfo.InvariantInfo);
//
//					height				*= pxtocm;
//					width				*= pxtocm;
//				}
//				else if(this.Frame.GraphicHeight.IndexOf("in") > 0)
//				{
//					height		= Convert.ToDouble(this.Frame.GraphicHeight.Replace("in",""), 
//						System.Globalization.NumberFormatInfo.InvariantInfo);
//					width		= Convert.ToDouble(this.Frame.GraphicWidth.Replace("in",""),
//						System.Globalization.NumberFormatInfo.InvariantInfo);
//
//					height				*= intocm*pxtocm;
//					width				*= intocm*pxtocm;
//				}
//				else if(this.Frame.GraphicHeight.IndexOf("px") > 0)
//				{
//					height		= Convert.ToDouble(this.Frame.GraphicHeight.Replace("px",""), 
//						System.Globalization.NumberFormatInfo.InvariantInfo);
//					width		= Convert.ToDouble(this.Frame.GraphicWidth.Replace("px",""),
//						System.Globalization.NumberFormatInfo.InvariantInfo);
//				}
//
//
//				Size size			= new Size((int)width, (int)height);
//
//				return size;
//			}
//			catch(Exception ex)
//			{
//				throw;
//			}
//		}
//
//		#endregion

		#region IContentContainer Member

		private IContentCollection _content;
		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>The content.</value>
		public IContentCollection Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		#endregion
	}
}
