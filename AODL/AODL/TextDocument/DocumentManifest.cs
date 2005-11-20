/*
 * $Id: DocumentManifest.cs,v 1.2 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using System.Xml;
using System.Reflection;
using System.IO;

namespace AODL.TextDocument
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
		public void New()
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
				throw ex;
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
				throw ex;
			}
		}
	}
}

/*
 * $Log: DocumentManifest.cs,v $
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