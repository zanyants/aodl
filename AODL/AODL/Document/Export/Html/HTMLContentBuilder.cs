/*
 * $Id: HTMLContentBuilder.cs,v 1.4 2006/02/21 19:34:55 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, Lars Behrmann, lb@OpenDocument4all.com
 * 
 * Last changes:
 * 
 */

using System;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using AODL.Document;
using AODL.Document.Exceptions;
using AODL.Document.Content;
using AODL.Document.Helper;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.Content.Text;
using AODL.Document.Content.Tables;
using AODL.Document.Content.Draw;
using AODL.Document.Content.Text.Indexes;
using AODL.Document.Content.Text.TextControl;

namespace AODL.Document.Export.Html
{
	/// <summary>
	/// HTMLContentBuilder offer public  methods
	/// to build HTML element string from AODL
	/// OpenDocument objects.
	/// </summary>
	public class HTMLContentBuilder
	{
		/// <summary>
		/// Warning delegate
		/// </summary>
		public delegate void Warning(AODLWarning warning);
		/// <summary>
		/// OnWarning event fired if something unexpected
		/// occour.
		/// </summary>
		public event Warning OnWarning;

		private HTMLStyleBuilder _hTMLStyleBuilder;
		/// <summary>
		/// Gets or sets the HTML style builder.
		/// </summary>
		/// <value>The HTML style builder.</value>
		public HTMLStyleBuilder HTMLStyleBuilder
		{
			get { return this._hTMLStyleBuilder; }
			set { this._hTMLStyleBuilder = value; }
		}

		private string _graphicTargetFolder;
		/// <summary>
		/// Gets or sets the graphic target folder.
		/// </summary>
		/// <value>The graphic target folder.</value>
		public string GraphicTargetFolder
		{
			get { return this._graphicTargetFolder; }
			set { this._graphicTargetFolder = value; }
		}

		/// <summary>
		/// The next image map name. A frame will check if it contain
		/// a image map and will set the name which will be used
		/// by the next graphic and image map.
		/// </summary>
		private string _nextImageMapName;

		/// <summary>
		/// Initializes a new instance of the <see cref="HTMLContentBuilder"/> class.
		/// </summary>
		public HTMLContentBuilder()
		{
			this.HTMLStyleBuilder			= new HTMLStyleBuilder();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HTMLContentBuilder"/> class.
		/// </summary>
		/// <param name="graphicTargetFolder">The graphic target folder.</param>
		public HTMLContentBuilder(string graphicTargetFolder)
		{
			this.GraphicTargetFolder		= graphicTargetFolder;
			this.HTMLStyleBuilder			= new HTMLStyleBuilder();
		}

		/// <summary>
		/// Gets the I content collection as HTML.
		/// </summary>
		/// <param name="iContentCollection">The i content collection.</param>
		/// <returns></returns>
		public string GetIContentCollectionAsHtml(IContentCollection iContentCollection)
		{
			string html					= "";

			try
			{
				if(iContentCollection != null)
				{
					foreach(IContent iContent in iContentCollection)
					{
						//determine type
						if(iContent is Table)
							html			+= this.GetTableAsHtml(iContent as Table);
						else if(iContent is Paragraph)
							html			+= this.GetParagraphAsHtml(iContent as Paragraph);
						else if(iContent is List)
							html			+= this.GetListAsHtml(iContent as List);
						else if(iContent is Frame)
							html			+= this.GetDrawFrameAsHtml(iContent as Frame);
						else if(iContent is DrawTextBox)
							html			+= this.GetDrawTextBoxAsHtml(iContent as DrawTextBox);
						else if(iContent is Graphic)
							html			+= this.GetGraphicAsHtml(iContent as Graphic);
						else if(iContent is ListItem)
							html			+= this.GetListItemAsHtml(iContent as ListItem);
						else if(iContent is Header)
							html			+= this.GetHeadingAsHtml(iContent as Header);
						else if(iContent is TableOfContents)
							html			+= this.GetTableOfContentsAsHtml(iContent as TableOfContents);
						else if(iContent is UnknownContent)
							html			+= this.GetUnknowContentAsHtml(iContent as UnknownContent);
						else if(iContent is ImageMap)
							html			+= this.GetImageMapAsHtml(iContent as ImageMap);
						else if(iContent is DrawArea)
							html			+= this.GetDrawAreaAsHtml(iContent as DrawArea);
						else
							//this should never happens, because all not implemented elements 
							//are unknwon content
							if(OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Finding total unknown content. This should (could) never happen.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							OnWarning(warning);
						}
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from an IContentCollection.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			return html;
		}

		/// <summary>
		/// Gets the I text collection as HTML.
		/// </summary>
		/// <param name="iTextCollection">The i text collection.</param>
		/// <param name="paragraphStyle">The paragraph style.</param>
		/// <returns></returns>
		public string GetITextCollectionAsHtml(ITextCollection iTextCollection, ParagraphStyle paragraphStyle)
		{
			string html					= "";
			int tabStopCnt				= 0;

			try
			{
				if(iTextCollection != null)
				{
					foreach(IText iText in iTextCollection)
					{
						//determine type
						if(iText is SimpleText)
						{
							string textContent	= iText.Node.InnerText;
							html				+= this.ReplaceControlNodes(textContent);
						}
						else if(iText is FormatedText)
							html			+= this.GetFormatedTextAsHtml(iText as FormatedText);
						else if(iText is WhiteSpace)
							html			+= this.GetWhiteSpacesAsHtml(iText as WhiteSpace);
						else if(iText is TabStop)
						{
							html			+= this.GetTabStopAsHtml(iText as TabStop, tabStopCnt, html, paragraphStyle);
							tabStopCnt++;
						}
						else if(iText is XLink)
							html			+= this.GetXLinkAsHtml(iText as XLink);
						else if(iText is LineBreak)
							html			+= this.GetLineBreakAsHtml();
						else if(iText is UnknownTextContent)
							html			+= this.GetUnknowTextContentAsHtml(iText as UnknownTextContent);
						else
							//this should never happens, because all not implemented elements 
							//are unknwon content
							if(OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Finding total unknown text content. This should (could) never happen.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							OnWarning(warning);
						}
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from an ITextCollection.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			return html;
		}

		/// <summary>
		/// Gets the mixed content as HTML.
		/// </summary>
		/// <param name="mixedContent">ArrayList of objects. The objects could be
		/// IContent or IText.</param>
		/// <param name="paragraphStyle">The paragraph style.</param>
		/// <returns></returns>
		public string GetMixedContentAsHTML(ArrayList mixedContent, ParagraphStyle paragraphStyle)
		{
			string html					= "";
			int tabStopCnt				= 0;

			try
			{
				if(mixedContent != null)
				{
					foreach(object ob in mixedContent)
					{
						//determine type text content types
						if(ob is SimpleText)
						{
							html			+= this.ReplaceControlNodes(((IText)ob).Node.InnerText);
						}
						else if(ob is FormatedText)
							html			+= this.GetFormatedTextAsHtml(ob as FormatedText);
						else if(ob is WhiteSpace)
							html			+= this.GetWhiteSpacesAsHtml(ob as WhiteSpace);
						else if(ob is TabStop)
						{
							html			+= this.GetTabStopAsHtml(ob as TabStop, tabStopCnt, html, paragraphStyle);
							tabStopCnt++;
						}
						else if(ob is XLink)
							html			+= this.GetXLinkAsHtml(ob as XLink);
						else if(ob is LineBreak)
							html			+= this.GetLineBreakAsHtml();
						else if(ob is UnknownTextContent)
							html			+= this.GetUnknowTextContentAsHtml(ob as UnknownTextContent);
							//determine type
						else if(ob is Table)
							html			+= this.GetTableAsHtml(ob as Table);
						else if(ob is Paragraph)
							html			+= this.GetParagraphAsHtml(ob as Paragraph);
						else if(ob is List)
							html			+= this.GetListAsHtml(ob as List);
						else if(ob is Frame)
							html			+= this.GetDrawFrameAsHtml(ob as Frame);
						else if(ob is Graphic)
							html			+= this.GetGraphicAsHtml(ob as Graphic);
						else if(ob is ListItem)
							html			+= this.GetListItemAsHtml(ob as ListItem);
						else if(ob is Header)
							html			+= this.GetHeadingAsHtml(ob as Header);
						else if(ob is UnknownContent)
							html			+= this.GetUnknowContentAsHtml(ob as UnknownContent);
						else
							//this should never happens, because all not implemented elements 
							//are unknwon content
							if(OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Finding total unknown content in mixed content. This should (could) never happen.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							OnWarning(warning);
						}
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from an ITextCollection.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			return html;			
		}

		/// <summary>
		/// Gets the table of contents as HTML.
		/// </summary>
		/// <param name="tableOfContents">The table of contents.</param>
		/// <returns></returns>
		public string GetTableOfContentsAsHtml(TableOfContents tableOfContents)
		{
			string html					= "";

			try
			{
				if(tableOfContents != null)
				{
					XmlNode nodeTitle	= tableOfContents.Node.SelectSingleNode(
						"text:index-body/text:index-title/text:p",
						tableOfContents.Document.NamespaceManager);
					if(nodeTitle != null)
					{
						html			+= "<p "+this.HTMLStyleBuilder.HeaderHtmlStyles[0]+">\n";
						html			+= nodeTitle.InnerText;
						html			+= "\n</p>\n";
					}

					if(tableOfContents.Content != null)
						html			+= this.GetIContentCollectionAsHtml(tableOfContents.Content);
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a TableOfContents.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			return html;
		}

		/// <summary>
		/// Gets the table as HTML.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <returns></returns>
		public string GetTableAsHtml(Table table)
		{
			//TODO: Implement table border algo
			string html					= "<table border=\"1\" ";

			try
			{
				if(table != null)
				{
					string style		= this.HTMLStyleBuilder.GetTableStyleAsHtml(table.TableStyle);
					if(style.Length > 0)
					{
						html			+= style;
						html			+= ">\n";
					}

					if(table.RowCollection != null)
						foreach(Row row in table.RowCollection)
							html		+= this.GetRowAsHtml(row);
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a Table object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			if(!html.Equals("<table border=\"1\" "))
				html				+= "</table>\n";
			else
				html				= "";

			return html;
		}

		/// <summary>
		/// Gets the row as HTML.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <returns></returns>
		public string GetRowAsHtml(Row row)
		{
			string html					= "<tr>\n";

			try
			{
				if(row != null)
				{
					if(row.CellCollection != null)
					{
						foreach(Cell cell in row.CellCollection)
							html		+= this.GetCellAsHtml(cell);
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a Row object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			if(!html.Equals("<tr>\n"))
				html				+= "</tr>\n";
			else
				html				= "";

			return html;
		}

		/// <summary>
		/// Gets the cell as HTML.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <returns></returns>
		public string GetCellAsHtml(Cell cell)
		{
			string html					= "<td ";

			try
			{
				if(cell != null)
				{
					if(cell.ColumnRepeating != null)
						html			+= "columnspan=\""+cell.ColumnRepeating+"\" ";

					string cellStyle	= this.HTMLStyleBuilder.GetCellStyleAsHtml(cell.CellStyle);
					if(cellStyle.Length > 0)
						html			+= cellStyle;
					
					int cellIndex		= -1;
					if(cell.Row != null)
						cellIndex		= cell.Row.GetCellIndex(cell);

					ColumnStyle colStyle	= null;
					if(cellIndex != -1 && cell.Table != null)
						if(cell.Table.ColumnCollection != null)
							if(cell.Table.ColumnCollection.Count-1 <= cellIndex)
								if(cell.Table.ColumnCollection[cellIndex].ColumnStyle != null)
									colStyle	= cell.Table.ColumnCollection[cellIndex].ColumnStyle;

					string colHtmlStyle		= this.HTMLStyleBuilder.GetColumnStyleAsHtml(colStyle);
					if(colHtmlStyle.Length > 0)
						html			+= colHtmlStyle;

					html				+= ">\n";

					string contentHtml	= this.GetIContentCollectionAsHtml(cell.Content);
					if(contentHtml.Length > 0)
						html			+= contentHtml;
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a Cell object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			if(!html.Equals("<td "))
				html				+= "</td>\n";
			else
				html				= "";

			return html;
		}

		/// <summary>
		/// Gets the paragraph as HTML.
		/// </summary>
		/// <param name="paragraph">The paragraph.</param>
		/// <returns></returns>
		public string GetParagraphAsHtml(Paragraph paragraph)
		{
			string html					= "<p ";

			try
			{
				if(paragraph != null)
				{
					if(paragraph.StyleName != null)
						if(paragraph.StyleName != "Text_20_body" 
							&& paragraph.StyleName != "standard"
							&& paragraph.StyleName != "Table_20_body")
						{
							string style	= this.HTMLStyleBuilder.GetParagraphStyleAsHtml(paragraph.ParagraphStyle);							

							if(style.Length > 0)
								html		+= style;//+">\n";
							else
							{
								//Check against a possible common style
								IStyle iStyle		= paragraph.Document.CommonStyles.GetStyleByName(paragraph.StyleName);
								string commonStyle	= "";
								if(iStyle != null && iStyle is ParagraphStyle)
								{
									commonStyle		= this.HTMLStyleBuilder.GetParagraphStyleAsHtml(iStyle as ParagraphStyle);
									if(commonStyle.Length > 0)
										html		+= commonStyle;
									else
										html		= html.Replace(" ", "");
								}
								else
									html		= html.Replace(" ", "");
							}
						}
						else
						{
							html			= html.Replace(" ", "");
						}

					html					+= ">\n";

					string txtstyle	= "<span ";
					bool useTextStyle = false;
					if(paragraph.ParagraphStyle != null)
					{
						string tstyle	= this.HTMLStyleBuilder.GetTextStyleAsHtml(paragraph.ParagraphStyle.TextProperties);
						if(txtstyle.Length > 0)
						{
							txtstyle	+= tstyle+">\n";
							html		+= txtstyle;
							useTextStyle = true;
						}
					}
					else
					{
						//Check again a possible common style
						string commonstyle	= "";
						IStyle iStyle	= paragraph.Document.CommonStyles.GetStyleByName(paragraph.StyleName);
						if(iStyle != null && iStyle is ParagraphStyle)
						{
							commonstyle	= this.HTMLStyleBuilder.GetTextStyleAsHtml(((ParagraphStyle)iStyle).TextProperties);
							if(commonstyle.Length > 0)
							{
								txtstyle	+= commonstyle+">\n";
								html		+= txtstyle;
								useTextStyle = true;
							}
						}
					}

					string mixedCont	= this.GetMixedContentAsHTML(paragraph.MixedContent, paragraph.ParagraphStyle);
					if(mixedCont.Length > 0)
						html			+= mixedCont+"&nbsp;";
					else
						html			+= "&nbsp;";

					if(!html.Equals("<p "))
						if(useTextStyle)
							html				+= "</span>\n</p>\n";
						else
							html				+= "</p>\n";
					else
						html				= "";

			if(html.Equals("<p >"))
				html				= "";
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a Heading object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}			

			return html;
		}

		/// <summary>
		/// Gets the heading as HTML.
		/// </summary>
		/// <param name="heading">The heading.</param>
		/// <returns></returns>
		public string GetHeadingAsHtml(Header heading)
		{
			string html					= "<p ";

			try
			{
				if(heading != null)
				{
					string style			= this.HTMLStyleBuilder.GetHeadingStyleAsHtml(heading);
					if(style.Length > 0)
					{
						html				+= style;
						html				+= ">\n";
					}
					else
						html				= html.Replace(" ", "")+">\n";

					
					string headerText		= "";
					string outlineLevel		= this.GetOutlineString(heading);

					string textContent		= this.GetITextCollectionAsHtml(heading.TextContent, null);
					if(textContent.Length > 0)
						headerText			+= textContent;
					
					//create an anchor
					html					+= "<a name=\""+headerText+"\">\n"+outlineLevel+" "+headerText+"\n</a>\n";
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a Heading object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			if(!html.Equals("<p "))
				html				+= "</p>\n";
			else
				html				= "";

			return html;
		}

		/// <summary>
		/// Gets the outline string.
		/// </summary>
		/// <param name="header">The header.</param>
		/// <returns></returns>
		private string GetOutlineString(Header header)
		{
			try
			{
				int outline1		= 0;
				int outline2		= 0;
				int outline3		= 0;
				int outline4		= 0;
				int outline5		= 0;
				int outline6		= 0;

				if(header.Document != null)
				{
					if(header.Document is TextDocuments.TextDocument && header.Document.Content != null)
					{
						foreach(IContent content in header.Document.Content)
							if(content is Header)
								if(((Header)content).OutLineLevel != null)
								{
									int no	= Convert.ToInt32(((Header)content).OutLineLevel);
									if(no == 1)
									{
										outline1++;
										outline2	= 0;
										outline3	= 0;
										outline4	= 0;
										outline5	= 0;
										outline6	= 0;
									}
									else if(no == 2)
										outline2++;
									else if(no == 3)
										outline3++;
									else if(no == 4)
										outline4++;
									else if(no == 5)
										outline5++;
									else if(no == 6)
										outline6++;

									if(content == header)
									{
										string sNumber		= outline1.ToString()+".";
										string sNumber1		= "";
										if(outline6 != 0)
											sNumber1		= "."+outline6.ToString()+".";
										if(outline5 != 0)
											sNumber1		= sNumber1+"."+outline5.ToString()+".";
										if(outline4 != 0)
											sNumber1		= sNumber1+"."+outline4.ToString()+".";
										if(outline3 != 0)
											sNumber1		= sNumber1+"."+outline3.ToString()+".";
										if(outline2 != 0)
											sNumber1		= sNumber1+"."+outline2.ToString()+".";
								
										sNumber				+= sNumber1;

										return sNumber.Replace("..",".");
									}
								}
					}
				}
			}
			catch(Exception ex)
			{
				if(OnWarning != null)
				{
					AODLWarning warning			= new AODLWarning("Exception while trying to get a outline string for a heading.");
					warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
					warning.OriginalException	= ex;
					warning.Node				= header.Node;
					OnWarning(warning);
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the list as HTML.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		public string GetListAsHtml(List list)
		{
			string html					= "<";

			try
			{
				if(list != null)
				{
					if(list.ListType == ListStyles.Number)
						html		+= "ol>\n";
					else
						html		+= "ul>\n";
					
					if(list.Content != null)
					{
						foreach(IContent iContent in list.Content)
							html		+= this.GetIContentCollectionAsHtml(list.Content);
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a List object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			if(html.StartsWith("<ol"))
				html				+= "</ol>\n";
			else if(html.StartsWith("<ul"))
				html				+= "</ul>\n";
			else
				html				= "";

			return html;
		}

		/// <summary>
		/// Gets the list item as HTML.
		/// </summary>
		/// <param name="listItem">The list item.</param>
		/// <returns></returns>
		public string GetListItemAsHtml(ListItem listItem)
		{
			string html					= "<li>\n";

			try
			{
				if(listItem != null)
				{
					if(listItem.Content != null)
					{
						html			+= this.GetIContentCollectionAsHtml(listItem.Content);
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a ListItem object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			if(!html.Equals("<li>\n"))
				html				+= "</li>\n";
			else
				html				= "";

			return html;
		}

		/// <summary>
		/// Gets the draw frame as HTML.
		/// </summary>
		/// <param name="frame">The frame.</param>
		/// <returns></returns>
		public string GetDrawFrameAsHtml(Frame frame)
		{
			string html					= "<p>\n";

			try
			{
				if(frame != null)
				{
					if(frame.Content != null)
					{
						//Check for possible Image Map
						bool containsImageMap			= false;
						foreach(IContent iContent in frame.Content)
							if(iContent is ImageMap)
							{
								this._nextImageMapName	= Guid.NewGuid().ToString();
								containsImageMap		= true;
								break;
							}
						if(!containsImageMap)
							this._nextImageMapName		= null;

						html			+= this.GetIContentCollectionAsHtml(frame.Content);
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a Frame object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			if(!html.Equals("<p>\n"))
				html				+= "</p>\n";
			else
				html				= "";

			return html;
		}

		/// <summary>
		/// Gets the draw text box as HTML.
		/// </summary>
		/// <param name="drawTextBox">The draw text box.</param>
		/// <returns></returns>
		public string GetDrawTextBoxAsHtml(DrawTextBox drawTextBox)
		{
			string html					= "";

			try
			{
				if(drawTextBox != null)
				{
					if(drawTextBox.Content != null)
					{
						html			+= this.GetIContentCollectionAsHtml(drawTextBox.Content);
					}
				}

				html					+= "\n";
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a ImageMap object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			return html;
		}

		/// <summary>
		/// Gets the image map as HTML.
		/// </summary>
		/// <param name="imageMap">The image map.</param>
		/// <returns></returns>
		public string GetImageMapAsHtml(ImageMap imageMap)
		{
			string html					= "<div>\n<map name=\""+this._nextImageMapName+"\">\n";

			try
			{
				if(imageMap != null)
				{
					if(imageMap.Content != null)
					{
						html			+= this.GetIContentCollectionAsHtml(imageMap.Content);
					}
				}

				html					+= "</map>\n</div>\n";
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a ImageMap object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			return html;
		}

		/// <summary>
		/// Gets the draw rectangle as HTML.
		/// </summary>
		/// <param name="drawArea">The draw area.</param>
		/// <returns></returns>
		public string GetDrawAreaAsHtml(DrawArea drawArea)
		{
			string html					= "<area shape=\"#type#\" coords=\"#coords#\" href=\"#link#\" target=\"_top\">\n";
			string coords				= null;
			int cx, cy, cxx, cyy, r		= 0;

			try
			{
				if(drawArea != null)
				{
					if(drawArea is DrawAreaRectangle)
					{
						html				= html.Replace("#link#", ((DrawAreaRectangle)drawArea).Href);
						
						cx					= SizeConverter.GetPixelFromAnOfficeSizeValue(
							((DrawAreaRectangle)drawArea).X);
						cy					= SizeConverter.GetPixelFromAnOfficeSizeValue(
							((DrawAreaRectangle)drawArea).Y);
						int w				= SizeConverter.GetPixelFromAnOfficeSizeValue(
							((DrawAreaRectangle)drawArea).Width);
						int h				= SizeConverter.GetPixelFromAnOfficeSizeValue(
							((DrawAreaRectangle)drawArea).Height);
						
						cxx					= cx+w;
						cyy					= cy+h;

						coords				= cx.ToString()+","+cy.ToString()+","+cxx.ToString()+","+cyy.ToString();
						html				= html.Replace("#coords#", coords);
						html				= html.Replace("#type#", "rect");
					}
					else if(drawArea is DrawAreaCircle)
					{
						html				= html.Replace("#link#", ((DrawAreaCircle)drawArea).Href);
						
						cx					= SizeConverter.GetPixelFromAnOfficeSizeValue(
							((DrawAreaCircle)drawArea).CX);
						cy					= SizeConverter.GetPixelFromAnOfficeSizeValue(
							((DrawAreaCircle)drawArea).CY);
						r					= SizeConverter.GetPixelFromAnOfficeSizeValue(
							((DrawAreaCircle)drawArea).Radius);

						coords				= cx.ToString()+","+cy.ToString()+","+r.ToString();
						html				= html.Replace("#coords#", coords);
						html				= html.Replace("#type#", "circle");
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a ImageMap object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			return html;
		}

		/// <summary>
		/// Gets the graphic as HTML.
		/// </summary>
		/// <param name="graphic">The graphic.</param>
		/// <returns></returns>
		public string GetGraphicAsHtml(Graphic graphic)
		{
			//standard space around 12px
			string html					= "<img hspace=\"12\" vspace=\"12\" ";

			try
			{
				if(graphic != null)
				{
					if(graphic.HRef != null)
						html			+= "src=\""+this.GraphicTargetFolder+"/"+graphic.HRef+"\" ";
					
					string graphStyle	= "";
					if(graphic.Frame != null)
						graphStyle		= this.HTMLStyleBuilder.GetFrameStyleAsHtml(graphic.Frame);
					if(graphStyle.Length > 0)
						html			+= graphStyle+" ";

					//Image map?
					if(this._nextImageMapName != null)
						html			+= "usemap=\"#"+this._nextImageMapName+"\"";

					html				+= ">\n";
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a Graphic object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			if(!html.Equals("<img "))
				html				+= "</img>\n";
			else
				html				= "";

			return html;
		}

		/// <summary>
		/// Gets the unknow content as HTML.
		/// AODL will try to search inside the node of the UnknownContent
		/// object for content this could be displayed.
		/// </summary>
		/// <param name="unknownContent">Content of the unknown.</param>
		/// <returns></returns>
		public string GetUnknowContentAsHtml(UnknownContent unknownContent)
		{
			string html					= "<span>\n";

			try
			{
				if(unknownContent != null)
				{
					if(unknownContent.Node != null)
					{
						foreach(XmlNode node in unknownContent.Node.ChildNodes)
							if(node.InnerText != null)
								html	+= this.ReplaceControlNodes(node.InnerText+" ");
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a UnknownContent object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			if(!html.Equals("<span>\n"))
				html				+= "</span>\n";
			else
				html				= "";

			return html;
		}

		/// <summary>
		/// Gets the formated text as HTML.
		/// </summary>
		/// <param name="formatedText">The formated text.</param>
		/// <returns></returns>
		public string GetFormatedTextAsHtml(FormatedText formatedText)
		{
			string html					= "<span ";

			try
			{
				if(formatedText.TextContent != null)
				{
					string textStyle	= "";
					if(formatedText.TextStyle != null)
						textStyle		= this.HTMLStyleBuilder.GetTextStyleAsHtml(formatedText.TextStyle.TextProperties);
					if(textStyle.Length > 0)
					{
						html			+= textStyle;						
					}
					else
					{
						//Check again a possible common style
						string commonstyle	= "";
						IStyle iStyle	= formatedText.Document.CommonStyles.GetStyleByName(formatedText.StyleName);
						if(iStyle != null && iStyle is TextStyle)
						{
							commonstyle	= this.HTMLStyleBuilder.GetTextStyleAsHtml(((TextStyle)iStyle).TextProperties);
							if(commonstyle.Length > 0)
								html	+= commonstyle;
						}
					}
					html			+= ">\n";

					string textContent	= this.GetITextCollectionAsHtml(formatedText.TextContent, null);
					if(textContent.Length > 0)
					{
//						textContent		= textContent.Replace("<", "&lt;");
//						textContent		= textContent.Replace(">", "&gt;");
						html			+= textContent;
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a FormatedText object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			
			if(!html.Equals("<span "))
				html				+= "</span>\n";
			else
				html				= "";

			return html;
		}

		/// <summary>
		/// Gets the X link as HTML.
		/// </summary>
		/// <param name="xLink">The x link.</param>
		/// <returns></returns>
		public string GetXLinkAsHtml(XLink xLink)
		{
			string html					= "<a ";

			try
			{
				if(xLink != null)
				{
					if(xLink.Href != null)
						if(xLink.Href.ToLower().IndexOf("|outline") == -1)
							html			+= "href=\""+xLink.Href+"\" ";
						else
						{
							string anchor	= this.GetAnchorLink(xLink.Href, xLink);
							if(anchor != null)
								html		+= "href=\"#"+anchor+"\" ";
							else
								html		+= "href=\""+xLink.Href+"\" ";
						}
					if(xLink.TargetFrameName != null)
						html			+= "target=\""+xLink.TargetFrameName+"\">\n";
					if(!html.EndsWith(">\n"))
						html			+= ">\n";

					string textContent	= this.GetITextCollectionAsHtml(xLink.TextContent, null);
					if(textContent.Length > 0)
						html			+= textContent;
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML string from a XLink object.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			if(!html.Equals("<a "))
				html				+= "</a>\n";
			else
				html				= "";

			return html;
		}

		/// <summary>
		/// Gets the anchor link.
		/// </summary>
		/// <param name="outlineLinkTarget">The outline link target.</param>
		/// <param name="xLink">The x link.</param>
		/// <returns></returns>
		private string GetAnchorLink(string outlineLinkTarget, XLink xLink)
		{
			try
			{
				string replaceMent			= "|outline";
				outlineLinkTarget			= outlineLinkTarget.Replace(replaceMent, "");
				//Get only the last part of the target and try to match a header 
				//beginning afer char index 6 should be a good decision
				//TODO: Build the outline numbering via the outline element from the global styles
				outlineLinkTarget			= outlineLinkTarget.Substring(6);
				
				if(xLink.Document != null)
					if(xLink.Document.Content != null)
							foreach(IContent iContent in xLink.Document.Content)
								if(iContent is Header)
									if(((Header)iContent).OutLineLevel != null)
									{
										string headerText		= "";
										//Get text only
										foreach(IText iText in ((Header)iContent).TextContent)
											if(iText.Text != null)
												headerText		+= iText.Text;
										if(headerText.EndsWith(outlineLinkTarget))
											return headerText;
									}
			}
			catch(Exception ex)
			{
				if(OnWarning != null)
				{
					AODLWarning warning			= new AODLWarning("Exception while trying to get an anchor string from a XLink object.");
					warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
					warning.OriginalException	= ex;
					warning.Node				= xLink.Node;
					OnWarning(warning);
				}
			}

			return null;
		}

		/// <summary>
		/// Gets the unknow text content as HTML.
		/// Maybe it's a bibliographic index or something else.
		/// The method will try to find text that could be displayed.
		/// </summary>
		/// <param name="unknownTextContent">Content of the unknown text.</param>
		/// <returns></returns>
		public string GetUnknowTextContentAsHtml(UnknownTextContent unknownTextContent)
		{
			string html					= "";

			try
			{
				if(unknownTextContent != null)
				{
					if(unknownTextContent.Node != null)
						html			+= this.ReplaceControlNodes(unknownTextContent.Node.InnerText);
				}
			}
			catch(Exception ex)
			{
			}

			return html;
		}
	
		/// <summary>
		/// Gets the tab stop as HTML.
		/// Because of the fact that no tabstop html element exist,
		/// AODL will try to simulate this with a the non breaking 
		/// line entity.
		/// </summary>
		/// <param name="tabStop">The tab stop.</param>
		/// <param name="tabStopIndex">The tab stop position from all tabstops from the textcollectio,
		/// where this tabstop belongs to.</param>
		/// <param name="htmlStringBefore">The complete html string before this tabstop.</param>
		/// <param name="paragraphStyle">The paragraph style from the enclosing paragraph.</param>
		/// <returns></returns>
		public string GetTabStopAsHtml(TabStop tabStop, int tabStopIndex, string htmlStringBefore, ParagraphStyle paragraphStyle)
		{
			//simulate a tabstop in html
			string htmlTab				= "&nbsp;&nbsp;&nbsp;&nbsp;";
			string html					= "";

			try
			{
				if(paragraphStyle != null)
					if(paragraphStyle.ParagraphProperties != null)
						if(paragraphStyle.ParagraphProperties.TabStopStyleCollection != null)
							if(paragraphStyle.ParagraphProperties.TabStopStyleCollection.Count-1 <= tabStopIndex)
							{
								TabStopStyle tabStopStyle = paragraphStyle.ParagraphProperties.TabStopStyleCollection[tabStopIndex];
								
								string leadingChar			= "&nbsp;";								
								if(tabStopStyle.LeaderText != null)
									leadingChar				= tabStopStyle.LeaderText;
								
								string[] grabInt			= tabStopStyle.Position.Split('.');
								if(grabInt.Length == 2)
								{
									double position			= Convert.ToDouble(grabInt[0]);
									//expecting that one displaying character will ~ .5cm
									if(htmlStringBefore != null)
									{
										for(int i=0; i<htmlStringBefore.Length; i++)
											position		-= 0.5;
									}
									
									if(position > 0.5)
										for(double i=0; i<position; i+=0.25)
											html			+= leadingChar;
								}
							}
			}
			catch(Exception ex)
			{
				if(OnWarning != null)
				{
					AODLWarning warning			= new AODLWarning("Exception while trying to build a simulated html tabstop.");
					warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
					warning.OriginalException	= ex;
					OnWarning(warning);
				}
			}

			if(html.Length == 0)
				html				= htmlTab;

			return html;
		}

		/// <summary>
		/// Gets the white spaces as HTML.
		/// </summary>
		/// <param name="whiteSpace">The white space.</param>
		/// <returns></returns>
		public string GetWhiteSpacesAsHtml(WhiteSpace whiteSpace)
		{
			string html					= "";
			int count					= 0;
			try
			{
				if(whiteSpace.Count != null)
					count				= Convert.ToInt32(whiteSpace.Count);

				for(int i=0; i<count; i++)
					html				+= "&nbsp;";
			}
			catch(Exception ex)
			{
				//send warning
				if(OnWarning != null)
				{
					AODLWarning warning			= new AODLWarning("Exception while trying to build HTML whitespaces.");
					warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));				
					warning.OriginalException	= ex;
					OnWarning(warning);
				}
			}

			return html;
		}

		/// <summary>
		/// Gets the line break as HTML.
		/// </summary>
		/// <returns></returns>
		public string GetLineBreakAsHtml()
		{
			return "<br>\n";
		}

		/// <summary>
		/// Replaces the control nodes.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>The cleaned text</returns>
		private string ReplaceControlNodes(string text)
		{
			try
			{
				text		= text.Replace("<", "&lt;");
				text		= text.Replace(">", "&gt;");
			}
			catch(Exception ex)
			{
				//unhandled, only some textnodes will be left
			}
			return text;
		}
	}
}

/*
 * $Log: HTMLContentBuilder.cs,v $
 * Revision 1.4  2006/02/21 19:34:55  larsbm
 * - Fixed Bug text that contains a xml tag will be imported  as UnknowText and not correct displayed if document is exported  as HTML.
 * - Fixed Bug [ 1436080 ] Common styles
 *
 * Revision 1.3  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */