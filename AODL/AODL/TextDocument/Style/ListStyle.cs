/*
 * $Id: ListStyle.cs,v 1.2 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style.Properties;

namespace AODL.TextDocument.Style
{
	/// <summary>
	/// Represent the ListStyle for a List.
	/// </summary>
	public class ListStyle : IStyle
	{
		private List _list;
		/// <summary>
		/// The List object to this object belongs.
		/// </summary>
		public List List
		{
			get { return this._list; }
			set { this._list = value; }
		}

		private ListLevelStyleCollection _listlevelcollection;
		/// <summary>
		/// The ListLevelStyles which belongs to this object.
		/// </summary>
		public ListLevelStyleCollection ListlevelStyles
		{
			get { return this._listlevelcollection; }
			set { this._listlevelcollection = value; }
		}

		/// <summary>
		/// Create a new ListStyle object.
		/// </summary>
		/// <param name="li">The List</param>
		/// <param name="stylename">The style name</param>
		public ListStyle(List li, string stylename)
		{
			this.List				= li;
			this.Document			= li.Document;
			this.ListlevelStyles	= new ListLevelStyleCollection();
			this.NewXmlNode(li.Document, stylename);
		}

		/// <summary>
		/// Add all possible ListLevelStyle objects automatically.
		/// Throws exception, if there are already ListLevelStyles
		/// which could'nt removed.
		/// </summary>
		/// <param name="typ">The Liststyle bullet, numbered, ..s</param>
		public void AutomaticAddListLevelStyles(ListStyles typ)
		{
			if(this.Node.ChildNodes.Count != 0)
			{
				try
				{
					foreach(XmlNode xn in this.Node.ChildNodes)
						this.Node.RemoveChild(xn);
				}
				catch(Exception ex)
				{
					throw;
				}
			}

			this.ListlevelStyles.Clear();

			for(int i = 1; i <= 10; i++)
				this.ListlevelStyles.Add(new ListLevelStyle(this, typ, i));

			foreach(ListLevelStyle lls in this.ListlevelStyles)
				this.Node.AppendChild(lls.Node);
		}

		/// <summary>
		/// Create the XmlNode that represent this element.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		/// <param name="name">The style name.</param>
		private void NewXmlNode(TextDocument td, string name)
		{			
			this.Node		= td.CreateNode("list-style", "text");
			XmlAttribute xa = td.CreateAttribute("name", "style");
			xa.Value		= name;
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

		private string _stylename;
		/// <summary>
		/// The style name.
		/// </summary>
		public string Name
		{
			get
			{
				return this._stylename;
			}
			set
			{
				//TODO: map style name attribute
				this._stylename = value;
			}
		}

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to this ListStyle belongs.
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
 * $Log: ListStyle.cs,v $
 * Revision 1.2  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.1  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 */