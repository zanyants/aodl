/*
 * $Id: OpenDocumentImporter.cs,v 1.3 2006/02/02 21:55:59 larsbm Exp $
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
using System.Drawing.Imaging;
using System.Reflection;
using System.IO;
using System.Xml;
using AODL.Document;
using AODL.Document.Import;
using AODL.Document.Export;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.TextDocuments;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Content;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.GZip;

namespace AODL.Document.Import.OpenDocument
{
	/// <summary>
	/// OpenDocumentImporter - Importer for OpenDocuments in different formats.
	/// </summary>
	public class OpenDocumentImporter : IImporter, IPublisherInfo
	{
		internal static readonly string dir		= Environment.CurrentDirectory+@"\aodlread\";
		internal static readonly string dirpics	= Environment.CurrentDirectory+@"\PicturesRead\";

		/// <summary>
		/// The document to fill with content.
		/// </summary>
		internal IDocument _document;

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenDocumentImporter"/> class.
		/// </summary>
		public OpenDocumentImporter()
		{
			this._importError					= new ArrayList();
			
			this._supportedExtensions			= new ArrayList();
			this._supportedExtensions.Add(new DocumentSupportInfo(".odt", DocumentTypes.TextDocument));
			this._supportedExtensions.Add(new DocumentSupportInfo(".ods", DocumentTypes.SpreadsheetDocument));

			this._author						= "Lars Behrmann, lb@OpenDocument4all.com";
			this._infoUrl						= "http://AODL.OpenDocument4all.com";
			this._description					= "This the standard importer of the OpenDocument library AODL.";
		}

		#region IExporter Member

		private ArrayList _supportedExtensions;
		/// <summary>
		/// Gets the document support infos.
		/// </summary>
		/// <value>The document support infos.</value>
		public ArrayList DocumentSupportInfos
		{
			get { return this._supportedExtensions; }
		}

		/// <summary>
		/// Imports the specified filename.
		/// </summary>
		/// <param name="document">The TextDocument to fill.</param>
		/// <param name="filename">The filename.</param>		
		/// <returns>The created TextDocument</returns>
		public void Import(IDocument document, string filename)
		{
			try
			{
				this._document		= document;
				this.UnpackFiles(filename);
				this.ReadContent();
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		private ArrayList _importError;
		/// <summary>
		/// Gets the import errors as ArrayList of strings.
		/// </summary>
		/// <value>The import errors.</value>
		public System.Collections.ArrayList ImportError
		{
			get
			{
				return this._importError;
			}
		}

		/// <summary>
		/// If the import file format isn't any OpenDocument
		/// format you have to return true and AODL will
		/// create a new one.
		/// </summary>
		/// <value></value>
		public bool NeedNewOpenDocument
		{
			get { return false; }
		}

		#endregion

		#region IPublisherInfo Member

		private string _author;
		/// <summary>
		/// The name the Author
		/// </summary>
		/// <value></value>
		public string Author
		{
			get
			{
				return this._author;
			}
		}

		private string _infoUrl;
		/// <summary>
		/// Url to a info site
		/// </summary>
		/// <value></value>
		public string InfoUrl
		{
			get
			{
				return this._infoUrl;
			}
		}

		private string _description;
		/// <summary>
		/// Description about the exporter resp. importer
		/// </summary>
		/// <value></value>
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		#endregion

		#region unpacking files and images

		/// <summary>
		/// Unpacks the files.
		/// </summary>
		/// <param name="file">The file.</param>
		private void UnpackFiles(string file)
		{
			try
			{
				if(!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				ZipInputStream s = new ZipInputStream(File.OpenRead(file));
		
				ZipEntry theEntry;
				while ((theEntry = s.GetNextEntry()) != null) 
				{
			
					string directoryName = Path.GetDirectoryName(theEntry.Name);
					string fileName      = Path.GetFileName(theEntry.Name);

					if(directoryName != String.Empty)
						Directory.CreateDirectory(dir+directoryName);
			
					if (fileName != String.Empty) 
					{
						FileStream streamWriter = File.Create(dir+theEntry.Name);
				
						int size = 2048;
						byte[] data = new byte[2048];
						while (true) 
						{
							size = s.Read(data, 0, data.Length);
							if (size > 0) 
							{
								streamWriter.Write(data, 0, size);
							} 
							else 
							{
								break;
							}
						}
				
						streamWriter.Close();
					}
				}
				s.Close();
				
				this.MovePictures();
				this.ReadResources();
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Moves the pictures folder
		/// To avoid gdi errors.
		/// </summary>
		private void MovePictures()
		{
//			if(Directory.Exists(dir+"Pictures"))
//			{
//				if(Directory.Exists(dirpics))
//					Directory.Delete(dirpics, true);
//				Directory.Move(dir+"Pictures", dirpics);			
//			}
		}

		/// <summary>
		/// Reads the resources.
		/// </summary>
		private void ReadResources()
		{
			try
			{
				this._document.DocumentConfigurations2		= new DocumentConfiguration2();
				this.ReadDocumentConfigurations2();

				this._document.DocumentMetadata				= new DocumentMetadata(this._document);
				this._document.DocumentMetadata.LoadFromFile(dir+DocumentMetadata.FileName);

				if(this._document is TextDocument)
				{
					((TextDocument)this._document).DocumentSetting				= new  AODL.Document.TextDocuments.DocumentSetting();
					string file		= AODL.Document.TextDocuments.DocumentSetting.FileName;
					((TextDocument)this._document).DocumentSetting.LoadFromFile(dir+file);

					((TextDocument)this._document).DocumentManifest				= new AODL.Document.TextDocuments.DocumentManifest();
					string folder	= AODL.Document.TextDocuments.DocumentManifest.FolderName;
					file			= AODL.Document.TextDocuments.DocumentManifest.FileName;
					((TextDocument)this._document).DocumentManifest.LoadFromFile(dir+folder+"\\"+file);

					((TextDocument)this._document).DocumentStyles				= new AODL.Document.TextDocuments.DocumentStyles();
					file			= AODL.Document.TextDocuments.DocumentStyles.FileName;
					((TextDocument)this._document).DocumentStyles.LoadFromFile(dir+file);
				}
				else if(this._document is SpreadsheetDocument)
				{
					((SpreadsheetDocument)this._document).DocumentSetting				= new  AODL.Document.SpreadsheetDocuments.DocumentSetting();
					string file		= AODL.Document.SpreadsheetDocuments.DocumentSetting.FileName;
					((SpreadsheetDocument)this._document).DocumentSetting.LoadFromFile(dir+file);

					((SpreadsheetDocument)this._document).DocumentManifest				= new AODL.Document.SpreadsheetDocuments.DocumentManifest();
					string folder	= AODL.Document.SpreadsheetDocuments.DocumentManifest.FolderName;
					file			= AODL.Document.SpreadsheetDocuments.DocumentManifest.FileName;
					((SpreadsheetDocument)this._document).DocumentManifest.LoadFromFile(dir+folder+"\\"+file);

					((SpreadsheetDocument)this._document).DocumentStyles				= new AODL.Document.SpreadsheetDocuments.DocumentStyles();
					file			= AODL.Document.SpreadsheetDocuments.DocumentStyles.FileName;
					((SpreadsheetDocument)this._document).DocumentStyles.LoadFromFile(dir
						+file);
				}

				this._document.DocumentPictures				= this.ReadImageResources(dir+"Pictures");

				this._document.DocumentThumbnails			= this.ReadImageResources(dir+"Thumbnails");

				//There's no really need to read the fonts.

				this.InitMetaData();
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Reads the document configurations2.
		/// </summary>
		private void ReadDocumentConfigurations2()
		{
			try
			{
				if(!Directory.Exists(dir+DocumentConfiguration2.FolderName))
					return;
				DirectoryInfo di		= new DirectoryInfo(dir+DocumentConfiguration2.FolderName);
				foreach(FileInfo fi	in di.GetFiles())
				{
					this._document.DocumentConfigurations2.FileName	= fi.Name;
					string line			= null;
					StreamReader sr		= new StreamReader(fi.FullName);
					while((line	= sr.ReadLine())!=null)
						this._document.DocumentConfigurations2.Configurations2Content	+=line;
					sr.Close();
					break;
				}
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Reads the image resources.
		/// </summary>
		/// <param name="folder">The folder.</param>
		private DocumentPictureCollection ReadImageResources(string folder)
		{
			DocumentPictureCollection dpc	= new DocumentPictureCollection();
			try
			{
				//If folder not exists, return (folder will only unpacked if not empty)
				if(!Directory.Exists(folder))
					return dpc;
				//Only image files should be in this folder, if not -> Exception
				DirectoryInfo di				= new DirectoryInfo(folder);
				foreach(FileInfo fi in di.GetFiles())
				{
					DocumentPicture dp			= new DocumentPicture(fi.FullName);
					dpc.Add(dp);
				}
			}
			catch(Exception ex)
			{
				throw;
			}

			return dpc;
		}

		#endregion

		/// <summary>
		/// Reads the content.
		/// </summary>
		private void ReadContent()
		{
			try
			{
				this._document.XmlDoc			= new XmlDocument();
				this._document.XmlDoc.Load(dir+"\\content.xml");
				//Read local styles
				LocalStyleProcessor lsp			= new LocalStyleProcessor(this._document, false);
				lsp.ReadStyles();
				//Import common styles and read common styles
				this.ImportCommonStyles();
				lsp								= new LocalStyleProcessor(this._document, true);
				lsp.ReadStyles();
				
				MainContentProcessor mcp		= new MainContentProcessor(this._document);
				mcp.OnWarning					+=new AODL.Document.Import.OpenDocument.NodeProcessors.MainContentProcessor.Warning(mcp_OnWarning);
				TextContentProcessor.OnWarning	+=new AODL.Document.Import.OpenDocument.NodeProcessors.TextContentProcessor.Warning(TextContentProcessor_OnWarning);
				mcp.ReadContentNodes();
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// If the common styles are placed in the DocumentStyles,
		/// they will be imported into the content file.
		/// </summary>
		public void ImportCommonStyles()
		{
			string xPathToStyles				= "office:document-styles/office:styles";
			string xPathOfficeDocument			= "office:document-content";

			try
			{
				XmlNode nodeStyles				= null;
				XmlNode nodeOfficeDocument		= null;

				if(this._document is TextDocument)
					nodeStyles					= ((TextDocument)this._document).DocumentStyles.Styles.SelectSingleNode(
						xPathToStyles, this._document.NamespaceManager);
				else if(this._document is SpreadsheetDocument)
					nodeStyles					= ((SpreadsheetDocument)this._document).DocumentStyles.Styles.SelectSingleNode(
						xPathToStyles, this._document.NamespaceManager);

				nodeOfficeDocument				= this._document.XmlDoc.SelectSingleNode(
					xPathOfficeDocument, this._document.NamespaceManager);

				if(nodeOfficeDocument != null && nodeStyles != null)
				{
					nodeStyles					= this._document.XmlDoc.ImportNode(nodeStyles, true);
					nodeOfficeDocument.AppendChild(nodeStyles);
				}
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Inits the meta data.
		/// </summary>
		private void InitMetaData()
		{
			try
			{
				this._document.DocumentMetadata.ImageCount		= 0;
				this._document.DocumentMetadata.ObjectCount		= 0;
				this._document.DocumentMetadata.ParagraphCount	= 0;
				this._document.DocumentMetadata.TableCount		= 0;
				this._document.DocumentMetadata.WordCount		= 0;
				this._document.DocumentMetadata.CharacterCount	= 0;
				this._document.DocumentMetadata.LastModified	= DateTime.Now.ToString("s");
			}
			catch(Exception ex)
			{
				//unhandled only meta data maybe not exact
			}
		}

		/// <summary>
		/// MCP_s the on warning.
		/// </summary>
		/// <param name="warning">The warning.</param>
		private void mcp_OnWarning(AODL.Document.Exceptions.AODLWarning warning)
		{
			this._importError.Add(warning);
		}

		private void TextContentProcessor_OnWarning(AODL.Document.Exceptions.AODLWarning warning)
		{
			this._importError.Add(warning);
		}		
	}
}

/*
 * $Log: OpenDocumentImporter.cs,v $
 * Revision 1.3  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.4  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.3  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.2  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */