/*
 * $Id: CellSpan.cs,v 1.1 2006/01/29 11:28:22 larsbm Exp $
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

namespace AODL.Document.Content.Tables
{
	/// <summary>
	/// CellSpan used when cells are merged.
	/// </summary>
	public class CellSpan : IContent
	{
		private Row _row;
		/// <summary>
		/// Gets or sets the row.
		/// </summary>
		/// <value>The row.</value>
		public Row Row
		{
			get { return this._row; }
			set { this._row = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CellSpan"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public CellSpan(IDocument document, XmlNode node)
		{
			this.Document			= document;
			this.Node				= node;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CellSpan"/> class.
		/// The CellSpan class is only usable in TextDocuments.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="document">The document.</param>
		public CellSpan(Row row, IDocument document)
		{
			this.Document	= document;
			this.NewXmlNode();
		}

		/// <summary>
		/// News the XML node.
		/// </summary>
		private void NewXmlNode()
		{			
			this.Node		= this.Document.CreateNode("covered-table-cell", "table");
		}

		#region IContent Member

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

		private IDocument _document;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his document.
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

		/// <summary>
		/// The stylename wihich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		/// <value></value>
		public string StyleName
		{
			get
			{
				// TODO:  Getter-Implementierung für CellSpan.StyleName hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für CellSpan.StyleName hinzufügen
			}
		}

		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		/// <value></value>
		public AODL.Document.Styles.IStyle Style
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

		#endregion
	}
}

/*
 * $Log: CellSpan.cs,v $
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
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
