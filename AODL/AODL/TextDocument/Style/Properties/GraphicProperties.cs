/*
 * $Id: GraphicProperties.cs,v 1.3 2005/12/18 18:29:46 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Style.Properties
{
	/// <summary>
	/// Zusammenfassung für GraphicProperties.
	/// </summary>
	public class GraphicProperties : IProperty
	{
		private FrameStyle _framestyle;
		/// <summary>
		/// Gets or sets the frame style.
		/// </summary>
		/// <value>The frame style.</value>
		public FrameStyle FrameStyle
		{
			get { return this._framestyle; }
			set { this._framestyle = value; }
		}

		/// <summary>
		/// Gets or sets the horizontal position. e.g center, left, right
		/// </summary>
		/// <value>The horizontal position.</value>
		public string HorizontalPosition
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:horizontal-pos",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:horizontal-pos",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("horizontal-pos", value, "style");
				this._node.SelectSingleNode("@style:horizontal-pos",
					this.FrameStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the horizontal relative. e.g. paragraph
		/// </summary>
		/// <value>The horizontal relative.</value>
		public string HorizontalRelative
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:horizontal-rel",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:horizontal-rel",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("horizontal-rel", value, "style");
				this._node.SelectSingleNode("@style:horizontal-rel",
					this.FrameStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the mirror. e.g. none
		/// </summary>
		/// <value>The mirror.</value>
		public string Mirror
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:mirror",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:mirror",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("mirror", value, "style");
				this._node.SelectSingleNode("@style:mirror",
					this.FrameStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the clip. e.g rect(0cm 0cm 0cm 0cm)
		/// </summary>
		/// <value>The clip value.</value>
		public string Clip
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:clip",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:clip",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("clip", value, "fo");
				this._node.SelectSingleNode("@fo:clip",
					this.FrameStyle.Document.NamespaceManager).InnerText = value;
			}
		}


		/// <summary>
		/// Gets or sets the luminance in procent. e.g 10%
		/// </summary>
		/// <value>The luminance in procent.</value>
		public string LuminanceInProcent
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:luminance",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:luminance",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("luminance", value, "draw");
				this._node.SelectSingleNode("@draw:luminance",
					this.FrameStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the contrast in procent. e.g 10%
		/// </summary>
		/// <value>The contrast in procent.</value>
		public string ContrastInProcent
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:contrast",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:contrast",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("contrast", value, "draw");
				this._node.SelectSingleNode("@draw:contrast",
					this.FrameStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the draw red in procent. e.g. 10%
		/// </summary>
		/// <value>The draw red in procent.</value>
		public string DrawRedInProcent
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:red",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:red",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("red", value, "draw");
				this._node.SelectSingleNode("@draw:red",
					this.FrameStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the draw green in procent. e.g 10%
		/// </summary>
		/// <value>The draw green in procent.</value>
		public string DrawGreenInProcent
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:green",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:green",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("green", value, "draw");
				this._node.SelectSingleNode("@draw:green",
					this.FrameStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the draw blue in procent. e.g 0%
		/// </summary>
		/// <value>The draw blue in procent.</value>
		public string DrawBlueInProcent
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:blue",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:blue",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("blue", value, "draw");
				this._node.SelectSingleNode("@draw:blue",
					this.FrameStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the draw gamma in procent. e.g. 100%
		/// </summary>
		/// <value>The draw gamma in procent.</value>
		public string DrawGammaInProcent
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:gamma",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:gamma",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("gamma", value, "draw");
				this._node.SelectSingleNode("@draw:gamma",
					this.FrameStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [color inversion].
		/// </summary>
		/// <value><c>true</c> if [color inversion]; otherwise, <c>false</c>.</value>
		public bool ColorInversion
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:color-inversion",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return Convert.ToBoolean(xn.InnerText);
				return false;
			}
			set
			{
				string val = (value)?"true":"false";
				XmlNode xn = this._node.SelectSingleNode("@draw:color-inversion",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("color-inversion", val, "draw");
				this._node.SelectSingleNode("@draw:color-inversion",
					this.FrameStyle.Document.NamespaceManager).InnerText = val;
			}
		}

		/// <summary>
		/// Gets or sets the image opacity in procent. e.g. 100%
		/// </summary>
		/// <value>The image opacity in procent.</value>
		public string ImageOpacityInProcent
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:image-opacity",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:image-opacity",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("image-opacity", value, "draw");
				this._node.SelectSingleNode("@draw:image-opacity",
					this.FrameStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the color mode. e.g. standard
		/// </summary>
		/// <value>The color mode.</value>
		public string ColorMode
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:color-mode",
					this.FrameStyle.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:color-mode",
					this.FrameStyle.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("color-mode", value, "draw");
				this._node.SelectSingleNode("@draw:color-mode",
					this.FrameStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicProperties"/> class.
		/// </summary>
		/// <param name="framestyle">The framestyle.</param>
		public GraphicProperties(FrameStyle framestyle)
		{
			this.FrameStyle			= framestyle;
			this.NewXmlNode(framestyle.Document);
			this.InitStandardImplemenation();
		}

		/// <summary>
		/// Inits the standard implemenation.
		/// </summary>
		private void InitStandardImplemenation()
		{
			this.Clip					= "rect(0cm 0cm 0cm 0cm)";
			this.ColorInversion			= false;
			this.ColorMode				= "standard";
			this.ContrastInProcent		= "0%";
			this.DrawBlueInProcent		= "0%";
			this.DrawGammaInProcent		= "100%";
			this.DrawGreenInProcent		= "0%";
			this.DrawRedInProcent		= "0%";
			this.HorizontalPosition		= "center";
			this.HorizontalRelative		= "paragraph";
			this.ImageOpacityInProcent	= "100%";
			this.LuminanceInProcent		= "0%";
			this.Mirror					= "none";
		}

		/// <summary>
		/// Create the XmlNode which represent the propertie element.
		/// </summary>
		/// <param name="td">The TextDocument</param>
		private void NewXmlNode(TextDocument td)
		{
			this.Node		= td.CreateNode("graphic-properties", "style");
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.FrameStyle.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		#region IProperty Member
		private XmlNode _node;
		/// <summary>
		/// The XmlNode which represent the property element.
		/// </summary>
		/// <value></value>
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

		#endregion
	}
}

/*
 * $Log: GraphicProperties.cs,v $
 * Revision 1.3  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.2  2005/10/22 10:47:41  larsbm
 * - add graphic support
 *
 * Revision 1.1  2005/10/17 19:32:47  larsbm
 * - start vers. 1.0.3.0
 * - add frame, framestyle, graphic, graphicproperties
 *
 */