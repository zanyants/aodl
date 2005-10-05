using System;
using System.Xml;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style;
using AODL.TextDocument.Style.Properties;

namespace AODL.TextDocument
{
	/// <summary>
	/// Represent a opendocument text document.
	/// </summary>
	/// <example>
	/// <code>
	/// 	TextDocument td		= new TextDocument();
	/// 	//Create a blank document
	///		td.New();
	///		//Create a new paragraph
	///		Paragraph p			= new Paragraph(td, "P1");
	///		//Add some content
	///		p.TextContent.Add( new SimpleText((IContent)p, "Hallo i'm simple text!"));
	///		//Create and add some formated text tho the paragraph
	///		FormatedText ft = new FormatedText((IContent)p, "T1", " And i'm formated text!");
	///		((TextProperties)ft.Style.Properties).Bold = "bold";
	///		p.TextContent.Add(ft);
	///		//Add as document content 
	///		td.Content.Add(p);
	/// </code>
	/// </example>
	public class TextDocument
	{
		private XmlDocument _xmldoc;
		/// <summary>
		/// The xmldocument the textdocument based on.
		/// </summary>
		public XmlDocument XmlDoc
		{
			get { return this._xmldoc; }
			set { this._xmldoc = value; }
		}

		private XmlNamespaceManager _namespacemanager;
		/// <summary>
		/// The namespacemanager to access the documents namespace uris.
		/// </summary>
		public XmlNamespaceManager NamespaceManager
		{
			get { return this._namespacemanager; }
			set { this._namespacemanager = value; }
		}

		private IContentCollection _content;
		/// <summary>
		/// The contentcollection of the document. Represents all
		/// data to display.
		/// </summary>
		public IContentCollection Content
		{
			get { return this._content; }
			set { this._content = value; }
		}

		/// <summary>
		/// Create a new TextDocument object.
		/// </summary>
		public TextDocument()
		{
			this.Content	= new IContentCollection();
			this.Content.Inserted +=new AODL.Collections.CollectionWithEvents.CollectionChange(Content_Inserted);
		}

		/// <summary>
		/// Create a blank new document.
		/// </summary>
		public void New()
		{
			this._xmldoc = new XmlDocument();
			this._xmldoc.LoadXml(TextDocumentHelper.GetBlankDocument());
			this.NamespaceManager = TextDocumentHelper.NameSpace(this._xmldoc.NameTable);
		}

		/// <summary>
		/// Create a new XmlNode for this document.
		/// </summary>
		/// <param name="name">The elementname.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The new XmlNode.</returns>
		public XmlNode CreateNode(string name, string prefix)
		{
			if(this.XmlDoc == null)
				throw new NullReferenceException("There is no XmlDocument loaded. Couldn't create Node "+name+" with Prefix "+prefix+". "+this.GetType().ToString());
			string nuri = this.GetNamespaceUri(prefix);
			return this.XmlDoc.CreateElement(prefix, name, nuri);
		}

		/// <summary>
		/// Create a new XmlAttribute for this document.
		/// </summary>
		/// <param name="name">The attributename.</param>
		/// <param name="prefix">The prefixname.</param>
		/// <returns>The new XmlAttribute.</returns>
		public XmlAttribute CreateAttribute(string name, string prefix)
		{
			if(this.XmlDoc == null)
				throw new NullReferenceException("There is no XmlDocument loaded. Couldn't create Attribue "+name+" with Prefix "+prefix+". "+this.GetType().ToString());
			string nuri = this.GetNamespaceUri(prefix);
			return this.XmlDoc.CreateAttribute(prefix, name, nuri);
		}

		/// <summary>
		/// Save the TextDocument as OpenDocument textdocument.
		/// </summary>
		/// <param name="filename">The filename. With or without full path. Without will save the file to application path!</param>
		public void SaveTo(string filename)
		{
			try
			{
				Publish.Publisher.PublishTo(this, filename);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Return the namespaceuri for the given prefixname.
		/// </summary>
		/// <param name="prefix">The prefixname.</param>
		/// <returns></returns>
		private string GetNamespaceUri(string prefix)
		{
			foreach (string prefixx in NamespaceManager)
				if(prefix == prefixx)				
					return NamespaceManager.LookupNamespace(prefixx);
			return null;
		}

		/// <summary>
		/// Insert all XmlNodes of the inserted IContent object into
		/// the current XmlDocument.
		/// </summary>
		/// <param name="index">The index of the inserted IContent object.</param>
		/// <param name="value">The inserted IContent object.</param>
		private void Content_Inserted(int index, object value)
		{
			this.XmlDoc.SelectSingleNode(TextDocumentHelper.OfficeTextPath, 
				this.NamespaceManager).AppendChild(((IContent)value).Node);
			IStyle style	= ((IContent)value).Style;			
			this.XmlDoc.SelectSingleNode(TextDocumentHelper.AutomaticStylePath,
				this.NamespaceManager).AppendChild(style.Node);
			foreach(IText it in ((IContent)value).TextContent)
				if(it.GetType().Name == "FormatedText")
					this.XmlDoc.SelectSingleNode(TextDocumentHelper.AutomaticStylePath,
						this.NamespaceManager).AppendChild(((FormatedText)it).Style.Node);					
		}
	}
}
