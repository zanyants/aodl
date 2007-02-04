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
						mixedPdfContent.Add(ParagraphConverter.Convert(
							content as AODL.Document.Content.Text.Paragraph));
					}
					else if(content is AODL.Document.Content.Text.Header)
					{
						mixedPdfContent.Add(HeadingConverter.Convert(
							content as AODL.Document.Content.Text.Header));
					}
					else if(content is AODL.Document.Content.Tables.Table)
					{
						mixedPdfContent.Add(TableConverter.Convert(
							content as AODL.Document.Content.Tables.Table));
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
