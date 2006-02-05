/*
 * $Id: OpenDocumentTextExporter.cs,v 1.3 2006/02/05 20:03:32 larsbm Exp $
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
using System.Drawing.Imaging;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using AODL.Document.TextDocuments;
using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Content.Draw;
using AODL.Document.Import.OpenDocument;
using AODL.Document.Exceptions;

namespace AODL.Document.Export.OpenDocument
{
	/// <summary>
	/// OpenDocumentTextExporter is the standard exporter of AODL for the export
	/// of documents in the OpenDocument format.
	/// </summary>
	public class OpenDocumentTextExporter : IExporter, IPublisherInfo
	{
		private static readonly string dir		= Environment.CurrentDirectory+@"\aodlwrite\";
		private string[] _directories			= {"Configurations2", "META-INF", "Pictures", "Thumbnails"};
		private IDocument _document				= null;

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenDocumentTextExporter"/> class.
		/// </summary>
		public OpenDocumentTextExporter()
		{
			this._exportError					= new ArrayList();
			
			this._supportedExtensions			= new ArrayList();
			this._supportedExtensions.Add(new DocumentSupportInfo(".odt", DocumentTypes.TextDocument));
			this._supportedExtensions.Add(new DocumentSupportInfo(".ods", DocumentTypes.SpreadsheetDocument));

			this._author						= "Lars Behrmann, lb@OpenDocument4all.com";
			this._infoUrl						= "http://AODL.OpenDocument4all.com";
			this._description					= "This the standard OpenDocument format exporter of the OpenDocument library AODL.";
		}

		#region IExporter Member

		private ArrayList _supportedExtensions;
		/// <summary>
		/// ArrayList of DocumentSupportInfo objects
		/// </summary>
		/// <value>ArrayList of DocumentSupportInfo objects.</value>
		public ArrayList DocumentSupportInfos
		{
			get { return this._supportedExtensions; }
		}

		private System.Collections.ArrayList _exportError;
		/// <summary>
		/// Gets the export error.
		/// </summary>
		/// <value>The export error.</value>
		public System.Collections.ArrayList ExportError
		{
			get
			{
				return this._exportError;
			}
		}

		/// <summary>
		/// Exports the specified document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="filename">The filename.</param>
		public void Export(AODL.Document.IDocument document, string filename)
		{
			try
			{
				this._document			= document;
				PrepareDirectory(dir);
				//Write content
				if(document is TextDocument)
				{
					this.WriteSingleFiles(((TextDocument)document).DocumentManifest.Manifest, dir+DocumentManifest.FolderName+"\\"+DocumentManifest.FileName);
					this.WriteSingleFiles(((TextDocument)document).DocumentMetadata.Meta, dir+DocumentMetadata.FileName);
					this.WriteSingleFiles(((TextDocument)document).DocumentSetting.Settings, dir+DocumentSetting.FileName);
					this.WriteSingleFiles(((TextDocument)document).DocumentStyles.Styles, dir+DocumentStyles.FileName);
					this.WriteSingleFiles(((TextDocument)document).XmlDoc, dir+"content.xml");
					//Save graphics, which were build during creating a new document
					SaveGraphic(((TextDocument)document), dir);
				}
				else if(document is AODL.Document.SpreadsheetDocuments.SpreadsheetDocument)
				{
					this.WriteSingleFiles(((AODL.Document.SpreadsheetDocuments.SpreadsheetDocument)document).DocumentManifest.Manifest, dir+DocumentManifest.FolderName+"\\"+DocumentManifest.FileName);
					this.WriteSingleFiles(((AODL.Document.SpreadsheetDocuments.SpreadsheetDocument)document).DocumentMetadata.Meta, dir+DocumentMetadata.FileName);
					this.WriteSingleFiles(((AODL.Document.SpreadsheetDocuments.SpreadsheetDocument)document).DocumentSetting.Settings, dir+DocumentSetting.FileName);
					this.WriteSingleFiles(((AODL.Document.SpreadsheetDocuments.SpreadsheetDocument)document).DocumentStyles.Styles, dir+DocumentStyles.FileName);
					this.WriteSingleFiles(((AODL.Document.SpreadsheetDocuments.SpreadsheetDocument)document).XmlDoc, dir+"content.xml");
				}
				else
					throw new Exception("Unsupported document type!");
				//Write Pictures and Thumbnails
//				this.SaveExistingGraphics(document.DocumentPictures, dir+"Pictures\\");
//				this.SaveExistingGraphics(document.DocumentThumbnails, dir+"Thumbnails\\");
				//Don't know why VS couldn't read a textfile resource without file prefix
				WriteMimetypeFile(dir+@"\mimetyp");				
				//Now create the document
				CreateOpenDocument(filename, dir);
				//Clean up resources
				//this.CleanUpDirectory(dir);
			}
			catch(Exception ex)
			{
				throw; 
			}
		}

		/// <summary>
		/// Saves the existing graphics.
		/// </summary>
		/// <param name="pictures">The pictures.</param>
		/// <param name="folder">The folder.</param>
		private void SaveExistingGraphics(DocumentPictureCollection pictures, string folder)
		{
			try
			{
				foreach(DocumentPicture dpic in pictures)
					if(!File.Exists(folder+dpic.ImageName))
						dpic.Image.Save(folder+dpic.ImageName);
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Writes the single files.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="filename">The filename.</param>
		private void WriteSingleFiles(System.Xml.XmlDocument document, string filename)
		{
			try
			{
				//document.Save(filename);
				XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
				writer.Formatting = Formatting.None;
				document.WriteContentTo( writer );
				writer.Flush();
				writer.Close();
			}
			catch(Exception ex)
			{
				throw;
			}
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

		/// <summary>
		/// Create a zip archive with .odt.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="directory">The directory to zip.</param>
		private static void CreateOpenDocument(string filename, string directory)
		{
			try
			{
				FastZip fz = new FastZip();
				fz.CreateEmptyDirectories = true;
				fz.CreateZip(filename, directory, true, "");
				fz			= null;
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Create an output directory with all necessary subfolders.
		/// </summary>
		/// <param name="directory">The directory.</param>
		private void PrepareDirectory(string directory)
		{
			try
			{
				if(Directory.Exists(directory))
					Directory.Delete(directory, true);

				foreach(string d in this._directories)
					Directory.CreateDirectory(directory+@"\"+d);
			}
			catch(Exception ex)
			{
				throw;
			}	
		}

		/// <summary>
		/// Helper Method: Don't know why, but it seems to be impossible
		/// to embbed a textfile as resource
		/// </summary>
		/// <param name="file">The filename.</param>
		private void WriteMimetypeFile(string file)
		{
			//Don't know why, but it seems to be impossible
			//to embbed a textfile as resource
			try
			{
				if(File.Exists(file))
					File.Delete(file);
				StreamWriter sw = File.CreateText(file);
				if(this._document is AODL.Document.TextDocuments.TextDocument)
				{
					sw.WriteLine("application/vnd.oasis.opendocument.text");
				}
				else if(this._document is AODL.Document.SpreadsheetDocuments.SpreadsheetDocument)
				{
					sw.WriteLine("application/vnd.oasis.opendocument.spreadsheet");
				}
				sw.Close();
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		#region old code Todo: Delete
//		/// <summary>
//		/// Cleans the up directory.
//		/// </summary>
//		/// <param name="directory">The directory.</param>
//		private void CleanUpDirectory(string directory)
//		{
//			string dirpics	= Environment.CurrentDirectory+@"\PicturesRead\";
//
//			try
//			{
//				foreach(string d in this._directories)
//					Directory.Delete(directory+@"\"+d, true);
//
////				if(Directory.Exists(OpenDocumentTextImporter.dirpics))
////					Directory.Delete(OpenDocumentTextImporter.dirpics, true);
//				if(Directory.Exists(dirpics))
//					Directory.Delete(dirpics, true);
//
//				File.Delete(directory+DocumentMetadata.FileName);
//				File.Delete(directory+DocumentSetting.FileName);
//				File.Delete(directory+DocumentStyles.FileName);
//				File.Delete(directory+"content.xml");
//			}
//			catch(Exception ex)
//			{
//				throw;
//			}
//		}
		#endregion

		/// <summary>
		/// Cleans the up read and write directories.
		/// </summary>
		internal static void CleanUpReadAndWriteDirectories()
		{
			try
			{
				if(Directory.Exists(OpenDocumentImporter.dir))
					Directory.Delete(OpenDocumentImporter.dir, true);
				if(Directory.Exists(OpenDocumentImporter.dir))
					Directory.Delete(OpenDocumentImporter.dir, true);
				if(Directory.Exists(OpenDocumentTextExporter.dir))
					Directory.Delete(OpenDocumentTextExporter.dir, true);
			}
			catch(Exception ex)
			{
				AODLWarning aodlWarning				= new AODLWarning("An exception ouccours while trying to remove the temp read directories.");
				aodlWarning.InMethod				= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				aodlWarning.OriginalException		= ex;

				throw ex;
			}
		}

		/// <summary>
		/// Saves all graphics used within the textdocument.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="directory">The directory.</param>
		internal static void SaveGraphic(IDocument document, string directory)
		{
			CopyGraphics(document, directory);
			#region old code TODO: delete
//			foreach(IContent content in document.Content)
//				if(content.GetType().GetInterface("IContentContainer") != null)
//					foreach(IContent continner in ((IContentContainer)content).Content)
//						if(continner.GetType().Name == "Frame")
//							if(((Frame)continner).Graphic != null)
//								SaveGraphic((Frame)continner, directory);
//							{
//								try
//								{
//									//TODO: check supported image types
//									string picturedir		= directory+@"\Pictures\";
//									if(File.Exists(picturedir+((Frame)continner).RealGraphicName))
//										return;
//									string name				= picturedir+((Frame)continner).RealGraphicName;
//									((Frame)continner).Image.Save(name);
//								}
//								catch(Exception ex)
//								{
//									throw;
//								}
//							}
//			foreach(IContent content in document.Content)
//				if(content is Table)
//					foreach(Row row in ((Table)content).Rows)
//						foreach(Cell cell in row.Cells)
//							foreach(IContent content1 in cell.Content)
//								if(content1 is IContentContainer)
//									foreach(IContent content2 in ((IContentContainer)content1).Content)
//										if(content2 is Frame)
//											if(((Frame)content2).Graphic != null)
//												SaveGraphic((Frame)content2, directory);
			#endregion
		}
		
		/// <summary>
		/// Copies the graphics.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="directory">The directory.</param>
		private static void CopyGraphics(IDocument document, string directory)
		{
			try
			{
				string picturedir		= directory+@"\Pictures\";

				foreach(Graphic graphic in document.Graphics)
				{
					if(graphic.Frame != null)
						if(graphic.Frame.RealGraphicName != null)
						{
							//Only if new pictures are added
							string target		= picturedir+graphic.Frame.RealGraphicName;
							File.Copy(graphic.Frame.GraphicSourcePath,  target, true);
						}
				}
				MovePicturesIfLoaded(document, picturedir);
			}
			catch(Exception ex)
			{
				Console.WriteLine("CopyGraphics: {0}", ex.Message);
				throw;
			}
		}

		/// <summary>
		/// Moves the pictures if loaded.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="targetDir">The target dir.</param>
		private static void MovePicturesIfLoaded(IDocument document,string targetDir)
		{
			if(document.DocumentPictures.Count > 0)
			{
				foreach(DocumentPicture docPic in document.DocumentPictures)
				{
					if(File.Exists(docPic.ImagePath))
					{
						FileInfo fInfo			= new FileInfo(docPic.ImagePath);
						File.Copy(docPic.ImagePath, targetDir+fInfo.Name, true);
					}
					
				}
			}
		}
		
	}
}

/*
 * $Log: OpenDocumentTextExporter.cs,v $
 * Revision 1.3  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.5  2006/01/05 10:28:06  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
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