/*
 * $Id: Table.cs,v 1.2 2005/10/15 11:40:31 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Zusammenfassung für Table.
	/// </summary>
	public class Table : IContent, IContentContainer
	{
		private ColumnCollection _columns;
		/// <summary>
		/// The Columns that belong to this belong.
		/// </summary>
		public ColumnCollection Columns
		{
			get { return this._columns; }
			set { this._columns = value; }
		}

		private RowCollection _rows;
		/// <summary>
		/// Gets or sets the rows.
		/// </summary>
		/// <value>The rows.</value>
		public RowCollection Rows
		{
			get { return this._rows; }
			set { this._rows = value; }
		}

		/// <summary>
		/// Constructor, create a new instance of table.
		/// Attention: You have to call the Init() method
		/// to create the complete table with columns and rows.
		/// </summary>
		/// <param name="document">The Document.</param>
		/// <param name="stylename">The style name.</param>
		public Table(TextDocument document, string stylename)
		{
			this.Document		= document;
			this.Content		= new IContentCollection();
			this.NewXmlNode(stylename);
			this.Style			= (TableStyle)new TableStyle(this, stylename);
		}

		/// <summary>
		/// Initiates the table with given count of rows and columns
		/// and the given width (width is cm).
		/// </summary>
		/// <param name="rows"></param>
		/// <param name="columns"></param>
		public void Init(int rows, int columns, double width)
		{
			((TableStyle)this.Style).Properties.Width	= width.ToString("F2")+"cm";
			this.Columns								= new ColumnCollection();
			this.Rows									= new RowCollection();

			this.AddColumns(columns, width);
			this.AddRows(rows, columns);
		}

		/// <summary>
		/// Create the columns for this table.
		/// </summary>
		/// <param name="cnt">The count of columns.</param>
		/// <param name="tablewidth">The table width.</param>
		private void AddColumns(int cnt, double tablewidth)
		{
			double colwidth		= tablewidth/(double)cnt;
			for(int i=0; i<cnt; i++)
			{
				Column c									= new Column(this, this.Stylename+"."+GetChar(i).ToString());
				((ColumnStyle)c.Style).Properties.Width		= colwidth.ToString("F3")+"cm".Replace(",",".");
				this.Columns.Add(c);
				this.Node.AppendChild(c.Node);
			}
		}

		/// <summary>
		/// Adds the rows.
		/// </summary>
		/// <param name="count">The row count.</param>
		/// <param name="cells">The cell count.</param>
		private void AddRows(int count, int cells)
		{
			for(int i=0; i<count; i++)
			{
				int irow		= i+1;
				Row r			= new Row(this, this.Stylename+"."+irow.ToString());
				this.Rows.Add(r);
//				if(i=0)
//				{
					for(int ii=0; ii<cells; ii++)
					{
						int icell		= ii+1;
						Cell c			= new Cell(r, this.Stylename+"."+GetChar(ii).ToString()+icell.ToString());
						((CellStyle)c.Style).CellProperties.Border = "0.002cm solid #000000";
						r.Cells.Add(c);
					}
				this.Node.AppendChild(r.Node);
//				}
//				else if(i=count-1)
//				{
//					//last row
//				}
//				else
//				{
//					//row in the middle
//				}
			}
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="stylename">The stylename which should be referenced with this table.</param>
		private void NewXmlNode(string stylename)
		{			
			this.Node		= this.Document.CreateNode("table", "table");
			XmlAttribute xa = this.Document.CreateAttribute("style-name", "table");
			xa.Value		= stylename;

			this.Node.Attributes.Append(xa);
		}

		#region IContent Member

		private TextDocument _document;
		/// <summary>
		/// The TextDocument object to this table belong.
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

		private XmlNode _node;
		/// <summary>
		/// The XmlNode
		/// </summary>
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
		/// The style name which is referenced whith this table.
		/// </summary>
		public string Stylename
		{
			get
			{
				return this.Style.Name;
			}
			set
			{
				this.Style.Name	= value;
				this._node.SelectSingleNode("@table:style-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// The TableStyle object which belong to this Table.
		/// </summary>
		public IStyle Style
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

		/// <summary>
		/// The TextCollection. Not implemented, table is a 
		/// ContentContainer, use the IContentCollection
		/// to access rows, columns, and cells.
		/// </summary>
		public ITextCollection TextContent
		{
			get
			{
				// TODO:  Getter-Implementierung für Table.TextContent hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für Table.TextContent hinzufügen
			}
		}

		#endregion

		#region IContentContainer Member

		private IContentCollection _content;
		/// <summary>
		/// The IContentCollection. Here find rows, columns and cells.
		/// </summary>
		public IContentCollection Content
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

		public static char GetChar(int number)
		{
			//TODO: Complete alphabet for more than 26 entries
			char[] alpha = new char[]
				{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};

			if(number < alpha.Length)
				return alpha[number];
			return 'Z';
		}
	}
}

/*
 * $Log: Table.cs,v $
 * Revision 1.2  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 * Revision 1.1  2005/10/12 19:52:10  larsbm
 * - start table implementation
 * - added uml diagramm
 *
 */