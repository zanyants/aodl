using System;
using AODL.Document.Content.Text;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.ExternalExporter.PDF.Document.StyleConverter;

namespace AODL.ExternalExporter.PDF.Document.ContentConverter
{
	/// <summary>
	/// Summary HeadingConverter.
	/// </summary>
	public class HeadingConverter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HeadingConverter"/> class.
		/// </summary>
		public HeadingConverter()
		{
		}

		/// <summary>
		/// Converts the specified heading.
		/// </summary>
		/// <param name="heading">The heading.</param>
		/// <returns>A PDF paragraph representing the ODF heading</returns>
		public static iTextSharp.text.Paragraph Convert(Header heading)
		{
			try
			{
				iTextSharp.text.Font font = DefaultDocumentStyles.Instance().DefaultTextFont;
				IStyle style = heading.Style != null ? heading.Style : heading.Document.CommonStyles.GetStyleByName(heading.StyleName);
				string alignementParagraph = string.Empty;
				
				if(style != null && style is ParagraphStyle)
				{
					if(((ParagraphStyle)style).ParentStyle != null)
					{
							IStyle parentStyle = heading.Document.CommonStyles.GetStyleByName(
								((ParagraphStyle)style).ParentStyle);
							if(parentStyle != null 
								&& parentStyle is ParagraphStyle
								&& ((ParagraphStyle)parentStyle).TextProperties != null
								&& ((ParagraphStyle)style).TextProperties != null)
							{
								// get parent style first
								font = TextPropertyConverter.GetFont(((ParagraphStyle)parentStyle).TextProperties);
								// now use the orignal style as multiplier
								font = TextPropertyConverter.FontMultiplier(((ParagraphStyle)style).TextProperties, font);
							}
							else
							{
								font = TextPropertyConverter.GetFont(((ParagraphStyle)style).TextProperties);
							}
					}
					else
					{
						font = TextPropertyConverter.GetFont(((ParagraphStyle)style).TextProperties);
					}
					ParagraphProperties paragraphProperties = ((ParagraphStyle)style).ParagraphProperties;
					if(paragraphProperties != null && !string.IsNullOrEmpty(paragraphProperties.Alignment))
						alignementParagraph = paragraphProperties.Alignment;
				}
				iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph("", font); // default ctor protected - why ??
				paragraph.SetAlignment(alignementParagraph);
				paragraph.AddRange(FormatedTextConverter.GetTextContents(heading.TextContent, font));
				return paragraph;
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}
