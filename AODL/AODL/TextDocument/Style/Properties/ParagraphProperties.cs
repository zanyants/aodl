/*
 * $Id: ParagraphProperties.cs,v 1.4 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Style.Properties
{
	/// <summary>
	/// Represent access to the possible attributes of of an paragraph-propertie element.
	/// </summary>
	public class ParagraphProperties : IProperty
	{
		private ParagraphStyle _paragraphStyle;
		/// <summary>
		/// The ParagraphStyle object to this object belongs.
		/// </summary>
		public ParagraphStyle Paragraphstyle
		{
			get { return this._paragraphStyle; }
			set { this._paragraphStyle = value; }
		}

		private XmlNode _node;
		/// <summary>
		/// The represented XmlNode.
		/// </summary>
		public XmlNode Node
		{
			get { return this._node; }
			set { this._node = value; }
		}

		/// <summary>
		/// Margin left. in cm an object .MarginLeft = "1cm";
		/// </summary>
		public string MarginLeft
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:margin-left", 
					  this.Paragraphstyle.Content.Document.NamespaceManager) ;
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:margin-left",
					  this.Paragraphstyle.Content.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("margin-left", value, "fo");
				this._node.SelectSingleNode("@fo:margin-left",
					  this.Paragraphstyle.Content.Document.NamespaceManager).InnerText = value;
			}
		}

		private TabStopStyleCollection _tabstopstylecollection;
		/// <summary>
		/// Gets or sets the tab stop style collection.
		/// <b>Notice:</b> A TabStopStyleCollection will not work
		/// within a Standard Paragraph!
		/// </summary>
		/// <value>The tab stop style collection.</value>
		public TabStopStyleCollection TabStopStyleCollection
		{
			get { return this._tabstopstylecollection; }
			set 
			{ 
				if(this.Paragraphstyle.Name == "Standard")
					return;
				if(this._tabstopstylecollection != null)
				{
					//Remove node and reset the collection
					this.Node.RemoveChild(this._tabstopstylecollection.Node);
					this._tabstopstylecollection = null; 
				}
				
				this._tabstopstylecollection	= value;
				this.Node.AppendChild(this._tabstopstylecollection.Node);
			}
		}

		/// <summary>
		/// Set paragraph alignment - object.Alignment = TextAlignments.right.ToString()
		/// </summary>
		public string Alignment
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@fo:text-align",
					this.Paragraphstyle.Content.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@fo:text-align",
					this.Paragraphstyle.Content.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("text-align", value, "fo");
				this._node.SelectSingleNode("@fo:text-align",
					this.Paragraphstyle.Content.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Create a new ParagraphProperties object.
		/// </summary>
		/// <param name="pstyle">The ParagraphStyle object to this object belongs.</param>
		public ParagraphProperties(ParagraphStyle pstyle)
		{
			this.Paragraphstyle = pstyle;
			this.NewXmlNode(pstyle.Content.Document);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ParagraphProperties"/> class.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="node">The node.</param>
		public ParagraphProperties(ParagraphStyle style, XmlNode node)
		{
			this.Paragraphstyle		= style;
			this.Node				= node;
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Paragraphstyle.Content.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// Create the XmlNode.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		private void NewXmlNode(TextDocument td)
		{
			this.Node		= td.CreateNode("paragraph-properties", "style");
		}
	}
}

/*
 * $Log: ParagraphProperties.cs,v $
 * Revision 1.4  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.3  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */