/*
 * $Id: TextContentProcessor.cs,v 1.2 2005/11/20 19:30:23 larsbm Exp $
 */

using System;
using System.Collections;
using System.Xml;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style;
using AODL.TextDocument.Style.Properties;

namespace AODL.Import.XmlHelper
{
	/// <summary>
	/// Represent a Text Content Processor.
	/// </summary>
	internal class TextContentProcessor
	{
		public TextContentProcessor()
		{
		}

		public static Paragraph SplitTextContent(AODL.TextDocument.TextDocument document, Paragraph paragraph)
		{
			ITextCollection itextcol		= new ITextCollection();
			Console.WriteLine("-----------\nInner Xml : {0}", paragraph.Node.InnerXml);
			Console.WriteLine("Para cnt {0}", paragraph.Node.ChildNodes.Count);
			try
			{
				foreach(XmlNode child in paragraph.Node.ChildNodes)
				{
					//nodelist.Add(child);
					Console.WriteLine("Child {1}  : {0}",child.OuterXml, child.Name);
					Console.WriteLine("Child cnt {0}", child.ChildNodes.Count);
					switch(child.Name)
					{
						case "#text":
							itextcol.Add(new SimpleText(paragraph, child.Value.ToString()));
							break;
						case "text:span":
							itextcol.Add(CreateFormatedText(document, paragraph ,child));
							break;
						case "text:bookmark":
							itextcol.Add(CreateBookmark(child, paragraph, BookmarkType.Standard));
							break;
						case "text:bookmark-start":
							itextcol.Add(CreateBookmark(child, paragraph, BookmarkType.Start));
							break;
						case "text:bookmark-end":
							itextcol.Add(CreateBookmark(child, paragraph, BookmarkType.End));
							break;
						case "text:a":
							itextcol.Add(CreateXLink(child, paragraph));
							break;
						case "text:note":
							itextcol.Add(CreateFootnote(child, paragraph));
							break;
						default:
							break;
					}					
				}
				//Clear the child
				paragraph.Node.InnerXml		= "";
				//Attach content
				foreach(IText itext in itextcol)
					paragraph.TextContent.Add(itext);

				//Console.WriteLine("Content has {0} childs\n-----------", nodelist.Count.ToString());
				return paragraph;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		
		/// <summary>
		/// Creates the formated text.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="paragraph">The paragraph.</param>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		public static FormatedText CreateFormatedText(AODL.TextDocument.TextDocument document, Paragraph paragraph, XmlNode node)
		{
			try
			{
				string stylename			= node.SelectSingleNode(
					"@text:style-name", document.NamespaceManager).InnerText;

				XmlNode styleNode			= document.XmlDoc.SelectSingleNode(
					"/office:document-content/office:automatic-styles/style:style[@style:name='"+stylename+"']", 
					document.NamespaceManager);

				XmlNode propnode			= styleNode.SelectSingleNode("style:text-properties",
					document.NamespaceManager);

				FormatedTextCollection ftc	= new FormatedTextCollection();

				FormatedText ft				= new FormatedText(paragraph, stylename, node.InnerText);
				ft.Style.Node				= styleNode.CloneNode(true);

				if(propnode != null)
					((TextStyle)ft.Style).Properties.Node	= propnode;

				//The 1. node is always XmlText, means the text that belongs
				//to this FormatedText
				for(int i=1; i < node.ChildNodes.Count; i++)
				{
					ftc.Add(CreateFormatedText(document, paragraph, node.ChildNodes.Item(i).CloneNode(true)));
				}
				ft.FormatedTextCollection	= ftc;

				return ft;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		
		/// <summary>
		/// Creates the bookmark.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="paragraph">The paragraph.</param>
		/// <param name="type">The type.</param>
		/// <returns>The Bookmark</returns>
		public static Bookmark CreateBookmark(XmlNode node, Paragraph paragraph, BookmarkType type)
		{
			try
			{
				Bookmark bookmark		= null;
				if(type == BookmarkType.Standard)
					bookmark			= new Bookmark(paragraph, BookmarkType.Standard, "noname");
				else if(type == BookmarkType.Start)
					bookmark			= new Bookmark(paragraph, BookmarkType.Start, "noname");
				else
					bookmark			= new Bookmark(paragraph, BookmarkType.End, "noname");

				bookmark.Node			= node.CloneNode(true);

				return bookmark;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Creates the X link.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="paragraph">The paragraph.</param>
		/// <returns>The XLink object</returns>
		public static XLink CreateXLink(XmlNode node, Paragraph paragraph)
		{
			try
			{
				XLink xlink				= new XLink(paragraph);
				xlink.Node				= node.CloneNode(true);

				return xlink;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Creates the footnote.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="paragraph">The paragraph.</param>
		/// <returns>The Footnote</returns>
		public static Footnote CreateFootnote(XmlNode node, Paragraph paragraph)
		{
			try
			{
				Footnote fnote			= new Footnote(paragraph);
				fnote.Node				= node.CloneNode(true);

				return fnote;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}
}
