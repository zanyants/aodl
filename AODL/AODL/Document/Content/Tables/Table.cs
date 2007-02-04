/*
 * $Id: Table.cs,v 1.5 2007/02/04 22:52:57 larsbm Exp $
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
using AODL.Document.Styles;
using AODL.Document.Content;

namespace AODL.Document.Content.Tables
{
	/// <summary>
	/// Table represent a table that is used within a spreadsheet document
	/// or a TextDocument! 
	/// </summary>
	public class Table : IContent, IHtml
	{
		private RowHeader _rowHeader;
		/// <summary>
		/// Gets or sets the row header.
		/// </summary>
		/// <value>The row header.</value>
		public RowHeader RowHeader
		{
			get { return this._rowHeader; }
			set { this._rowHeader = value; }
		}

		/// <summary>
		/// Gets or sets the table style.
		/// </summary>
		/// <value>The table style.</value>
		public TableStyle TableStyle
		{
			get { return (TableStyle)this.Style; }
			set { this.Style = value; }
		}

		private RowCollection _rowCollection;
		/// <summary>
		/// Gets or sets the row collection.
		/// </summary>
		/// <value>The row collection.</value>
		public RowCollection RowCollection
		{
			get { return this._rowCollection; }
			set { this._rowCollection = value; }
		}

		private ColumnCollection _columnCollection;
		/// <summary>
		/// Gets or sets the column collection.
		/// </summary>
		/// <value>The column collection.</value>
		public ColumnCollection ColumnCollection
		{
			get { return this._columnCollection; }
			set { this._columnCollection = value; }
		}

		/// <summary>
		/// Gets or sets the name of the table.
		/// </summary>
		/// <value>The name of the table.</value>
		public string TableName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@table:name",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@table:name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("name", value, "table");
				this._node.SelectSingleNode("@table:name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Table"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public Table(IDocument document, XmlNode node)
		{
			this.Document					= document;
			this.Node						= node;

			this.RowCollection				= new RowCollection();
			this.ColumnCollection			= new ColumnCollection();

			Row.OnRowChanged				+=new AODL.Document.Content.Tables.Row.RowChanged(Row_OnRowChanged);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Table"/> class.
		/// </summary>
		/// <param name="document">The spreadsheet document.</param>
		/// <param name="name">The name.</param>
		/// <param name="styleName">Name of the style.</param>
		public Table(IDocument document, string name, string styleName)
		{
			this.Document		= document;
			this.NewXmlNode(name, styleName);
			this.TableStyle					= new TableStyle(this.Document, styleName);	
			this.Document.Styles.Add(this.TableStyle);

			this.RowCollection				= new RowCollection();
			this.ColumnCollection			= new ColumnCollection();

			Row.OnRowChanged				+=new AODL.Document.Content.Tables.Row.RowChanged(Row_OnRowChanged);
		}

		/// <summary>
		/// Create a new cell within this table which use the standard style.
		/// The cell isn't part of the table until you insert it
		/// via the InsertCellAt(int rowIndex, int columnIndex, Cell cell)
		/// </summary>
		/// <returns>The new cell</returns>
		public Cell CreateCell()
		{
			Cell cell			= new Cell(this);
			return cell;
		}

		/// <summary>
		/// Create a new cell within this table which will have a custom
		/// cellstyle.
		/// The cell isn't part of the table until you insert it
		/// via the InsertCellAt(int rowIndex, int columnIndex, Cell cell)
		/// </summary>
		/// <param name="styleName">Name of the style.</param>
		/// <returns>The new cell.</returns>
		public Cell CreateCell(string styleName)
		{
			Cell cell			= new Cell(this, styleName);
			return cell;
		}

		/// <summary>
		/// Create a new cell within this table which will have a custom
		/// cellstyle and the cell content is type of the given office value typ.
		/// The cell isn't part of the table until you insert it
		/// via the InsertCellAt(int rowIndex, int columnIndex, Cell cell)
		/// </summary>
		/// <param name="styleName">Name of the style.</param>
		/// <param name="officeValueType">Type of the office value.</param>
		/// <returns></returns>
		public Cell CreateCell(string styleName, string officeValueType)
		{
			Cell cell			= new Cell(this, styleName, officeValueType);
			return cell;
		}

		/// <summary>
		/// Inserts the cell at the specified position. Both indexes are zero
		/// based indexes!
		/// The RowCollection, the rows CellCollection and the ColumnCollection
		/// will be resized automatically.
		/// </summary>
		/// <param name="rowIndex">Index of the row.</param>
		/// <param name="columnIndex">Index of the column.</param>
		/// <param name="cell">The cell.</param>
		public void InsertCellAt(int rowIndex, int columnIndex, Cell cell)
		{
			if(this.RowCollection.Count <= rowIndex)
			{
				for(int i=0; i < rowIndex-this.RowCollection.Count; i++)
					this.RowCollection.Add(new Row(this, "row"+this.RowCollection.Count.ToString()+i.ToString()));

				this.RowCollection[rowIndex-1].InsertCellAt(columnIndex-1, cell);
				cell.Row				= this.RowCollection[rowIndex-1];
			}
			else if(this.RowCollection.Count+1 == rowIndex)
			{
				Row row					= new Row(this, this.RowCollection[this.RowCollection.Count-1].StyleName);
				row.InsertCellAt(columnIndex-1, cell);
				cell.Row				= row;
				this.RowCollection.Add(row);
			}
			else
			{
				this.RowCollection[rowIndex-1].InsertCellAt(columnIndex-1, cell);
				cell.Row				= this.RowCollection[rowIndex-1];
			}
		}

		/// <summary>
		/// Create a new Xml node.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="styleName">Name of the style.</param>
		private void NewXmlNode(string name, string styleName)
		{
			this.Node		= this.Document.CreateNode("table", "table");

			XmlAttribute xa = this.Document.CreateAttribute("style-name", "table");
			xa.Value		= styleName;
			this.Node.Attributes.Append(xa);

			xa = this.Document.CreateAttribute("name", "table");
			xa.Value		= name;
			this.Node.Attributes.Append(xa);
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

		/// <summary>
		/// Builds the node.
		/// </summary>
		/// <returns></returns>
		public XmlNode BuildNode()
		{
			foreach(Column column in this.ColumnCollection)
				this.Node.AppendChild(column.Node);

			if(this.RowHeader != null)
				this.Node.AppendChild(this.RowHeader.Node);

			foreach(Row row in this.RowCollection)
			{
				//check for nested tables
				foreach(Cell cell in row.CellCollection)
					foreach(IContent iContent in cell.Content)
						if(iContent is Table)
							((Table)iContent).BuildNode();
				//now, append the row node
				this.Node.AppendChild(row.Node);
			}

			return this.Node;
		}

		/// <summary>
		/// Resets the table node.
		/// </summary>
		public void Reset()
		{
			string name			= this.TableName;
			string styleName	= this.StyleName;

			this.Node.RemoveAll();
			this.NewXmlNode(name, styleName);
		}

		#region Eventhandling
		/// <summary>
		/// After a row size changed all rows before the changed row
		/// maybe need to be resized. This also belongs to the columns.
		/// </summary>
		/// <param name="rowNumber">The row number.</param>
		/// <param name="cellCount">The cell count.</param>
		private void Row_OnRowChanged(int rowNumber, int cellCount)
		{
			if(this.ColumnCollection.Count <= cellCount)
			{
				for(int i=0; i <= cellCount-this.ColumnCollection.Count; i++)
					this.ColumnCollection.Add(new Column(this, "col"+this.ColumnCollection.Count.ToString()+i.ToString()));
			}

			for(int i=0; i<rowNumber; i++)
			{
				if(this.RowCollection[i].CellCollection.Count < cellCount)
				{
					for(int ii=cellCount-this.RowCollection[i].CellCollection.Count; ii<cellCount; ii++);
						this.RowCollection[i].CellCollection.Add(new Cell(this));
				}
			}
		}
		#endregion

		#region IContent Member

		/// <summary>
		/// Gets or sets the name of the style.
		/// </summary>
		/// <value>The name of the style.</value>
		public string StyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@table:style-name",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@table:style-name",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("style-name", value, "table");
				this._node.SelectSingleNode("@table:style-name",
					this.Document.NamespaceManager).InnerText = value;
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

		private IStyle _style;
		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this.StyleName	= value.StyleName;
				this._style = value;
			}
		}

		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node
		{
			get { return this._node; }
			set { this._node = value; }
		}

		#endregion

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			bool isRightAlign	= false;
			string outerHtml	= "<table border=0 cellspacing=0 cellpadding=0 border=0 style=\"width: 16.55cm; \">\n\n<tr>\n<td align=right>\n";
			string html			= "<table hspace=\"14\" vspace=\"14\" cellpadding=\"2\" cellspacing=\"2\" border=\"0\" bgcolor=\"#000000\" ";
			string htmlRight	= "<table cellpadding=\"2\" cellspacing=\"2\" border=\"0\" bgcolor=\"#000000\" ";

			if(this.TableStyle.TableProperties != null)
			{				
				if(this.TableStyle.TableProperties.Align != null)
				{
					string align		= this.TableStyle.TableProperties.Align.ToLower();
					if(align == "right")
					{
						isRightAlign	= true;
						html			= htmlRight;
					}
					else if(align == "margin")
						align			= "left";
					html		+= " align=\""+align+"\" ";

				}
				html			+= this.TableStyle.TableProperties.GetHtmlStyle();
			}

			html				+= ">\n";

			if(this.RowHeader != null)
				html			+= this.RowHeader.GetHtml();

			foreach(Row	row in this.RowCollection)
				html		+= row.GetHtml()+"\n";

			html				+= "</table>\n";

			//Wrapp a right align table with outer table,
			//because following content will be right to
			//the table!
			if(isRightAlign)
				html			= outerHtml + html + "\n</td>\n</tr>\n</table>\n";

			return html;
		}

		#endregion
	}
}

/*
 * $Log: Table.cs,v $
 * Revision 1.5  2007/02/04 22:52:57  larsbm
 * - fixed bug in resize algorithm for rows and cells
 * - extending IDocument, overload SaveTo to accept external exporter impl.
 * - initial version of AODL PDF exporter add on
 *
 * Revision 1.4  2006/02/08 16:37:36  larsbm
 * - nested table test
 * - AODC spreadsheet
 *
 * Revision 1.3  2006/02/05 20:02:25  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */