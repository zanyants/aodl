/*
 * $Id: HTMLStyleBuilder.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
	/// HTMLStyleBuilder offer public  methods
	/// to build HTML style strings from AODL
	/// OpenDocument IStyle objects and from
	/// global style nodes
	/// </summary>
	public class HTMLStyleBuilder
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

		private string[] _headings		= new string[] {
			//Heading
			"style=\"font-family: Arial; font-weight: bold; font-size: 14pt; margin-top: 0.25cm; margin-bottom: 0.25cm; \"",
			//Heading 1
			"style=\"font-family: Arial; font-weight: bold; font-size: 16pt; margin-top: 0.25cm; margin-bottom: 0.25cm; \"",
			//Heading 2
			"style=\"font-family: Arial; font-weight: bold; font-style:italic; font-size: 14pt; margin-top: 0.25cm; margin-bottom: 0.25cm; \"",
			};
		/// <summary>
		/// Gets the header html styles.
		/// </summary>
		/// <value>The headings.</value>
		public string[] HeaderHtmlStyles
		{
			get { return this._headings; }
		}

		/// <summary>
		/// Global css style for text standards
		/// </summary>
		private static string CSSStyle = 
				"<style type=\"text/css\">\n<!--\n"
				+".p {font-family: Times New Roman, Arial; font-weight: bold; font-size: 12pt; }"
				+".td {font-family: Times New Roman, Arial; font-weight: bold; font-size: 12pt; }"
				+".li {font-family: Times New Roman, Arial; font-weight: bold; font-size: 12pt; }"
				+"\n-->\n</style>\n";

		/// <summary>
		/// Initializes a new instance of the <see cref="HTMLStyleBuilder"/> class.
		/// </summary>
		public HTMLStyleBuilder()
		{
		}

		/// <summary>
		/// Gets the paragraph style as HTML.
		/// </summary>
		/// <param name="paragraphStyle">The paragraph style.</param>
		/// <returns></returns>
		public string GetParagraphStyleAsHtml(ParagraphStyle paragraphStyle)
		{
			string style		= "style=\"";

			try
			{
				if(paragraphStyle != null)
				{
					if(paragraphStyle.ParagraphProperties != null)
					{
						if(paragraphStyle.ParagraphProperties.Alignment != null)
							style	+= "text-align: "+paragraphStyle.ParagraphProperties.Alignment+"; ";
						if(paragraphStyle.ParagraphProperties.MarginLeft != null)
							style	+= "text-indent: "+paragraphStyle.ParagraphProperties.MarginLeft+"; ";
						if(paragraphStyle.ParagraphProperties.LineSpacing != null)
							style	+= "line-height: "+paragraphStyle.ParagraphProperties.LineSpacing+"; ";
						if(paragraphStyle.ParagraphProperties.Border != null 
							&& paragraphStyle.ParagraphProperties.Padding == null)
							style	+= "border-width:1px; border-style:solid; padding: 0.5cm; ";
						if(paragraphStyle.ParagraphProperties.Border != null 
							&& paragraphStyle.ParagraphProperties.Padding != null)
							style	+= "border-width:1px; border-style:solid; padding:"+paragraphStyle.ParagraphProperties.Padding+"; ";
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML style string from a ParagraphStyle.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				if(paragraphStyle != null)
					if(paragraphStyle.Node != null)
						exception.Node		= paragraphStyle.Node;

				throw exception;
			}

			if(!style.EndsWith("; "))
				style	= "";
			else
				style	+= "\"";

			return style;
		}

		/// <summary>
		/// Gets the cell style as HTML.
		/// </summary>
		/// <param name="cellStyle">The cell style.</param>
		/// <returns></returns>
		public string GetCellStyleAsHtml(CellStyle cellStyle)
		{
			string style		= "";

			try
			{
				if(cellStyle != null)
				{
					if(cellStyle.CellProperties != null)
					{
						if(cellStyle.CellProperties.BackgroundColor != null)
							style			+= "bgcolor=\""+cellStyle.CellProperties.BackgroundColor+"\" ";
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML style string from a CellStyle.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				if(cellStyle != null)
					if(cellStyle.Node != null)
						exception.Node		= cellStyle.Node;

				throw exception;
			}

			return style;
		}

		/// <summary>
		/// Gets the column style as HTML.
		/// </summary>
		/// <param name="columnStyle">The column style.</param>
		/// <returns></returns>
		public string GetColumnStyleAsHtml(ColumnStyle columnStyle)
		{
			string style		= "";

			try
			{
				if(columnStyle != null)
				{
					if(columnStyle.ColumnProperties != null)
					{
						if(columnStyle.ColumnProperties.Width != null)
						{
							string width	= columnStyle.ColumnProperties.Width;
							if(width.EndsWith("cm"))
								width		= width.Replace("cm", "");
							else if(width.EndsWith("in"))
								width		= width.Replace("in", "");

							try
							{
								double wd	= Convert.ToDouble(width, System.Globalization.NumberFormatInfo.InvariantInfo);
								string wdPx	= "";
								if(columnStyle.ColumnProperties.Width.EndsWith("cm"))
									wdPx	= SizeConverter.CmToPixelAsString(wd);
								else if(columnStyle.ColumnProperties.Width.EndsWith("in"))
									wdPx	= SizeConverter.InchToPixelAsString(wd);

								if(wdPx.Length > 0)
									style	= "width=\""+wdPx+"\" ";
							}
							catch(Exception ex)
							{
								if(this.OnWarning != null)
								{
									AODLWarning warning			= new AODLWarning("Exception while trying to build a column width.: "
										+ columnStyle.ColumnProperties.Width);
									warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
									warning.OriginalException	= ex;
									OnWarning(warning);
								}
							}							
						}
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML style string from a CellStyle.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				if(columnStyle != null)
					if(columnStyle.Node != null)
						exception.Node		= columnStyle.Node;

				throw exception;
			}

			return style;
		}

		/// <summary>
		/// Gets the table style as HTML.
		/// </summary>
		/// <param name="tableStyle">The table style.</param>
		/// <returns></returns>
		public string GetTableStyleAsHtml(TableStyle tableStyle)
		{
			string style		= "";

			try
			{
				if(tableStyle != null)
				{
					if(tableStyle.TableProperties != null)
					{
						if(tableStyle.TableProperties.Width != null)
						{
							string width	= tableStyle.TableProperties.Width;
							if(width.EndsWith("cm"))
								width		= width.Replace("cm", "");
							else if(width.EndsWith("in"))
								width		= width.Replace("in", "");

							try
							{
								double wd	= Convert.ToDouble(width, System.Globalization.NumberFormatInfo.InvariantInfo);
								string wdPx	= "";
								if(tableStyle.TableProperties.Width.EndsWith("cm"))
									wdPx	= SizeConverter.CmToPixelAsString(wd);
								else if(tableStyle.TableProperties.Width.EndsWith("in"))
									wdPx	= SizeConverter.InchToPixelAsString(wd);

								if(wdPx.Length > 0)
									style	= "width=\""+wdPx+"\" ";
							}
							catch(Exception ex)
							{
								if(this.OnWarning != null)
								{
									AODLWarning warning			= new AODLWarning("Exception while trying to build a table width width.: "
										+ tableStyle.TableProperties.Width);
									warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
									warning.OriginalException	= ex;
									OnWarning(warning);
								}
							}
						}
						if(tableStyle.TableProperties.Align != null)
							if(tableStyle.TableProperties.Align != "margin")
								if(tableStyle.TableProperties.Align == "center")
									style	+= "align=\"center\" ";
								else if(tableStyle.TableProperties.Align == "right")
									style	+= "align=\"right\" ";
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML style string from a TableStyle.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				if(tableStyle != null)
					if(tableStyle.Node != null)
						exception.Node		= tableStyle.Node;

				throw exception;
			}

			return style;
		}

		/// <summary>
		/// Gets the frame style as HTML.
		/// </summary>
		/// <param name="frame">The frame.</param>
		/// <returns></returns>
		public string GetFrameStyleAsHtml(Frame frame)
		{
			string style		= "";

			try
			{
				if(frame != null)
				{
					string width		= frame.SvgWidth;
					if(width != null)
						if(width.EndsWith("cm"))
							width		= width.Replace("cm", "");
						else if(width.EndsWith("in"))
							width		= width.Replace("in", "");

					string height		= frame.SvgHeight;
					if(height != null)
						if(height.EndsWith("cm"))
							height		= height.Replace("cm", "");
						else if(height.EndsWith("in"))
							height		= height.Replace("in", "");

					try
					{
						if(width != null)
						{
							double wd	= Convert.ToDouble(width, System.Globalization.NumberFormatInfo.InvariantInfo);
							string wdPx	= "";
							if(frame.SvgWidth.EndsWith("cm"))
								wdPx	= SizeConverter.CmToPixelAsString(wd);
							else if(frame.SvgWidth.EndsWith("in"))
								wdPx	= SizeConverter.InchToPixelAsString(wd);

							if(wdPx.Length > 0)
								style	= "width=\""+wdPx+"\" ";
						}

						if(height != null)
						{
							double wd	= Convert.ToDouble(height, System.Globalization.NumberFormatInfo.InvariantInfo);
							string wdPx	= "";
							if(frame.SvgHeight.EndsWith("cm"))
								wdPx	= SizeConverter.CmToPixelAsString(wd);
							else if(frame.SvgHeight.EndsWith("in"))
								wdPx	= SizeConverter.InchToPixelAsString(wd);

							if(wdPx.Length > 0)
								style	= "height=\""+wdPx+"\" ";
						}
					}
					catch(Exception ex)
					{
						if(this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Exception while trying to build a graphic width & height.: "
								+ frame.SvgWidth + "/" + frame.SvgHeight);
							warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.OriginalException	= ex;
							OnWarning(warning);
						}
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML style string from a FrameStyle.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				if(frame != null)
					if(frame.Node != null)
						exception.Node		= frame.Node;

				throw exception;
			}

			return style;
		}

		/// <summary>
		/// Gets the text style as HTML.
		/// </summary>
		/// <param name="textStyle">The text style.</param>
		/// <returns></returns>
		public string GetTextStyleAsHtml(TextProperties textStyle)
		{
			string style		= "style=\"";

			try
			{
				if(textStyle != null)
				{
					if(textStyle.Italic != null)
						if(textStyle.Italic != "normal")
							style	+= "font-style: italic; ";
					if(textStyle.Bold != null)
						style	+= "font-weight: "+textStyle.Bold+"; ";
					if(textStyle.Underline != null)
						style	+= "text-decoration: underline; ";
					if(textStyle.TextLineThrough != null)
						style	+= "text-decoration: line-through; ";
					if(textStyle.FontName != null)
						style	+= "font-family: "+FontFamilies.HtmlFont(textStyle.FontName)+"; ";
					if(textStyle.FontSize != null)
						style	+= "font-size: "+FontFamilies.PtToPx(textStyle.FontSize)+"; ";
					if(textStyle.FontColor != null)
						style	+= "color: "+textStyle.FontColor+"; ";
					if(textStyle.BackgroundColor != null)
						style	+= "background-color: "+textStyle.BackgroundColor+"; ";
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML style string from a TextStyle.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				if(textStyle != null)
					if(textStyle.Node != null)
						exception.Node		= textStyle.Node;

				throw exception;
			}

			if(!style.EndsWith("; "))
				style	= "";
			else
				style	+= "\"";

			return style;
		}

		/// <summary>
		/// Gets the heading style as HTML.
		/// </summary>
		/// <param name="header">The header.</param>
		/// <returns></returns>
		public string GetHeadingStyleAsHtml(Header header)
		{
			try
			{
				if(header != null)
				{
					if(header.StyleName != null)
					{
						if(header.StyleName.Equals(Headings.Heading_20_1.ToString()))
							return this.HeaderHtmlStyles[1];
						else if(header.StyleName.Equals(Headings.Heading_20_2.ToString()))
							return this.HeaderHtmlStyles[2];
						else
							return this.HeaderHtmlStyles[0];
					}
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML style string from a TextStyle.");
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				if(header != null)
					if(header.Node != null)
						exception.Node		= header.Node;

				throw exception;
			}

			return this.HeaderHtmlStyles[0];
		}

		/// <summary>
		/// Gets the A global styl as HTML.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="styleName">Name of the style.</param>
		/// <returns></returns>
		public string GetAGlobalStylAsHtml(IDocument document, string styleName)
		{ 
			string style		= "style=\"";

			try
			{
				if(document != null)
				{
				}
			}
			catch(Exception ex)
			{
				AODLException exception		= new AODLException("Exception while trying to build a HTML style from a global style:"+styleName);
				exception.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
				exception.OriginalException	= ex;

				throw exception;
			}

			if(!style.EndsWith("; "))
				style	= "";
			else
				style	+= "\"";

			return style;
		}
	}
}

/*
 * $Log: HTMLStyleBuilder.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */