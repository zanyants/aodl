/*
 * $Id: Publisher.cs,v 1.4 2005/10/22 15:52:10 larsbm Exp $
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

namespace AODL.Publish
{
	/// <summary>
	/// Class for generating an open documents.
	/// </summary>
	public class Publisher
	{
		/// <summary>
		/// Generate an open document.
		/// </summary>
		/// <param name="td">The textdocument object.</param>
		/// <param name="filename">The filename. Ending with .odt</param>
		public static void PublishTo(AODL.TextDocument.TextDocument td, string filename)
		{
			string dir		= Environment.CurrentDirectory+@"\tmp";

			PrepareDirectory(dir);

			Assembly ass	= Assembly.GetExecutingAssembly();

			Stream[] st		= new Stream[5];
			st[0]			= ass.GetManifestResourceStream("AODL.Resources.OD.manifest.xml");
			st[1]			= ass.GetManifestResourceStream("AODL.Resources.OD.meta.xml");			
			st[3]			= ass.GetManifestResourceStream("AODL.Resources.OD.settings.xml");
			st[4]			= ass.GetManifestResourceStream("AODL.Resources.OD.styles.xml");

			Write(dir+@"\META-INF\manifest.xml", st[0]);
			Write(dir+@"\meta.xml", st[1]);
			Write(dir+@"\settings.xml", st[3]);
			Write(dir+@"\styles.xml", st[4]);
			td.XmlDoc.Save(dir+@"\content.xml");
			//Don't know why VS couldn't read a textfile resource
			WriteMimetypeFile(dir+@"\mimetyp");
			//Save graphics
			SaveGraphic(td, dir);

			CreateOpenDocument(filename, dir);
		}

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
		/// Write the resource files.
		/// </summary>
		/// <param name="filename">The resource filename.</param>
		/// <param name="s">The resource stream.</param>
		private static void Write(string filename, Stream s)
		{
			StreamWriter sw = null;
			StreamReader sr = null;
			try
			{
				if(File.Exists(filename))
					File.CreateText(filename);
				sw = new StreamWriter(filename);
				sr = new StreamReader(s);
				string line = null;
				while ((line = sr.ReadLine()) != null) 
				{
					sw.WriteLine(line);
				}
			}
			catch(Exception ex)
			{
				throw new Exception("Error while writing open document resource "+filename+"!", ex);
			}
			finally
			{
				sw.Close();
				sr.Close();
				s.Close();
			}
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
 * $Log: Publisher.cs,v $
 * Revision 1.4  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.3  2005/10/22 10:47:41  larsbm
 * - add graphic support
 *
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */