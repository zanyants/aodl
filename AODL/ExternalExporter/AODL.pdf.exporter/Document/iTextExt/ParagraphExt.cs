using System;
using iTextSharp.text;

namespace AODL.ExternalExporter.PDF.Document.iTextExt
{
	/// <summary>
	/// Summary for ParagraphExt.
	/// iText paragraph extension for making it ODF compliant.
	/// </summary>
	public class ParagraphExt : Paragraph
	{
		private bool _pageBreakBefore;
		/// <summary>
		/// Gets a value indicating whether [page break before].
		/// </summary>
		/// <value><c>true</c> if [page break before]; otherwise, <c>false</c>.</value>
		public bool PageBreakBefore
		{
			get { return this._pageBreakBefore; }
			set { this._pageBreakBefore = value; }
		}

		private bool _pageBreakAfter;
		/// <summary>
		/// Gets a value indicating whether [page break after].
		/// </summary>
		/// <value><c>true</c> if [page break after]; otherwise, <c>false</c>.</value>
		public bool PageBreakAfter
		{
			get { return this._pageBreakBefore; }
			set { this._pageBreakAfter = value; }
		}

		public ParagraphExt(string text, Font font) : base(text, font)
		{
		}
	}
}
