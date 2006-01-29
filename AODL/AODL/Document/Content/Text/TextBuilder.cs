/*
 * $Id: TextBuilder.cs,v 1.1 2006/01/29 11:28:22 larsbm Exp $
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
using AODL.Document;
using AODL.Document.Content.Text.TextControl;

namespace AODL.Document.Content.Text
{
	/// <summary>
	/// TextBuilder use this class to build TextCollection from
	/// text that contains text control character like whitespaces,
	/// tab stops and line breaks.
	/// </summary>
	public class TextBuilder
	{
		/// <summary>
		/// Builds the text collection.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static ITextCollection BuildTextCollection(IDocument document, string text)
		{
			string xmlStartTag				= "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
			ITextCollection txtCollection	= new ITextCollection();
			text							= WhiteSpaceHelper.GetWhiteSpaceXml(text);
			text							= text.Replace("\t", "<t/>");
			text							= text.Replace("\n", "<n/>");
			xmlStartTag						+= "<txt>"+text+"</txt>";

			XmlDocument xmlDoc				= new XmlDocument();
			xmlDoc.LoadXml(xmlStartTag);

			XmlNode nodeStart				= xmlDoc.DocumentElement;
			if(nodeStart != null)
				if(nodeStart.HasChildNodes)
				{
					foreach(XmlNode childNode in nodeStart.ChildNodes)
					{
						if(childNode.NodeType == XmlNodeType.Text)
							txtCollection.Add(new SimpleText(document, childNode.InnerText));
						else if(childNode.Name == "ws")
						{
							if(childNode.Attributes.Count == 1)
							{
								XmlNode nodeCnt = childNode.Attributes.GetNamedItem("id");
								if(nodeCnt != null)
									txtCollection.Add(new WhiteSpace(document, Convert.ToInt32(nodeCnt.InnerText)));
							}
						}
						else if(childNode.Name == "t")
						{
							txtCollection.Add(new TabStop(document));
						}
						else if(childNode.Name == "n")
						{
							txtCollection.Add(new LineBreak(document));
						}
					}
				}
				else
				{
					txtCollection.Add(new SimpleText(document, text));
				}
			return txtCollection;
		}
	}
}
