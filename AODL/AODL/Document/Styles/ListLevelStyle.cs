/*
 * $Id: ListLevelStyle.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
using System.Xml;
using AODL.Document;
using AODL.Document.Styles.Properties;

namespace AODL.Document.Styles
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

		/// <summary>
		/// Gets or sets the text properties.
		/// </summary>
		/// <value>The text properties.</value>
		public ListLevelProperties ListLevelProperties
		{
			get
			{
				foreach(IProperty property in this.PropertyCollection)
					if(property is ListLevelProperties)
						return (ListLevelProperties)property;
				ListLevelProperties listLevelProperties	= new ListLevelProperties(this);
				this.PropertyCollection.Add((IProperty)listLevelProperties);
				return listLevelProperties;
			}
			set
			{
				if(this.PropertyCollection.Contains((IProperty)value))
					this.PropertyCollection.Remove((IProperty)value);
				this.PropertyCollection.Add(value);
			}
		}

		/// <summary>
		/// Gets or sets the text properties.
		/// </summary>
		/// <value>The text properties.</value>
		public TextProperties TextProperties
		{
			get
			{
				foreach(IProperty property in this.PropertyCollection)
					if(property is TextProperties)
						return (TextProperties)property;
				TextProperties textProperties	= new TextProperties(this);
				this.PropertyCollection.Add((IProperty)textProperties);
				return textProperties;
			}
			set
			{
				if(this.PropertyCollection.Contains((IProperty)value))
					this.PropertyCollection.Remove((IProperty)value);
				this.PropertyCollection.Add(value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ListLevelStyle"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="style">The style.</param>
		/// <param name="typ">The typ.</param>
		/// <param name="level">The level.</param>
		public ListLevelStyle(IDocument document,ListStyle style, ListStyles typ, int level)
		{
			this.Document				= document;			
			this.ListStyle				= style;
			this.InitStandards(level);
			
			this.NewXmlNode(typ, level);
			this.AddListLevel(level);
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards(int level)
		{
			this.PropertyCollection				= new IPropertyCollection();
			this.PropertyCollection.Inserted	+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(PropertyCollection_Inserted);
			this.PropertyCollection.Removed		+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(PropertyCollection_Removed);
			this.Document.Styles.Add(this);
		}

		/// <summary>
		/// Adds the list level.
		/// </summary>
		private void AddListLevel(int level)
		{
			this.ListLevelProperties			= new ListLevelProperties(this);
			double spacebefore					= 0.635;
			spacebefore							*= level;
			string space						= spacebefore.ToString().Replace(",",".")+"cm";
			string minlabelwidth				= "0.635cm";
			this.ListLevelProperties.MinLabelWidth	= minlabelwidth;
			this.ListLevelProperties.SpaceBefore	= space;
		}

		/// <summary>
		/// Create the XmlNode that represent the Style element.
		/// </summary>
		/// <param name="typ">The style name.</param>
		/// <param name="level">The level number which represent this style.</param>
		private void NewXmlNode(ListStyles typ, int level)
		{
			XmlAttribute xa		= null;
			if(typ == ListStyles.Bullet)
			{
				this.Node		= this.Document.CreateNode("list-level-style-bullet", "text");

				xa				= this.Document.CreateAttribute("style-name", "text");
				xa.Value		= "Bullet_20_Symbols";
				this.Node.Attributes.Append(xa);

				xa				= this.Document.CreateAttribute("bullet-char", "text");
				xa.Value		= "\u2022";
				this.Node.Attributes.Append(xa);

				this.AddTextPropertie();
			}
			else if(typ == ListStyles.Number)
			{
				this.Node		= this.Document.CreateNode("list-level-style-number", "text");

				xa				= this.Document.CreateAttribute("style-name", "text");
				xa.Value		= "Numbering_20_Symbols";
				this.Node.Attributes.Append(xa);

				xa				= this.Document.CreateAttribute("num-format", "style");
				xa.Value		= "1";
				this.Node.Attributes.Append(xa);				
			}
			else
				throw new Exception("Unknown ListStyles typ");

			xa				= this.Document.CreateAttribute("level", "text");
			xa.Value		= level.ToString();
			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("num-suffix", "style");
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

		#region IStyle Member

		private XmlNode _node;
		/// <summary>
		/// The Xml node which represent the
		/// style
		/// </summary>
		/// <value></value>
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
		/// The style name
		/// </summary>
		/// <value></value>
		public string StyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:name",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("name", value, "style");
				this._node.SelectSingleNode("@style:name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		private IDocument _document;
		/// <summary>
		/// The document to which this style
		/// belongs.
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

		private IPropertyCollection _propertyCollection;
		/// <summary>
		/// Collection of properties.
		/// </summary>
		/// <value></value>
		public IPropertyCollection PropertyCollection
		{
			get { return this._propertyCollection; }
			set { this._propertyCollection = value; }
		}
		#endregion

		/// <summary>
		/// Properties the collection_ inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void PropertyCollection_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IProperty)value).Node);
		}

		/// <summary>
		/// Properties the collection_ removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void PropertyCollection_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IProperty)value).Node);
		}
	}
}

/*
 * $Log: ListLevelStyle.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
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