/*
 * $Id: OpenDocumentTextExporter.cs,v 1.5 2006/01/05 10:28:06 larsbm Exp $
 */

using System;
using System.Drawing.Imaging;
using System.Reflection;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.Import;

namespace AODL.Export
{
	/// <summary>
	/// Zusammenfassung für OpenDocumentTextExporter.
	/// </summary>
	public class OpenDocumentTextExporter : IExporter
	{
		private static readonly string dir		= Environment.CurrentDirectory+@"\tmp\";
		private string[] _directories			= {"Configurations2", "META-INF", "Pictures", "Thumbnails"};

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenDocumentTextExporter"/> class.
		/// </summary>
		public OpenDocumentTextExporter()
		{
		}

		#region IExporter Member

		private System.Collections.ArrayList _exportErrors;
		/// <summary>
		/// Gets the export erros.
		/// </summary>
		/// <value>The export erros.</value>
		public System.Collections.ArrayList ExportErros
		{
			get
			{
				return this._exportErrors;
			}
		}

		/// <summary>
		/// Exports the specified document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="filename">The filename.</param>
		public void Export(AODL.TextDocument.TextDocument document, string filename)
		{
			try
			{
				PrepareDirectory(dir);
				//Write content
				this.WriteSingleFiles(document.DocumentManifest.Manifest, dir+DocumentManifest.FolderName+"\\"+DocumentManifest.FileName);
				this.WriteSingleFiles(document.DocumentMetadata.Meta, dir+DocumentMetadata.FileName);
				this.WriteSingleFiles(document.DocumentSetting.Settings, dir+DocumentSetting.FileName);
				this.WriteSingleFiles(document.DocumentStyles.Styles, dir+DocumentStyles.FileName);
				this.WriteSingleFiles(document.XmlDoc, dir+"content.xml");
				//Write Pictures and Thumbnails
//				this.SaveExistingGraphics(document.DocumentPictures, dir+"Pictures\\");
//				this.SaveExistingGraphics(document.DocumentThumbnails, dir+"Thumbnails\\");
				//Don't know why VS couldn't read a textfile resource without file prefix
				WriteMimetypeFile(dir+@"\mimetyp");
				//Save graphics, which were build during creating a new document
				SaveGraphic(document, dir);
				//Now create the document
				CreateOpenDocument(filename, dir);
				//Clean up resources
				this.CleanUpDirectory(dir);
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
				document.Save(filename);
			}
			catch(Exception ex)
			{
				throw;
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

//			Directory.CreateDirectory(directory+@"\META-INF");			
//			Directory.CreateDirectory(directory+@"\Configurations2");
//			Directory.CreateDirectory(directory+@"\Pictures");
//			Directory.CreateDirectory(directory+@"\Thumbnails");	
		}

		/// <summary>
		/// Helper Method: Don't know why, but it seems to be impossible
		/// to embbed a textfile as resource
		/// </summary>
		/// <param name="file">The filename.</param>
		private static void WriteMimetypeFile(string file)
		{
			//Don't know why, but it seems to be impossible
			//to embbed a textfile as resource
			try
			{
				if(File.Exists(file))
					File.Delete(file);
				StreamWriter sw = File.CreateText(file);
				sw.WriteLine("application/vnd.oasis.opendocument.text");
				sw.Close();
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Cleans the up directory.
		/// </summary>
		/// <param name="directory">The directory.</param>
		private void CleanUpDirectory(string directory)
		{
			try
			{
				foreach(string d in this._directories)
					Directory.Delete(directory+@"\"+d, true);

				if(Directory.Exists(OpenDocumentTextImporter.dirpics))
					Directory.Delete(OpenDocumentTextImporter.dirpics, true);

				File.Delete(directory+DocumentMetadata.FileName);
				File.Delete(directory+DocumentSetting.FileName);
				File.Delete(directory+DocumentStyles.FileName);
				File.Delete(directory+"content.xml");
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Saves all graphics used within the textdocument.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="directory">The directory.</param>
		internal static void SaveGraphic(AODL.TextDocument.TextDocument document, string directory)
		{
			CopyGraphics(document, directory);
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
		}

		private static void SaveGraphic(Frame continner, string directory)
		{
			try
			{
				//TODO: check supported image types
				string picturedir		= directory+@"\Pictures\";
				if(File.Exists(picturedir+((Frame)continner).RealGraphicName))
					return;
				string name				= picturedir+((Frame)continner).RealGraphicName;
				((Frame)continner).Image.Save(name);
			}
			catch(Exception ex)
			{
				throw;
			}
		}
		
		private static void CopyGraphics(AODL.TextDocument.TextDocument document, string directory)
		{
			try
			{
				string picturedir		= directory+@"\Pictures\";

				foreach(Graphic graphic in document.Graphics)
				{
					string target		= picturedir+graphic.Frame.RealGraphicName;
					File.Copy(graphic.Frame.GraphicSourcePath,  target, true);
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine("CopyGraphics: {0}", ex.Message);
				throw;
			}
		}
	}
}

/*
 * $Log: OpenDocumentTextExporter.cs,v $
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