/*
 * $Id: CellSpan.cs,v 1.2 2006/01/05 10:31:10 larsbm Exp $
 */

using System;
using System.Xml;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// CellSpan used when cells are merged.
	/// </summary>
	public class CellSpan
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
			this.Node		= td.CreateNode("covered-table-cell", "table");
		}

		/// <summary>
		/// The Xml node
		/// </summary>
		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
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
	}
}

/*
 * $Log: CellSpan.cs,v $
 * Revision 1.2  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 * Revision 1.1  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 */
