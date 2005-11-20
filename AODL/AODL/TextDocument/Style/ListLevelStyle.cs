/*
 * $Id: ListLevelStyle.cs,v 1.2 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style.Properties;

namespace AODL.TextDocument.Style
{
	/// <summary>
	/// Represent the list level style.
	/// </summary>
	public class ListLevelStyle : IStyle
	{
		private ListStyle _liststyle;
		/// <summary>
		/// The ListStyle to this object belongs.
		/// </summary>
		public ListStyle ListStyle
		{
			get { return this._liststyle; }
			set { this._liststyle = value; }
		}

		private ListLevelProperties _listlevelproperties;
		/// <summary>
		/// The list level properties.
		/// </summary>
		public ListLevelProperties ListlevelProperties
		{
			get { return this._listlevelproperties; }
			set 
			{ 
				this._listlevelproperties = value;
				if(this.Node.ChildNodes.Count != 0)
				{
					XmlNode xn	= this.Node.ChildNodes.Item(0);
					this.Node.RemoveChild(xn);
				}
				this.Node.AppendChild(((ListLevelProperties)value).Node);
			}
		}

		private TextProperties _textproperties;
		/// <summary>
		/// Gets or sets the text properties.
		/// </summary>
		/// <value>The text properties.</value>
		public TextProperties TextProperties
		{
			get { return this._textproperties; }
			set 
			{ 
				this._textproperties = value;
//				if(this.Node.ChildNodes.Count != 0)
//				{
//					XmlNode xn	= this.Node.ChildNodes.Item(0);
//					this.Node.RemoveChild(xn);
//				}
//				this.Node.AppendChild(((TextProperties)value).Node);
			}
		}

		/// <summary>
		/// Create a new ListLevelStyle object.
		/// </summary>
		public ListLevelStyle(ListStyle style, ListStyles typ, int level)
		{
			this.ListStyle				= style;
			this.Document				= style.Document;
			this.NewXmlNode(style.Document, typ, level);
			//TODO: Set spacebefore as propertie
			double spacebefore			= 0.635;
			spacebefore					*= level;
			string space				= spacebefore.ToString().Replace(",",".")+"cm";
			//TODO: Set minlabelwidth as propertie
			string minlabelwidth		= "0.635cm";
			this.ListlevelProperties	= new ListLevelProperties(this, space, minlabelwidth);
		}

		/// <summary>
		/// Create the XmlNode that represent the Style element.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		/// <param name="typ">The style name.</param>
		/// <param name="level">The level number which represent this style.</param>
		private void NewXmlNode(TextDocument td, ListStyles typ, int level)
		{
			XmlAttribute xa		= null;
			if(typ == ListStyles.Bullet)
			{
				this.Node		= td.CreateNode("list-level-style-bullet", "text");

				xa				= td.CreateAttribute("style-name", "text");
				xa.Value		= "Bullet_20_Symbols";
				this.Node.Attributes.Append(xa);

				xa				= td.CreateAttribute("bullet-char", "text");
				xa.Value		= "\u2022";
				this.Node.Attributes.Append(xa);

				this.AddTextPropertie();
			}
			else if(typ == ListStyles.Number)
			{
				this.Node		= td.CreateNode("list-level-style-number", "text");

				xa				= td.CreateAttribute("style-name", "text");
				xa.Value		= "Numbering_20_Symbols";
				this.Node.Attributes.Append(xa);

				xa				= td.CreateAttribute("num-format", "style");
				xa.Value		= "1";
				this.Node.Attributes.Append(xa);				
			}
			else
				throw new Exception("Unknown ListStyles typ");

			xa				= td.CreateAttribute("level", "text");
			xa.Value		= level.ToString();
			this.Node.Attributes.Append(xa);

			xa				= td.CreateAttribute("num-suffix", "style");
			xa.Value		= ".";
			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// Add the TextPropertie, necessary if the list is a bullet list.
		/// </summary>
		private void AddTextPropertie()
		{
			this.TextProperties				= new TextProperties(this);
			this.TextProperties.FontName	= "StarSymbol";

			this.Node.AppendChild(this.TextProperties.Node);
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

		private string _stylename;
		/// <summary>
		/// The style name
		/// </summary>
		public string Name
		{
			get
			{
				return this._stylename;
			}
			set
			{
				//TODO: Map attribute
				this._stylename = value;
			}
		}

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to this ListLevelStyle object belongs.
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

		#endregion
	}
}

/*
 * $Log: ListLevelStyle.cs,v $
 * Revision 1.2  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.1  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 */