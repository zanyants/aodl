/*
 * $Id: WhiteSpace.cs,v 1.2 2006/02/05 20:02:25 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, Lars Behrmann, lb@OpenDocument4all.com
 * 
 * Last changes:
 * 
 */

using System;
using System.Xml;
using System.Collections;
using System.Text.RegularExpressions;
using AODL.Document.Content.Text;
using AODL.Document.Styles;
using AODL.Document;

namespace AODL.Document.Content.Text.TextControl
{

	/// <summary>
	/// WhiteSpace represent a white space element.
	/// </summary>
	public class WhiteSpace : IText
	{
		/// <summary>
		/// Gets or sets the count.
		/// </summary>
		/// <value>The count.</value>
		public string Count
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@text:c",
					this.Document.NamespaceManager);
				if(xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@text:c",
					this.Document.NamespaceManager);
				if(xn == null)
					this.CreateAttribute("c", value, "text");
				this._node.SelectSingleNode("@text:c",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WhiteSpace"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public WhiteSpace(IDocument document, XmlNode node)
		{
			this.Document	= document;
			this.Node		= node;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WhiteSpace"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="whiteSpacesCount">The document.</param>
		public WhiteSpace(IDocument document, int whiteSpacesCount)
		{
			this.Document	= document;
			this.NewXmlNode();
			this.Count		= whiteSpacesCount.ToString();
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		private void NewXmlNode()
		{			
			this.Node		= this.Document.CreateNode("s", "text");
		}

		#region IText Member

		private XmlNode _node;
		/// <summary>
		/// The node that represent the text content.
		/// </summary>
		/// <value></value>
		public XmlNode Node
		{
			get
			{
				return this._node;
			}
			set
			{
				this._node = value;
			}
		}

		/// <summary>
		/// A tab stop doesn't have a text.
		/// </summary>
		/// <value></value>
		public string Text
		{
			get
			{
				return null;
			}
			set
			{				
			}
		}

		private IDocument _document;
		/// <summary>
		/// The document to which this text content belongs to.
		/// </summary>
		/// <value></value>
		public IDocument Document
		{
			get
			{
				return this._document;
			}
			set
			{
				this._document = value;
			}
		}

		/// <summary>
		/// Is null no style is available.
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get { return null; }
			set { }
		}

		/// <summary>
		/// No style name available
		/// </summary>
		/// <value></value>
		public string StyleName
		{
			get 
			{ 
				return null;
			}
			set
			{
			}
		}

		#endregion
	}


	/// <summary>
	/// WhiteSpace convesation
	/// </summary>
	public class WhiteSpaceHelper
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
					WhiteSpaceHelper w		= new WhiteSpaceHelper();
					for(int i=0; i<m.Length; i++)
						w.Value			+= " ";
					w.Replacement		= "<ws id=\""+m.Length.ToString()+"\"/>";//GetXmlWhiteSpace(m.Length);
					matchList.Add(w);
					m = m.NextMatch();
				}

				foreach(WhiteSpaceHelper w in matchList)
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
						//Console.WriteLine(html);
						break;
					}
					if(html.Length > 0)
					{
						WhiteSpaceHelper w		= new WhiteSpaceHelper();
						w.Value				= html;
						w.Replacement		= m.Value;
						matchList.Add(w);
					}
					m = m.NextMatch();
				}
				
				foreach(WhiteSpaceHelper ws in matchList)
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
 * Revision 1.2  2006/02/05 20:02:25  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.2  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.1  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 */