/*
 * $Id: TextDocument.cs,v 1.13 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Xml;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style;
using AODL.TextDocument.Style.Properties;
using AODL.Export;
using AODL.Import;

namespace AODL.TextDocument
{
	/// <summary>
	/// Represent a opendocument text document.
	/// </summary>
	/// <example>
	/// <code>
	/// TextDocument td = new TextDocument();
	/// td.New();
	/// Paragraph p = new Paragraph(td, "P1");
	/// //add text
	/// p.TextContent.Add(new SimpleText(p, "HallO"));
	/// //Add the Paragraph
	/// td.Content.Add((IContent)p);
	/// //Blank para
	/// td.Content.Add(new Paragraph(td, ParentStyles.Standard.ToString()));
	/// // new para
	/// p = new Paragraph(td, "P2");
	/// p.TextContent.Add(new SimpleText(p, "Hallo"));
	/// td.Content.Add(p);
	/// td.SaveTo("parablank.odt");
	/// </code>
	/// </example>
	public class TextDocument : IContentContainer
	{
		private XmlDocument _xmldoc;
		/// <summary>
		/// The xmldocument the textdocument based on.
		/// </summary>
		public XmlDocument XmlDoc
		{
			get { return this._xmldoc; }
			set { this._xmldoc = value; }
		}

		private DocumentStyles _documentStyles;
		/// <summary>
		/// Gets or sets the document styes.
		/// </summary>
		/// <value>The document styes.</value>
		public DocumentStyles DocumentStyles
		{
			get { return this._documentStyles; }
			set { this._documentStyles = value; }
		}

		private DocumentSetting _documentSetting;
		/// <summary>
		/// Gets or sets the document setting.
		/// </summary>
		/// <value>The document setting.</value>
		public DocumentSetting DocumentSetting
		{
			get { return this._documentSetting; }
			set { this._documentSetting = value; }
		}

		private DocumentMetadata _documentMetadata;
		/// <summary>
		/// Gets or sets the document metadata.
		/// </summary>
		/// <value>The document metadata.</value>
		public DocumentMetadata DocumentMetadata
		{
			get { return this._documentMetadata; }
			set { this._documentMetadata = value; }
		}

		private DocumentManifest _documentManifest;
		/// <summary>
		/// Gets or sets the document manifest.
		/// </summary>
		/// <value>The document manifest.</value>
		public DocumentManifest DocumentManifest
		{
			get { return this._documentManifest; }
			set { this._documentManifest = value; }
		}

		private DocumentConfiguration2 _documentConfigurations2;
		/// <summary>
		/// Gets or sets the document configurations2.
		/// </summary>
		/// <value>The document configurations2.</value>
		public DocumentConfiguration2 DocumentConfigurations2
		{
			get { return this._documentConfigurations2; }
			set { this._documentConfigurations2 = value; }
		}

		private DocumentPictureCollection _documentPictures;
		/// <summary>
		/// Gets or sets the document pictures.
		/// </summary>
		/// <value>The document pictures.</value>
		public DocumentPictureCollection DocumentPictures
		{
			get { return this._documentPictures; }
			set { this._documentPictures = value; }
		}

		private DocumentPictureCollection _documentThumbnails;
		/// <summary>
		/// Gets or sets the document thumbnails.
		/// </summary>
		/// <value>The document thumbnails.</value>
		public DocumentPictureCollection DocumentThumbnails
		{
			get { return this._documentThumbnails; }
			set { this._documentThumbnails = value; }
		}

		private string _mimeTyp		= "application/vnd.oasis.opendocument.text";
		/// <summary>
		/// Gets the MIME typ.
		/// </summary>
		/// <value>The MIME typ.</value>
		public string MimeTyp
		{
			get { return this._mimeTyp; }
		}

		private XmlNamespaceManager _namespacemanager;
		/// <summary>
		/// The namespacemanager to access the documents namespace uris.
		/// </summary>
		public XmlNamespaceManager NamespaceManager
		{
			get { return this._namespacemanager; }
			set { this._namespacemanager = value; }
		}

		private IContentCollection _content;
		/// <summary>
		/// The contentcollection of the document. Represents all
		/// data to display.
		/// </summary>
		public IContentCollection Content
		{
			get { return this._content; }
			set { this._content = value; }
		}

		private ArrayList _fontList;
		/// <summary>
		/// Gets or sets the font list.
		/// </summary>
		/// <value>The font list.</value>
		public ArrayList FontList
		{
			get { return this._fontList; }
			set { this._fontList = value; }
		}

		/// <summary>
		/// Create a new TextDocument object.
		/// </summary>
		public TextDocument()
		{
			this.Content			= new IContentCollection();
			this.FontList			= new ArrayList();
			//this.Content.Inserted	+=new AODL.Collections.CollectionWithEvents.CollectionChange(Content_Inserted);
		}

		/// <summary>
		/// Create a blank new document.
		/// </summary>
		public void New()
		{
			this._xmldoc					= new XmlDocument();
			this._xmldoc.LoadXml(TextDocumentHelper.GetBlankDocument());
			this.NamespaceManager			= TextDocumentHelper.NameSpace(this._xmldoc.NameTable);

			this.DocumentConfigurations2	= new DocumentConfiguration2();

			this.DocumentManifest			= new DocumentManifest();
			this.DocumentManifest.New();

			this.DocumentMetadata			= new DocumentMetadata();
			this.DocumentMetadata.New();

			this.DocumentPictures			= new DocumentPictureCollection();

			this.DocumentSetting			= new DocumentSetting();
			this.DocumentSetting.New();

			this.DocumentStyles				= new DocumentStyles();
			this.DocumentStyles.New();

			this.DocumentThumbnails			= new DocumentPictureCollection();
		}

		/// <summary>
		/// Loads the document by using the specified importer.
		/// </summary>
		/// <param name="file">The the file.</param>
		public void Load(string file)
		{
			try
			{
				this._xmldoc					= new XmlDocument();
				this._xmldoc.LoadXml(TextDocumentHelper.GetBlankDocument());

				this.NamespaceManager			= TextDocumentHelper.NameSpace(this._xmldoc.NameTable);

				IImporter importer				= this.GetImporter(file);
				importer.Import(this,file);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Create a new XmlNode for this document.
		/// </summary>
		/// <param name="name">The elementname.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The new XmlNode.</returns>
		public XmlNode CreateNode(string name, string prefix)
		{
			if(this.XmlDoc == null)
				throw new NullReferenceException("There is no XmlDocument loaded. Couldn't create Node "+name+" with Prefix "+prefix+". "+this.GetType().ToString());
			string nuri = this.GetNamespaceUri(prefix);
			return this.XmlDoc.CreateElement(prefix, name, nuri);
		}

		/// <summary>
		/// Create a new XmlAttribute for this document.
		/// </summary>
		/// <param name="name">The attributename.</param>
		/// <param name="prefix">The prefixname.</param>
		/// <returns>The new XmlAttribute.</returns>
		public XmlAttribute CreateAttribute(string name, string prefix)
		{
			if(this.XmlDoc == null)
				throw new NullReferenceException("There is no XmlDocument loaded. Couldn't create Attribue "+name+" with Prefix "+prefix+". "+this.GetType().ToString());
			string nuri = this.GetNamespaceUri(prefix);
			return this.XmlDoc.CreateAttribute(prefix, name, nuri);
		}

		/// <summary>
		/// Save the TextDocument as OpenDocument textdocument.
		/// </summary>
		/// <param name="filename">The filename. With or without full path. Without will save the file to application path!</param>
		public void SaveTo(string filename)
		{
			try
			{
				this.AddIContentCollectionToDocument();
				IExporter exporter			= this.GetExporter(filename);
				exporter.Export(this, filename);
				//Publish.Publisher.PublishTo(this, filename);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Gets the exporter.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <returns></returns>
		private IExporter GetExporter(string filename)
		{
			if(filename.EndsWith(".odt"))
				return new AODL.Export.OpenDocumentTextExporter();

			throw new Exception("Unknown Exporter name exception. No exporter found for file "+filename);
		}

		/// <summary>
		/// Gets the importer.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <returns></returns>
		private IImporter GetImporter(string filename)
		{
			if(filename.EndsWith(".odt"))
				return new OpenDocumentTextImporter();

			throw new Exception("Unkown importer name! No importer found for file "+filename);
		}

		/// <summary>
		/// Adds the I content collection to document.
		/// </summary>
		private void AddIContentCollectionToDocument()
		{
			foreach(string fontname in this.FontList)
				this.AddFont(fontname);
			for(int i=0; i < this.Content.Count; i++)
				this.Content_Inserted(i, this.Content[i]);
		}

		/// <summary>
		/// Adds a font to the document. All fonts that you use
		/// within your text must be added to the document.
		/// The class FontFamilies represent all available fonts.
		/// </summary>
		/// <param name="fontname">The fontname take it from class FontFamilies.</param>
		private void AddFont(string fontname)
		{
			try
			{
				Assembly ass		= Assembly.GetExecutingAssembly();
				Stream stream		= ass.GetManifestResourceStream("AODL.Resources.OD.fonts.xml");

				XmlDocument fontdoc	= new XmlDocument();
				fontdoc.Load(stream);

				XmlNode exfontnode	= this.XmlDoc.SelectSingleNode(
					"/office:document-content/office:font-face-decls/style:font-face[@style:name='"+fontname+"']", this.NamespaceManager);

				if(exfontnode != null)
					return; //Font exist;

				XmlNode newfontnode	= fontdoc.SelectSingleNode(
					"/office:document-content/office:font-face-decls/style:font-face[@style:name='"+fontname+"']", this.NamespaceManager);

				if(newfontnode != null)
				{
					XmlNode fontsnode	= this.XmlDoc.SelectSingleNode(
						"/office:document-content", this.NamespaceManager);
					if(fontsnode != null)
					{
						foreach(XmlNode xn in fontsnode)
							if(xn.Name == "office:font-face-decls")
							{
								XmlNode node		= this.CreateNode("font-face", "style");
								foreach(XmlAttribute xa in newfontnode.Attributes)
								{
									XmlAttribute xanew	= this.CreateAttribute(xa.LocalName, xa.Prefix);
									xanew.Value			= xa.Value;
									node.Attributes.Append(xanew);
								}
								xn.AppendChild(node);
								break;
							}
					}
				}				
			}
			catch(Exception ex)
			{
				//Should never happen
				throw ex;
			}
		}

		/// <summary>
		/// Return the namespaceuri for the given prefixname.
		/// </summary>
		/// <param name="prefix">The prefixname.</param>
		/// <returns></returns>
		private string GetNamespaceUri(string prefix)
		{
			foreach (string prefixx in NamespaceManager)
				if(prefix == prefixx)				
					return NamespaceManager.LookupNamespace(prefixx);
			return null;
		}

		/// <summary>
		/// Insert all XmlNodes of the inserted IContent object into
		/// the current XmlDocument.
		/// </summary>
		/// <param name="index">The index of the inserted IContent object.</param>
		/// <param name="value">The inserted IContent object.</param>
		private void Content_Inserted(int index, object value)
		{
			if(value.GetType().Name == "Table")
			{
				this.InsertTable((Table)value);
				return;
			}

			this.XmlDoc.SelectSingleNode(TextDocumentHelper.OfficeTextPath, 
				this.NamespaceManager).AppendChild(((IContent)value).Node);

			if(((IContent)value).GetType().GetInterface("IContentContainer") != null)
				foreach(IContent content in ((IContentContainer)value).Content)
					if(content.Style != null)
						if(content.Style.Node != null)
							this.AppendStyleNode(content.Style.Node);

			if(((IContent)value).Style != null)
			{
				IStyle style	= ((IContent)value).Style;			
				this.XmlDoc.SelectSingleNode(TextDocumentHelper.AutomaticStylePath,
					this.NamespaceManager).AppendChild(style.Node);

				if(((IContent)value).GetType().Name == "List")
					this.XmlDoc.SelectSingleNode(TextDocumentHelper.AutomaticStylePath,
						this.NamespaceManager).AppendChild(((List)value).ParagraphStyle.Node);

			}

			if(((IContent)value).TextContent != null)
				foreach(IText it in ((IContent)value).TextContent)
					if(it.GetType().Name == "FormatedText")
						this.XmlDoc.SelectSingleNode(TextDocumentHelper.AutomaticStylePath,
							this.NamespaceManager).AppendChild(((FormatedText)it).Style.Node);
		}

		/// <summary>
		/// Inserts the table.
		/// </summary>
		/// <param name="table">The table.</param>
		private void InsertTable(Table table)
		{
			this.AppendStyleNode(table.Style.Node);

			this.XmlDoc.SelectSingleNode(TextDocumentHelper.OfficeTextPath, 
				this.NamespaceManager).AppendChild(table.Node);

			foreach(Column col in table.Columns)
				this.AppendStyleNode(col.Style.Node);

			foreach(Row row in table.Rows)
			{
				this.AppendStyleNode(row.Style.Node);
				foreach(Cell cell in row.Cells)
				{
					this.AppendStyleNode(cell.Style.Node);
					foreach(IContent content in cell.Content)
					{
						if(content.GetType().Name == "Paragraph")
						{
							if(((Paragraph)content).ParentStyle != ParentStyles.Table)
							{
								this.AppendStyleNode(content.Style.Node);
								foreach(IText text in ((Paragraph)content).TextContent)
									if(text.GetType().Name == "FormatedText")
										this.AppendStyleNode(text.Style.Node);
							}
						}
						else if(content.GetType().Name == "List")
						{
							this.AppendStyleNode(content.Style.Node);
							this.AppendStyleNode(((List)content).ParagraphStyle.Node);
						}
					}
				}
			}
		}

		/// <summary>
		/// Appends the style node.
		/// </summary>
		/// <param name="node">The node.</param>
		private void AppendStyleNode(XmlNode node)
		{
			this.XmlDoc.SelectSingleNode(TextDocumentHelper.AutomaticStylePath,
				this.NamespaceManager).AppendChild(node);
		}
	}
}

/*
 * $Log: TextDocument.cs,v $
 * Revision 1.13  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.12  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 * Revision 1.11  2005/10/23 16:47:48  larsbm
 * - Bugfix ListItem throws IStyleInterface not implemented exeption
 * - now. build the document after call saveto instead prepare the do at runtime
 * - add remove support for IText objects in the paragraph class
 *
 * Revision 1.10  2005/10/23 09:17:20  larsbm
 * - Release 1.0.3.0
 *
 * Revision 1.9  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.8  2005/10/22 10:47:41  larsbm
 * - add graphic support
 *
 * Revision 1.7  2005/10/16 08:36:29  larsbm
 * - Fixed bug [ 1327809 ] Invalid Cast Exception while insert table with cells that contains lists
 * - Fixed bug [ 1327820 ] Cell styles run into loop
 *
 * Revision 1.6  2005/10/15 12:13:20  larsbm
 * - fixed bug in add pargraph to cell
 *
 * Revision 1.5  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 * Revision 1.4  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 * Revision 1.3  2005/10/08 12:31:33  larsbm
 * - better usabilty of paragraph handling
 * - create paragraphs with text and blank paragraphs with one line of code
 *
 * Revision 1.2  2005/10/08 07:50:15  larsbm
 * - added cvs tags
 *
 */