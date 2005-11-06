/*
 * $Id: OpenDocumentTextExporter.cs,v 1.1 2005/11/06 14:55:25 larsbm Exp $
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

namespace AODL.Export
{
	/// <summary>
	/// Zusammenfassung für OpenDocumentTextExporter.
	/// </summary>
	public class OpenDocumentTextExporter : IExporter
	{
		private static readonly string dir		= Environment.CurrentDirectory+@"\tmp\";

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
				this.SaveExistingGraphics(document.DocumentPictures, dir+"Pictures\\");
				this.SaveExistingGraphics(document.DocumentThumbnails, dir+"Thumbnails\\");
				//Don't know why VS couldn't read a textfile resource without file prefix
				WriteMimetypeFile(dir+@"\mimetyp");
				//Save graphics, which were build during creating a new document
				SaveGraphic(document, dir);
				//Now create the document
				CreateOpenDocument(filename, dir);
			}
			catch(Exception ex)
			{
				throw ex; 
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
					dpic.Image.Save(folder+dpic.ImageName);
			}
			catch(Exception ex)
			{
				throw ex;
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
				throw ex;
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
			FastZip fz = new FastZip();
			fz.CreateEmptyDirectories = true;
			fz.CreateZip(filename, directory, true, "");
		}

		/// <summary>
		/// Create an output directory with all necessary subfolders.
		/// </summary>
		/// <param name="directory">The directory.</param>
		private static void PrepareDirectory(string directory)
		{
			if(Directory.Exists(directory))
				Directory.Delete(directory, true);
			
			Directory.CreateDirectory(directory+@"\META-INF");			
			Directory.CreateDirectory(directory+@"\Configurations2");
			Directory.CreateDirectory(directory+@"\Pictures");
			Directory.CreateDirectory(directory+@"\Thumbnails");	
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
			if(File.Exists(file))
				File.Delete(file);
			StreamWriter sw = File.CreateText(file);
			sw.WriteLine("application/vnd.oasis.opendocument.text");
			sw.Close();
		}

		/// <summary>
		/// Saves all graphics used within the textdocument.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="directory">The directory.</param>
		private static void SaveGraphic(AODL.TextDocument.TextDocument document, string directory)
		{
			foreach(IContent content in document.Content)
				if(content.GetType().GetInterface("IContentContainer") != null)
					foreach(IContent continner in ((IContentContainer)content).Content)
						if(continner.GetType().Name == "Frame")
							if(((Frame)continner).Graphic != null)
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
									throw ex;
								}
							}
		}
		
	}
}

/*
 * $Log: OpenDocumentTextExporter.cs,v $
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */