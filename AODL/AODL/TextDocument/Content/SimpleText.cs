/*
 * $Id: SimpleText.cs,v 1.4 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a simple non formated Displaytext.
	/// </summary>
	public class SimpleText : IText, IHtml
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleText"/> class.
		/// </summary>
		public SimpleText()
		{
		}

		/// <summary>
		/// Overloaded constructor.
		/// </summary>
		/// <param name="content">The IContent object the SimpleText object belongs to.</param>
		/// <param name="text">The Displaytext.</param>
		public SimpleText(IContent content,string text)
		{
			this.Text		= text;
			this.Content	= content;
		}

		/// <summary>
		/// Transform control character like \n, \t into
		/// their xml pendants.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>The transformed text</returns>
		private string ControlCharTransformer(string text)
		{
//			text		= text.Replace("&", "&amp;");
//			text		= text.Replace("<", "&lt;");
//			text		= text.Replace(">", "&gt;");
//
//			text		= text.Replace(@"\n", "<text:line-break xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");
//			text		= text.Replace(@"\t", "<text:tab xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");
//
//			text		= WhiteSpace.GetWhiteSpaceXml(text);
			
			return TextContentSpecialCharacter.ReplaceSpecialCharacter(text);
		}

		#region IText Member

		/// <summary>
		/// This property isn't needed it's only simpletext.
		/// </summary>
		/// <remarks>
		/// Not implemented. Throws a NotSupportedException.
		/// </remarks>
		public System.Xml.XmlNode Node
		{
			get
			{
				// TODO:  Getter-Implementierung für SimpleText.Node hinzufügen
				return null;
			}
			set
			{
				throw new NotSupportedException("SimpleText doesn't support the Property Node!");
			}
		}

		private string _text;
		/// <summary>
		/// The Displaytext.
		/// </summary>
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				//this._text = value.Replace(@"\n", "<text:line-break xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");
				this._text	= this.ControlCharTransformer(value);
			}
		}
		
		/// <summary>
		/// Only the displaytext.
		/// </summary>
		public string Xml
		{
			get
			{
				return this._text;
			}			
		}


		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <remarks>Set throws NotSupportedException.</remarks>
		public IStyle Style
		{
			get
			{
				// TODO:  Getter-Implementierung für SimpleText.Style hinzufügen
				return null;
			}
			set
			{
				throw new NotSupportedException("SimpleText doesn't support the Property IStyle!");
			}
		}

		private IContent _content;
		/// <summary>
		/// The IContent object the SimpleText object belongs to.
		/// </summary>
		public IContent Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		#endregion

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string text = "";
											 
			text		= this.Text.Replace("<text:line-break xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />", @"<br>");
			text		= this.Text.Replace("<text:tab xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />", "&nbsp;&nbsp;&nbsp;");
			text		= this.Text.Replace("<text:line-break />", @"<br>");
			text		= this.Text.Replace("<text:tab />", "&nbsp;&nbsp;&nbsp;");
			text		= this.Text.Replace("<text:line-break/>", @"<br>");
			text		= this.Text.Replace("<text:tab/>", "&nbsp;&nbsp;&nbsp;");

			return WhiteSpace.GetWhiteSpaceHtml(text);;
		}

		#endregion
	}
}

/*
 * $Log: SimpleText.cs,v $
 * Revision 1.4  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.3  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */