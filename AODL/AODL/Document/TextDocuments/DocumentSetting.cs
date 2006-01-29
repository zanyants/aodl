/*
 * $Id: DocumentSetting.cs,v 1.1 2006/01/29 11:28:30 larsbm Exp $
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
	/// DocumentSetting global Document Metadata
	/// </summary>
	public class DocumentSetting
	{
		/// <summary>
		/// The file name.
		/// </summary>
		public static readonly string FileName	= "settings.xml";

		private XmlDocument _settings;
		/// <summary>
		/// Gets or sets the styles.
		/// </summary>
		/// <value>The styles.</value>
		public XmlDocument Settings
		{
			get { return this._settings; }
			set { this._settings = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentSetting"/> class.
		/// </summary>
		public DocumentSetting()
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
				Stream str			= ass.GetManifestResourceStream("AODL.Resources.OD.settings.xml");
				this.Settings		= new XmlDocument();
				this.Settings.Load(str);
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
				this.Settings		= new XmlDocument();
				this.Settings.Load(file);
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}

/*
 * $Log: DocumentSetting.cs,v $
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