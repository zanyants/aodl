using System;
using AODL.ExternalExporter.PDF.Document.StyleConverter;
using AODL.Document.Content.Text;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;

namespace AODL.ExternalExporter.PDF.Document.ContentConverter
{
	/// <summary>
	/// Summary for ParagraphConverter.
	/// </summary>
	public class ParagraphConverter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ParagraphConverter"/> class.
		/// </summary>
		public ParagraphConverter()
		{
		}

		/// <summary>
		/// Converts the specified paragraph.
		/// </summary>
		/// <param name="paragraph">The paragraph.</param>
		/// <returns>The PDF paragraph.</returns>
		public static iTextSharp.text.Paragraph Convert(AODL.Document.Content.Text.Paragraph paragraph)
		{
			try
			{
				iTextSharp.text.Font font;
				if((ParagraphStyle)paragraph.Style != null 
					&& ((ParagraphStyle)paragraph.Style).TextProperties != null
					&& ((ParagraphStyle)paragraph.Style).TextProperties.FontName != null)
					font = TextPropertyConverter.GetFont(
						((ParagraphStyle)paragraph.Style).TextProperties);
				else
					font = DefaultDocumentStyles.Instance().DefaultTextFont;

				iTextSharp.text.Paragraph paragraphPDF = new iTextSharp.text.Paragraph("", font);				
				foreach(object obj in paragraph.MixedContent)
				{
					if(obj is AODL.Document.Content.Text.FormatedText)
					{
						paragraphPDF.Add(FormatedTextConverter.Convert(
							obj as AODL.Document.Content.Text.FormatedText));
					}
					if(obj is AODL.Document.Content.Text.SimpleText)
					{
						paragraphPDF.Add(SimpleTextConverter.Convert(
							obj as AODL.Document.Content.Text.SimpleText, font));
					}
					else if(obj is AODL.Document.Content.Text.TextControl.TabStop)
					{
						paragraphPDF.Add(SimpleTextConverter.ConvertTabs(
							obj as AODL.Document.Content.Text.TextControl.TabStop, font));
					}
					else if(obj is AODL.Document.Content.Text.TextControl.WhiteSpace)
					{
						paragraphPDF.Add(SimpleTextConverter.ConvertWhiteSpaces(
							obj as AODL.Document.Content.Text.TextControl.WhiteSpace, font));
					}
				}
				paragraphPDF = ParagraphConverter.ConvertParagraphStyles(paragraph, paragraphPDF);
				// add new line
				paragraphPDF.Add(iTextSharp.text.Chunk.NEWLINE);
				return paragraphPDF;
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Converts the paragraph styles.
		/// </summary>
		/// <param name="paragraph">The paragraph.</param>
		/// <param name="paragraphPDF">The paragraph PDF.</param>
		/// <returns>The iText paragraph with converted styles</returns>
		public static iTextSharp.text.Paragraph ConvertParagraphStyles(
			AODL.Document.Content.Text.Paragraph paragraph, 
			iTextSharp.text.Paragraph paragraphPDF)
		{
			try
			{
				if(paragraph.Style != null)
				{
					if(paragraph.Style is ParagraphStyle 
						&& ((ParagraphStyle)paragraph.Style).ParagraphProperties != null)
					{
						paragraphPDF.Alignment = (ParagraphPropertyConverter.GetAlignMent(
							((ParagraphStyle)paragraph.Style).ParagraphProperties.Alignment));
						
					}
				}
				return paragraphPDF;
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
