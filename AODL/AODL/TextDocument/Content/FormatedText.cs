/*
 * $Id: FormatedText.cs,v 1.4 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a formated Text e.g bold, italic, underline etc.
	/// </summary>
	public class FormatedText : IText, IHtml
	{
		private FormatedTextCollection _formatedtextcollecion;
		/// <summary>
		/// Gets or sets the formated text collection.
		/// </summary>
		/// <value>The formated text collection.</value>
		public FormatedTextCollection FormatedTextCollection
		{
			get { return this._formatedtextcollecion; }
			set { this._formatedtextcollecion = value; }
		}

		/// <summary>
		/// Empty default constructor.
		/// </summary>
		internal FormatedText()
		{			
		}

		/// <summary>
		/// Overloaded constructor.
		/// </summary>
		/// <param name="content">The content object to which the formated text belongs to.</param>
		/// <param name="name">The stylename which should be referenced with this FormatedText object.</param>
		/// <param name="text">The Displaytext.</param>
		public FormatedText(IContent content, string name, string text)
		{
			this.NewXmlNode(content.Document, name);

			this.Content	= content;
			this.Text		= text;
			this.Style		= (IStyle)new TextStyle(this, name);

			this._formatedtextcollecion				= new FormatedTextCollection();
			this._formatedtextcollecion.Inserted	+= new AODL.Collections.CollectionWithEvents.CollectionChange(_formatedtextcollecion_Inserted);
			this._formatedtextcollecion.Removed		+=new AODL.Collections.CollectionWithEvents.CollectionChange(_formatedtextcollecion_Removed);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		/// <param name="stylename">The stylename which should be referenced with this FormatedText.</param>
		private void NewXmlNode(TextDocument td, string stylename)
		{			
			this.Node		= td.CreateNode("span", "text");
			XmlAttribute xa = td.CreateAttribute("style-name", "text");
			xa.Value		= stylename;

			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// Transform control character like \n, \t into
		/// their xml pendants.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>The transformed text</returns>
		private string ControlCharTransformer(string text)
		{
//			text		= text.Replace("&", "&amp;");
//			text		= text.Replace("<", "&lt;");
//			text		= text.Replace(">", "&gt;");
//
//			text		= text.Replace(@"\n", "<text:line-break xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");
//			text		= text.Replace(@"\t", "<text:tab xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");
//
//			text		= WhiteSpace.GetWhiteSpaceXml(text);

			return TextContentSpecialCharacter.ReplaceSpecialCharacter(text);
		}

		#region IText Member

		private XmlNode _node;
		/// <summary>
		/// The XmlNode.
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
		/// The Displaytext.
		/// </summary>
		public string Text
		{
			get
			{
				return this.Node.InnerXml;
			}
			set
			{
				//this.Node.InnerXml = value.Replace(@"\n", "<text:line-break xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");
				this.Node.InnerXml	= this.ControlCharTransformer(value);
			}
		}

		/// <summary>
		/// Returns the Displaytext with embbed XmlNode.
		/// </summary>
		public string Xml
		{
			get
			{
				return this.Node.OuterXml;
			}			
		}

		private IStyle _style;
		/// <summary>
		/// The IStyle object which is referenced with the FormatedText object.
		/// </summary>
		public AODL.TextDocument.Style.IStyle Style
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

		private IContent _content;
		/// <summary>
		/// The IContent object the FormatedText object belongs to.
		/// </summary>
		public IContent Content
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

		/// <summary>
		/// _formatedtextcollecion_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void _formatedtextcollecion_Inserted(int index, object value)
		{
			this.Node.InnerXml += ((FormatedText)value).Xml;
		}

		/// <summary>
		/// _formatedtextcollecion_s the removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void _formatedtextcollecion_Removed(int index, object value)
		{
			string inner	= this.Node.InnerXml;
			string replace	= ((FormatedText)value).Xml;
			inner			= inner.Replace(replace, "");
			this.Node.InnerXml	= inner;
		}

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string style	= ((TextStyle)this.Style).Properties.GetHtmlStyle();
			string html		= "<span ";
			string text		= this.GetTextWithHtmlControl();

			if(style.Length > 0)
				html		= html + style + ">\n";
			else
				html		+= ">\n";

			if(text.Length > 0)
				html		+= text;

			html			+= "</span>\n";

			html			= this.GetSubOrSupStartTag()+html+this.GetSubOrSupEndTag();

			return html;
		}

		/// <summary>
		/// Gets the text with HTML controls
		/// as Tab as &nbsp; and line-break as br tag
		/// </summary>
		/// <returns>The string</returns>
		private string GetTextWithHtmlControl()
		{
			string text		= "";

			foreach(XmlNode node in this.Node)
			{
				if(node.LocalName == "tab")
					text	+= "&nbsp;&nbsp;&nbsp;";
				else if(node.LocalName == "line-break")
					text	+= "<br>";
				else if(node.LocalName == "s")
					text	+= WhiteSpace.GetWhiteSpaceHtml(node.OuterXml);
				else if(node.InnerText.Length > 0)
					text	+= node.InnerText;
			}

			return text;
		}

		/// <summary>
		/// Gets the sub or sup start tag.
		/// </summary>
		/// <returns></returns>
		private string GetSubOrSupStartTag()
		{
			if(((TextStyle)this.Style).Properties.Position != null)
				if(((TextStyle)this.Style).Properties.Position.Length > 0)
					if(((TextStyle)this.Style).Properties.Position.ToLower().StartsWith("sub"))
						return "<sub>";
					else
						return "<sup>";
			
			return "";
		}

		/// <summary>
		/// Gets the sub or sup end tag.
		/// </summary>
		/// <returns></returns>
		private string GetSubOrSupEndTag()
		{
			if(((TextStyle)this.Style).Properties.Position != null)
				if(((TextStyle)this.Style).Properties.Position.Length > 0)
					if(((TextStyle)this.Style).Properties.Position.ToLower().StartsWith("sub"))
						return "</sub>";
					else
						return "</sup>";

			return "";
		}

		#endregion
	}
}

/*
 * $Log: FormatedText.cs,v $
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
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */