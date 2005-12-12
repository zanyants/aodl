/*
 * $Id: OpenDocumentHtmlExporter.cs,v 1.1 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.IO;
using System.Reflection;
using System.Collections;
using AODL.TextDocument;
using AODL.TextDocument.Content;

namespace AODL.Export
{
	/// <summary>
	/// Export the OpenDocument content as Html
	/// </summary>
	public class OpenDocumentHtmlExporter : IExporter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OpenDocumentHtmlExporter"/> class.
		/// </summary>
		public OpenDocumentHtmlExporter()
		{
			this._exporterrors		= new ArrayList();
		}

		#region IExporter Member

		private ArrayList _exporterrors;
		/// <summary>
		/// Gets the export erros.
		/// </summary>
		/// <value>The export erros.</value>
		public System.Collections.ArrayList ExportErros
		{
			get
			{
				return this._exporterrors;
			}
		}

		/// <summary>
		/// Exports the specified document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="filename">The filename.</param>
		public void Export(AODL.TextDocument.TextDocument document, string filename)
		{
			try
			{
				string targDir		= Environment.CurrentDirectory;
				int index			= filename.LastIndexOf(@"\");
				if(index != -1)
					targDir			= filename.Substring(0, index);
				string htmlsite		= this.AppendHtml(document.Content, this.GetTemplate());
				this.WriteHtmlFile(filename, htmlsite);
				string pictures		= "\\Pictures";
				string imgfolder	= targDir+"\\temphtmlimg";
				if(!Directory.Exists(imgfolder+pictures))
					Directory.CreateDirectory(imgfolder+pictures);
				OpenDocumentTextExporter.SaveGraphic(document, imgfolder);
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		#endregion

		/// <summary>
		/// Gets the template.
		/// </summary>
		/// <returns>The template as sring</returns>
		private string GetTemplate()
		{
			try
			{
				Assembly ass		= Assembly.GetExecutingAssembly();
				Stream str			= ass.GetManifestResourceStream("AODL.Resources.OD.htmltemplate.html");

				string text			= null;
				using (StreamReader sr = new StreamReader(str)) 
				{
					String line		= null;
					while ((line = sr.ReadLine()) != null) 
					{
						text		+= line+"\n";
					}
					sr.Close();
				}
				str.Close();

				return text;
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Appends the HTML.
		/// </summary>
		/// <param name="contentlist">The contentlist.</param>
		/// <param name="template">The template.</param>
		/// <returns>The filled template string</returns>
		private string AppendHtml(IContentCollection contentlist, string template)
		{
			try
			{
				foreach(IContent content in contentlist)
					if(content is IHtml)
						template	+= this.ReplaceControlNodes(((IHtml)content).GetHtml());

				template		+= "</body>\n</html>";
				
				return template;
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Writes the HTML file.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="html">The HTML.</param>
		private void WriteHtmlFile(string filename, string html)
		{
			try
			{
				FileStream fstream		= File.Create(filename);
				StreamWriter swriter	= new StreamWriter(fstream, System.Text.Encoding.UTF8);
				swriter.WriteLine(html);
				swriter.Close();
				fstream.Close();
			}
			catch(Exception ex)
			{
				throw;
			}
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
				text		= text.Replace("<text:line-break xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />", @"<br>");
				text		= text.Replace("<text:tab xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />", "&nbsp;&nbsp;&nbsp;");
				text		= text.Replace("<text:line-break />", @"<br>");
				text		= text.Replace("<text:tab />", "&nbsp;&nbsp;&nbsp;");
				text		= text.Replace("<text:line-break/>", @"<br>");
				text		= text.Replace("<text:tab/>", "&nbsp;&nbsp;&nbsp;");
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
 * $Log: OpenDocumentHtmlExporter.cs,v $
 * Revision 1.1  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 */