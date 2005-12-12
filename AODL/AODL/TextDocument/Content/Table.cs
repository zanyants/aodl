/*
 * $Id: Table.cs,v 1.5 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Zusammenfassung für Table.
	/// </summary>
	public class Table : IContent, IContentContainer, IHtml
	{
		/// <summary>
		/// The first row is a header row.
		/// </summary>
		private bool _firstRowIsHeader	= false;

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
		/// Inits the rows and columns for this instance.
		/// </summary>
		internal void Init()
		{
			this.Columns			= new ColumnCollection();
			this.Rows				= new RowCollection();

			this.Columns.Inserted	+=new AODL.Collections.CollectionWithEvents.CollectionChange(Columns_Inserted);
			this.Rows.Inserted		+=new AODL.Collections.CollectionWithEvents.CollectionChange(Rows_Inserted);
		}
		
		/// <summary>
		/// Appends the content node.
		/// </summary>
		/// <param name="node">The node.</param>
		internal void AppendContentNode(XmlNode node)
		{
			if(this.Node != null)
				this.Node.AppendChild(node);
		}

		/// <summary>
		/// Initiates the table with given count of rows and columns
		/// and the given width (width is cm).
		/// </summary>
		/// <param name="rows">Count of rows</param>
		/// <param name="columns">Count of columns</param>
		/// <param name="width">Width of the table e.g 16.99</param>
		public void Init(int rows, int columns, double width)
		{
			((TableStyle)this.Style).Properties.Width	= width.ToString("F2")+"cm";
			this.Columns								= new ColumnCollection();
			this.Rows									= new RowCollection();

			this.Columns.Inserted	+=new AODL.Collections.CollectionWithEvents.CollectionChange(Columns_Inserted);
			this.Rows.Inserted		+=new AODL.Collections.CollectionWithEvents.CollectionChange(Rows_Inserted);

			this.AddColumns(columns, width);
			if(this._firstRowIsHeader)
				this.Node.AppendChild(this.RowHeader.Node);
			this.AddRows(rows, columns);
		}

		/// <summary>
		/// Initiates the table with given count of rows and columns
		/// and the given width (width is cm) and set first row as
		/// header row if firstHeaderRow is set to true.
		/// </summary>
		/// <param name="rows">The rows.</param>
		/// <param name="columns">The columns.</param>
		/// <param name="width">The width.</param>
		/// <param name="firstRowIsHeader">if set to <c>true</c> [first row is header].</param>
		public void Init(int rows, int columns, double width, bool firstRowIsHeader)
		{
			this._firstRowIsHeader	= firstRowIsHeader;
			this.RowHeader			= new RowHeader(this);			
			this.Init(rows, columns, width);
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
				//this.Node.AppendChild(c.Node);
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
				bool rowHeader	= false;
				int irow		= i+1;
				Row r			= new Row(this, this.Stylename+"."+irow.ToString());
				
				//If first row is a header row
				if(this._firstRowIsHeader && i==0)
				{
					this.RowHeader.RowCollection.Add(r);
					rowHeader	= true;
				}

				if(!rowHeader)
					this.Rows.Add(r);
					

				for(int ii=0; ii<cells; ii++)
				{
					int icell		= ii+1;
					Cell c			= new Cell(r, 
										this.Stylename+"."		//tablename
										+GetChar(ii).ToString() //column char
										+irow.ToString()		//row number
										+icell.ToString());		//cell number
					((CellStyle)c.Style).CellProperties.Border = "0.002cm solid #000000";
					r.Cells.Add(c);
				}
				//this.Node.AppendChild(r.Node);
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

		/// <summary>
		/// Get a char from the alphabet at the given position.
		/// </summary>
		/// <param name="number">The position</param>
		/// <returns>The char</returns>
		public static char GetChar(int number)
		{
			//TODO: Complete alphabet for more than 26 entries
			char[] alpha = new char[]
				{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};

			if(number < alpha.Length)
				return alpha[number];
			return 'Z';
		}

		/// <summary>
		/// Columns_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Columns_Inserted(int index, object value)
		{
			if(this.Node != null)
				this.Node.AppendChild(((Column)value).Node);
		}

		/// <summary>
		/// Rows_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Rows_Inserted(int index, object value)
		{
			if(this.Node != null)
				this.Node.AppendChild(((Row)value).Node);
		}

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string html			= "<table hspace=\"14\" vspace=\"14\" cellpadding=\"2\" cellspacing=\"1\" border=\"0\" bgcolor=\"#000000\" ";

			if(((TableStyle)this.Style).Properties != null)
				html			+= ((TableStyle)this.Style).Properties.GetHtmlStyle();

			html				+= ">\n";

			if(this.RowHeader != null)
				html			+= this.RowHeader.GetHtml();

			foreach(Row	row in this.Rows)
					html		+= row.GetHtml()+"\n";

			html				+= "</table>\n";

			return html;
		}

		#endregion
	}
}

/*
 * $Log: Table.cs,v $
 * Revision 1.5  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.4  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.3  2005/10/16 08:36:29  larsbm
 * - Fixed bug [ 1327809 ] Invalid Cast Exception while insert table with cells that contains lists
 * - Fixed bug [ 1327820 ] Cell styles run into loop
 *
 * Revision 1.2  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 * Revision 1.1  2005/10/12 19:52:10  larsbm
 * - start table implementation
 * - added uml diagramm
 *
 */