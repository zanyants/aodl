/*
 * $Id: TextContentProcessor.cs,v 1.3 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;
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
//			Console.WriteLine("-----------\nInner Xml : {0}", paragraph.Node.InnerXml);
//			Console.WriteLine("Para cnt {0}", paragraph.Node.ChildNodes.Count);
			try
			{
				foreach(XmlNode child in paragraph.Node.ChildNodes)
				{
					//nodelist.Add(child);
//					Console.WriteLine("Child {1}  : {0}",child.OuterXml, child.Name);
//					Console.WriteLine("Child cnt {0}", child.ChildNodes.Count);
					switch(child.Name)
					{
						case "#text":
//							int i=0;
//							if(child.InnerText.IndexOf("your first module")!=-1)
//								i=1;//
							child.InnerText	= SpecialCharcterParser(child.InnerText);
//							child.InnerText	= child.InnerText.Replace("<", "&lt;");
//							child.InnerText	= child.InnerText.Replace(">", "&gt;");
//							child.InnerText	= child.InnerText.Replace("&", "&amp;");
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
							//Console.WriteLine("Unknown Text: {0}", child.OuterXml);
							SimpleText sText	= CreateSimpleTextFromUnknown(child, paragraph);
							if(sText != null)
								itextcol.Add(sText);
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
				throw;
			}
		}

		public static Header SplitTextContent(AODL.TextDocument.TextDocument document, Header header)
		{
			ITextCollection itextcol		= new ITextCollection();
			try
			{
				foreach(XmlNode child in header.Node.ChildNodes)
				{
					switch(child.Name)
					{
						case "#text":
							child.InnerText	= SpecialCharcterParser(child.InnerText);
//							child.InnerText	= child.InnerText.Replace("<", "&lt;");
//							child.InnerText	= child.InnerText.Replace(">", "&gt;");
//							child.InnerText	= child.InnerText.Replace("&", "&amp;");
							itextcol.Add(new SimpleText(header, child.Value.ToString()));
							break;
						case "text:span":
							itextcol.Add(CreateFormatedText(document, header,child));
							break;
						case "text:bookmark":
							itextcol.Add(CreateBookmark(child, header, BookmarkType.Standard));
							break;
						case "text:bookmark-start":
							itextcol.Add(CreateBookmark(child, header, BookmarkType.Start));
							break;
						case "text:bookmark-end":
							itextcol.Add(CreateBookmark(child, header, BookmarkType.End));
							break;
						case "text:a":
							itextcol.Add(CreateXLink(child, header));
							break;
						case "text:note":
							itextcol.Add(CreateFootnote(child, header));
							break;
						default:
							//Console.WriteLine("Unknown Text: {0}", child.OuterXml);
							SimpleText sText	= CreateSimpleTextFromUnknown(child, header);
							if(sText != null)
								itextcol.Add(sText);
							break;
					}					
				}
				//Clear the child
				header.Node.InnerXml		= "";
				//Attach content
				foreach(IText itext in itextcol)
					header.TextContent.Add(itext);

				//Console.WriteLine("Content has {0} childs\n-----------", nodelist.Count.ToString());
				return header;
			}
			catch(Exception ex)
			{
				throw;
			}
		}
		
		/// <summary>
		/// Creates the formated text.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="paragraph">The paragraph.</param>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		public static FormatedText CreateFormatedText(AODL.TextDocument.TextDocument document, IContent paragraph, XmlNode node)
		{
			try
			{
				//Console.WriteLine("Ftext innertext: {0}", node.InnerText);
				string stylename			= node.SelectSingleNode(
					"@text:style-name", document.NamespaceManager).InnerText;

				//TODO: Clear console
				//Console.WriteLine("Ftext stylename: {0}", stylename);
				int x= 0;
				if(node.InnerText == "&lt;")
					x=0;

				XmlNode styleNode			= document.XmlDoc.SelectSingleNode(
					"/office:document-content/office:automatic-styles/style:style[@style:name='"+stylename+"']", 
					document.NamespaceManager);

				//Console.WriteLine("Ftext stylenode: {0}", styleNode.OuterXml);
				XmlNode propnode	= null;
				if(styleNode != null)
					propnode			= styleNode.SelectSingleNode("style:text-properties",
					document.NamespaceManager);

				FormatedTextCollection ftc	= null;
				FormatedText ft				= null;

				try
				{
					ftc	= new FormatedTextCollection();					
					
					node.InnerText	= SpecialCharcterParser(node.InnerText);
//					node.InnerText	= node.InnerText.Replace("<", "&lt;");
//					node.InnerText	= node.InnerText.Replace(">", "&gt;");
//					node.InnerText	= node.InnerText.Replace("&", "&amp;");

					ft				= new FormatedText(paragraph, stylename, node.InnerText);					
					//TODO: Check against styles.xml
					if(styleNode != null)
						ft.Style.Node				= styleNode.CloneNode(true);

					if(propnode != null)
						((TextStyle)ft.Style).Properties.Node	= propnode;

					//Console.WriteLine("Ftext propnode: {0}", node.OuterXml);
				}
				catch(Exception ex)
				{
					throw;
				}

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
				throw;
			}
		}
		
		/// <summary>
		/// Creates the bookmark.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="paragraph">The paragraph.</param>
		/// <param name="type">The type.</param>
		/// <returns>The Bookmark</returns>
		public static Bookmark CreateBookmark(XmlNode node, IContent paragraph, BookmarkType type)
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
				throw;
			}
		}

		/// <summary>
		/// Creates the X link.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="paragraph">The paragraph.</param>
		/// <returns>The XLink object</returns>
		public static XLink CreateXLink(XmlNode node, IContent paragraph)
		{
			try
			{
				XLink xlink				= new XLink(paragraph);
				xlink.Node				= node.CloneNode(true);

				return xlink;
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Creates the footnote.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="paragraph">The paragraph.</param>
		/// <returns>The Footnote</returns>
		public static Footnote CreateFootnote(XmlNode node, IContent paragraph)
		{
			try
			{
				Footnote fnote			= new Footnote(paragraph);
				fnote.Node				= node.CloneNode(true);

				return fnote;
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Creates the simple text from unknown content.
		/// Maybe some style information will be lost,
		/// but the displayed Text will be obtained.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="content">The content.</param>
		/// <returns>The SimpleText object.</returns>
		public static SimpleText CreateSimpleTextFromUnknown(XmlNode node, IContent content)
		{
			try
			{
				if(node.InnerText != null)
					if(node.InnerText.Length > 0)
					{
						node.InnerText		= SpecialCharcterParser(node.InnerText);
						SimpleText sText	= new SimpleText(content, node.InnerText);
						return sText;
					}

				//Check against a whitespace node
				if(node.Name == "text:s")
				{
					string pat	= @"\d{1,}";
					Regex r		= new Regex(pat, RegexOptions.IgnoreCase);
					Match m		= r.Match(node.OuterXml);
					string ws	= "";
					while (m.Success) 
					{
						try
						{
							int wsCnt	= Convert.ToInt32(m.Value);
							if(wsCnt == 1)
								break;
							for(int i=0; i<wsCnt; i++)
							{
								ws		+= " ";
							}
						}
						catch(Exception ex)
						{
							//unhandled, only whitespaces won't displayed
						}
						break;
					}
					if(ws.Length > 0)
						return new SimpleText(content, ws);
				}
			}
			catch(Exception ex)
			{
				//unhandled, node type wasn't specified
			}
			//Console.WriteLine("Total unknown content: {0}", node.OuterXml);
			return null;
		}

		/// <summary>
		/// Replacement for special xml characters
		/// </summary>
		/// <param name="texttoparse">Text to parse</param>
		/// <returns>The parsed text</returns>
		public static string SpecialCharcterParser(string texttoparse)
		{
			try
			{
				texttoparse			= texttoparse.Replace("&", "&amp;");
				texttoparse			= texttoparse.Replace("<", "&lt;");
				texttoparse			= texttoparse.Replace(">", "&gt;");

				texttoparse			= texttoparse.Replace("&amp;", "&");
				texttoparse			= texttoparse.Replace("&lt;", "<");
				texttoparse			= texttoparse.Replace("&gt;", ">");				

				return texttoparse;
			}
			catch(Exception ex)
			{
			}

			return texttoparse;
		}
	}
}
