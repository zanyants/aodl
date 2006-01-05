/*
 * $Id: XmlNodeProcessor.cs,v 1.5 2006/01/05 10:28:06 larsbm Exp $
 */

using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
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
					this.CreateContent(node);
//					this._textDocument.XmlDoc.RemoveChild(node);
//					foreach(XmlNode nodeChild in node.ChildNodes)
//					{
//						switch(nodeChild.Name)
//						{
//							case "text:p":
//								Paragraph para		= this.CreateParagraph(nodeChild.CloneNode(true));
//								this._textDocument.Content.Add(para);
//								break;
//							case "text:list":
//								List list			= this.CreateList(nodeChild.CloneNode(true), null);
//								this._textDocument.Content.Add(list);
//								break;
//							case "table:table":
//								Table table			= this.CreateTable(nodeChild.CloneNode(true));
//								this._textDocument.Content.Add(table);
//								break;
//							default:
//								this.CreateUnknownContent(nodeChild.CloneNode(true));
//								break;
//						}
//					}
				}
				//Remove all existing content and office styles, will be created new
				node.RemoveAll();
				this.RemoveStyleNodes();
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		public void CreateContent(XmlNode node)
		{
			try
			{
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
						case "text:h":
							Header header		= this.CreateHeader(nodeChild.CloneNode(true));
							if(header != null)
								this._textDocument.Content.Add(header);
							break;
						case "text:table-of-content":
							TableOfContents toc	= this.CreateTableOfContents(nodeChild.CloneNode(true));
							if(toc != null)
								this._textDocument.Content.Add(toc);
							break;
						default:
							this.CreateUnknownContent(nodeChild.CloneNode(true));
							break;
					}
				}
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Creates the table of contents.
		/// </summary>
		/// <param name="tocNode">The toc node.</param>
		/// <returns></returns>
		private TableOfContents CreateTableOfContents(XmlNode tocNode)
		{
			try
			{
				//return null;
				string styleName					= "Table_Of_Contents";
				TableOfContents tableOfContents		= new TableOfContents(
					this._textDocument, tocNode);
				SectionStyle sectionStyle			= new SectionStyle(
					tableOfContents, styleName);
				tableOfContents.Style				= sectionStyle;
				
				XmlNode styleNode					= null;

				XmlNode styleNameNode				= tocNode.SelectSingleNode(
					"@text:style-name", this._textDocument.NamespaceManager);
				
				if(styleNameNode != null)
					styleName						= styleNameNode.InnerText;
				
				if(styleName.Length > 0)
					styleNode					= this.GetAStyleNode("style:style", styleName);

				if(styleNode != null)
					tableOfContents.Style.Node	= styleNode;
				
				//Create the text entries
				XmlNodeList paragraphNodeList	= tocNode.SelectNodes(
					"text:index-body/text:p", this._textDocument.NamespaceManager);
				XmlNode indexBodyNode			= tocNode.SelectSingleNode("text:index-body",
					this._textDocument.NamespaceManager);
				tableOfContents._indexBodyNode	= indexBodyNode;
				IContentCollection pCollection	= new IContentCollection();

				foreach(XmlNode paragraphnode in paragraphNodeList)
				{
					Paragraph paragraph			= this.CreateParagraph(paragraphnode);					
					if(indexBodyNode != null)
						indexBodyNode.RemoveChild(paragraphnode);
					pCollection.Add(paragraph);
				}

				foreach(IContent content in pCollection)
					tableOfContents.Content.Add(content);

				return tableOfContents;
			}
			catch(Exception ex)
			{
				throw;
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

					XmlNode txtpropertieNode	= styleNode.SelectSingleNode("style:text-properties",
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

					if(txtpropertieNode != null)
					{
						TextProperties tt		= new TextProperties(p.Style);
						tt.Node					= txtpropertieNode;
						((ParagraphStyle)p.Style).Textproperties = tt;
					}

//					if(styles != null)
//						styles.RemoveChild(styleNode);
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
					IContent content	= this.CreateFrame(framenode);
					if(content != null)
						paragraph.Content.Add(content);

					return paragraph;
				}

			string text					= this.SetControlChars(paragraph.Node.InnerXml);
			//paragraph.Node.InnerXml	= "";
			paragraph.Node.InnerXml		= text;

			//Console.WriteLine(paragraph.Node.OuterXml);

			paragraph					= TextContentProcessor.SplitTextContent(this._textDocument, paragraph);	
			#region old
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
			#endregion

			return paragraph;
		}

		private Header CreateHeader(XmlNode headernode)
		{
			try
			{
				XmlNode node			= headernode.SelectSingleNode("//@text:style-name", this._textDocument.NamespaceManager);
				if(node != null)
				{
					if(!node.InnerText.StartsWith("Heading"))
					{
						//Check if a the referenced paragraphstyle reference a heading as parentstyle
						XmlNode stylenode	= this._textDocument.XmlDoc.SelectSingleNode("//style:style[@style:name='"+node.InnerText+"']", this._textDocument.NamespaceManager);
						if(stylenode != null)
						{
							XmlNode parentstyle	= stylenode.SelectSingleNode("@style:parent-style-name",
								this._textDocument.NamespaceManager);
							if(parentstyle != null)
								if(parentstyle.InnerText.StartsWith("Heading"))
									headernode.SelectSingleNode("@text:style-name", 
										this._textDocument.NamespaceManager).InnerText = parentstyle.InnerText;
						}
					}
				}

				Header header			= new Header(headernode, this._textDocument);
				string text				= this.SetControlChars(header.Node.InnerXml);
				header.Node.InnerXml	= text;

				return TextContentProcessor.SplitTextContent(this._textDocument, header);
			}
			catch(Exception ex)
			{
			}

			return null;
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
				throw;
			}
		}

		/// <summary>
		/// Creates the frame.
		/// </summary>
		/// <param name="framenode">The framenode.</param>
		/// <returns>The Frame object.</returns>
		internal Frame CreateFrame(XmlNode framenode)
		{
			try
			{
				Frame frame					= null;
				XmlNode graphicnode			= null;
				XmlNode graphicproperties	= null;
				string realgraphicname		= "";
				string stylename			= "";
				stylename					= this.GetStyleName(framenode.OuterXml);
				XmlNode stylenode			= this.GetAStyleNode("style:style", stylename);
				realgraphicname				= this.GetAValueFromAnAttribute(framenode, "@draw:name");				

				//Console.WriteLine("frame: {0}", framenode.OuterXml);

				//Up to now, the only sopported, inner content of a frame is a graphic
				if(framenode.ChildNodes.Count > 0)
					if(framenode.ChildNodes.Item(0).OuterXml.StartsWith("<draw:image"))
						graphicnode			= framenode.ChildNodes.Item(0).CloneNode(true);

				//If not graphic, it could be text-box, ole or something else
				//try to find graphic frame inside
				if(graphicnode == null)
				{
					XmlNode child		= framenode.SelectSingleNode("//draw:frame", this._textDocument.NamespaceManager);
					if(child != null)
						frame		= this.CreateFrame(child);
					return frame;
				}

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

					XmlNode nodeSize		= framenode.SelectSingleNode("@svg:height", 
						this._textDocument.NamespaceManager);

					if(nodeSize != null)
						if(nodeSize.InnerText != null)
							frame.GraphicHeight	= nodeSize.InnerText;

					nodeSize		= framenode.SelectSingleNode("@svg:width", 
						this._textDocument.NamespaceManager);

					if(nodeSize != null)
						if(nodeSize.InnerText != null)
							frame.GraphicWidth	= nodeSize.InnerText;

					//The image is loaded, so delete it
					//File.Delete(graphicpath);
//AODLTest.ImportTest.RealContentLoadTest : System.IO.FileNotFoundException : D:\OpenDocument\AODL\AODLTest\bin\Debug\PicturesRead\Pictures\lb.png
				}

				return frame;
			}
			catch(Exception ex)
			{
				throw;
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
									//Console.WriteLine("ListItem Content: {0}, ", node.ChildNodes.Item(i).OuterXml);
									Paragraph para	= this.CreateParagraph(node.ChildNodes.Item(i).CloneNode(true));
									li.Content.Add(para);
//									li.Paragraph	= this.CreateParagraph(node.ChildNodes.Item(i).CloneNode(true));
//									list.Content.Add(li);
								}
								else if(node.ChildNodes.Item(i).Name == "table:table")
								{
									Table table		= this.CreateTable(node.ChildNodes.Item(i).CloneNode(true));
									if(table != null)
										li.Content.Add(table);
								}
								else if(node.ChildNodes.Item(i).OuterXml.StartsWith("<text:list"))
								{
									List innerlist	= this.CreateList(node.ChildNodes.Item(i).CloneNode(true), list);
									li.Content.Add(innerlist);
								}
							}
							list.Content.Add(li);
						}
					}
				}
				return list;
			}
			catch(Exception ex)
			{
				throw;
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

				if(tablestylenode != null)
					if(tablestylenode.ChildNodes.Count > 0)
						if(tablestylenode.ChildNodes.Item(0).Name == "style:table-properties")
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
						
						if(colstylenode != null)
							if(colstylenode.ChildNodes.Count > 0)
								if(colstylenode.ChildNodes.Item(0).Name == "style:table-column-properties")
									((ColumnStyle)col.Style).Properties.Node	=
										colstylenode.ChildNodes.Item(0).CloneNode(true);
						
						table.Columns.Add(col);
					}
					else if(node.OuterXml.StartsWith("<table:table-row"))
					{
						Row row						= this.CreateRow(node, table);
						
						#region old Todo: delete
						//						stylename					= this.GetStyleName(node.OuterXml);
						//						XmlNode rowstylenode		= this.GetAStyleNode("style:style", stylename);						
						//
						//						Row row						= new Row(table, stylename);
						//						row.Style.Node				= rowstylenode;
						//
						//						if(rowstylenode.ChildNodes.Count > 0)
						//							if(rowstylenode.ChildNodes.Item(0).Name == "style:table-row-properties")
						//								((RowStyle)row.Style).RowProperties.Node	=
						//									rowstylenode.ChildNodes.Item(0).CloneNode(true);
						//												
						//						foreach(XmlNode nodecell in node.ChildNodes)
						//						{
						//							stylename					= this.GetStyleName(nodecell.OuterXml);
						//							XmlNode cellstylenode		= this.GetAStyleNode("style:style", stylename);
						//
						//							Cell cell					= new Cell(row, stylename);
						//							cell.Style.Node				= cellstylenode;
						//
						//							if(cellstylenode.ChildNodes.Count > 0)
						//								if(cellstylenode.ChildNodes.Item(0).Name == "style:table-cell-properties")
						//									((CellStyle)cell.Style).CellProperties.Node	=
						//										cellstylenode.ChildNodes.Item(0).CloneNode(true);
						//
						//							foreach(XmlNode cellcontent in nodecell.ChildNodes)
						//							{
						//								IContent icontent		= this.GetContent(cellcontent);
						//								if(icontent != null)
						//									cell.Content.Add(icontent);
						//							}
						//
						//							row.Cells.Add(cell);
						//						}
						#endregion

						if(row != null)
							table.Rows.Add(row);
					}
					else if(node.OuterXml.StartsWith("<table:table-header-rows"))
					{
						RowHeader rowHeader			= new RowHeader(table);
						foreach(XmlNode child in node.ChildNodes)
							if(child.Name == "table:table-row")
							{
								Row row				= this.CreateRow(child, table);
								if(row != null)
									rowHeader.RowCollection.Add(row);
							}
						table.RowHeader				= rowHeader;
					}
				}
				return table;
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Creates the row.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="table">The table.</param>
		/// <returns></returns>
		private Row CreateRow(XmlNode node, Table table)
		{
			try
			{
				string stylename			= this.GetStyleName(node.OuterXml);
				XmlNode rowstylenode		= this.GetAStyleNode("style:style", stylename);						

				Row row						= new Row(table, stylename);
				row.Style.Node				= rowstylenode;

				if(rowstylenode != null)
					if(rowstylenode.ChildNodes.Count > 0)
						if(rowstylenode.ChildNodes.Item(0).Name == "style:table-row-properties")
							((RowStyle)row.Style).RowProperties.Node	=
								rowstylenode.ChildNodes.Item(0).CloneNode(true);
												
				foreach(XmlNode nodecell in node.ChildNodes)
				{
					if(nodecell.Name != "table:covered-table-cell")
					{
						stylename					= this.GetStyleName(nodecell.OuterXml);
						XmlNode cellstylenode		= this.GetAStyleNode("style:style", stylename);

						Cell cell					= new Cell(row, stylename);
						cell.Style.Node				= cellstylenode;

						XmlNode nodeColRepeating	= nodecell.SelectSingleNode("@table:number-columns-spanned",
							this._textDocument.NamespaceManager);

						if(nodeColRepeating != null)
							if(nodeColRepeating.InnerText.Length > 0)
								cell.ColumnRepeating	= nodeColRepeating.InnerText;

						if(cellstylenode != null)
							if(cellstylenode.ChildNodes.Count > 0)
								if(cellstylenode.ChildNodes.Item(0).Name == "style:table-cell-properties")
									((CellStyle)cell.Style).CellProperties.Node	=
										cellstylenode.ChildNodes.Item(0).CloneNode(true);

						foreach(XmlNode cellcontent in nodecell.ChildNodes)
						{
							IContent icontent		= this.GetContent(cellcontent);
							if(icontent != null)
								cell.Content.Add(icontent);
						}

						row.Cells.Add(cell);
					}
					else if(nodecell.Name == "table:covered-table-cell")
					{
						row.CellSpans.Add(new CellSpan(row));
					}
				}
				return row;
			}
			catch(Exception ex)
			{
				throw;
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
				throw;
			}
		}

		/// <summary>
		/// Creates unknown content 
		/// </summary>
		/// <param name="unknownNode">The unknown node.</param>
		private void CreateUnknownContent(XmlNode unknownNode)
		{
			//Console.WriteLine("Unknown: {0}", unknownNode.OuterXml);
			try
			{
//				foreach(XmlNode node in unknownNode.ChildNodes)
//				{
//					Console.WriteLine("Create Unknown Content from: {0}", node.OuterXml);
//					if(node.OuterXml.IndexOf("OOoPDLtext") != -1)
//						Console.WriteLine("H");
//					this.CreateContent(node);
//				}
				this.CreateContent(unknownNode);
			}
			catch(Exception ex)
			{
			}
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

				if(styleNode != null)
					return styleNode.CloneNode(true);
				
				return null;
			}
			catch(Exception ex)
			{
				throw;
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
				Console.WriteLine(attributname);
				XmlNode nodeValue			= node.SelectSingleNode(attributname,
					this._textDocument.NamespaceManager);

				if(nodeValue != null)
					return nodeValue.InnerText;
			}
			catch(Exception ex)
			{
				throw;
			}
			return "";
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
				throw;
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
				throw;
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

		/// <summary>
		/// Replaces the white space tags.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		private string ReplaceWhiteSpaceTags(string text)
		{
			try
			{
				ArrayList matchList		= new ArrayList();
				string pat = @"<text:s text:c="+'"'.ToString()+@"\d+"+'"'.ToString()+" />";
				Regex r = new Regex(pat, RegexOptions.IgnoreCase);
				Match m = r.Match(text);
				while (m.Success) 
				{
					WhiteSpace w		= new WhiteSpace();
					w.Value				= m.Value;
					string number		= m.Value.Replace(@"<text:s text:c="+'"'.ToString(), "");
					number				= number.Replace('"'.ToString()+" />", "");
					w.Replacement		= "\\ws"+number;					
					matchList.Add(w);
					m = m.NextMatch();
				}
				
				foreach(WhiteSpace w in matchList)
					text		= text.Replace(w.Value, w.Replacement);
			}
			catch(Exception ex)
			{
				//unhandled, only whitespaces aren' displayed correct
			}
			return text;
		}
	}
}

/*
 * $Log: XmlNodeProcessor.cs,v $
 * Revision 1.5  2006/01/05 10:28:06  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 * Revision 1.4  2005/12/21 17:17:12  larsbm
 * - AODL new feature save gui settings
 * - Bugfixes, in XmlNodeProcessor
 *
 * Revision 1.3  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.2  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.1  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 */