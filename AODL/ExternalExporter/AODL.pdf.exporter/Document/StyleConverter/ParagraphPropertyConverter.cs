using System;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using iTextSharp.text;

namespace AODL.ExternalExporter.PDF.Document.StyleConverter
{
	/// <summary>
	/// Zusammenfassung für ParagraphPropertyConverter.
	/// </summary>
	public class ParagraphPropertyConverter
	{
		public ParagraphPropertyConverter()
		{
		}

		/// <summary>
		/// Gets the align ment.
		/// </summary>
		/// <param name="paragraphProperties">The ODF align ment.</param>
		/// <returns>The align ment</returns>
		public static int GetAlignMent(string odfAlignMent)
		{
			try
			{
				switch(odfAlignMent)
				{
					case "right":
						return Element.ALIGN_RIGHT;
					case "justify":
						return Element.ALIGN_JUSTIFIED;
					case "start":
						return Element.ALIGN_LEFT;
					case "end":
						return Element.ALIGN_RIGHT;
					default:
						return Element.ALIGN_LEFT;
				}
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
