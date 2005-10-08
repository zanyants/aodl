/*
 * $Id: ParagraphStyle.cs,v 1.2 2005/10/08 07:55:35 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style.Properties;
using AODL.TextDocument;
using AODL.TextDocument.Content;

namespace AODL.TextDocument.Style
{
	/// <summary>
	/// Represent the style for a Paragraph object.
	/// </summary>
	public class ParagraphStyle : IStyle, IFamilyStyle
	{
		private Paragraph _paragraph;
		/// <summary>
		/// The Paragraph object to this object belongs.
		/// </summary>
		public Paragraph Paragraph
		{
			get { return this._paragraph; }
			set { this._paragraph = value; }
		}

		private IProperty _properties;
		/// <summary>
		/// The IProperties object which is linked with this object.
		/// </summary>
		public IProperty Properties
		{
			get { return this._properties; }
			set { this._properties = value; }
		}

		private string _parentStyle;
		/// <summary>
		/// The parent style of this object.
		/// </summary>
		public string ParentStyle
		{
			get
			{	
				return this._node.SelectSingleNode("@style:parent-style-name", 
					this.Paragraph.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:parent-style-name", 
					this.Paragraph.Document.NamespaceManager).InnerText = value.ToString();
			}
		}

		private TextProperties _textproperties;
		/// <summary>
		/// The TextProperties object to this object belongs.
		/// </summary>
		/// <remarks>
		/// This is optional. Only if used, the Node will attached to the
		/// ParagraphStyle node.
		/// </remarks>
		public TextProperties Textproperties
		{
			get
			{
				if(this._textproperties == null)
				{
					this._textproperties	= new TextProperties(this);
					this.Node.AppendChild(this.Textproperties.Node);
				}
				return this._textproperties;
			}
			set { this._textproperties = value; }
		}

		/// <summary>
		/// Create a new ParagraphStyle object.
		/// </summary>
		/// <param name="p">The Paragraph object to this object belongs.</param>
		/// <param name="name">The style name.</param>
		public ParagraphStyle(Paragraph p, string name)
		{
			this.Paragraph	= p;
			this.Document	= p.Document;
			this.Properties = (IProperty)new ParagraphProperties(this);
			this.NewXmlNode(p.Document, name);
			this.Node.AppendChild(this.Properties.Node);
		}

		/// <summary>
		/// Create the XmlNode that represent this element.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		/// <param name="name">The style name.</param>
		private void NewXmlNode(TextDocument td, string name)
		{			
			this.Node		= td.CreateNode("style", "style");
			XmlAttribute xa = td.CreateAttribute("name", "style");
			xa.Value		= name;
			this.Node.Attributes.Append(xa);
			xa				= td.CreateAttribute("family", "style");
			xa.Value		= FamiliyStyles.paragraph.ToString();
			this.Node.Attributes.Append(xa);
			xa				= td.CreateAttribute("parent-style-name", "style");
			xa.Value		= ParentStyles.Standard.ToString();
			this.Node.Attributes.Append(xa);
		}

		#region IStyle Member

		private XmlNode _node;
		/// <summary>
		/// The XmlNode
		/// </summary>
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
		public string Name
		{
			get
			{	
				return this._node.SelectSingleNode("@style:name", 
					this.Paragraph.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:name", 
					this.Paragraph.Document.NamespaceManager).InnerText = value.ToString();
			}
		}

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to this object belongs.
		/// </summary>
		public TextDocument Document
		{
			get { return this._document; }
			set { this._document = value; }
		}

		#endregion

		#region IFamilyStyle Member
		/// <summary>
		/// The family style.
		/// </summary>
		public string Family
		{
			get
			{	
				return this._node.SelectSingleNode("@style:family", 
					this.Paragraph.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:family", 
					this.Paragraph.Document.NamespaceManager).InnerText = value.ToString();
			}
		}

		#endregion
	}
}

/*
 * $Log: ParagraphStyle.cs,v $
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */