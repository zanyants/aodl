/*
 * $Id: FrameStyle.cs,v 1.1 2005/10/17 19:32:47 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style.Properties;

namespace AODL.TextDocument.Style
{
	/// <summary>
	/// Zusammenfassung für FrameStyle.
	/// </summary>
	public class FrameStyle : IStyle
	{
		private GraphicProperties _graphicproperties;
		/// <summary>
		/// Gets or sets the graphic properties.
		/// </summary>
		/// <value>The graphic properties.</value>
		public GraphicProperties GraphicProperties
		{
			get { return this._graphicproperties; }
			set { this._graphicproperties = value; }
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
		/// Initializes a new instance of the <see cref="FrameStyle"/> class.
		/// </summary>
		/// <param name="frame">The frame.</param>
		/// <param name="stylename">The stylename.</param>
		public FrameStyle(Frame frame, string stylename)
		{
			this.Frame				= frame;
			this.Document			= frame.Document;
			this.GraphicProperties	= new GraphicProperties(this);
			this.NewXmlNode(stylename);
		}

		/// <summary>
		/// Create the XmlNode that represent this element.
		/// </summary>
		/// <param name="name">The style name.</param>
		private void NewXmlNode(string name)
		{			
			this.Node		= this.Document.CreateNode("style", "style");

			XmlAttribute xa = this.Document.CreateAttribute("name", "style");
			xa.Value		= name;
			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("family", "style");
			xa.Value		= "graphic"; //TODO: Change enum to class (statics)
			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("parent-style-name", "style");
			xa.Value		= "Graphics"; //TODO: Change enum to class (statics)
			this.Node.Attributes.Append(xa);
		}

		#region IStyle Member

		private XmlNode _node;
		/// <summary>
		/// The XmlNode.
		/// </summary>
		/// <value>The Node</value>
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

		/// <summary>
		/// The style name.
		/// </summary>
		/// <value></value>
		public string Name
		{
			get
			{
				return  this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to this object belongs.
		/// </summary>
		/// <value></value>
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

		#endregion
	}
}

/*
 * $Log: FrameStyle.cs,v $
 * Revision 1.1  2005/10/17 19:32:47  larsbm
 * - start vers. 1.0.3.0
 * - add frame, framestyle, graphic, graphicproperties
 *
 */