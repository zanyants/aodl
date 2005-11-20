/*
 * $Id: ListLevelProperties.cs,v 1.2 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using System.Xml;

namespace AODL.TextDocument.Style.Properties
{
	/// <summary>
	/// Represent the properties of the list levels.
	/// </summary>
	public class ListLevelProperties : IProperty
	{
		private IStyle _style;
		/// <summary>
		/// The style to this ListLevelProperties object belongs.
		/// </summary>
		public IStyle Style
		{
			get { return this._style; }
			set { this._style = value; }
		}

		/// <summary>
		/// Constructor create a new ListLevelProperties object.
		/// </summary>
		public ListLevelProperties(IStyle style, string spacebefore, string minlabelwidth)
		{
			this.Style		= style;
			this.NewXmlNode(style.Document, spacebefore, minlabelwidth);
		}

		/// <summary>
		/// Create the XmlNode which represent the propertie element.
		/// </summary>
		/// <param name="td">The TextDocument</param>
		/// <param name="spacebefore">The space before the list item</param>
		/// <param name="minlabelwidth">The minimal label width</param>
		private void NewXmlNode(TextDocument td, string spacebefore, string minlabelwidth)
		{
			this.Node			= td.CreateNode("list-level-properties", "style");

			XmlAttribute xa		= td.CreateAttribute("space-before", "text");
			xa.Value			= spacebefore;
			this.Node.Attributes.Append(xa);

			xa					= td.CreateAttribute("min-label-width", "text");
			xa.Value			= minlabelwidth;
			this.Node.Attributes.Append(xa);
		}

		#region IProperty Member

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

		#endregion
	}
}
