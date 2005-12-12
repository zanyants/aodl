/*
 * $Id: WhiteSpace.cs,v 1.1 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// WhiteSpace convesation
	/// </summary>
	public class WhiteSpace
	{
		/// <summary>
		/// Value -> \ws3 which means 3 whitespace
		/// </summary>
		internal string Value;
		/// <summary>
		/// Replace with the real Xml textnode &gt;text:s text:c="2" /&lt;
		/// </summary>
		internal string Replacement;

		/// <summary>
		/// Initializes a new instance of the <see cref="WhiteSpace"/> class.
		/// </summary>
		public WhiteSpace()
		{
		}

		/// <summary>
		/// Convert all whitespace groups "    "
		/// into OpenDocument Xml textnodes
		/// </summary>
		/// <param name="stringToConvert">The string to convert.</param>
		/// <returns>The parsed string</returns>
		public static string GetWhiteSpaceXml(string stringToConvert)
		{
			try
			{
				ArrayList matchList		= new ArrayList();
				string pat = @"\s{2,}";
				Regex r = new Regex(pat, RegexOptions.IgnoreCase);
				Match m = r.Match(stringToConvert);

				while (m.Success) 
				{
					WhiteSpace w		= new WhiteSpace();
					for(int i=0; i<m.Length; i++)
						w.Value			+= " ";
					w.Replacement		= GetXmlWhiteSpace(m.Length);
					matchList.Add(w);
					m = m.NextMatch();
				}

				foreach(WhiteSpace w in matchList)
					stringToConvert		= stringToConvert.Replace(w.Value, w.Replacement);
				
			}
			catch(Exception ex)
			{
				//unhandled, only whitespaces arent displayed correct
			}
			return stringToConvert;
		}

		/// <summary>
		/// Convert all AODL whitespace control character \ws3
		/// into their OpenDocument Xml textnodes
		/// </summary>
		/// <param name="text">The string to convert.</param>
		/// <returns>The parsed string</returns>
		public static string GetWhiteSpaceHtml(string text)
		{
			try
			{
				ArrayList matchList		= new ArrayList();
				string pat = @"<text:s text:c="+'"'.ToString()+@"\d+"+'"'.ToString()+" xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />";
				Regex r = new Regex(pat, RegexOptions.IgnoreCase);
				Match m = r.Match(text);

				while (m.Success) 
				{
					Regex r1			= new Regex(@"\d", RegexOptions.IgnoreCase);
					Match m1			= r1.Match(m.Value);
					string html			= "";

					while(m1.Success)
					{
						int cnt			= Convert.ToInt32(m1.Value);
						for(int i=0; i<cnt; i++)
							html			+= "&nbsp;";
						Console.WriteLine(html);
						break;
					}
					if(html.Length > 0)
					{
						WhiteSpace w		= new WhiteSpace();
						w.Value				= html;
						w.Replacement		= m.Value;
						matchList.Add(w);
					}
					m = m.NextMatch();
				}
				
				foreach(WhiteSpace ws in matchList)
					text		= text.Replace(ws.Replacement, ws.Value);		
			}
			catch(Exception ex)
			{
				//unhandled, only whitespaces arent displayed correct
			}
			return text;
		}

		/// <summary>
		/// Gets the HTML white space.
		/// </summary>
		/// <param name="length">The length.</param>
		/// <returns></returns>
		private static string GetHtmlWhiteSpace(int length)
		{
			string html					= "";
			for(int i=0; i<length; i++)
				html		+= "&nbsp;";
			return html;
		}

		/// <summary>
		/// Gets the XML white space.
		/// </summary>
		/// <param name="length">The length.</param>
		/// <returns></returns>
		private static string GetXmlWhiteSpace(int length)
		{
			return "<text:s text:c=\""+length+"\" xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />";
		}
	}
}

/*
 * $Log: WhiteSpace.cs,v $
 * Revision 1.1  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 */