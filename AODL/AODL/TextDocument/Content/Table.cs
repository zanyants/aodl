/*
 * $Id: Table.cs,v 1.1 2005/10/12 19:52:10 larsbm Exp $
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

			this.AddColumns(columns, width);
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
				((ColumnStyle)c.Style).Properties.Width		= colwidth.ToString("F2")+"cm";
				this.Columns.Add(c);
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
 * Revision 1.1  2005/10/12 19:52:10  larsbm
 * - start table implementation
 * - added uml diagramm
 *
 */