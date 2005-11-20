/*
 * $Id: XmlNodeProcessor.cs,v 1.1 2005/11/20 17:31:20 larsbm Exp $
 */

using System;
using System.Collections;
using System.IO;
using System.Xml;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style;
using AODL.TextDocument.Style.Properties;

namespace AODL.Import.XmlHelper
{
	/// <summary>
	/// Internal class XmlNodeProcessor. This class is for
	/// internal usage only!
	/// </summary>
	internal class XmlNodeProcessor
	{
		/// <summary>
		/// The textdocument
		/// </summary>
		private AODL.TextDocument.TextDocument _textDocument;

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlNodeProcessor"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		public XmlNodeProcessor(AODL.TextDocument.TextDocument document)
		{
			this._textDocument			= document;
		}

		/// <summary>
		/// Reads the content nodes.
		/// </summary>
		/// <param name="contentFile">The content file.</param>
		public void ReadContentNodes(string contentFile)
		{
			try
			{
//				this._xdoc			= new XmlDocument();
//				this._xdoc.Load(contentFile);
				this._textDocument.XmlDoc	= new XmlDocument();
				this._textDocument.XmlDoc.Load(contentFile);

				XmlNode node				= this._textDocument.XmlDoc.SelectSingleNode(TextDocumentHelper.OfficeTextPath, this._textDocument.NamespaceManager);
				if(node != null)
				{
//					this._textDocument.XmlDoc.RemoveChild(node);
					foreach(XmlNode nodeChild in node.ChildNodes)
					{
						switch(nodeChild.Name)
						{
							case "text:p":
								Paragraph para		= this.CreateParagraph(nodeChild.CloneNode(true));
								this._textDocument.Content.Add(para);
								break;
							case "text:list":
								List list			= this.CreateList(nodeChild.CloneNode(true), null);
								this._textDocument.Content.Add(list);
								break;
							case "table:table":
								Table table			= this.CreateTable(nodeChild.CloneNode(true));
								this._textDocument.Content.Add(table);
								break;
							default:
								this.CreateUnknownContent(nodeChild.CloneNode(true));
								break;
						}
					}
				}
				//Remove all existing content and office styles, will be created new
				node.RemoveAll();
				this.RemoveStyleNodes();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Creates the paragraph.
		/// </summary>
		/// <param name="paragraphNode">The paragraph node.</param>
		private Paragraph CreateParagraph(XmlNode paragraphNode)
		{
			Paragraph p					= new Paragraph(paragraphNode, this._textDocument);

			if(p.Stylename != "Standard" && p.Stylename != "Table_20_Contents")
			{
				XmlNode styleNode		= this._textDocument.XmlDoc.SelectSingleNode(
						"/office:document-content/office:automatic-styles/style:style[@style:name='"+p.Stylename+"']", 
						this._textDocument.NamespaceManager);

				XmlNode styles			= this._textDocument.XmlDoc.SelectSingleNode(
					"/office:document-content/office:automatic-styles", 
					this._textDocument.NamespaceManager);

				if(styleNode != null)
				{
					ParagraphStyle pstyle		= new ParagraphStyle(p, styleNode);

					XmlNode propertieNode		= styleNode.SelectSingleNode("style:paragraph-properties",
						this._textDocument.NamespaceManager);

					ParagraphProperties pp		= null;

					XmlNode tabstyles			= null;

					if(propertieNode != null)
					{
						tabstyles				= styleNode.SelectSingleNode("style:tab-stops",
							this._textDocument.NamespaceManager);
						pp	= new ParagraphProperties(pstyle, propertieNode);
					}
					else
						pp	= new ParagraphProperties(pstyle);					

					pstyle.Properties			= pp;
					if(tabstyles != null)
						pstyle.Properties.TabStopStyleCollection	= this.GetTabStopStyles(tabstyles);
					p.Style						= pstyle;

					if(styles != null)
						styles.RemoveChild(styleNode);
				}
			}
//			Console.WriteLine("Create para: {0}", p.Node.OuterXml);
//			p				= this.ReadParagraphTextContent(p);
//			this._textDocument.Content.Add(p);
			return this.ReadParagraphTextContent(p);
		}

		/// <summary>
		/// Reads the content of the paragraph text.
		/// </summary>
		/// <param name="paragraph">The paragraph.</param>
		/// <returns></returns>
		private Paragraph ReadParagraphTextContent(Paragraph paragraph)
		{
			//TextContentProcessor.SplitTextContent(this._textDocument, paragraph);	
			//TODO: Refactor paragraph content parsing
			if(paragraph.Node.ChildNodes.Count > 0)
				if(paragraph.Node.ChildNodes.Item(0).OuterXml.StartsWith("<draw:frame"))
				{
					XmlNode framenode	= paragraph.Node.ChildNodes.Item(0).CloneNode(true);
					paragraph.Node.RemoveAll();
					paragraph.Content.Add(this.CreateFrame(framenode));

					return paragraph;
				}

			string text					= this.SetControlChars(paragraph.Node.InnerXml);
			//paragraph.Node.InnerXml		= "";
			paragraph.Node.InnerXml		= text;

			paragraph					= TextContentProcessor.SplitTextContent(this._textDocument, paragraph);	

//			if(text.IndexOf("/>") == 0)
//			{
//				//Is only simple text
//				SimpleText stext		= new SimpleText(paragraph, text);
//				paragraph.TextContent.Add((IText)stext);
//			}
//			else
//			{
//				//Contains inner xml
//				string[] innerxml		= text.Split('<');
//				for(int i=0; i < innerxml.Length; i++)
//				{
//					if(!innerxml[i].StartsWith("text:") && !innerxml[i].StartsWith("draw:"))
//					{
//						//Is simple text
//						SimpleText stext	= new SimpleText(paragraph, innerxml[i]);
//						paragraph.TextContent.Add((IText)stext);
//					}
//					else if(innerxml[i].StartsWith("text:span"))
//					{
//						//Is formated text
//						string stylename	= this.GetStyleName(innerxml[i]);
//						string itext			= this.GetTextBlock(innerxml[i]);
//						FormatedText ft		= new FormatedText(paragraph, stylename, itext);
//
//						XmlNode styleNode		= this._textDocument.XmlDoc.SelectSingleNode(
//							"/office:document-content/office:automatic-styles/style:style[@style:name='"+stylename+"']", 
//							this._textDocument.NamespaceManager);
//
//						XmlNode styles			= this._textDocument.XmlDoc.SelectSingleNode(
//							"/office:document-content/office:automatic-styles", 
//							this._textDocument.NamespaceManager);
//
//						if(stylename != null)
//						{
//							ft.Style.Node		= styleNode.CloneNode(true);
//
//							XmlNode propnode	= styleNode.SelectSingleNode("style:text-properties",
//								this._textDocument.NamespaceManager);
//
//							if(propnode != null)
//							{
//								((TextStyle)ft.Style).Properties.Node	= propnode.CloneNode(true);;
//							}
//						}
//
//						if(styles != null)
//							styles.RemoveChild(styleNode);
//
//						paragraph.TextContent.Add((IText)ft);
//
//						//Next string is close element, so jump forward
//						i++;
//					}
//					else if(innerxml[i].StartsWith("text:bookmark"))
//					{
//						Bookmark bookmark		= null;
//						if(innerxml[i].StartsWith("text:bookmark "))
//							bookmark			= new Bookmark(paragraph, BookmarkType.Standard, "noname");
//						else if(innerxml[i].StartsWith("text:bookmark-start"))
//							bookmark			= new Bookmark(paragraph, BookmarkType.Start, "noname");
//						else
//							bookmark			= new Bookmark(paragraph, BookmarkType.End, "noname");
//
//						bookmark.BookmarkName	= ""; //Reset the placeholder name
//						string name				= "name=\"";
//						int index				= innerxml[i].IndexOf(name);
//						string substr			= innerxml[i].Substring(index+name.Length);
//						
//						foreach(Char c in substr.ToCharArray())
//							if(c == '"')
//								break;
//							else
//								bookmark.BookmarkName	+= c.ToString();
//
//						paragraph.TextContent.Add(bookmark);
//
//						//Text attached ?
//						index					= substr.IndexOf("/>");
//						if(index != substr.Length-1)
//						{
//							SimpleText stext	= new SimpleText(paragraph, substr.Substring(index+2));
//							paragraph.TextContent.Add(stext);
//						}
//						
//					}
//				}
//			}

			return paragraph;
		}

		/// <summary>
		/// Creates the graphic.
		/// </summary>
		/// <param name="graphicnode">The graphicnode.</param>
		/// <returns>The Graphic object</returns>
		private Graphic CreateGraphic(XmlNode graphicnode)
		{
			try
			{
				return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Creates the frame.
		/// </summary>
		/// <param name="framenode">The framenode.</param>
		/// <returns>The Frame object.</returns>
		private Frame CreateFrame(XmlNode framenode)
		{
			try
			{
				Frame frame					= null;
				string stylename			= this.GetStyleName(framenode.OuterXml);
				XmlNode stylenode			= this.GetAStyleNode("style:style", stylename);				
				string realgraphicname		= this.GetAValueFromAnAttribute(framenode, "@draw:name");
				XmlNode graphicnode			= null;
				XmlNode graphicproperties	= null;

				if(framenode.ChildNodes.Count > 0)
					if(framenode.ChildNodes.Item(0).OuterXml.StartsWith("<draw:image"))
						graphicnode			= framenode.ChildNodes.Item(0).CloneNode(true);

				string graphicpath			= this.GetAValueFromAnAttribute(graphicnode, "@xlink:href");

				if(stylenode != null)
					if(stylenode.ChildNodes.Count > 0)
						if(stylenode.ChildNodes.Item(0).OuterXml.StartsWith("<style:graphic-properties"))
							graphicproperties	= stylenode.ChildNodes.Item(0).CloneNode(true);

				if(stylename.Length > 0 && stylenode != null && realgraphicname.Length > 0
					&& graphicnode != null && graphicpath.Length > 0 && graphicproperties != null)
				{
					graphicpath				= graphicpath.Replace("Pictures", "");
					graphicpath				= OpenDocumentTextImporter.dirpics+graphicpath.Replace("/", @"\");

					frame					= new Frame(this._textDocument, stylename, 
												realgraphicname, graphicpath);
					
					frame.Style.Node		= stylenode;
					frame.Graphic.Node		= graphicnode;
					((FrameStyle)frame.Style).GraphicProperties.Node = graphicproperties;

					//The image is loaded, so delete it
					//File.Delete(graphicpath);
//AODLTest.ImportTest.RealContentLoadTest : System.IO.FileNotFoundException : D:\OpenDocument\AODL\AODLTest\bin\Debug\PicturesRead\Pictures\lb.png
				}

				return frame;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Creates the list.
		/// </summary>
		/// <param name="listNode">The list node.</param>
		/// <param name="outerlist">The outer list.</param>
		/// <returns>The List object</returns>
		private List CreateList(XmlNode listNode, List outerlist)
		{
			try
			{
				string stylename				= null;
				XmlNode	stylenode				= null;
				ListStyles liststyles			= ListStyles.Bullet; //as default
				string paragraphstylename		= null;

				if(outerlist == null)
				{
					stylename			= this.GetStyleName(listNode.OuterXml);
					stylenode			= this.GetAStyleNode("text:list-style", stylename);				
					liststyles			= this.GetListStyle(listNode);					
				}
				List list					= null;

				if(listNode.ChildNodes.Count > 0)
				{
					try
					{
						paragraphstylename	= this.GetAValueFromAnAttribute(listNode.ChildNodes.Item(0).ChildNodes.Item(0), "@style:style-name");
					}
					catch(Exception ex)
					{
						paragraphstylename	= "P1";
					}
				}
				if(outerlist == null)
					list					= new List(this._textDocument, stylename, liststyles, paragraphstylename);
				else
					list					= new List(this._textDocument, outerlist);
				
				foreach(XmlNode node in listNode)
				{
					//Console.WriteLine("Listnode foreach: {0}, ", node.OuterXml);
					if(node.OuterXml.StartsWith("<text:list-item"))
					{
						if(node.ChildNodes.Count > 0)
						{
							ListItem li		= new ListItem(list);
							//Console.WriteLine("Listnode childnotes count: {0}, ", node.ChildNodes.Item(0).OuterXml);
							for(int i=0; i<node.ChildNodes.Count; i++)
							{
								if(node.ChildNodes.Item(i).OuterXml.StartsWith("<text:p"))
								{
									li.Paragraph	= this.CreateParagraph(node.ChildNodes.Item(i).CloneNode(true));
									list.Content.Add(li);
								}
								else if(node.ChildNodes.Item(i).OuterXml.StartsWith("<text:list"))
								{
									List innerlist	= this.CreateList(node.ChildNodes.Item(i).CloneNode(true), list);
									li.Content.Add(innerlist);
								}
							}
						}
					}
				}
				return list;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Creates the table.
		/// </summary>
		/// <param name="tablenode">The tablenode.</param>
		/// <returns></returns>
		private Table CreateTable(XmlNode tablenode)
		{
			try
			{
				string stylename			= this.GetStyleName(tablenode.OuterXml);
				XmlNode tablestylenode		= this.GetAStyleNode("style:style", stylename);
				Table table					= new Table(this._textDocument, stylename);
				table.Init(); //call internal method
				table.Style.Node			= tablestylenode;

				if(tablestylenode.ChildNodes.Count > 0)
					if(tablestylenode.ChildNodes.Item(0).Name == "style:text-properties")
						((TableStyle)table.Style).Properties.Node	=
							tablestylenode.ChildNodes.Item(0).CloneNode(true);

				foreach(XmlNode node in tablenode.ChildNodes)
				{
					if(node.OuterXml.StartsWith("<table:table-column"))
					{
						stylename					= this.GetStyleName(node.OuterXml);
						XmlNode colstylenode		= this.GetAStyleNode("style:style", stylename);						

						Column col			= new Column(table, stylename);
						col.Style.Node		= colstylenode;

						if(colstylenode.ChildNodes.Count > 0)
							if(colstylenode.ChildNodes.Item(0).Name == "style:table-column-properties")
								((ColumnStyle)col.Style).Properties.Node	=
									colstylenode.ChildNodes.Item(0).CloneNode(true);
						
						table.Columns.Add(col);
					}
					else if(node.OuterXml.StartsWith("<table:table-row"))
					{
						stylename					= this.GetStyleName(node.OuterXml);
						XmlNode rowstylenode		= this.GetAStyleNode("style:style", stylename);						

						Row row						= new Row(table, stylename);
						row.Style.Node				= rowstylenode;

						if(rowstylenode.ChildNodes.Count > 0)
							if(rowstylenode.ChildNodes.Item(0).Name == "style:table-row-properties")
								((RowStyle)row.Style).RowProperties.Node	=
									rowstylenode.ChildNodes.Item(0).CloneNode(true);
												
						foreach(XmlNode nodecell in node.ChildNodes)
						{
							stylename					= this.GetStyleName(nodecell.OuterXml);
							XmlNode cellstylenode		= this.GetAStyleNode("style:style", stylename);

							Cell cell					= new Cell(row, stylename);
							cell.Style.Node				= cellstylenode;

							if(nodecell.ChildNodes.Count > 0)
								if(nodecell.ChildNodes.Item(0).Name == "style:table-cell-properties")
									((CellStyle)cell.Style).CellProperties.Node	=
										nodecell.ChildNodes.Item(0).CloneNode(true);

							foreach(XmlNode cellcontent in nodecell.ChildNodes)
							{
								IContent icontent		= this.GetContent(cellcontent);
								if(icontent != null)
									cell.Content.Add(icontent);
							}

							row.Cells.Add(cell);
						}
						table.Rows.Add(row);
					}
				}
				return table;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Gets the tab stop styles.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private TabStopStyleCollection GetTabStopStyles(XmlNode node)
		{
			try
			{
				TabStopStyleCollection tsc			= new TabStopStyleCollection(this._textDocument);
				
				foreach(XmlNode tabstylesnode in node.ChildNodes)
				{
					TabStopStyle ts		= new TabStopStyle(this._textDocument, tabstylesnode);
					tsc.Add(ts);
				}

				return tsc;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Creates unknown content 
		/// </summary>
		/// <param name="unknownNode">The unknown node.</param>
		private void CreateUnknownContent(XmlNode unknownNode)
		{
			Console.WriteLine("Unknown: {0}", unknownNode.OuterXml);
		}

		/// <summary>
		/// Gets the name of the style.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		private string GetStyleName(string text)
		{
			string pattern		= "style-name=\"";
			int index			= text.IndexOf(pattern);
			text				= text.Remove(0, index+pattern.Length);
			int index1			= text.IndexOf("\"");

			return text.Substring(0, index1);
		}

		/// <summary>
		/// Gets the text block.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		private string GetTextBlock(string text)
		{
			int indexStart		= text.IndexOf(">");
			return text.Substring(indexStart+1);
		}

		/// <summary>
		/// Gets the A style node.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="name">The name.</param>
		/// <returns>The style node</returns>
		private XmlNode GetAStyleNode(string style, string name)
		{
			try
			{
				XmlNode styleNode		= this._textDocument.XmlDoc.SelectSingleNode(
					"/office:document-content/office:automatic-styles/"+style+"[@style:name='"+name+"']", 
					this._textDocument.NamespaceManager);

				return styleNode.CloneNode(true);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Gets the A value from an attribute.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="attributname">The attributname.</param>
		/// <returns></returns>
		private string GetAValueFromAnAttribute(XmlNode node, string attributname)
		{
			try
			{
				string avalue			= node.SelectSingleNode(attributname,
					this._textDocument.NamespaceManager).Value;

				return avalue;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Gets the list style.
		/// </summary>
		/// <param name="node">The main list node.</param>
		/// <returns></returns>
		private ListStyles GetListStyle(XmlNode node)
		{
			try
			{
				if(node.ChildNodes.Count > 0)
				{
					XmlNode child		= node.ChildNodes.Item(0);
					string name			= child.Name;
					switch(name)
					{
						case "text:list-level-style-bullet":
							return ListStyles.Bullet;
						default:
							return ListStyles.Number;
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return ListStyles.Number;
		}

		/// <summary>
		/// Gets the content.
		/// </summary>
		/// <param name="contentnode">The contentnode.</param>
		/// <returns></returns>
		private IContent GetContent(XmlNode contentnode)
		{
			switch(contentnode.Name)
			{
				case "text:p":
					Paragraph para		= this.CreateParagraph(contentnode.CloneNode(true));
					return para;
				case "text:list":
					List list			= this.CreateList(contentnode.CloneNode(true), null);
					return list;
				case "table:table":
					Table table			= this.CreateTable(contentnode.CloneNode(true));
					return table;
				default:
					return null; //unknown content;
			}
		}

		/// <summary>
		/// Removes the style node.
		/// </summary>
		private void RemoveStyleNodes()
		{
			try
			{
				XmlNode styles			= this._textDocument.XmlDoc.SelectSingleNode(
					"/office:document-content/office:automatic-styles", 
					this._textDocument.NamespaceManager);

				styles.RemoveAll();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Replace the xml elements for linebreak and tabstop
		/// with control chars.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>The text with control chars</returns>
		private string SetControlChars(string text)
		{
			text		= text.Replace("<text:line-break xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />", @"\n");
			text		= text.Replace("<text:line-break />", @"\n");
			text		= text.Replace("<text:line-break/>", @"\n");

			text		= text.Replace("<text:tab xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />", @"\t");
			text		= text.Replace("<text:tab />", @"\t");
			text		= text.Replace("<text:tab/>", @"\t");

			return text;
		}
	}
}

/*
 * $Log: XmlNodeProcessor.cs,v $
 * Revision 1.1  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 */