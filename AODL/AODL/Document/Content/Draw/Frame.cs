/*
 * $Id: Frame.cs,v 1.5 2006/05/02 17:37:16 larsbm Exp $
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
using System.IO;
using System.Drawing;
using System.Xml;
using AODL.Document.Styles;
using AODL.Document.Content.Text;
using AODL.Document.Content;
using AODL.Document;

namespace AODL.Document.Content.Draw
{
	/// <summary>
	/// Frame represent graphic resp. a draw container.
	/// </summary>
	public class Frame : IContent, IContentContainer
	{
		/// <summary>
		/// Gets or sets the frame style.
		/// </summary>
		/// <value>The frame style.</value>
		public FrameStyle FrameStyle
		{
			get { return (FrameStyle)this.Style; }
			set { this.Style = (IStyle) value; }
		}

		private string _realgraphicname;
		/// <summary>
		/// Gets the name of the real graphic.
		/// </summary>
		/// <value>The name of the real graphic.</value>
		public string RealGraphicName
		{
			get { return this._realgraphicname; }
			set { this._realgraphicname = value; }
		}

		private string _graphicSourcePath;
		/// <summary>
		/// Gets or sets the graphic source path.
		/// </summary>
		/// <value>The graphic source path.</value>
		public string GraphicSourcePath
		{
			get { return this._graphicSourcePath; }
			set { this._graphicSourcePath = value; }
		}

		/// <summary>
		/// Gets or sets the draw name.
		/// </summary>
		/// <value>The name of the graphic.</value>
		public string DrawName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:name",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("name", value, "draw");
				this._node.SelectSingleNode("@draw:name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the type of the anchor. e.g paragraph
		/// </summary>
		/// <value>The type of the anchor.</value>
		public string AnchorType
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@text:anchor-type",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@text:anchor-type",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("anchor-type", value, "text");
				this._node.SelectSingleNode("@text:anchor-type",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the z index.
		/// </summary>
		/// <value>The index of the Z.</value>
		public string ZIndex
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:z-index",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:z-index",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("z-index", value, "draw");
				this._node.SelectSingleNode("@draw:z-index",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the width of the frame. e.g 2.98cm
		/// </summary>
		/// <value>The width of the graphic.</value>
		public string SvgWidth
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@svg:width",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@svg:width",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("width", value, "svg");
				this._node.SelectSingleNode("@svg:width",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the height of the frame. e.g 3.00cm
		/// </summary>
		/// <value>The height of the graphic.</value>
		public string SvgHeight
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@svg:height",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@svg:height",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("height", value, "svg");
				this._node.SelectSingleNode("@svg:height",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the horizontal position where
		/// the hosted drawing e.g Graphic should be
		/// anchored. 
		/// </summary>
		/// <example>myFrame.SvgX = "1.5cm"</example>
		/// <value>The SVG X.</value>
		public string SvgX
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@svg:x",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@svg:x",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("x", value, "svg");
				this._node.SelectSingleNode("@svg:x",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the vertical position where
		/// the hosted drawing e.g Graphic should be
		/// anchored. 
		/// </summary>
		/// <example>myFrame.SvgY = "1.5cm"</example>
		/// <value>The SVG Y.</value>
		public string SvgY
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@svg:y",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@svg:y",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("y", value, "svg");
				this._node.SelectSingleNode("@svg:y",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		private Guid _graphicIdentifier;
		/// <summary>
		/// Gets or sets the graphic identifier.
		/// </summary>
		/// <value>The graphic identifier.</value>
		public Guid GraphicIdentifier
		{
			get { return this._graphicIdentifier; }
			set { this._graphicIdentifier = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Frame"/> class.
		/// </summary>
		/// <param name="document">The textdocument.</param>
		/// <param name="stylename">The stylename.</param>
		public Frame(IDocument document, string stylename)
		{
			this.Document			= document;

			this.NewXmlNode();			
			this.InitStandards();

			if(stylename != null)
			{
				this.Style				= (IStyle)new FrameStyle(this.Document, stylename);
				this.StyleName			= stylename;
				this.Document.Styles.Add(this.Style);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Frame"/> class.
		/// </summary>
		/// <param name="document">The textdocument.</param>
		/// <param name="stylename">The stylename.</param>
		/// <param name="drawName">The  draw name.</param>
		/// <param name="graphicfile">The graphicfile.</param>
		public Frame(IDocument document, string stylename, string drawName, string graphicfile)
		{
			this.Document			= document;			
			this.NewXmlNode();
			this.InitStandards();

			this.StyleName			= stylename;
//			this.AnchorType			= "paragraph";

			this.DrawName			= drawName;
			this.GraphicSourcePath	= graphicfile;

			this._graphicIdentifier = Guid.NewGuid();
			this._realgraphicname	= this._graphicIdentifier.ToString()+this.LoadImageFromFile(graphicfile);
			Graphic graphic			= new Graphic(this.Document, this, this._realgraphicname);
			graphic.GraphicRealPath	= this.GraphicSourcePath;
			this.Content.Add(graphic);
			this.Style				= (IStyle)new FrameStyle(this.Document, stylename);	
			this.Document.Styles.Add(this.Style);
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards()
		{
			//Todo: FrameBuilder
			this.AnchorType				= "paragraph";

			this.Content				= new IContentCollection();
			this.Content.Inserted		+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(Content_Inserted);
			this.Content.Removed		+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(Content_Removed);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		private void NewXmlNode()
		{			
			this.Node		= this.Document.CreateNode("frame", "draw");
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
		/// Loads the image from file.
		/// </summary>
		/// <param name="graphicfilename">The graphicfilename.</param>
		private string LoadImageFromFile(string graphicfilename)
		{
			try
			{
				double pxtocm		= 37.7928; //TODO: Check ! px to cm
				Image image			= Image.FromFile(graphicfilename);
				double pixelheight	= Convert.ToDouble(image.Height)/pxtocm;
				double pixelweidth	= Convert.ToDouble(image.Width)/pxtocm;
				this.SvgHeight		= pixelheight.ToString("F3").Replace(",",".")+"cm";
				this.SvgWidth		= pixelweidth.ToString("F3").Replace(",",".")+"cm";
				image.Dispose();

				return new FileInfo(graphicfilename).Name;
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Content_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Inserted(int index, object value)
		{
			if(value is Graphic)
				if(((Graphic)value).Frame == null)
				{
					((Graphic)value).Frame = this;
					if(((Graphic)value).GraphicRealPath != null
						&& this.GraphicSourcePath == null)
						this.GraphicSourcePath = ((Graphic)value).GraphicRealPath;
				}
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
			//if graphic remove it
			if(value is Graphic)
				if(this.Document.Graphics.Contains(value as Graphic))
					this.Document.Graphics.Remove(value as Graphic);
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
				XmlNode xn = this._node.SelectSingleNode("@draw:style-name",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:style-name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("style-name", value, "draw");
				this._node.SelectSingleNode("@draw:style-name",
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

		#region IHtml Member
//
//		/// <summary>
//		/// Return the content as Html string
//		/// </summary>
//		/// <returns>The html string</returns>
//		public string GetHtml()
//		{
//			string html			= "";
//			if(this.Content.Count > 0)
//				if(this.Content[0] is Graphic)
//					html			= ((Graphic)this.Content[0]).GetHtml();
//
//			foreach(IContent content in this.Content)
//				if(content is IHtml)
//					html		+= ((IHtml)content).GetHtml();
//
//			return html;
//		}

		#endregion


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

/*
 * $Log: Frame.cs,v $
 * Revision 1.5  2006/05/02 17:37:16  larsbm
 * - Flag added graphics with guid
 * - Set guid based read and write directories
 *
 * Revision 1.4  2006/02/16 18:35:41  larsbm
 * - Add FrameBuilder class
 * - TextSequence implementation (Todo loading!)
 * - Free draing postioning via x and y coordinates
 * - Graphic will give access to it's full qualified path
 *   via the GraphicRealPath property
 * - Fixed Bug with CellSpan in Spreadsheetdocuments
 * - Fixed bug graphic of loaded files won't be deleted if they
 *   are removed from the content.
 * - Break-Before property for Paragraph properties for Page Break
 *
 * Revision 1.3  2006/02/05 20:02:25  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:29:46  larsbm
 * *** empty log message ***
 *
 * Revision 1.5  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.4  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.3  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.2  2005/10/22 10:47:41  larsbm
 * - add graphic support
 *
 * Revision 1.1  2005/10/17 19:32:47  larsbm
 * - start vers. 1.0.3.0
 * - add frame, framestyle, graphic, graphicproperties
 *
 */