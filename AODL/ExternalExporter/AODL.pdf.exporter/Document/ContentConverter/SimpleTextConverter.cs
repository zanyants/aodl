using System;

namespace AODL.ExternalExporter.PDF.Document.ContentConverter
{
	/// <summary>
	/// Summary for SimpleTextConverter.
	/// </summary>
	public class SimpleTextConverter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleTextConverter"/> class.
		/// </summary>
		public SimpleTextConverter()
		{
		}

		/// <summary>
		/// Converts the specified simple text.
		/// </summary>
		/// <param name="simpleText">The simple text.</param>
		/// <returns></returns>
		public static iTextSharp.text.Chunk Convert(AODL.Document.Content.Text.SimpleText simpleText, iTextSharp.text.Font font)
		{
			try
			{
				return new iTextSharp.text.Chunk(simpleText.Text, font);
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Converts the tabs.
		/// </summary>
		/// <param name="tabStop">The tab stop.</param>
		/// <returns>Chunkck containing whitespace for a tab.</returns>
		public static iTextSharp.text.Phrase ConvertTabs(AODL.Document.Content.Text.TextControl.TabStop tabStop, iTextSharp.text.Font font)
		{
			try
			{
				// Only a trick since PDF doesn't support tab stops as know from other
				// formats, so we only use whitespace character for the beginning
				// TODO: do it better
				iTextSharp.text.Phrase phrase = new iTextSharp.text.Phrase("     ", font);
				return phrase;
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Converts the white spaces.
		/// </summary>
		/// <param name="whiteSpace">The white space.</param>
		/// <returns>Chunck containing the whitspaces.</returns>
		public static iTextSharp.text.Phrase ConvertWhiteSpaces(AODL.Document.Content.Text.TextControl.WhiteSpace whiteSpace, iTextSharp.text.Font font)
		{
			try
			{
				string simulatedWhitespaces = "";
				if(whiteSpace.Count != null)
				{
					for(int i=0; i < Int32.Parse(whiteSpace.Count); i++)
					{
						simulatedWhitespaces += " ";
					}
				}
				return new iTextSharp.text.Phrase(simulatedWhitespaces, font);
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
