using System;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// TextContentSpecialCharacter parser
	/// </summary>
	internal class TextContentSpecialCharacter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TextContentSpecialCharacter"/> class.
		/// </summary>
		public TextContentSpecialCharacter()
		{
		}

		internal static string ReplaceSpecialCharacter(string textToParse)
		{
			textToParse		= textToParse.Replace("&", "&amp;");
			textToParse		= textToParse.Replace("<", "&lt;");
			textToParse		= textToParse.Replace(">", "&gt;");

			textToParse		= textToParse.Replace(@"\n", "<text:line-break xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");
			textToParse		= textToParse.Replace("\n", "<text:line-break xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");
			textToParse		= textToParse.Replace("\t", "<text:tab xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");
			textToParse		= textToParse.Replace(@"\t", "<text:tab xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");

			textToParse			= WhiteSpace.GetWhiteSpaceXml(textToParse);

			return textToParse;
		}
	}
}
