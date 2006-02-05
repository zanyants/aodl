/*
 * $Id: MainContentProcessor.cs,v 1.4 2006/02/05 20:03:32 larsbm Exp $
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
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using AODL.Document.Content;
using AODL.Document.Content.Text;
using AODL.Document.Content.Text.Indexes;
using AODL.Document.Content.Draw;
using AODL.Document.Content.OfficeEvents;
using AODL.Document.Content.Tables;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL;
using AODL.Document.TextDocuments;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Exceptions;

namespace AODL.Document.Import.OpenDocument.NodeProcessors
{
	/// <summary>
	/// Internal class MainContentProcessor. This class is for
	/// internal usage only!
	/// </summary>
	internal class MainContentProcessor
	{
		/// <summary>
		/// If set to true all node content would be directed
		/// to Console.Out
		/// </summary>
		private bool _debugMode			= false;
		/// <summary>
		/// The textdocument
		/// </summary>
		private IDocument _document;
		/// <summary>
		/// Warning delegate
		/// </summary>
		public delegate void Warning(AODLWarning warning);
		/// <summary>
		/// OnWarning event fired if something unexpected
		/// occour.
		/// </summary>
		public event Warning OnWarning;

		/// <summary>
		/// Initializes a new instance of the <see cref="MainContentProcessor"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		public MainContentProcessor(IDocument document)
		{
			this._document			= document;
		}

		/// <summary>
		/// Reads the content nodes.
		/// </summary>
		public void ReadContentNodes()
		{
			try
			{
//				this._document.XmlDoc	= new XmlDocument();
//				this._document.XmlDoc.Load(contentFile);

				XmlNode node				= null;

				if(this._document is TextDocument)
					node	= this._document.XmlDoc.SelectSingleNode(TextDocumentHelper.OfficeTextPath, this._document.NamespaceManager);
				else if(this._document is SpreadsheetDocument)
					node	= this._document.XmlDoc.SelectSingleNode(
						"/office:document-content/office:body/office:spreadsheet", this._document.NamespaceManager);

				if(node != null)
				{
					this.CreateMainContent(node);
				}
				else
				{
					AODLException exception		= new AODLException("Unknow content type.");
					exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
					throw exception;
				}
				//Remove all existing content will be created new
				node.RemoveAll();
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Error while trying to load the content file!");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				throw exception;
			}
		}

		/// <summary>
		/// Creates the content.
		/// </summary>
		/// <param name="node">The node.</param>
		public void CreateMainContent(XmlNode node)
		{
			try
			{
				foreach(XmlNode nodeChild in node.ChildNodes)
				{
					IContent iContent		= this.CreateContent(nodeChild.CloneNode(true));

					if(iContent != null)
						this._document.Content.Add(iContent);
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("A couldn't create any content from an an first level node!.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= nodeChild;
							this.OnWarning(warning);
						}
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while processing a content node.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= node;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Gets the content.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		public IContent CreateContent(XmlNode node)
		{
			try
			{
				switch(node.Name)
				{
					case "text:p":
						return CreateParagraph(node.CloneNode(true));
					case "text:list":
						return CreateList(node.CloneNode(true));
					case "text:list-item":
						return CreateListItem(node.CloneNode(true));
					case "table:table":
						return CreateTable(node.CloneNode(true));
					case "table:table-column":
						return CreateTableColumn(node.CloneNode(true));
					case "table:table-row":
						return CreateTableRow(node.CloneNode(true));
					case "table:table-header-rows":
						return CreateTableHeaderRow(node.CloneNode(true));
					case "table:table-cell":
						return CreateTableCell(node.CloneNode(true));
					case "table:covered-table-cell":
						return CreateTableCellSpan(node.CloneNode(true));
					case "text:h":
						return CreateHeader(node.CloneNode(true));
					case "text:table-of-content":
						//Possible?
						return CreateTableOfContents(node.CloneNode(true));
					case "draw:frame":
						return CreateFrame(node.CloneNode(true));
					case "draw:text-box":
						return CreateDrawTextBox(node.CloneNode(true));
					case "draw:image":
						return CreateGraphic(node.CloneNode(true));
					case "draw:area-rectangle":
						return CreateDrawAreaRectangle(node.CloneNode(true));
					case "draw:area-circle":
						return CreateDrawAreaCircle(node.CloneNode(true));
					case "draw:image-map":
						return CreateImageMap(node.CloneNode(true));
					case "office:event-listeners":
						return CreateEventListeners(node.CloneNode(true));
					case "script:event-listener":
						return CreateEventListeners(node.CloneNode(true));
					default:
						return new UnknownContent(this._document, node.CloneNode(true));
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while processing a content node.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= node;
				exception.OriginalException	= ex;

				throw exception;
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
				if(this._document is TextDocument)
				{
					//Create the TableOfContents object
					TableOfContents tableOfContents		= new TableOfContents(
						((TextDocument)this._document), tocNode);
					//Recieve the Section style
					IStyle sectionStyle					= this._document.Styles.GetStyleByName(tableOfContents.StyleName);

					if(sectionStyle != null)
						tableOfContents.Style				= sectionStyle;
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("A SectionStyle for the TableOfContents object wasn't found.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= tocNode;
							this.OnWarning(warning);
						}
					}
				
					//Create the text entries
					XmlNodeList paragraphNodeList	= tocNode.SelectNodes(
						"text:index-body/text:p", this._document.NamespaceManager);
					XmlNode indexBodyNode			= tocNode.SelectSingleNode("text:index-body",
						this._document.NamespaceManager);
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

				return null;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a TableOfContents.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= tocNode;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the paragraph.
		/// </summary>
		/// <param name="paragraphNode">The paragraph node.</param>
		public Paragraph CreateParagraph(XmlNode paragraphNode)
		{
			try
			{
				//Create a new Paragraph
				Paragraph paragraph				= new Paragraph(paragraphNode, this._document);
				//Recieve the ParagraphStyle
				IStyle paragraphStyle			= this._document.Styles.GetStyleByName(paragraph.StyleName);

				if(paragraphStyle != null)
				{
					paragraph.Style				= paragraphStyle;
				}
				else if(paragraph.StyleName != "Standard" 
						&& paragraph.StyleName != "Table_20_Contents"
						&& paragraph.StyleName != "Text_20_body"
						&& this._document is TextDocument)
				{
					//Check if it's a user defined style
					IStyle commonStyle			= this._document.CommonStyles.GetStyleByName(paragraph.StyleName);
					if(commonStyle == null)
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("A ParagraphStyle wasn't found.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= paragraphNode;
							this.OnWarning(warning);
						}
						#region Old code Todo: delete
						//					XmlNode styleNode		= this._document.XmlDoc.SelectSingleNode(
						//						"/office:document-content/office:automatic-styles/style:style[@style:name='"+p.Stylename+"']", 
						//						this._document.NamespaceManager);
						//
						//					XmlNode styles			= this._document.XmlDoc.SelectSingleNode(
						//						"/office:document-content/office:automatic-styles", 
						//						this._document.NamespaceManager);
						//
						//					if(styleNode != null)
						//					{
						//						ParagraphStyle pstyle		= new ParagraphStyle(p, styleNode);
						//
						//						XmlNode propertieNode		= styleNode.SelectSingleNode("style:paragraph-properties",
						//							this._document.NamespaceManager);
						//
						//						XmlNode txtpropertieNode	= styleNode.SelectSingleNode("style:text-properties",
						//							this._document.NamespaceManager);
						//
						//						ParagraphProperties pp		= null;
						//
						//						XmlNode tabstyles			= null;
						//
						//						if(propertieNode != null)
						//						{
						//							tabstyles				= styleNode.SelectSingleNode("style:tab-stops",
						//								this._document.NamespaceManager);
						//							pp	= new ParagraphProperties(pstyle, propertieNode);
						//						}
						//						else
						//							pp	= new ParagraphProperties(pstyle);					
						//
						//						pstyle.Properties			= pp;
						//						if(tabstyles != null)
						//							pstyle.Properties.TabStopStyleCollection	= this.GetTabStopStyles(tabstyles);
						//						p.Style						= pstyle;
						//
						//						if(txtpropertieNode != null)
						//						{
						//							TextProperties tt		= new TextProperties(p.Style);
						//							tt.Node					= txtpropertieNode;
						//							((ParagraphStyle)p.Style).Textproperties = tt;
						//						}
						//					}
						#endregion
					}
				}

				return this.ReadParagraphTextContent(paragraph);
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a Paragraph.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= paragraphNode;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Reads the content of the paragraph text.
		/// </summary>
		/// <param name="paragraph">The paragraph.</param>
		/// <returns></returns>
		private Paragraph ReadParagraphTextContent(Paragraph paragraph)
		{
			try
			{
				if(this._debugMode)
					this.LogNode(paragraph.Node, "Log Paragraph node before");
				
				ArrayList mixedContent			= new ArrayList();
				foreach(XmlNode nodeChild in paragraph.Node.ChildNodes)
				{
					//Check for IText content first
					TextContentProcessor tcp	= new TextContentProcessor();
					IText iText					= tcp.CreateTextObject(this._document, nodeChild.CloneNode(true));
					
					if(iText != null)
						mixedContent.Add(iText);
					else
					{
						//Check against IContent
						IContent iContent		= this.CreateContent(nodeChild);
						
						if(iContent != null)
							mixedContent.Add(iContent);
					}
				}

				//Remove all
				paragraph.Node.InnerXml			= "";

				foreach(Object ob in mixedContent)
				{
					if(ob is IText)
					{
						if(this._debugMode)
							this.LogNode(((IText)ob).Node, "Log IText node read");
						paragraph.TextContent.Add(ob as IText);
					}
					else if(ob is IContent)
					{
						if(this._debugMode)
							this.LogNode(((IContent)ob).Node, "Log IContent node read");
						paragraph.Content.Add(ob as IContent);
					}
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't determine the type of a paragraph child node!.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= paragraph.Node;
							this.OnWarning(warning);
						}
					}
				}

				if(this._debugMode)
					this.LogNode(paragraph.Node, "Log Paragraph node after");

				return paragraph;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create the Paragraph content.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= paragraph.Node;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the header.
		/// </summary>
		/// <param name="headernode">The headernode.</param>
		/// <returns></returns>
		public Header CreateHeader(XmlNode headernode)
		{
			try
			{
				#region Old code Todo: delete
//				XmlNode node			= headernode.SelectSingleNode("//@text:style-name", this._document.NamespaceManager);
//				if(node != null)
//				{
//					if(!node.InnerText.StartsWith("Heading"))
//					{
//						//Check if a the referenced paragraphstyle reference a heading as parentstyle
//						XmlNode stylenode	= this._document.XmlDoc.SelectSingleNode("//style:style[@style:name='"+node.InnerText+"']", this._document.NamespaceManager);
//						if(stylenode != null)
//						{
//							XmlNode parentstyle	= stylenode.SelectSingleNode("@style:parent-style-name",
//								this._document.NamespaceManager);
//							if(parentstyle != null)
//								if(parentstyle.InnerText.StartsWith("Heading"))
//									headernode.SelectSingleNode("@text:style-name", 
//										this._document.NamespaceManager).InnerText = parentstyle.InnerText;
//						}
//					}
//				}
				#endregion
				if(this._debugMode)
					this.LogNode(headernode, "Log header node before");

				//Create a new Header
				Header header				= new Header(headernode, this._document);
				//Create a ITextCollection
				ITextCollection textColl	= new ITextCollection();
				//Recieve the HeaderStyle
				IStyle headerStyle			= this._document.Styles.GetStyleByName(header.StyleName);

				if(headerStyle != null)
					header.Style			= headerStyle;

				//Create the IText content
				foreach(XmlNode nodeChild in header.Node.ChildNodes)
				{
					TextContentProcessor tcp	= new TextContentProcessor();
					IText iText					= tcp.CreateTextObject(this._document, nodeChild);
					
					if(iText != null)
						textColl.Add(iText);
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't create IText object from header child node!.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= nodeChild;
							this.OnWarning(warning);
						}
					}
				}

				//Remove all
				header.Node.InnerXml		= "";

				foreach(IText iText in textColl)
				{
					if(this._debugMode)
						this.LogNode(iText.Node, "Log IText node read from header");
					header.TextContent.Add(iText);
				}

				return header;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a Header.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= headernode;
				exception.OriginalException	= ex;

				throw exception;
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
				Graphic graphic				= new Graphic(this._document, null, null);
				graphic.Node				= graphicnode;

				return graphic;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a Graphic.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= graphicnode;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the draw text box.
		/// </summary>
		/// <param name="drawTextBoxNode">The draw text box node.</param>
		/// <returns></returns>
		private DrawTextBox CreateDrawTextBox(XmlNode drawTextBoxNode)
		{
			try
			{
				DrawTextBox drawTextBox		= new DrawTextBox(this._document, drawTextBoxNode);
				IContentCollection iColl	= new IContentCollection();

				foreach(XmlNode nodeChild in drawTextBox.Node.ChildNodes)
				{
					IContent iContent				= this.CreateContent(nodeChild);
					if(iContent != null)
						iColl.Add(iContent);
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't create a IContent object for a DrawTextBox.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= nodeChild;
							this.OnWarning(warning);
						}
					}
				}

				drawTextBox.Node.InnerXml					= "";

				foreach(IContent iContent in iColl)
					drawTextBox.Content.Add(iContent);

				return drawTextBox;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a Graphic.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= drawTextBoxNode;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the draw area rectangle.
		/// </summary>
		/// <param name="drawAreaRectangleNode">The draw area rectangle node.</param>
		/// <returns></returns>
		private DrawAreaRectangle CreateDrawAreaRectangle(XmlNode drawAreaRectangleNode)
		{
			try
			{
				DrawAreaRectangle dAreaRec	= new DrawAreaRectangle(this._document, drawAreaRectangleNode);
				IContentCollection iCol		= new IContentCollection();

				if(dAreaRec.Node != null)
					foreach(XmlNode nodeChild in dAreaRec.Node.ChildNodes)
					{
						IContent iContent	= this.CreateContent(nodeChild);
						if(iContent != null)
							iCol.Add(iContent);
					}

				dAreaRec.Node.InnerXml		= "";

				foreach(IContent iContent in iCol)
					dAreaRec.Content.Add(iContent);

				return dAreaRec;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a DrawAreaRectangle.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= drawAreaRectangleNode;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the draw area circle.
		/// </summary>
		/// <param name="drawAreaCircleNode">The draw area circle node.</param>
		/// <returns></returns>
		private DrawAreaCircle CreateDrawAreaCircle(XmlNode drawAreaCircleNode)
		{
			try
			{
				DrawAreaCircle dAreaCirc	= new DrawAreaCircle(this._document, drawAreaCircleNode);
				IContentCollection iCol		= new IContentCollection();

				if(dAreaCirc.Node != null)
					foreach(XmlNode nodeChild in dAreaCirc.Node.ChildNodes)
					{
						IContent iContent	= this.CreateContent(nodeChild);
						if(iContent != null)
							iCol.Add(iContent);
					}

				dAreaCirc.Node.InnerXml		= "";

				foreach(IContent iContent in iCol)
					dAreaCirc.Content.Add(iContent);

				return dAreaCirc;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a DrawAreaCircle.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= drawAreaCircleNode;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the image map.
		/// </summary>
		/// <param name="imageMapNode">The image map node.</param>
		/// <returns></returns>
		private ImageMap CreateImageMap(XmlNode imageMapNode)
		{
			try
			{
				ImageMap imageMap			= new ImageMap(this._document, imageMapNode);
				IContentCollection iCol		= new IContentCollection();

				if(imageMap.Node != null)
					foreach(XmlNode nodeChild in imageMap.Node.ChildNodes)
					{
						IContent iContent	= this.CreateContent(nodeChild);
						if(iContent != null)
							iCol.Add(iContent);
					}

				imageMap.Node.InnerXml		= "";

				foreach(IContent iContent in iCol)
					imageMap.Content.Add(iContent);

				return imageMap;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a ImageMap.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= imageMapNode;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the event listener.
		/// </summary>
		/// <param name="eventListenerNode">The event listener node.</param>
		/// <returns></returns>
		public EventListener CreateEventListener(XmlNode eventListenerNode)
		{
			try
			{
				EventListener eventListener	= new EventListener(this._document, eventListenerNode);				

				return eventListener;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a EventListener.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= eventListenerNode;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the event listeners.
		/// </summary>
		/// <param name="eventListenersNode">The event listeners node.</param>
		/// <returns></returns>
		public EventListeners CreateEventListeners(XmlNode eventListenersNode)
		{
			try
			{
				EventListeners eventList	= new EventListeners(this._document, eventListenersNode);
				IContentCollection iCol		= new IContentCollection();

				if(eventList.Node != null)
					foreach(XmlNode nodeChild in eventList.Node.ChildNodes)
					{
						IContent iContent	= this.CreateContent(nodeChild);
						if(iContent != null)
							iCol.Add(iContent);
					}

				eventList.Node.InnerXml		= "";

				foreach(IContent iContent in iCol)
					eventList.Content.Add(iContent);

				return eventList;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a ImageMap.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= eventListenersNode;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the frame.
		/// </summary>
		/// <param name="frameNode">The framenode.</param>
		/// <returns>The Frame object.</returns>
		public Frame CreateFrame(XmlNode frameNode)
		{
			try
			{
				#region Old code Todo: delete
//				Frame frame					= null;
//				XmlNode graphicnode			= null;
//				XmlNode graphicproperties	= null;
//				string realgraphicname		= "";
//				string stylename			= "";
//				stylename					= this.GetStyleName(framenode.OuterXml);
//				XmlNode stylenode			= this.GetAStyleNode("style:style", stylename);
//				realgraphicname				= this.GetAValueFromAnAttribute(framenode, "@draw:name");				
//
//				//Console.WriteLine("frame: {0}", framenode.OuterXml);
//
//				//Up to now, the only sopported, inner content of a frame is a graphic
//				if(framenode.ChildNodes.Count > 0)
//					if(framenode.ChildNodes.Item(0).OuterXml.StartsWith("<draw:image"))
//						graphicnode			= framenode.ChildNodes.Item(0).CloneNode(true);
//
//				//If not graphic, it could be text-box, ole or something else
//				//try to find graphic frame inside
//				if(graphicnode == null)
//				{
//					XmlNode child		= framenode.SelectSingleNode("//draw:frame", this._document.NamespaceManager);
//					if(child != null)
//						frame		= this.CreateFrame(child);
//					return frame;
//				}
//
//				string graphicpath			= this.GetAValueFromAnAttribute(graphicnode, "@xlink:href");
//
//				if(stylenode != null)
//					if(stylenode.ChildNodes.Count > 0)
//						if(stylenode.ChildNodes.Item(0).OuterXml.StartsWith("<style:graphic-properties"))
//							graphicproperties	= stylenode.ChildNodes.Item(0).CloneNode(true);
//
//				if(stylename.Length > 0 && stylenode != null && realgraphicname.Length > 0
//					&& graphicnode != null && graphicpath.Length > 0 && graphicproperties != null)
//				{
//					graphicpath				= graphicpath.Replace("Pictures", "");
//					graphicpath				= OpenDocumentTextImporter.dirpics+graphicpath.Replace("/", @"\");
//
//					frame					= new Frame(this._document, stylename, 
//												realgraphicname, graphicpath);
//					
//					frame.Style.Node		= stylenode;
//					frame.Graphic.Node		= graphicnode;
//					((FrameStyle)frame.Style).GraphicProperties.Node = graphicproperties;
//
//					XmlNode nodeSize		= framenode.SelectSingleNode("@svg:height", 
//						this._document.NamespaceManager);
//
//					if(nodeSize != null)
//						if(nodeSize.InnerText != null)
//							frame.GraphicHeight	= nodeSize.InnerText;
//
//					nodeSize		= framenode.SelectSingleNode("@svg:width", 
//						this._document.NamespaceManager);
//
//					if(nodeSize != null)
//						if(nodeSize.InnerText != null)
//							frame.GraphicWidth	= nodeSize.InnerText;
//				}
				#endregion
				
				//Create a new Frame
				Frame frame					= new Frame(this._document, null);
				frame.Node					= frameNode;
				IContentCollection iColl	= new IContentCollection();
				//Revieve the FrameStyle
				IStyle frameStyle			= this._document.Styles.GetStyleByName(frame.StyleName);

				if(frameStyle != null)
					frame.Style					= frameStyle;
				else
				{
					if(this.OnWarning != null)
					{
						AODLWarning warning			= new AODLWarning("Couldn't recieve a FrameStyle.");
						warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
						warning.Node				= frameNode;
						this.OnWarning(warning);
					}
				}

				//Create the frame content
				foreach(XmlNode nodeChild in frame.Node.ChildNodes)
				{
					IContent iContent				= this.CreateContent(nodeChild);
					if(iContent != null)
						iColl.Add(iContent);
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't create a IContent object for a frame.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= nodeChild;
							this.OnWarning(warning);
						}
					}
				}

				frame.Node.InnerXml					= "";

				foreach(IContent iContent in iColl)
					frame.Content.Add(iContent);

				return frame;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a Frame.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= frameNode;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the list.
		/// </summary>
		/// <param name="listNode">The list node.</param>
		/// <returns>The List object</returns>
		private List CreateList(XmlNode listNode)
		{
			try
			{
				#region Old code Todo: delete
//				string stylename				= null;
//				XmlNode	stylenode				= null;
//				ListStyles liststyles			= ListStyles.Bullet; //as default
//				string paragraphstylename		= null;
//
//				if(outerlist == null)
//				{
//					stylename			= this.GetStyleName(listNode.OuterXml);
//					stylenode			= this.GetAStyleNode("text:list-style", stylename);				
//					liststyles			= this.GetListStyle(listNode);					
//				}
//				List list					= null;
//
//				if(listNode.ChildNodes.Count > 0)
//				{
//					try
//					{
//						paragraphstylename	= this.GetAValueFromAnAttribute(listNode.ChildNodes.Item(0).ChildNodes.Item(0), "@style:style-name");
//					}
//					catch(Exception ex)
//					{
//						paragraphstylename	= "P1";
//					}
//				}
				#endregion
				//Create a new List
				List list					= new List(this._document, listNode);
				IContentCollection iColl	= new IContentCollection();
				//Revieve the ListStyle
				IStyle listStyle			= this._document.Styles.GetStyleByName(list.StyleName);

				if(listStyle != null)
					list.Style				= listStyle;

				foreach(XmlNode nodeChild in list.Node.ChildNodes)
				{
					IContent iContent		= this.CreateContent(nodeChild);

					if(iContent != null)
						iColl.Add(iContent);
				}

				list.Node.InnerXml			= "";

				foreach(IContent iContent in iColl)
					list.Content.Add(iContent);
				
				return list;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a List.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= listNode;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the list item.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private ListItem CreateListItem(XmlNode node)
		{
			try
			{

				ListItem listItem			= new ListItem(this._document);
				IContentCollection iColl	= new IContentCollection();
				listItem.Node				= node;

				foreach(XmlNode nodeChild in listItem.Node.ChildNodes)
				{
					IContent iContent		= this.CreateContent(nodeChild);
					if(iContent != null)
						iColl.Add(iContent);
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't create a IContent object for a ListItem.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= nodeChild;
							this.OnWarning(warning);
						}
					}
				}

				listItem.Node.InnerXml		= "";

				foreach(IContent iContent in iColl)
					listItem.Content.Add(iContent);

				return listItem;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a ListItem.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= node;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the table.
		/// </summary>
		/// <param name="tableNode">The tablenode.</param>
		/// <returns></returns>
		private Table CreateTable(XmlNode tableNode)
		{
			try
			{
				//Create a new table
				Table table					= new Table(this._document, tableNode);
				IContentCollection iColl	= new IContentCollection();
				//Recieve the table style
				IStyle tableStyle		= this._document.Styles.GetStyleByName(table.StyleName);

				if(tableStyle != null)
					table.Style				= tableStyle;
				else
				{
					if(this.OnWarning != null)
					{
						AODLWarning warning			= new AODLWarning("Couldn't recieve a TableStyle.");
						warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
						warning.Node				= tableNode;
						this.OnWarning(warning);
					}
				}

				//Create the table content
				foreach(XmlNode nodeChild in table.Node.ChildNodes)
				{
					IContent iContent				= this.CreateContent(nodeChild);
					
					if(iContent != null)
					{
						iColl.Add(iContent);
					}
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't create IContent from a table node. Content is unknown table content!");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= iContent.Node;
							this.OnWarning(warning);
						}
					}
				}

				table.Node.InnerText					= "";

				foreach(IContent iContent in iColl)
				{
					if(iContent is Column)
					{
						((Column)iContent).Table	= table;
						table.ColumnCollection.Add(iContent as Column);
					}
					else if(iContent is Row)		
					{
						((Row)iContent).Table		= table;
						table.RowCollection.Add(iContent as Row);
					}
					else if(iContent is RowHeader)
					{
						((RowHeader)iContent).Table	= table;
						table.RowHeader			= iContent as RowHeader;
					}
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't create IContent from a table node.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= tableNode;
							this.OnWarning(warning);
							table.Node.AppendChild(iContent.Node);
						}
					}
				}
				
				return table;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a Table.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= tableNode;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the table row.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private Row CreateTableRow(XmlNode node)
		{
			try
			{
				//Create a new Row
				Row row						= new Row(this._document, node);
				IContentCollection iColl	= new IContentCollection();
				//Recieve RowStyle
				IStyle rowStyle				= this._document.Styles.GetStyleByName(row.StyleName);

				if(rowStyle != null)
					row.Style				= rowStyle;
				//No need for a warning

				//Create the cells
				foreach(XmlNode nodeChild in row.Node.ChildNodes)
				{
					IContent iContent		= this.CreateContent(nodeChild);

					if(iContent != null)
					{
						iColl.Add(iContent);
					}
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't create IContent from a table row.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= nodeChild;
							this.OnWarning(warning);
						}
					}
				}

				row.Node.InnerXml			= "";

				foreach(IContent iContent in iColl)
				{
					if(iContent is Cell)
					{
						((Cell)iContent).Row		= row;
						row.CellCollection.Add(iContent as Cell);
					}
					else if(iContent is CellSpan)
					{
						((CellSpan)iContent).Row	= row;
						row.CellSpanCollection.Add(iContent as CellSpan);
					}
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't create IContent from a row node. Content is unknown table row content!");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= iContent.Node;
							this.OnWarning(warning);
						}
					}
				}

				return row;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a Table Row.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= node;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the table header row.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private RowHeader CreateTableHeaderRow(XmlNode node)
		{
			try
			{
				//Create a new Row
				RowHeader rowHeader			= new RowHeader(this._document, node);
				IContentCollection iColl	= new IContentCollection();
				//Recieve RowStyle
				IStyle rowStyle				= this._document.Styles.GetStyleByName(rowHeader.StyleName);

				if(rowStyle != null)
					rowHeader.Style				= rowStyle;
				//No need for a warning

				//Create the cells
				foreach(XmlNode nodeChild in rowHeader.Node.ChildNodes)
				{
					IContent iContent			= this.CreateContent(nodeChild);

					if(iContent != null)
					{
						iColl.Add(iContent);
					}
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't create IContent from a table row.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= nodeChild;
							this.OnWarning(warning);
						}
					}
				}

				rowHeader.Node.InnerXml			= "";

				foreach(IContent iContent in iColl)
				{
					if(iContent is Row)
					{
						rowHeader.RowCollection.Add(iContent as Row);
					}
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't create IContent from a row header node. Content is unknown table row header content!");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= iContent.Node;
							this.OnWarning(warning);
						}
					}
				}
				return rowHeader;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a Table Row.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= node;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the table column.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private Column CreateTableColumn(XmlNode node)
		{
			try
			{
				//Create a new Row
				Column column				= new Column(this._document, node);
				//Recieve RowStyle
				IStyle columnStyle			= this._document.Styles.GetStyleByName(column.StyleName);

				if(columnStyle != null)
					column.Style			= columnStyle;
				//No need for a warning

				return column;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a Table Column.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= node;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the table cell span.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private CellSpan CreateTableCellSpan(XmlNode node)
		{
			try
			{
				//Create a new CellSpan
				CellSpan cellSpan			= new CellSpan(this._document, node);
				
				//No need for a warnings or styles

				return cellSpan;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a Table CellSpan.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= node;
				exception.OriginalException	= ex;

				throw exception;
			}
		}

		/// <summary>
		/// Creates the table cell.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private Cell CreateTableCell(XmlNode node)
		{
			try
			{
				//Create a new Cel
				Cell cell					= new Cell(this._document, node);
				IContentCollection iColl	= new IContentCollection();
				//Recieve CellStyle
				IStyle cellStyle			= this._document.Styles.GetStyleByName(cell.StyleName);

				if(cellStyle != null)
					cell.Style				= cellStyle;
				//No need for a warning

				//Create the cells content
				foreach(XmlNode nodeChild in cell.Node.ChildNodes)
				{
					IContent iContent		= this.CreateContent(nodeChild);

					if(iContent != null)
					{
						iColl.Add(iContent);
					}
					else
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't create IContent from a table cell.");
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= nodeChild;
							this.OnWarning(warning);
						}
					}
				}

				cell.Node.InnerXml			= "";

				foreach(IContent iContent in iColl)
					cell.Content.Add(iContent);
				return cell;
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to create a Table Row.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.Node				= node;
				exception.OriginalException	= ex;

				throw exception;
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
					this._document.NamespaceManager);

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
		/// Logs the node.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="msg">The MSG.</param>
		private void LogNode(XmlNode node, string msg)
		{
			Console.WriteLine("\n#############################\n{0}", msg);
			XmlTextWriter writer	= new XmlTextWriter(Console.Out);
			writer.Formatting		= Formatting.Indented;
			node.WriteTo(writer);

			int i=0;
			if(node.InnerText.StartsWith("Open your IDE"))
				i=1;
		}
	}
}

//AODLTest.DocumentImportTest.SimpleLoadTest : System.IO.DirectoryNotFoundException : Could not find a part of the path "D:\OpenDocument\AODL\AODLTest\bin\Debug\GeneratedFiles\OpenOffice.net.odt.rel.odt".
/*
 * $Log: MainContentProcessor.cs,v $
 * Revision 1.4  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.3  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.5  2006/01/05 10:28:06  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 * Revision 1.4  2005/12/21 17:17:12  larsbm
 * - AODL new feature save gui settings
 * - Bugfixes, in MainContentProcessor
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