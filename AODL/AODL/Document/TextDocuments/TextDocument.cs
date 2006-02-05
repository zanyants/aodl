/*
 * $Id: TextDocument.cs,v 1.4 2006/02/05 20:03:32 larsbm Exp $
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
using System.Collections;
using System.Reflection;
using System.IO;
using System.Xml;
using AODL.Document.Export;
using AODL.Document.Import;
using AODL.Document.Import.OpenDocument;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Exceptions;
using AODL.Document;
using AODL.Document.Styles;
using AODL.Document.Content;
using AODL.Document.Content.Tables;

namespace AODL.Document.TextDocuments
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
	/// p.TextContent.Add(new SimpleText(p, "Hello"));
	/// //Add the Paragraph
	/// td.Content.Add((IContent)p);
	/// //Blank para
	/// td.Content.Add(new Paragraph(td, ParentStyles.Standard.ToString()));
	/// // new para
	/// p = new Paragraph(td, "P2");
	/// p.TextContent.Add(new SimpleText(p, "Hello again"));
	/// td.Content.Add(p);
	/// td.SaveTo("parablank.odt");
	/// </code>
	/// </example>
	public class TextDocument : IDisposable, IDocument //AODL.Document.TextDocuments.Content.IContentContainer,
	{
		private int _tableOfContentsCount			= 0;
		/// <summary>
		/// Gets the tableof contents count.
		/// </summary>
		/// <value>The tableof contents count.</value>
		public int TableofContentsCount
		{
			get { return this._tableOfContentsCount; }
		}

		private int _tableCount						= 0;
		/// <summary>
		/// Gets the tableof contents count.
		/// </summary>
		/// <value>The tableof contents count.</value>
		public int TableCount
		{
			get { return this._tableCount; }
		}

		private XmlDocument _xmldoc;
		/// <summary>
		/// The xmldocument the textdocument based on.
		/// </summary>
		public XmlDocument XmlDoc
		{
			get { return this._xmldoc; }
			set { this._xmldoc = value; }
		}

		private bool _isLoadedFile;
		/// <summary>
		/// If this file was loaded
		/// </summary>
		/// <value></value>
		public bool IsLoadedFile
		{
			get { return this._isLoadedFile; }
		}

		private ArrayList _graphics;
		/// <summary>
		/// Gets the graphics.
		/// </summary>
		/// <value>The graphics.</value>
		public ArrayList Graphics
		{
			get { return this._graphics; }
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

//		private AODL.Document.TextDocuments.Content.IContentCollection _content;
//		/// <summary>
//		/// The contentcollection of the document. Represents all
//		/// data to display.
//		/// </summary>
//		public AODL.Document.TextDocuments.Content.IContentCollection Content
//		{
//			get { return this._content; }
//			set { this._content = value; }
//		}

		private AODL.Document.Content.IContentCollection _content;
		/// <summary>
		/// Collection of contents used by this document.
		/// </summary>
		/// <value></value>
		public AODL.Document.Content.IContentCollection Content
		{
			get { return this._content; }
			set { this._content = value; }
		}

		private AODL.Document.Styles.IStyleCollection _styles;
		/// <summary>
		/// Collection of local styles used with this document.
		/// </summary>
		/// <value></value>
		public AODL.Document.Styles.IStyleCollection Styles
		{
			get { return this._styles; }
			set { this._styles = value; }
		}

		private IStyleCollection _commonStyles;
		/// <summary>
		/// Collection of common styles used with this document.
		/// </summary>
		/// <value></value>
		public IStyleCollection CommonStyles
		{
			get { return this._commonStyles; }
			set { this._commonStyles = value; }
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
			this.Styles				= new IStyleCollection();
			this.CommonStyles		= new IStyleCollection();
			this.FontList			= new ArrayList();
			this._graphics			= new ArrayList();
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

			this.DocumentMetadata			= new DocumentMetadata(this);
			this.DocumentMetadata.New();

			this.DocumentPictures			= new DocumentPictureCollection();

			this.DocumentSetting			= new DocumentSetting();
			this.DocumentSetting.New();

			this.DocumentStyles				= new DocumentStyles();
			this.DocumentStyles.New();
			this.ReadCommonStyles();

			this.DocumentThumbnails			= new DocumentPictureCollection();
		}

		/// <summary>
		/// Reads the common styles.
		/// </summary>
		private void ReadCommonStyles()
		{
			OpenDocumentImporter odImporter	= new OpenDocumentImporter();
			odImporter._document			= this;
			odImporter.ImportCommonStyles();

			LocalStyleProcessor lsp			= new LocalStyleProcessor(this, true);
			lsp.ReadStyles();
		}

		/// <summary>
		/// Loads the document by using the specified importer.
		/// </summary>
		/// <param name="file">The the file.</param>
		public void Load(string file)
		{
			try
			{
				this._isLoadedFile				= true;
				this._xmldoc					= new XmlDocument();
				this._xmldoc.LoadXml(TextDocumentHelper.GetBlankDocument());

				this.NamespaceManager			= TextDocumentHelper.NameSpace(this._xmldoc.NameTable);

				ImportHandler importHandler		= new ImportHandler();
				IImporter importer				= importHandler.GetFirstImporter(DocumentTypes.TextDocument, file);
				if(importer != null)
				{
					if(importer.NeedNewOpenDocument)
						this.New();
					importer.Import(this,file);

					if(importer.ImportError != null)
						if(importer.ImportError.Count > 0)
							foreach(object ob in importer.ImportError)
								if(ob is AODLWarning)
								{
									if(((AODLWarning)ob).Message != null)
										Console.WriteLine("Err: {0}", ((AODLWarning)ob).Message);
									if(((AODLWarning)ob).Node != null)
									{
										XmlTextWriter writer	= new XmlTextWriter(Console.Out);
										writer.Formatting		= Formatting.Indented;
										((AODLWarning)ob).Node.WriteContentTo(writer);
									}
								}
				}

			}
			catch(Exception ex)
			{
				throw;
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
				//Build document first
				foreach(string font in this.FontList)
					this.AddFont(font);
				this.CreateContentBody();

				//Call the ExportHandler for the first matching IExporter
				ExportHandler exportHandler		= new ExportHandler();
				IExporter iExporter				= exportHandler.GetFirstExporter(
					DocumentTypes.TextDocument, filename);
				iExporter.Export(this, filename);
			}
			catch(Exception ex)
			{
				throw;
			}
		}

//		/// <summary>
//		/// Insert a footer. 
//		/// If a footer already exist it will be replaced.
//		/// </summary>
//		/// <param name="paragraph">The paragraph.</param>
//		public void InsertFooter(Paragraph paragraph)
//		{
//			try
//			{
//				if(this.DocumentStyles != null)
//					this.DocumentStyles.InsertFooter(paragraph, this);
//			}
//			catch(Exception ex)
//			{
//				throw;
//			}
//		}
//
//		/// <summary>
//		/// Insert a header 
//		/// If a header already exist it will be replaced.
//		/// </summary>
//		/// <param name="paragraph">The content.</param>
//		public void InsertHeader(Paragraph paragraph)
//		{
//			try
//			{
//				if(this.DocumentStyles != null)
//					this.DocumentStyles.InsertHeader(paragraph, this);
//			}
//			catch(Exception ex)
//			{
//				throw;
//			}
//		}

/*		/// <summary>
		/// Inserts the given IText object after the bookmark with the
		/// given name.
		/// </summary>
		/// <param name="bookmarkName">Name of the bookmark, where the IText object should
		/// be inserted.</param>
		/// <param name="textContent">The IText object to insert.</param>
		/// <returns>True if the IText object is inserted, otherwise false.</returns>
		public bool InsertTextAtBookmark(string bookmarkName, IText textContent)
		{
			try
			{
				return false;	
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		
		/// <summary>
		/// Search for the given text and replace it with the given IText object.
		/// The first text that match the search criteria will be replaced!
		/// </summary>
		/// <param name="searchText">The search text to replace.</param>
		/// <param name="textContent">The IText object to insert.</param>
		/// <remarks>Do not use linebreaks, tabstops and search text which reached 
		/// over more paragraphs</remarks>
		/// <returns>True, if the text was found and replaced otherwise false.</returns>
		public bool FindAndReplaceText(string searchText, IText textContent)
		{
			try
			{
				return false;
			}
			catch(Exception ex)
			{
				throw;
			}
		}
*/
//		/// <summary>
//		/// Gets the exporter.
//		/// </summary>
//		/// <param name="filename">The filename.</param>
//		/// <returns></returns>
//		private IExporter GetExporter(string filename)
//		{
//			if(filename.EndsWith(".odt"))
//				return new AODL.Document.Export.OpenDocument.OpenDocumentTextExporter();
//			else if(filename.EndsWith(".htm") || filename.EndsWith(".html"))
//				return new AODL.Document.Export.Html.OpenDocumentHtmlExporter();
//
//			throw new Exception("Unknown Exporter name exception. No exporter found for file "+filename);
//		}
//
//		/// <summary>
//		/// Gets the importer.
//		/// </summary>
//		/// <param name="filename">The filename.</param>
//		/// <returns></returns>
//		private IImporter GetImporter(string filename)
//		{
//			if(filename.EndsWith(".odt"))
//				return new OpenDocumentImporter();
//
//			throw new Exception("Unkown importer name! No importer found for file "+filename);
//		}

//		/// <summary>
//		/// Adds the I content collection to document.
//		/// </summary>
//		private void AddIContentCollectionToDocument()
//		{
//			foreach(string fontname in this.FontList)
//				this.AddFont(fontname);
//			for(int i=0; i < this.Content.Count; i++)
//				this.CreateDocument(this.Content[i]);
//		}

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
				throw;
			}
		}

		/// <summary>
		/// Return the namespaceuri for the given prefixname.
		/// </summary>
		/// <param name="prefix">The prefixname.</param>
		/// <returns></returns>
		internal string GetNamespaceUri(string prefix)
		{
			foreach (string prefixx in NamespaceManager)
				if(prefix == prefixx)				
					return NamespaceManager.LookupNamespace(prefixx);
			return null;
		}
		
		#region old delete
//		
//		/// <summary>
//		/// Creates the document.
//		/// </summary>
//		/// <param name="value">The value.</param>
//		private void CreateDocument(AODL.Document.TextDocuments.Content.IContent value)
//		{
//			if(value is Table)
//			{
//				this.InsertTable((Table)value);
//				return;
//			}
//
//			this.XmlDoc.SelectSingleNode(TextDocumentHelper.OfficeTextPath, 
//				this.NamespaceManager).AppendChild(((AODL.Document.TextDocuments.Content.IContent)value).Node);
//
//			if(((AODL.Document.TextDocuments.Content.IContent)value).GetType().GetInterface("IContentContainer") != null)
//				foreach(AODL.Document.TextDocuments.Content.IContent content in ((AODL.Document.TextDocuments.Content.IContentContainer)value).Content)
//					if(content.Style != null)
//						if(content.Style.Node != null)
//							this.AppendStyleNode(content.Style.Node);
//
//			if(((AODL.Document.TextDocuments.Content.IContent)value).Style != null)
//			{
//				AODL.Document.TextDocuments.Style.IStyle style	= ((AODL.Document.TextDocuments.Content.IContent)value).Style;			
//				this.XmlDoc.SelectSingleNode(TextDocumentHelper.AutomaticStylePath,
//					this.NamespaceManager).AppendChild(style.Node);
//
//				if(((AODL.Document.TextDocuments.Content.IContent)value).GetType().Name == "List")
//					this.XmlDoc.SelectSingleNode(TextDocumentHelper.AutomaticStylePath,
//						this.NamespaceManager).AppendChild(((List)value).ParagraphStyle.Node);
//
//			}
//
//			if(((AODL.Document.TextDocuments.Content.IContent)value).TextContent != null)
//				foreach(IText it in ((AODL.Document.TextDocuments.Content.IContent)value).TextContent)
//					if(it.GetType().Name == "FormatedText")
//						this.XmlDoc.SelectSingleNode(TextDocumentHelper.AutomaticStylePath,
//							this.NamespaceManager).AppendChild(((FormatedText)it).Style.Node);
//		}
//
//		/// <summary>
//		/// Inserts the table.
//		/// </summary>
//		/// <param name="table">The table.</param>
//		private void InsertTable(AODL.Document.TextDocuments.Content.Table table)
//		{
//			this.AppendStyleNode(table.Style.Node);
//
//			this.XmlDoc.SelectSingleNode(TextDocumentHelper.OfficeTextPath, 
//				this.NamespaceManager).AppendChild(table.Node);
//
//			foreach(Column col in table.Columns)
//				this.AppendStyleNode(col.Style.Node);
//
//			if(table.RowHeader != null)
//				this.InsertRows(table.RowHeader.RowCollection);
//
//			this.InsertRows(table.Rows);
//			#region old Todo: delete after test
////			foreach(Row row in table.Rows)
////			{
////				this.AppendStyleNode(row.Style.Node);
////				foreach(Cell cell in row.Cells)
////				{
////					this.AppendStyleNode(cell.Style.Node);
////					foreach(IContent content in cell.Content)
////					{
////						if(content.GetType().Name == "Paragraph")
////						{
////							if(((Paragraph)content).ParentStyle != ParentStyles.Table
////								&& ((Paragraph)content).ParentStyle != ParentStyles.Standard)
////							{
////								this.AppendStyleNode(content.Style.Node);
////								foreach(IText text in ((Paragraph)content).TextContent)
////									if(text.GetType().Name == "FormatedText")
////										this.AppendStyleNode(text.Style.Node);
////							}
////						}
////						else if(content.GetType().Name == "List")
////						{
////							this.AppendStyleNode(content.Style.Node);
////							this.AppendStyleNode(((List)content).ParagraphStyle.Node);
////						}
////					}
////				}
////			}
//			#endregion
//		}
//
//		/// <summary>
//		/// Inserts the rows.
//		/// </summary>
//		/// <param name="rows">The rows.</param>
//		private void InsertRows(AODL.Document.TextDocuments.Content.RowCollection rows)
//		{
//			foreach(Row row in rows)
//			{
//				this.AppendStyleNode(row.Style.Node);
//				foreach(Cell cell in row.Cells)
//				{
//					this.AppendStyleNode(cell.Style.Node);
//					foreach(AODL.Document.TextDocuments.Content.IContent content in cell.Content)
//					{
//						if(content.GetType().Name == "Paragraph")
//						{
//							if(((Paragraph)content).ParentStyle !=AODL.Document.TextDocuments.Style.ParentStyles.Table
//								&& ((Paragraph)content).ParentStyle != AODL.Document.TextDocuments.Style.ParentStyles.Standard)
//							{
//								this.AppendStyleNode(content.Style.Node);
//								foreach(IText text in ((Paragraph)content).TextContent)
//									if(text.GetType().Name == "FormatedText")
//										this.AppendStyleNode(text.Style.Node);
//							}
//						}
//						else if(content.GetType().Name == "List")
//						{
//							this.AppendStyleNode(content.Style.Node);
//							this.AppendStyleNode(((List)content).ParagraphStyle.Node);
//						}
//					}
//				}
//			}
//		}
//
//		/// <summary>
//		/// Appends the style node.
//		/// </summary>
//		/// <param name="node">The node.</param>
//		private void AppendStyleNode(XmlNode node)
//		{
//			this.XmlDoc.SelectSingleNode(TextDocumentHelper.AutomaticStylePath,
//				this.NamespaceManager).AppendChild(node);
//		}
		#endregion

		/// <summary>
		/// Creates the content body.
		/// </summary>
		private void CreateContentBody()
		{
			XmlNode nodeText		= this.XmlDoc.SelectSingleNode(
				TextDocumentHelper.OfficeTextPath, this.NamespaceManager);

			foreach(IContent content in this.Content)
			{
				if(content is Table)
					((Table)content).BuildNode();
				nodeText.AppendChild(content.Node);
			}

			this.CreateLocalStyleContent();
			this.CreateCommonStyleContent();
		}

		/// <summary>
		/// Creates the content of the local style.
		/// </summary>
		private void CreateLocalStyleContent()
		{
			XmlNode nodeAutomaticStyles		= this.XmlDoc.SelectSingleNode(
				TextDocumentHelper.AutomaticStylePath, this.NamespaceManager);

			foreach(IStyle style in this.Styles)
			{
				bool exist					= false;
				if(style.StyleName != null)
				{
					XmlNode node				= nodeAutomaticStyles.SelectSingleNode("style:style[@style:name='"+style.StyleName+"']",
						this.NamespaceManager);
					if(node != null)
						exist				= true;
				}
				if(!exist)
					nodeAutomaticStyles.AppendChild(style.Node);
			}
		}

		/// <summary>
		/// Creates the content of the common style.
		/// </summary>
		private void CreateCommonStyleContent()
		{
			XmlNode nodeCommonStyles		= this.DocumentStyles.Styles.SelectSingleNode(
				"office:document-styles/office:styles", this.NamespaceManager);
			nodeCommonStyles.InnerXml	= "";

			foreach(IStyle style in this.CommonStyles)
			{
				XmlNode nodeStyle			= this.DocumentStyles.Styles.ImportNode(style.Node, true);
				nodeCommonStyles.AppendChild(nodeStyle);
			}
		}


		#region IDisposable Member

		private bool _disposed = false;

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// is reclaimed by garbage collection.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Disposes the specified disposing.
		/// </summary>
		/// <param name="disposing">if set to <c>true</c> [disposing].</param>
		private void Dispose(bool disposing)
		{
			if(!this._disposed)
			{
				if(disposing)
				{
					try
					{
						AODL.Document.Export.OpenDocument.OpenDocumentTextExporter.CleanUpReadAndWriteDirectories();
					}
					catch(Exception ex)
					{
						throw ex;
					}
				}
			}
			_disposed = true;         
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="AODL.Document.TextDocuments.TextDocument"/> is reclaimed by garbage collection.
		/// </summary>
		~TextDocument()      
		{
			Dispose();
		}

		#endregion
	}
}

/*
 * $Log: TextDocument.cs,v $
 * Revision 1.4  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.3  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 * Revision 1.2  2006/01/29 18:52:51  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:30  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.18  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 * Revision 1.17  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.16  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.15  2005/11/23 19:18:17  larsbm
 * - New Textproperties
 * - New Paragraphproperties
 * - New Border Helper
 * - Textproprtie helper
 *
 * Revision 1.14  2005/11/22 21:09:19  larsbm
 * - Add simple header and footer support
 *
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