using System;
using AODL.Document.Content;
using AODL.Document.Content.Draw;
using AODL.Document.Content.Text;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;

namespace AODL.ExternalExporter.PDF.Document.ContentConverter
{
	/// <summary>
	/// Summary for DrawFrameConverter.
	/// </summary>
	public class DrawFrameConverter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DrawFrameConverter"/> class.
		/// </summary>
		public DrawFrameConverter()
		{
		}

		/// <summary>
		/// Converts the specified frame into an PDF paragraph.
		/// </summary>
		/// <param name="frame">The frame.</param>
		/// <returns>The paragraph representing the passed frame.</returns>
		public iTextSharp.text.Paragraph Convert(Frame frame)
		{
			try
			{
				iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph();
				foreach(iTextSharp.text.IElement pdfElement in MixedContentConverter.GetMixedPdfContent(frame.Content))
				{
					paragraph.Add(pdfElement);
				}
				return paragraph;
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
