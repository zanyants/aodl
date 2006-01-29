/*
 * $Id: DocumentManifest.cs,v 1.1 2006/01/29 11:28:30 larsbm Exp $
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
using System.Reflection;
using System.IO;

namespace AODL.Document.TextDocuments
{
	/// <summary>
	/// DocumentManifest global Document Manifest
	/// </summary>
	public class DocumentManifest
	{
		/// <summary>
		/// The folder name
		/// </summary>
		public static readonly string FolderName	= "META-INF";
		/// <summary>
		/// The file name
		/// </summary>
		public static readonly string FileName		= "manifest.xml";

		private XmlDocument _manifest;
		/// <summary>
		/// Gets or sets the styles.
		/// </summary>
		/// <value>The styles.</value>
		public XmlDocument Manifest
		{
			get { return this._manifest; }
			set { this._manifest = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentManifest"/> class.
		/// </summary>
		public DocumentManifest()
		{
		}

		/// <summary>
		/// Load the style from assmebly resource.
		/// </summary>
		public virtual void New()
		{
			try
			{
				Assembly ass		= Assembly.GetExecutingAssembly();
				Stream str			= ass.GetManifestResourceStream("AODL.Resources.OD.manifest.xml");
				this.Manifest		= new XmlDocument();
				this.Manifest.Load(str);
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Loads from file.
		/// </summary>
		/// <param name="file">The file.</param>
		public void LoadFromFile(string file)
		{
			try
			{
				this.Manifest		= new XmlDocument();
				this.Manifest.Load(file);
			}
			catch(Exception ex)
			{
				this.DTDReplacer(file);
				this.LoadFromFile(file);
			}
		}

		/// <summary>
		/// DTDs the replacer, XmlDocument couldn't be loaded
		/// because the DTD wasn't found
		/// </summary>
		/// <param name="file">The file.</param>
		private void DTDReplacer(string file)
		{
			try
			{
				string text				= null;
				using (StreamReader sr = new StreamReader(file)) 
				{
					String line;
					while ((line = sr.ReadLine()) != null) 
						text			+= line;
					sr.Close();
				}
				//replace it
				text					= text.Replace("<!DOCTYPE manifest:manifest PUBLIC \"-//OpenOffice.org//DTD Manifest 1.0//EN\" \"Manifest.dtd\">", "");
				//Overwrite it
				FileStream fstream		= File.Create(file);
				StreamWriter swriter	= new StreamWriter(fstream);
				swriter.WriteLine(text);
				swriter.Close();
				fstream.Close();
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}

/*
 * $Log: DocumentManifest.cs,v $
 * Revision 1.1  2006/01/29 11:28:30  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
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