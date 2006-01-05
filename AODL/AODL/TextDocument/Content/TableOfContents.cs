/*
 * $Id: TableOfContents.cs,v 1.1 2006/01/05 10:31:10 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style;
using AODL.TextDocument.Style.Properties;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Zusammenfassung für TableOfContent.
	/// </summary>
	public class TableOfContents : IContent, IContentContainer, IHtml
	{
		/// <summary>
		/// This node will represent the visible entries
		/// </summary>
		internal XmlNode _indexBodyNode;
		/// <summary>
		/// The style name for content entries
		/// </summary>
		private string _contentStyleName		= "Contents_20_";
		/// <summary>
		/// The display name for content entries
		/// </summary>
		private string _contentStyleDisplayName	= "Contents ";
		/// <summary>
		/// Is created
		/// </summary>
		private bool _isNew;

		private bool _useHyperlinks;
		/// <summary>
		/// Gets or sets a value indicating whether [use hyperlinks].
		/// If it's set to true, the text entries automaticaly will
		/// extended with Hyperlinks.
		/// </summary>
		/// <value><c>true</c> if [use hyperlinks]; otherwise, <c>false</c>.</value>
		public bool UseHyperlinks
		{
			get { return this._useHyperlinks; }
			set { this._useHyperlinks = value; }
		}

		/// <summary>
		/// Gets the title which is displayed
		/// for this table of content in english
		/// this will always be Table of Content,
		/// but this is free of choice.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@text:name",
					this.Document.NamespaceManager);
				if(xn != null)
					//Todo: Algo for more entries then 9
					return xn.InnerText.Substring(0, xn.InnerText.Length-1);
				return null;
			}
		}

		private Paragraph _titleParagraph;
		/// <summary>
		/// Gets or sets the title paragraph.
		/// </summary>
		/// <value>The title paragraph.</value>
		public Paragraph TitleParagraph
		{
			get { return this._titleParagraph; }
			set { this._titleParagraph = value; }
		}

		private TableOfContentsSource _tableOfContentsSource;
		/// <summary>
		/// Gets or sets the table of content source.
		/// </summary>
		/// <value>The table of content source.</value>
		public TableOfContentsSource TableOfContentsSource
		{
			get { return this._tableOfContentsSource; }
			set { this._tableOfContentsSource = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TableOfContents"/> class.
		/// </summary>
		/// <param name="textDocument">The text document.</param>
		/// <param name="styleName">Name of the style.</param>
		/// <param name="protectChanges">if set to <c>true</c> [protect changes].</param>
		/// <param name="textName">Title for the Table of content e.g. Table of Content</param>
		public TableOfContents(TextDocument textDocument, string styleName, bool useHyperlinks, bool protectChanges, string textName)
		{
			this.Document				= textDocument;
			this.UseHyperlinks			= useHyperlinks;
			this._isNew					= true;
			this.Style					= new SectionStyle(this, styleName);			
			
			this.NewXmlNode(styleName, protectChanges, textName);			
			
			this.TableOfContentsSource	= new TableOfContentsSource(this);
			this.TableOfContentsSource.InitStandardTableOfContentStyle();
			this.Node.AppendChild(this.TableOfContentsSource.Node);

			this.CreateIndexBody();
			this.CreateTitlePargraph();
			this.InsertContentStyle();
			this.SetOutlineStyle();
			this.RegisterEvents();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TableOfContents"/> class.
		/// </summary>
		/// <param name="textDocument">The text document.</param>
		/// <param name="tocNode">The toc node.</param>
		internal TableOfContents(TextDocument textDocument, XmlNode tocNode)
		{
			this.Document				= textDocument;
			this.Node					= tocNode;
			this.RegisterEvents();
		}

		/// <summary>
		/// Registers the events.
		/// </summary>
		private void RegisterEvents()
		{
			this.Content				= new IContentCollection();
			this.Content.Inserted		+=new AODL.Collections.CollectionWithEvents.CollectionChange(Content_Inserted);
			this.Content.Removed		+=new AODL.Collections.CollectionWithEvents.CollectionChange(Content_Removed);
		}
		
		/// <summary>
		/// News the XML node.
		/// </summary>
		/// <param name="stylename">The stylename.</param>
		/// <param name="protectChanges">if set to <c>true</c> [protect changes].</param>
		/// <param name="textName">Name of the text.</param>
		private void NewXmlNode(string stylename, bool protectChanges, string textName)
		{			
			this.Node		= this.Document.CreateNode("table-of-content", "text");

			XmlAttribute xa = this.Document.CreateAttribute("style-name", "text");
			xa.Value		= stylename;
			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("protected", "text");
			xa.Value		= protectChanges.ToString();
			this.Node.Attributes.Append(xa);

			xa				= this.Document.CreateAttribute("name", "text");			
			xa.Value		= ((textName!=null)?textName:"Table of Contents")+this.Document.TableofContentsCount;
			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// Creates the index body node.
		/// </summary>
		private void CreateIndexBody()
		{
			this._indexBodyNode		= this.Document.CreateNode("index-body", "text");
			
			//First not is always the index title
			XmlNode indexTitleNode	= this.Document.CreateNode("index-title", "text");
			
			//Create attributes for the index title
			XmlAttribute xa			= this.Document.CreateAttribute("style-name", "text");
			xa.Value				= this.Stylename;
			indexTitleNode.Attributes.Append(xa);
			
			xa						= this.Document.CreateAttribute("name", "text");
			xa.Value				= this.Title+"_Head";
			indexTitleNode.Attributes.Append(xa);

			this._indexBodyNode.AppendChild(indexTitleNode);
			this.Node.AppendChild(this._indexBodyNode);
		}

		/// <summary>
		/// Creates the title pargraph.
		/// </summary>
		private void CreateTitlePargraph()
		{
			this.TitleParagraph		= new Paragraph(this.Document, "Table_Of_Contents_Title");
			this.TitleParagraph.TextContent.Add(new SimpleText(this.TitleParagraph, this.Title));
			//Set default styles
			((ParagraphStyle)this.TitleParagraph.Style).Textproperties.Bold		= "bold";
			((ParagraphStyle)this.TitleParagraph.Style).Textproperties.FontName	= FontFamilies.Arial;
			((ParagraphStyle)this.TitleParagraph.Style).Textproperties.FontSize	= "20pt";
			//Add to the index title
			this._indexBodyNode.ChildNodes[0].AppendChild(this.TitleParagraph.Node);
		}

		/// <summary>
		/// Insert the content style nodes. These are 10 styles for
		/// each outline number one style.
		/// </summary>
		private void InsertContentStyle()
		{
			for(int i=1; i<=10; i++)
			{
				XmlNode styleNode	= this.Document.DocumentStyles.Styles.CreateElement(
					"style", "style", this.Document.GetNamespaceUri("style"));

				XmlAttribute xa		= this.Document.DocumentStyles.Styles.CreateAttribute(
					"style", "name", this.Document.GetNamespaceUri("style"));
				xa.InnerText		= this._contentStyleName+i.ToString();
				styleNode.Attributes.Append(xa);

				xa					= this.Document.DocumentStyles.Styles.CreateAttribute(
					"style", "display-name", this.Document.GetNamespaceUri("style"));
				xa.InnerText		= this._contentStyleDisplayName+i.ToString();
				styleNode.Attributes.Append(xa);

				xa					= this.Document.DocumentStyles.Styles.CreateAttribute(
					"style", "parent-style-name", this.Document.GetNamespaceUri("style"));
				xa.InnerText		= "Index";
				styleNode.Attributes.Append(xa);

				xa					= this.Document.DocumentStyles.Styles.CreateAttribute(
					"style", "family", this.Document.GetNamespaceUri("style"));
				xa.InnerText		= "paragraph";
				styleNode.Attributes.Append(xa);

				xa					= this.Document.DocumentStyles.Styles.CreateAttribute(
					"style", "class", this.Document.GetNamespaceUri("style"));
				xa.InnerText		= "index";
				styleNode.Attributes.Append(xa);
				
				XmlNode ppNode		= this.Document.DocumentStyles.Styles.CreateElement(
					"style", "paragraph-properties", this.Document.GetNamespaceUri("style"));

				xa					= this.Document.DocumentStyles.Styles.CreateAttribute(
					"fo", "margin-left", this.Document.GetNamespaceUri("fo"));
				xa.InnerText		= (0.499*(i-1)).ToString("F3").Replace(",",".")+"cm";
				ppNode.Attributes.Append(xa);

				xa					= this.Document.DocumentStyles.Styles.CreateAttribute(
					"fo", "margin-right", this.Document.GetNamespaceUri("fo"));
				xa.InnerText		= "0cm";
				ppNode.Attributes.Append(xa);

				xa					= this.Document.DocumentStyles.Styles.CreateAttribute(
					"fo", "text-indent", this.Document.GetNamespaceUri("fo"));
				xa.InnerText		= "0cm";
				ppNode.Attributes.Append(xa);

				xa					= this.Document.DocumentStyles.Styles.CreateAttribute(
					"fo", "auto-text-indent", this.Document.GetNamespaceUri("fo"));
				xa.InnerText		= "0cm";
				ppNode.Attributes.Append(xa);

				XmlNode tabsNode		= this.Document.DocumentStyles.Styles.CreateElement(
					"style", "tab-stops", this.Document.GetNamespaceUri("style"));

				XmlNode tabNode			= this.Document.DocumentStyles.Styles.CreateElement(
					"style", "tab-stop", this.Document.GetNamespaceUri("style"));

				xa					= this.Document.DocumentStyles.Styles.CreateAttribute(
					"style", "position", this.Document.GetNamespaceUri("style"));
				xa.InnerText		= (16.999-(i*0.499)).ToString("F3").Replace(",",".")+"cm";
				tabNode.Attributes.Append(xa);

				xa					= this.Document.DocumentStyles.Styles.CreateAttribute(
					"style", "type", this.Document.GetNamespaceUri("style"));
				xa.InnerText		= "right";
				tabNode.Attributes.Append(xa);

				xa					= this.Document.DocumentStyles.Styles.CreateAttribute(
					"style", "leader-style", this.Document.GetNamespaceUri("style"));
				xa.InnerText		= "dotted";
				tabNode.Attributes.Append(xa);

				xa					= this.Document.DocumentStyles.Styles.CreateAttribute(
					"style", "leader-text", this.Document.GetNamespaceUri("style"));
				xa.InnerText		= ".";
				tabNode.Attributes.Append(xa);

				tabsNode.AppendChild(tabNode);
				ppNode.AppendChild(tabsNode);
				styleNode.AppendChild(ppNode);

				this.Document.DocumentStyles.InsertOfficeStylesNode(
					styleNode, this.Document);
			}
		}

		/// <summary>
		/// Set the outline style.
		/// </summary>
		private void SetOutlineStyle()
		{
			try
			{
				for(int i=1; i<=10; i++)
				{
					this.Document.DocumentStyles.SetOutlineStyle(
						i, "1", this.Document);
				}
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		#region IContent Member

		private TextDocument _textDocument;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his TextDocument.
		/// </summary>
		/// <value></value>
		public TextDocument Document
		{
			get
			{
				return this._textDocument;
			}
			set
			{
				this._textDocument = value;
			}
		}

		private XmlNode _node;
		/// <summary>
		/// Represents the XmlNode within the content.xml from the odt file.
		/// </summary>
		/// <value></value>
		public XmlNode Node
		{
			get
			{
				return this._node;
			}
			set
			{
				this._node = value;
			}
		}

		/// <summary>
		/// The style name.
		/// </summary>
		/// <value></value>
		public string Stylename
		{
			get
			{
				return  this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager).InnerText;
			}
			set
			{
				this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this._style = value;
			}
		}
		
		/// <summary>
		/// The TableOfContent object doesn't have a TextCollection!
		/// </summary>
		/// <value>Return null</value>
		public ITextCollection TextContent
		{
			get
			{
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für TableOfContent.TextContent hinzufügen
			}
		}

		#endregion

		#region IContentContainer Member

		private IContentCollection _content;
		/// <summary>
		/// Represent all visible entries of this Table of contents.
		/// Normally you should only insert paragraph objects as
		/// entry which match the following structure
		/// e.g. 3.1 My header text \t
		/// </summary>
		/// <value>The content.</value>
		public IContentCollection Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		#endregion

		/// <summary>
		/// Content was inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Inserted(int index, object value)
		{
			this._indexBodyNode.AppendChild(((IContent)value).Node);
		}

		/// <summary>
		/// Content was removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Removed(int index, object value)
		{
			this._indexBodyNode.RemoveChild(((IContent)value).Node);
		}

		/// <summary>
		/// Gets the tab stop style.
		/// </summary>
		/// <param name="leaderStyle">The leader style.</param>
		/// <param name="leadingChar">The leading char.</param>
		/// <param name="position">The position.</param>
		/// <returns>A for a table of contents optimized TabStopStyleCollection</returns>
		public TabStopStyleCollection GetTabStopStyle(string leaderStyle, string leadingChar, double position)
		{
			TabStopStyleCollection tabStopStyleCol	= new TabStopStyleCollection(this.Document);
			//Create TabStopStyles
			TabStopStyle tabStopStyle				= new TabStopStyle(this.Document, position);
			tabStopStyle.LeaderStyle				= leaderStyle;
			tabStopStyle.LeaderText					= leadingChar;
			tabStopStyle.Type						= TabStopTypes.Center;
			//Add the tabstop
			tabStopStyleCol.Add(tabStopStyle);

			return tabStopStyleCol;
		}

		/// <summary>
		/// Insert the given text as an Table of contents entry.
		/// e.g. You just insert a Headline 1. My headline to
		/// the document and want this text also as an Table of
		/// contents entry, so you can simply add the text using
		/// this method.
		/// </summary>
		/// <param name="textEntry">The text entry.</param>
		/// <param name="outLineLevel">The outline level possible 1-10.</param>
		public void InsertEntry(string textEntry, int outLineLevel)
		{
			Paragraph paragraph				= new Paragraph(
				this.Document, "P1_Toc_Entry");
			((ParagraphStyle)paragraph.Style).ParentStyle =
				this._contentStyleName+outLineLevel.ToString();
			if(this.UseHyperlinks)
			{
				int firstWhiteSpace			= textEntry.IndexOf(" ");
				System.Text.StringBuilder sb = new System.Text.StringBuilder(textEntry);
				sb							= sb.Remove(firstWhiteSpace, 1);
				string link					= "#"+sb.ToString()+"|Outline";
				XLink xlink					= new XLink(paragraph, link, textEntry);
				paragraph.TextContent.Add(xlink);
				paragraph.TextContent.Add(new SimpleText(paragraph, "\t 1"));
			}
			else
			{
				//add the tabstop and the page number, the number is
				//always set to 1, but will be updated by the most 
				//word processors immediately to the correct page number.
				textEntry						+= "\t 1";
				paragraph.TextContent.Add(new SimpleText(paragraph, textEntry));
			}
			//There is a bug which deny to add new simple ta
			((ParagraphStyle)paragraph.Style).Properties.TabStopStyleCollection = 
				this.GetTabStopStyle(TabStopLeaderStyles.Dotted, ".", 16.999);

			this.Content.Add(paragraph);
		}

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string html					= "<br>&nbsp;\n";
			try
			{
				foreach(IContent content in this.Content)
					if(content is IHtml)
						html				+= ((IHtml)content).GetHtml()+"\n";				
			}
			catch(Exception ex)
			{
			}
			return html;
		}

		#endregion
	}
}

/*
 * $Log: TableOfContents.cs,v $
 * Revision 1.1  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 */