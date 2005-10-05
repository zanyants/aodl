using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a formated Text e.g bold, italic, underline etc.
	/// </summary>
	public class FormatedText : IText
	{
		/// <summary>
		/// Empty default constructor.
		/// </summary>
		public FormatedText()
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
				this.Node.InnerXml = value.Replace(@"\n", "<text:line-break xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");
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
	}
}
