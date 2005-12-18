/*
 * $Id: DocumentMetadata.cs,v 1.4 2005/12/18 18:29:46 larsbm Exp $
 */

using System;
using System.Xml;
using System.Reflection;
using System.IO;
using AODL.TextDocument.Content;

namespace AODL.TextDocument
{
	/// <summary>
	/// DocumentMetadata global Document Metadata
	/// </summary>
	public class DocumentMetadata : IHtml
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

		private TextDocument _textDocument;
		/// <summary>
		/// Gets or sets the text document.
		/// </summary>
		/// <value>The text document.</value>
		public TextDocument TextDocument
		{
			get { return this._textDocument; }
			set { this._textDocument = value; }
		}

		/// <summary>
		/// Gets or sets the generator.
		/// </summary>
		/// <value>The generator.</value>
		internal string Generator
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//meta:generator",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//meta:generator",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the title.
		/// April News
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//dc:title",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//dc:title",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the subject.
		/// e.g. April Mailing
		/// </summary>
		/// <value>The subject.</value>
		public string Subject
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//dc:subject",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//dc:subject",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the initial creator.
		/// e.g. Your Name
		/// </summary>
		/// <value>The initial creator.</value>
		public string InitialCreator
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//meta:initial-creator",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//meta:initial-creator",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the creator.
		/// </summary>
		/// <value>The creator.</value>
		public string Creator
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//dc:creator",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//dc:creator",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the creation date.
		/// e.g. 2003-02-11T16:41:37
		/// <code>
		/// //Get a DateTime which is needed
		/// DateTime.Now.ToString("s");
		/// </code>
		/// </summary>
		/// <value>The creation date.</value>
		/// <remarks>This is should not be set to the Last modified date.
		/// For last update use the last modified property.
		/// The date format must be: 2003-02-11T16:41:37
		/// otherwise maybe some Editors wouldn't load the file
		/// correct or crash.</remarks>
		public string CreationDate
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//meta:creation-date",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//meta:creation-date",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the last modified date.
		/// e.g. 2003-02-11T16:41:37
		/// </summary>
		/// <value>The last modified date.</value>
		/// <remarks>
		/// The date format must be: 2003-02-11T16:41:37
		/// otherwise maybe some Editors wouldn't load the file
		/// correct or crash.</remarks>
		public string LastModified
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//dc:date",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//dc:date",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the keywords.
		/// </summary>
		/// <value>The keywords.</value>
		/// <remarks>Set keywords sperated by whitespaces.</remarks>
		public string Keywords
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//meta:keyword",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//meta:keyword",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the language.
		/// e.g. en-US, de-DE, ...
		/// </summary>
		/// <value>The language.</value>
		public string Language
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//dc:language",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//dc:language",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the table count.
		/// </summary>
		/// <value>The table count.</value>
		public int TableCount
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:table-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
				{
					try
					{
						return Convert.ToInt32(xn.InnerText);
					}
					catch(Exception ex)
					{}
				}
				return 0;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:table-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value.ToString();
			}
		}

		/// <summary>
		/// Gets or sets the image count.
		/// </summary>
		/// <value>The image count.</value>
		public int ImageCount
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:image-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
				{
					try
					{
						return Convert.ToInt32(xn.InnerText);
					}
					catch(Exception ex)
					{}
				}
				return 0;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:image-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value.ToString();
			}
		}
		
		/// <summary>
		/// Gets or sets the object count.
		/// </summary>
		/// <value>The object count.</value>
		public int ObjectCount
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:object-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
				{
					try
					{
						return Convert.ToInt32(xn.InnerText);
					}
					catch(Exception ex)
					{}
				}
				return 0;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:object-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value.ToString();
			}
		}

		/// <summary>
		/// Gets or sets the page count.
		/// </summary>
		/// <value>The page count.</value>
		public int PageCount
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:page-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
				{
					try
					{
						return Convert.ToInt32(xn.InnerText);
					}
					catch(Exception ex)
					{}
				}
				return 0;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:page-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value.ToString();
			}
		}

		/// <summary>
		/// Gets or sets the paragraph count.
		/// </summary>
		/// <value>The paragraph count.</value>
		public int ParagraphCount
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:paragraph-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
				{
					try
					{
						return Convert.ToInt32(xn.InnerText);
					}
					catch(Exception ex)
					{}
				}
				return 0;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:paragraph-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value.ToString();
			}
		}

		/// <summary>
		/// Gets or sets the word count.
		/// </summary>
		/// <value>The word count.</value>
		public int WordCount
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:word-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
				{
					try
					{
						return Convert.ToInt32(xn.InnerText);
					}
					catch(Exception ex)
					{}
				}
				return 0;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:word-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value.ToString();
			}
		}

		/// <summary>
		/// Gets or sets the character count.
		/// </summary>
		/// <value>The character count.</value>
		public int CharacterCount
		{
			get 
			{ 
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:character-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
				{
					try
					{
						return Convert.ToInt32(xn.InnerText);
					}
					catch(Exception ex)
					{}
				}
				return 0;
			}
			set
			{
				XmlNode xn = this._meta.SelectSingleNode("//meta:document-statistic/@meta:character-count",
					this.TextDocument.NamespaceManager);
				if(xn != null)
					xn.InnerText = value.ToString();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentMetadata"/> class.
		/// </summary>
		/// <param name="textDocument">The text document.</param>
		public DocumentMetadata(TextDocument textDocument)
		{
			this.TextDocument		= textDocument;
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
				
				this.Init();
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

		/// <summary>
		/// Gets the user defined info.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <returns>Info string for the given field.</returns>
		public string GetUserDefinedInfo(UserDefinedInfo info)
		{
			try
			{ 
				XmlNode nodeInfo		= this.GetUserDefinedNode(info);

				if(nodeInfo != null)
					return nodeInfo.InnerText;
			}
			catch(Exception ex)
			{
				throw;
			}

			return null;
		}

		/// <summary>
		/// Sets the user defined info.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <param name="text">The text.</param>
		public void SetUserDefinedInfo(UserDefinedInfo info, string text)
		{
			try
			{ 
				XmlNode nodeInfo		= this.GetUserDefinedNode(info);

				if(nodeInfo != null)
					nodeInfo.InnerText	= text;
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Gets the user defined node.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <returns></returns>
		private XmlNode GetUserDefinedNode(UserDefinedInfo info)
		{
			try
			{ 
				XmlNodeList nodeList	 = this._meta.SelectNodes("//meta:user-defined",
					this.TextDocument.NamespaceManager);
				
				if(nodeList.Count == 4)
				{
					XmlNode nodeInfo	= nodeList[(int)info].SelectSingleNode("@meta:name",
						this.TextDocument.NamespaceManager);
					
					if(nodeInfo != null)
						return nodeInfo;
				}
			}
			catch(Exception ex)
			{
				throw;
			}

			return null;
		}

		/// <summary>
		/// Inits this instance.
		/// </summary>
		private void Init()
		{
			try
			{
				this.Generator			= "AODL - An OpenDocument Library";
				this.PageCount			= 0;
				this.ParagraphCount		= 0;
				this.ObjectCount		= 0;
				this.TableCount			= 0;
				this.ImageCount			= 0;
				this.WordCount			= 0;
				this.CharacterCount		= 0;
				this.CreationDate		= DateTime.Now.ToString("s");
				this.LastModified		= DateTime.Now.ToString("s");
				this.Language			= "en-US";
				this.Title				= "Untitled";
				this.Subject			= "No Subject";
				this.Keywords			= "";
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string html		= "";
			string autor	= "";
			string title	= "";

			try
			{
				if(this.InitialCreator != null)
					if(this.InitialCreator.Length > 0)
						autor	= this.InitialCreator;
	
				if(autor.Length == 0)
					if(this.Creator != null)
						if(this.Creator.Length > 0)
							autor	= this.Creator;

				if(autor.Length == 0)
					autor			= "unknown";

				if(this.Title != null)
					if(this.Title.Length > 0)
						title	= this.Title;

				if(title.Length == 0)
					title		= "Untitled";

				html		+= "<title>"+title+"</title>\n";
				html		+= "<meta content=\""+autor+"\" name=\"Author\">\n";
				html		+= "<meta content=\""+autor+"\" name=\"Publisher\">\n";
				html		+= "<meta content=\""+autor+"\" name=\"Copyright\">\n";
				if(this.Keywords != String.Empty)
					if(this.Keywords.Length > 0)
						html		+= "<meta content=\""+this.Keywords+"\" name=\"Keywords\">\n";
				if(this.Subject != String.Empty)
					if(this.Subject.Length > 0)
						html		+= "<meta content=\""+this.Subject+"\" name=\"Description\">\n";
				if(this.LastModified != String.Empty)
					if(this.LastModified.Length > 0)
						html		+= "<meta content=\""+this.LastModified+"\" name=\"Date\">\n";
			}
			catch(Exception ex)
			{
				//unhandled only html meta tags wouldn't be displayed
			}

			return html;
		}

		#endregion
	}

	/// <summary>
	/// UserDefinedInfo, each OpenDocument TextDocument
	/// support up to 4 user defined fields, where you
	/// can place additional information.
	/// </summary>
	public enum UserDefinedInfo
	{
		/// <summary>
		/// Info field 1
		/// </summary>
		Info1	= 0,
		/// <summary>
		/// Info field 2
		/// </summary>
		Info2	= 1,
		/// <summary>
		/// Info field 3
		/// </summary>
		Info3	= 2,
		/// <summary>
		/// Info field 4
		/// </summary>
		Info4	= 3
	}
}

/*
 * $Log: DocumentMetadata.cs,v $
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