/*
 * $Id: Graphic.cs,v 1.4 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Drawing;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Zusammenfassung für Graphic.
	/// </summary>
	public class Graphic : IContent, IHtml
	{
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
		/// <param name="frame">The frame.</param>
		/// <param name="graphiclink">The graphiclink. e.g. Pictures/1233.gif</param>
		public Graphic(Frame frame, string graphiclink)
		{
			this.Frame			= frame;
			this.Document		= frame.Document;
			this.NewXmlNode("Pictures/"+graphiclink);
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

		#region IContent Member

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to which this column.
		/// </summary>
		public TextDocument Document
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

		private XmlNode _node;
		/// <summary>
		/// The XmlNode.
		/// </summary>
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
		/// A graphic doesn't have a style name.
		/// </summary>
		/// <value></value>
		public string Stylename
		{
			get
			{
				return null;
			}
			set
			{
				
			}
		}

		//private IStyle _style;
		/// <summary>
		/// A graphic doesn't have a style.
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

		/// <summary>
		/// Not implemented!!! 
		/// </summary>
		public ITextCollection TextContent
		{
			get
			{
				return null;
			}
			set
			{				
			}
		}

		#endregion

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string html		= "<img src=\""+this.GetHtmlImgFolder()+"\" hspace=\"14\" vspace=\"14\">\n";

			return html;
		}

		/// <summary>
		/// Gets the HTML img folder.
		/// </summary>
		/// <returns></returns>
		private string GetHtmlImgFolder()
		{
			try
			{
				if(this.Frame.RealGraphicName != null)
				{
					return "temphtmlimg/Pictures/"+this.Frame.RealGraphicName;
				}
			}
			catch(Exception ex)
			{
			}
			return "";
		}

		#endregion
	}
}
