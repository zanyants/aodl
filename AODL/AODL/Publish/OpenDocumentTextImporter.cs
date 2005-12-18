/*
 * $Id: OpenDocumentTextImporter.cs,v 1.4 2005/12/18 18:29:46 larsbm Exp $
 */

using System;
using System.Collections;
using System.Drawing.Imaging;
using System.Reflection;
using System.IO;
using System.Xml;
//using ICSharpCode.SharpZipLib.Checksums;
//using ICSharpCode.SharpZipLib.Zip;
//using ICSharpCode.SharpZipLib.GZip;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.Import.XmlHelper;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.GZip;

namespace AODL.Import
{
	/// <summary>
	/// OpenDocumentTextImporter - Importer for OpenDocuments in the Text format.
	/// </summary>
	public class OpenDocumentTextImporter : IImporter
	{
		internal static readonly string dir		= Environment.CurrentDirectory+@"\tmp\";
		internal static readonly string dirpics	= Environment.CurrentDirectory+@"\PicturesRead\";

		private TextDocument.TextDocument _textdocument;

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenDocumentTextImporter"/> class.
		/// </summary>
		public OpenDocumentTextImporter()
		{
			this._importError		= new ArrayList();
		}

		#region IImporter Member

		/// <summary>
		/// Imports the specified filename.
		/// </summary>
		/// <param name="document">The TextDocument to fill.</param>
		/// <param name="filename">The filename.</param>		
		/// <returns>The created TextDocument</returns>
		public void Import(TextDocument.TextDocument document, string filename)
		{
			try
			{
				this._textdocument		= document;
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

		private string _openDocumentTextImporter	= "OpenDocumentText";
		/// <summary>
		/// Gets the name of the importer.
		/// </summary>
		/// <value>The name of the importer.</value>
		public string ImporterName
		{
			get
			{
				return this._openDocumentTextImporter;
			}
		}

		#endregion

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
//				ZipOutputStream zipStream = new ZipOutputStream(File.Create("demo.zip"));
//				ZipEntry zipEntry = new ZipEntry("emptyFolder/");
//				zipStream.PutNextEntry(zipEntry);
//
//				FileInfo fi		= new FileInfo(file);				
//				FastZip fz		= new FastZip();
//				fz.ExtractZip(file, dir, "");
//
//				int generation	= GC.GetGeneration(fz);
//				GC.Collect(generation);
				
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
			if(Directory.Exists(dir+"Pictures"))
			{
				if(Directory.Exists(dirpics))
					Directory.Delete(dirpics, true);
				Directory.Move(dir+"Pictures", dirpics);			
			}
			//AODLTest.ImportTest.RealContentLoadTest : System.IO.DirectoryNotFoundException : Could not find a part of the path.
		}

		/// <summary>
		/// Reads the resources.
		/// </summary>
		private void ReadResources()
		{
			try
			{
				this._textdocument.DocumentConfigurations2		= new DocumentConfiguration2();
				this.ReadDocumentConfigurations2();

				this._textdocument.DocumentManifest				= new DocumentManifest();
				this._textdocument.DocumentManifest.LoadFromFile(dir+DocumentManifest.FolderName+"\\"+DocumentManifest.FileName);

				this._textdocument.DocumentMetadata				= new DocumentMetadata(this._textdocument);
				this._textdocument.DocumentMetadata.LoadFromFile(dir+DocumentMetadata.FileName);

				this._textdocument.DocumentSetting				= new DocumentSetting();
				this._textdocument.DocumentSetting.LoadFromFile(dir+DocumentSetting.FileName);

				this._textdocument.DocumentStyles				= new DocumentStyles();
				this._textdocument.DocumentStyles.LoadFromFile(dir+DocumentStyles.FileName);

				this._textdocument.DocumentPictures				= this.ReadImageResources(dirpics);//+"Pictures");

				this._textdocument.DocumentThumbnails			= this.ReadImageResources(dir+"Thumbnails");

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
					this._textdocument.DocumentConfigurations2.FileName	= fi.Name;
					string line			= null;
					StreamReader sr		= new StreamReader(fi.FullName);
					while((line	= sr.ReadLine())!=null)
						this._textdocument.DocumentConfigurations2.Configurations2Content	+=line;
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

		/// <summary>
		/// Reads the content.
		/// </summary>
		private void ReadContent()
		{
			try
			{
				XmlNodeProcessor xnp		= new XmlNodeProcessor(this._textdocument);								
				xnp.ReadContentNodes(dir+"\\content.xml");
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
				this._textdocument.DocumentMetadata.ImageCount		= 0;
				this._textdocument.DocumentMetadata.ObjectCount		= 0;
				this._textdocument.DocumentMetadata.ParagraphCount	= 0;
				this._textdocument.DocumentMetadata.TableCount		= 0;
				this._textdocument.DocumentMetadata.WordCount		= 0;
				this._textdocument.DocumentMetadata.CharacterCount	= 0;
				this._textdocument.DocumentMetadata.LastModified	= DateTime.Now.ToString("s");
			}
			catch(Exception ex)
			{
				//unhandled only meta data maybe not exact
			}
		}
	}
}

/*
 * $Log: OpenDocumentTextImporter.cs,v $
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