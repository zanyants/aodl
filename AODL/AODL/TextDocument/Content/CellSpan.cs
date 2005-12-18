/*
 * $Id: CellSpan.cs,v 1.1 2005/12/18 18:29:46 larsbm Exp $
 */
/*
using System;
using System.Xml;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// CellSpan used when cells are merged.
	/// </summary>
	public class CellSpan : IContent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CellSpan"/> class.
		/// </summary>
		/// <param name="row">The row.</param>
		public CellSpan(Row row)
		{
			this.NewXmlNode(row.Document);
		}

		/// <summary>
		/// News the XML node.
		/// </summary>
		/// <param name="td">The td.</param>
		private void NewXmlNode(TextDocument td)
		{			
			this.Node		= td.CreateNode("table", "covered-table-cell");
		}

		#region IContent Member

		private TextDocument _document;
		public TextDocument Document
		{
			get
			{
				return _document;
			}
			set
			{
				this._document = value;
			}
		}

		private XmlNode _node;
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
		/// The stylename not available
		/// </summary>
		/// <value></value>
		public string Stylename
		{
			get
			{
				// TODO:  Getter-Implementierung für CellSpan.Stylename hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für CellSpan.Stylename hinzufügen
			}
		}

		/// <summary>
		/// A Style not available
		/// </summary>
		/// <value></value>
		public AODL.TextDocument.Style.IStyle Style
		{
			get
			{
				// TODO:  Getter-Implementierung für CellSpan.Style hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für CellSpan.Style hinzufügen
			}
		}

		/// <summary>
		/// CellSpan have no content
		/// </summary>
		/// <value></value>
		public ITextCollection TextContent
		{
			get
			{
				// TODO:  Getter-Implementierung für CellSpan.TextContent hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für CellSpan.TextContent hinzufügen
			}
		}

		#endregion
	}
}
*/
/*
 * $Log: CellSpan.cs,v $
 * Revision 1.1  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 */
