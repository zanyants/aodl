/*
 * $Id: MasterPageFactory.cs,v 1.1 2007/02/13 17:58:52 larsbm Exp $
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
 */

using System;
using AODL.Document.Content;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;
using System.Collections;
using System.Xml;

namespace AODL.Document.Styles.MasterStyles
{
	/// <summary>
	/// Summary for MasterPageFactory.
	/// </summary>
	public class MasterPageFactory
	{

		/// <summary>
		/// Fill/read the existing master page styles.
		/// </summary>
		/// <param name="textDocument">The owner text document.</param>
		public static void FillFromXMLDocument(TextDocument textDocument)
		{
			try
			{
				TextMasterPageCollection txtMPCollection = new TextMasterPageCollection();
				XmlNodeList masterPageNodes = textDocument.DocumentStyles.Styles.SelectNodes(
					"//style:master-page", textDocument.NamespaceManager);
				if(masterPageNodes != null)
				{
					foreach(XmlNode mpNode in masterPageNodes)
					{
						// Build the master page
						TextMasterPage txtMasterPage = new TextMasterPage(textDocument, mpNode);
						// Even if there is no usage of header within the master page style,
						// but of course there exists the header:style node, so we create
						// the TextPageHeader.
						txtMasterPage.TextPageHeader = new TextPageHeader();
						txtMasterPage.TextPageHeader.TextDocument = textDocument;
						txtMasterPage.TextPageHeader.TextMasterPage = txtMasterPage;
						// see comment above its the same procedure
						txtMasterPage.TextPageFooter = new TextPageFooter();
						txtMasterPage.TextPageFooter.TextDocument = textDocument;
						txtMasterPage.TextPageFooter.TextMasterPage = txtMasterPage;
						
						// Build header content
						XmlNode headerNode = mpNode.SelectSingleNode("//style:header", textDocument.NamespaceManager);
						if(headerNode != null)
						{
							txtMasterPage.TextPageHeader.ContentNode = headerNode;
						}

						// Build master page layout
						XmlNode txtPageLayoutNode = textDocument.DocumentStyles.Styles.SelectSingleNode(
							"//style:page-layout[@style:name='"+txtMasterPage.PageLayoutName+"']",
							textDocument.NamespaceManager);
						if(txtPageLayoutNode != null)
						{
							// Build master page layout properties
							XmlNode txtPageLayoutPropNode = txtPageLayoutNode.SelectSingleNode(
								"//style:page-layout-properties", textDocument.NamespaceManager);
							if(txtPageLayoutPropNode != null)
							{
								TextPageLayout txtPageLayout = new TextPageLayout(
									textDocument, txtPageLayoutNode, txtPageLayoutPropNode);
								txtMasterPage.TextPageLayout = txtPageLayout;
							}
							// Build master page header layout
							XmlNode txtHeaderStyleNode = txtPageLayoutNode.SelectSingleNode(
								"//style:header-style", textDocument.NamespaceManager);
							if(txtHeaderStyleNode != null)
							{
								txtMasterPage.TextPageHeader.StyleNode = txtHeaderStyleNode;
								if(txtHeaderStyleNode.FirstChild != null
									&& txtHeaderStyleNode.FirstChild.Name == "style:header-footer-properties")
									txtMasterPage.TextPageHeader.PropertyNode = txtHeaderStyleNode.FirstChild;
							}
							// Build master page footer layout
							XmlNode txtFooterStyleNode = txtPageLayoutNode.SelectSingleNode(
								"//style:footer-style", textDocument.NamespaceManager);
							if(txtFooterStyleNode != null)
							{
								txtMasterPage.TextPageFooter.StyleNode = txtFooterStyleNode;
								if(txtFooterStyleNode.FirstChild != null
									&& txtFooterStyleNode.FirstChild.Name == "style:header-footer-properties")
									txtMasterPage.TextPageFooter.PropertyNode = txtFooterStyleNode.FirstChild;
							}
						}
						
						txtMPCollection.Add(txtMasterPage);
					}
				}
				textDocument.DocumentStyles.TextMasterPageCollection = txtMPCollection;
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
