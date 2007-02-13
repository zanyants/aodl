using System;
using System.Collections;
using AODL.Document.Content;
using AODL.Document.Content.Text;
using AODL.Document.Content.Tables;
using AODL.Document.Content.Draw;

namespace AODL.ExternalExporter.PDF.Document.ContentConverter
{
	/// <summary>
	/// Summary for MixedContentConverter.
	/// </summary>
	public class MixedContentConverter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MixedContentConverter"/> class.
		/// </summary>
		public MixedContentConverter()
		{
		}

		/// <summary>
		/// Convert a AODL IContentCollection into an ArrayList of IElement iText objects.
		/// </summary>
		/// <param name="iContentCollection">The i content collection.</param>
		/// <returns>An ArrayList of iText IElement objects.</returns>
		public static ArrayList GetMixedPdfContent(IContentCollection iContentCollection)
		{
			try
			{
				ArrayList mixedPdfContent = new ArrayList();
				foreach(IContent content in iContentCollection)
				{
					if(content is AODL.Document.Content.Text.Paragraph)
					{
						if(((AODL.Document.Content.Text.Paragraph)content).MixedContent != null
							&& ((AODL.Document.Content.Text.Paragraph)content).MixedContent.Count > 0)
								mixedPdfContent.Add(ParagraphConverter.Convert(
									content as AODL.Document.Content.Text.Paragraph));
						else 
							mixedPdfContent.Add(iTextSharp.text.Chunk.NEWLINE);
					}
					else if(content is AODL.Document.Content.Text.Header)
					{
						mixedPdfContent.Add(HeadingConverter.Convert(
							content as AODL.Document.Content.Text.Header));
					}
					else if(content is AODL.Document.Content.Tables.Table)
					{
						TableConverter tc = new TableConverter();
						mixedPdfContent.Add(tc.Convert(
							content as AODL.Document.Content.Tables.Table));
					}
					else if(content is AODL.Document.Content.Draw.Frame)
					{
						DrawFrameConverter dfc = new DrawFrameConverter();
						mixedPdfContent.Add(dfc.Convert(
							content as AODL.Document.Content.Draw.Frame));
					}
					else if(content is AODL.Document.Content.Draw.Graphic)
					{
						 ImageConverter ic = new ImageConverter();
						mixedPdfContent.Add(ic.Convert(
							content as AODL.Document.Content.Draw.Graphic));
					}
				}
				return mixedPdfContent;
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}
