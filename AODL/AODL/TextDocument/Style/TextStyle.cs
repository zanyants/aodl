using System;
using System.Xml;
using AODL.TextDocument.Style.Properties;
using AODL.TextDocument;
using AODL.TextDocument.Content;

namespace AODL.TextDocument.Style
{
	/// <summary>
	/// Represent the style for a FormatedText object.
	/// </summary>
	public class TextStyle : IStyle, IFamilyStyle
	{
		private FormatedText _formatedText;
		/// <summary>
		/// The FormatedText object to this object belongs.
		/// </summary>
		public FormatedText FormatedText
		{
			get { return this._formatedText; }
			set { this._formatedText = value; }
		}

		/// <summary>
		/// Create a TextStyle object.
		/// </summary>
		/// <param name="ft">The FormatedText object to this object belongs.</param>
		/// <param name="name">The style name.</param>
		public TextStyle(FormatedText ft, string name)
		{
			this.FormatedText	= ft;
			this.Document		= ft.Content.Document;
			this.Properties		= (IProperty)new TextProperties(this);
			this.NewXmlNode(ft.Content.Document, name);
			this.Node.AppendChild(this.Properties.Node);
		}

		/// <summary>
		/// Create the XmlNode that represent the Style element.
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
			xa.Value		= FamiliyStyles.text.ToString();
			this.Node.Attributes.Append(xa);
		}

		#region IStyle Member

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
		/// The style name.
		/// </summary>
		public string Name
		{
			get
			{	
				return this._node.SelectSingleNode("@style:name", 
					this.FormatedText.Content.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:name", 
					this.FormatedText.Content.Document.NamespaceManager).InnerText = value.ToString();
			}
		}

		private IProperty _properties;
		/// <summary>
		/// The IProperty object that is linked to this object.
		/// </summary>
		public AODL.TextDocument.Style.Properties.IProperty Properties
		{
			get
			{
				return this._properties;
			}
			set
			{
				this._properties = value;
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
		/// The family tyle to this object belongs.
		/// </summary>
		public string Family
		{
			get
			{	
				return this._node.SelectSingleNode("@style:family", 
					this.FormatedText.Content.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:family", 
					this.FormatedText.Content.Document.NamespaceManager).InnerText = value.ToString();
			}
		}

		#endregion
	}
}
