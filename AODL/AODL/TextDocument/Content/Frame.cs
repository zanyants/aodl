/*
 * $Id: Frame.cs,v 1.4 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.IO;
using System.Drawing;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Zusammenfassung für Frame.
	/// </summary>
	public class Frame : IContent, IHtml, IDisposable
	{
		private Graphic _graphic;
		/// <summary>
		/// Gets the graphic.
		/// </summary>
		/// <value>The graphic.</value>
		public Graphic Graphic
		{
			get { return this._graphic; }
		}

		private string _realgraphicname;
		/// <summary>
		/// Gets the name of the real graphic.
		/// </summary>
		/// <value>The name of the real graphic.</value>
		public string RealGraphicName
		{
			get { return this._realgraphicname; }
		}

		private Image _image;
		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>The image.</value>
		public Image Image
		{
			get { return this._image; }
		}

		/// <summary>
		/// Gets or sets the name of the graphic.
		/// </summary>
		/// <value>The name of the graphic.</value>
		public string GraphicName
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
		/// Gets or sets the width of the graphic. e.g 2.98cm
		/// </summary>
		/// <value>The width of the graphic.</value>
		public string GraphicWidth
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
		/// Gets or sets the height of the graphic. e.g 3.00cm
		/// </summary>
		/// <value>The height of the graphic.</value>
		public string GraphicHeight
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
		/// Initializes a new instance of the <see cref="Frame"/> class.
		/// </summary>
		/// <param name="textdocument">The textdocument.</param>
		/// <param name="stylename">The stylename.</param>
		public Frame(TextDocument textdocument, string stylename)
		{
			this.Document		= textdocument;
			this.NewXmlNode(stylename);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Frame"/> class.
		/// </summary>
		/// <param name="textdocument">The textdocument.</param>
		/// <param name="stylename">The stylename.</param>
		/// <param name="graphicname">The graphicname.</param>
		/// <param name="graphicfile">The graphicfile.</param>
		public Frame(TextDocument textdocument, string stylename, string graphicname, string graphicfile)
		{
			this.Document			= textdocument;
			this.NewXmlNode(stylename);
			this.GraphicName		= graphicname;
			this._realgraphicname	= this.LoadImageFromFile(graphicfile);
			this._graphic			= new Graphic(this, this._realgraphicname);
			this.Style				= (FrameStyle)new FrameStyle(this, stylename);

			this.Node.AppendChild(this.Graphic.Node);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="stylename">The stylename which should be referenced with this frame.</param>
		private void NewXmlNode(string stylename)
		{			
			this.Node		= this.Document.CreateNode("frame", "draw");

			XmlAttribute xa = this.Document.CreateAttribute("style-name", "text");
			xa.Value		= stylename;

			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("anchor-type", "text");
			xa.Value		= "paragraph"; //TODO: static implementation

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
		/// Loads the image from file.
		/// </summary>
		/// <param name="graphicfilename">The graphicfilename.</param>
		private string LoadImageFromFile(string graphicfilename)
		{
			try
			{
				double pxtocm		= 37.7928; //TODO: Check ! px to cm
				this._image			= Image.FromFile(graphicfilename);
				double pixelheight	= Convert.ToDouble(this._image.Height)/pxtocm;
				double pixelweidth	= Convert.ToDouble(this._image.Width)/pxtocm;
				this.GraphicHeight	= pixelheight.ToString("F3").Replace(",",".")+"cm";
				this.GraphicWidth	= pixelweidth.ToString("F3").Replace(",",".")+"cm";

				return new FileInfo(graphicfilename).Name;
			}
			catch(Exception ex)
			{
				throw;
			}
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
		/// The stylename wihich is referenced with the content object.
		/// </summary>
		/// <value>The name</value>
		public string Stylename
		{
			get
			{
				return this.Style.Name;
			}
			set
			{
				this.Style.Name = value;
				this._node.SelectSingleNode("@table:style-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// The ColumnStyle which is referenced with this column.
		/// </summary>
		public IStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this._style = value;
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
			if(this.Graphic != null)
				return Graphic.GetHtml();
			return "";
		}

		#endregion

		#region IDisposable Member

		private bool _disposed = false;

		/// <summary>
		/// Führt anwendungsspezifische Aufgaben durch, die mit der Freigabe, der Zurückgabe oder dem Zurücksetzen von nicht verwalteten Ressourcen zusammenhängen.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Disposes the specified disposing.
		/// </summary>
		/// <param name="disposing">if set to <c>true</c> [disposing].</param>
		private void Dispose(bool disposing)
		{
			if(!this._disposed)
			{
				if(disposing)
				{
					if(this._image != null)
						this._image.Dispose();
				}
			}
			_disposed = true;         
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="AODL.TextDocument.Content.Frame"/> is reclaimed by garbage collection.
		/// </summary>
		~Frame()      
		{
			Dispose(false);
		}

		#endregion
	}
}

/*
 * $Log: Frame.cs,v $
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