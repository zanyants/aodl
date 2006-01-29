/*
 * $Id: SpreadsheetDocument.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
using System.IO;
using System.Xml;
using System.Reflection;
using AODL.Document.TextDocuments;
using AODL.Document.Content.Tables;
using AODL.Document;
using AODL.Document.Export;
using AODL.Document.Import;
using AODL.Document.Styles;
using AODL.Document.Content;
using AODL.Document.Exceptions;

namespace AODL.Document.SpreadsheetDocuments
{
	/// <summary>
	/// The SpreadsheetDocument class represent an OpenDocument
	/// spreadsheet document.
	/// </summary>
	public class SpreadsheetDocument : IDocument
	{
		private int _tableCount		= 0;
		/// <summary>
		/// Gets the table count.
		/// </summary>
		/// <value>The table count.</value>
		public int TableCount
		{
			get { return this._tableCount;  }
		}

		private XmlDocument _xmldoc;
		/// <summary>
		/// The xml document the spreadsheet document based on.
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

		private ArrayList _fontList;
		/// <summary>
		/// The font list
		/// </summary>
		/// <value></value>
		public ArrayList FontList
		{
			get { return this._fontList; }
			set { this._fontList = value; }
		}

		private IStyleCollection _styles;
		/// <summary>
		/// Collection of local styles used with this document.
		/// </summary>
		/// <value></value>
		public IStyleCollection Styles
		{
			get { return this._styles; }
			set { this._styles = value; }
		}

		private IContentCollection _contents;
		/// <summary>
		/// Collection of contents used by this document.
		/// </summary>
		/// <value></value>
		public IContentCollection Content
		{
			get { return this._contents; }
			set { this._contents = value; }
		}

		private TableCollection _tableCollection;
		/// <summary>
		/// Gets or sets the table collection.
		/// </summary>
		/// <value>The table collection.</value>
		public TableCollection TableCollection
		{
			get { return this._tableCollection; }
			set { this._tableCollection = value; }
		}

		private AODL.Document.SpreadsheetDocuments.DocumentStyles _documentStyles;
		/// <summary>
		/// Gets or sets the document styes.
		/// </summary>
		/// <value>The document styes.</value>
		public AODL.Document.SpreadsheetDocuments.DocumentStyles DocumentStyles
		{
			get { return this._documentStyles; }
			set { this._documentStyles = value; }
		}

		private AODL.Document.SpreadsheetDocuments.DocumentSetting _documentSetting;
		/// <summary>
		/// Gets or sets the document setting.
		/// </summary>
		/// <value>The document setting.</value>
		public AODL.Document.SpreadsheetDocuments.DocumentSetting DocumentSetting
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

		private DocumentConfiguration2 _documentConfiguations2;
		/// <summary>
		/// Gets or sets the document configuration2.
		/// </summary>
		/// <value>The document configuration2.</value>
		public DocumentConfiguration2 DocumentConfigurations2
		{
			get { return this._documentConfiguations2; }
			set { this._documentConfiguations2 = value; }
		}

		private AODL.Document.SpreadsheetDocuments.DocumentManifest _documentManifest;
		/// <summary>
		/// Gets or sets the document manifest.
		/// </summary>
		/// <value>The document manifest.</value>
		public AODL.Document.SpreadsheetDocuments.DocumentManifest DocumentManifest
		{
			get { return this._documentManifest; }
			set { this._documentManifest = value; }
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

		private ArrayList _graphics;
		/// <summary>
		/// Gets the graphics.
		/// </summary>
		/// <value>The graphics.</value>
		public ArrayList Graphics
		{
			get { return this._graphics; }
		}

		private string _mimeTyp		= "application/vnd.oasis.opendocument.spreadsheet";
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

		/// <summary>
		/// Initializes a new instance of the <see cref="SpreadsheetDocument"/> class.
		/// </summary>
		public SpreadsheetDocument()
		{
			this.TableCollection			= new TableCollection();
			this.Styles						= new IStyleCollection();
			this.Content					= new IContentCollection();
			this._graphics					= new ArrayList();
			this.FontList					= new ArrayList();
			this.TableCollection.Inserted	+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(TableCollection_Inserted);
			this.TableCollection.Removed	+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(TableCollection_Removed);
		}

		/// <summary>
		/// Create a new blank spreadsheet document.
		/// </summary>
		public void New()
		{
			this.LoadBlankContent();

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

			this.DocumentThumbnails			= new DocumentPictureCollection();
			
		}

		/// <summary>
		/// Save the SpreadsheetDocument as OpenDocument spreadsheet document
		/// </summary>
		/// <param name="filename">The filename. With or without full path. Without will save the file to application path!</param>
		public void SaveTo(string filename)
		{
			try
			{
//				IExporter exporter			= this.GetExporter(filename);
				this.CreateContentBody();
//				exporter.Export(this, filename);
				//Call the ExportHandler for the first matching IExporter
				ExportHandler exportHandler		= new ExportHandler();
				IExporter iExporter				= exportHandler.GetFirstExporter(
					DocumentTypes.SpreadsheetDocument, filename);
				iExporter.Export(this, filename);
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Load the given file.
		/// </summary>
		/// <param name="file"></param>
		public void Load(string file)
		{
			try
			{
				this._isLoadedFile				= true;
				this.LoadBlankContent();

				this.NamespaceManager			= TextDocumentHelper.NameSpace(this._xmldoc.NameTable);

				ImportHandler importHandler		= new ImportHandler();
				IImporter importer				= importHandler.GetFirstImporter(DocumentTypes.SpreadsheetDocument, file);
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
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Gets the exporter.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <returns></returns>
		private IExporter GetExporter(string filename)
		{
			if(filename.EndsWith(".ods"))
				return new AODL.Document.Export.OpenDocument.OpenDocumentTextExporter();			

			throw new Exception("Unknown Exporter name exception. No exporter found for file "+filename);
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

		/// <summary>
		/// Load a blank the spreadsheet content document.
		/// </summary>
		private void LoadBlankContent()
		{
			try
			{
				Assembly ass			= Assembly.GetExecutingAssembly();
				Stream str				= ass.GetManifestResourceStream("AODL.Resources.OD.spreadsheetcontent.xml");
				this._xmldoc			= new XmlDocument();
				this._xmldoc.Load(str);
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Tables the collection_ inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void TableCollection_Inserted(int index, object value)
		{
			this._tableCount++;
		}

		/// <summary>
		/// Tables the collection_ removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void TableCollection_Removed(int index, object value)
		{
			this._tableCount--;
		}

		/// <summary>
		/// Creates the content body.
		/// </summary>
		private void CreateContentBody()
		{
			XmlNode nodeSpreadSheets		= this.XmlDoc.SelectSingleNode(
				"/office:document-content/office:body/office:spreadsheet", this.NamespaceManager);

			foreach(IContent iContent in this.Content)
			{
				//only table content
				if(iContent is Table)
					nodeSpreadSheets.AppendChild(((Table)iContent).BuildNode());
			}

			this.CreateLocalStyleContent();
		}

		/// <summary>
		/// Creates the content of the local style.
		/// </summary>
		private void CreateLocalStyleContent()
		{
			XmlNode nodeAutomaticStyles		= this.XmlDoc.SelectSingleNode(
				"/office:document-content/office:automatic-styles", this.NamespaceManager);

			foreach(IStyle style in this.Styles)
				nodeAutomaticStyles.AppendChild(style.Node);
		}

		/// <summary>
		/// Styles the node exists.
		/// </summary>
		/// <param name="styleName">Name of the style.</param>
		/// <returns></returns>
		private bool StyleNodeExists(string styleName)
		{
			XmlNode styleNode				= this.XmlDoc.SelectSingleNode(
				"/office:document-content/office:automatic-styles/style:style[@style:name='"+styleName+"']", this.NamespaceManager);

			if(styleNode == null)
				return false;

			return true;
		}
	}
}

/*
 * $Log: SpreadsheetDocument.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */