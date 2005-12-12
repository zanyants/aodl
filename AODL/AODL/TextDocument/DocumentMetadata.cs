/*
 * $Id: DocumentMetadata.cs,v 1.3 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Xml;
using System.Reflection;
using System.IO;

namespace AODL.TextDocument
{
	/// <summary>
	/// DocumentMetadata global Document Metadata
	/// </summary>
	public class DocumentMetadata
	{
		/// <summary>
		/// The file name
		/// </summary>
		public static readonly string FileName	= "meta.xml";

		private XmlDocument _meta;
		/// <summary>
		/// Gets or sets the styles.
		/// </summary>
		/// <value>The styles.</value>
		public XmlDocument Meta
		{
			get { return this._meta; }
			set { this._meta = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentMetadata"/> class.
		/// </summary>
		public DocumentMetadata()
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
				Stream str			= ass.GetManifestResourceStream("AODL.Resources.OD.meta.xml");
				this.Meta			= new XmlDocument();
				this.Meta.Load(str);
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
				this.Meta		= new XmlDocument();
				this.Meta.Load(file);
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}

/*
 * $Log: DocumentMetadata.cs,v $
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