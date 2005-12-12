/*
 * $Id: DocumentStyles.cs,v 1.4 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Xml;
using System.Reflection;
using System.IO;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style;

namespace AODL.TextDocument
{
	/// <summary>
	/// DocumentStyles global Document Style
	/// </summary>
	public class DocumentStyles
	{
		/// <summary>
		/// The file name.
		/// </summary>
		public static readonly string FileName	= "styles.xml";

		private XmlDocument _styles;
		/// <summary>
		/// Gets or sets the styles.
		/// </summary>
		/// <value>The styles.</value>
		public XmlDocument Styles
		{
			get { return this._styles; }
			set { this._styles = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentStyles"/> class.
		/// </summary>
		public DocumentStyles()
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
				Stream str			= ass.GetManifestResourceStream("AODL.Resources.OD.styles.xml");
				this.Styles			= new XmlDocument();
				this.Styles.Load(str);
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
				this.Styles		= new XmlDocument();
				this.Styles.Load(file);
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Inserts the footer.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="document">The document.</param>
		internal void InsertFooter(Paragraph content, TextDocument document)
		{
			try
			{
				bool exist			= true;
				XmlNode node		= this._styles.SelectSingleNode("//office:master-styles/style:master-page/style:footer", document.NamespaceManager);//
				if(node != null)
					node.InnerXml	= "";
				else
				{
					node			= this.CreateNode("footer", "style", document);
					exist			= false;
				}

				XmlNode	impnode		= this.Styles.ImportNode(content.Node, true);
				node.AppendChild(impnode);

				if(!exist)
					this._styles.SelectSingleNode("//office:master-styles/style:master-page", 
						document.NamespaceManager).AppendChild(node);

				this.InsertParagraphStyle(content, document);
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Inserts the header.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="document">The document.</param>
		internal void InsertHeader(Paragraph content, TextDocument document)
		{
			try
			{
				bool exist			= true;
				XmlNode node		= this._styles.SelectSingleNode("//office:master-styles/style:master-page/style:header", document.NamespaceManager);//
				if(node != null)
					node.InnerXml	= "";
				else
				{
					node			= this.CreateNode("header", "style", document);
					exist			= false;
				}

				XmlNode	impnode		= this.Styles.ImportNode(content.Node, true);
				node.AppendChild(impnode);

				if(!exist)
					this._styles.SelectSingleNode("//office:master-styles/style:master-page", 
						document.NamespaceManager).AppendChild(node);

				this.InsertParagraphStyle(content, document);
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Inserts the paragraph style.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="document">The document.</param>
		private void InsertParagraphStyle(Paragraph content, TextDocument document)
		{
			try
			{
				if(content.Style != null)
				{
					XmlNode node		= this.Styles.ImportNode(content.Style.Node, true);
					this.Styles.SelectSingleNode("//office:styles",
						document.NamespaceManager).AppendChild(node);
				}

				if(content.TextContent != null)
					foreach(IText it in content.TextContent)
						if(it is FormatedText)
						{
							XmlNode node		= this.Styles.ImportNode(it.Style.Node, true);
							this.Styles.SelectSingleNode("//office:styles",
								document.NamespaceManager).AppendChild(node);
						}
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Creates the node.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="prefix">The prefix.</param>
		/// <param name="document">The prefix.</param>
		/// <returns>The XmlNode</returns>
		private XmlNode CreateNode(string name, string prefix, TextDocument document)
		{
			try
			{
				string nuri = document.GetNamespaceUri(prefix);
				return this.Styles.CreateElement(prefix, name, nuri);
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}

/*
 * $Log: DocumentStyles.cs,v $
 * Revision 1.4  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.3  2005/11/22 21:09:19  larsbm
 * - Add simple header and footer support
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