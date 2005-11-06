/*
 * $Id: DocumentSetting.cs,v 1.1 2005/11/06 14:55:25 larsbm Exp $
 */

using System;
using System.Xml;
using System.Reflection;
using System.IO;

namespace AODL.TextDocument
{
	/// <summary>
	/// DocumentSetting global Document Metadata
	/// </summary>
	public class DocumentSetting
	{
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
		public void New()
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
				throw ex;
			}
		}
	}
}

/*
 * $Log: DocumentSetting.cs,v $
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */