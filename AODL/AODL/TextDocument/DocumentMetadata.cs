/*
 * $Id: DocumentMetadata.cs,v 1.1 2005/11/06 14:55:25 larsbm Exp $
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
				throw ex;
			}
		}
	}
}

/*
 * $Log: DocumentMetadata.cs,v $
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */