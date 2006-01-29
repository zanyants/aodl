/*
 * $Id: TableOfContentsIndexTemplate.cs,v 1.1 2006/01/29 11:28:22 larsbm Exp $
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

namespace AODL.Document.Content.Text.Indexes
{
	/// <summary>
	/// TableOfContentsIndexTemplate represent the table of content
	/// index template. It is used to set the index entries and their
	/// order within a table of content.
	/// A common order would be:
	/// Chapter Text TabStop PageNumber
	/// </summary>
	public class TableOfContentsIndexTemplate
	{
		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node
		{
			get { return this._node; }
			set { this._node = value; }
		}

		private TableOfContents _tableOfContents;
		/// <summary>
		/// Gets or sets the content of the table of.
		/// </summary>
		/// <value>The content of the table of.</value>
		public TableOfContents TableOfContents
		{
			get { return this._tableOfContents; }
			set { this._tableOfContents = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TableOfContentsIndexTemplate"/> class.
		/// </summary>
		/// <param name="tableOfContents">Content of the table of.</param>
		/// <param name="outlineLevel">For which outline level this template should be used.</param>
		/// <param name="styleName">Style name.</param>
		public TableOfContentsIndexTemplate(TableOfContents tableOfContents, int outlineLevel, string styleName)
		{
			this.TableOfContents			= tableOfContents;
			this.NewXmlNode(outlineLevel, styleName);
		}

		/// <summary>
		/// Inits the standard template.
		/// </summary>
		public void InitStandardTemplate()
		{
			if(this.TableOfContents.UseHyperlinks)
				this.InsertIndexEntry(IndexEntryTypes.HyperlinkStart);
			
			this.InsertIndexEntry(IndexEntryTypes.Chapter);
			this.InsertIndexEntry(IndexEntryTypes.Text);				

			if(this.TableOfContents.UseHyperlinks)
				this.InsertIndexEntry(IndexEntryTypes.HyperlinkEnd);

			this.InsertIndexEntry(IndexEntryTypes.TabStop);		
			this.InsertIndexEntry(IndexEntryTypes.PageNumber);
		}

		/// <summary>
		/// Insert a index entry to the template.
		/// You can define the order through the order you insert
		/// the index entry types.
		/// </summary>
		/// <param name="indexEntryType">Type of the index entry.</param>
		public void InsertIndexEntry(IndexEntryTypes indexEntryType)
		{
			if(indexEntryType == IndexEntryTypes.Chapter)
			{
				this.AddIndexEntryNode("index-entry-chapter", indexEntryType);
			}
			else if(indexEntryType == IndexEntryTypes.Text)
			{
				this.AddIndexEntryNode("index-entry-text", indexEntryType);
			}
			else if(indexEntryType == IndexEntryTypes.TabStop)
			{
				this.AddIndexEntryNode("index-entry-tab-stop", indexEntryType);
			}
			else if(indexEntryType == IndexEntryTypes.PageNumber)
			{
				this.AddIndexEntryNode("index-entry-page-number", indexEntryType);
			}
			else if(indexEntryType == IndexEntryTypes.HyperlinkStart)
			{
				this.AddIndexEntryNode("index-entry-link-start", indexEntryType);
			}
			else if(indexEntryType == IndexEntryTypes.HyperlinkEnd)
			{
				this.AddIndexEntryNode("index-entry-link-end", indexEntryType);
			}
		}

		/// <summary>
		/// Create the XmlNode which represent this object.
		/// </summary>
		/// <param name="outlineLevel">For which outline level this template should
		/// be used.</param>
		/// <param name="styleName">The name of the style which is referenced with
		/// this template</param>
		private void NewXmlNode(int outlineLevel, string styleName)
		{			
			this.Node						= this.TableOfContents.Document.CreateNode(
				"table-of-content-entry-template", "text");

			XmlAttribute xa					= this.TableOfContents.Document.CreateAttribute(
				"outline-level", "text");
			xa.Value						= outlineLevel.ToString();
			this.Node.Attributes.Append(xa);

			xa								= this.TableOfContents.Document.CreateAttribute(
				"style-name", "text");
			xa.Value						= styleName;
			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// Add the index entry node, with given type
		/// </summary>
		/// <param name="nodeName">Name of the node.</param>
		/// <param name="indexEntryType">Type of the index entry.</param>
		private void AddIndexEntryNode(string nodeName, IndexEntryTypes indexEntryType)
		{
			XmlNode indexEntryNode			= this.TableOfContents.Document.CreateNode(
				nodeName, "text");

			if(indexEntryType == IndexEntryTypes.TabStop)
			{
				XmlAttribute xa				= this.TableOfContents.Document.CreateAttribute(
					"type", "style");
				//Fixed to be right align
				xa.Value					= "right";
				indexEntryNode.Attributes.Append(xa);

				xa							= this.TableOfContents.Document.CreateAttribute(
					"leader-char", "style");
				//Fixed usage of .
				xa.Value					= ".";
				indexEntryNode.Attributes.Append(xa);
			}

			this.Node.AppendChild(indexEntryNode);
		}
	}

	/// <summary>
	/// The possible index entry types
	/// </summary>
	public enum IndexEntryTypes
	{
		/// <summary>
		/// Index entry type Chapter
		/// </summary>
		Chapter,
		/// <summary>
		/// Index entry type Text
		/// </summary>
		Text,
		/// <summary>
		/// Index entry type TabStop
		/// </summary>
		TabStop,
		/// <summary>
		/// Index entry type PageNumber
		/// </summary>
		PageNumber,
		/// <summary>
		/// Index entry type Hyperlink start
		/// </summary>
		HyperlinkStart,
		/// <summary>
		/// Index entry type Hyperlink start
		/// </summary>
		HyperlinkEnd
	}
}

/*
 * $Log: TableOfContentsIndexTemplate.cs,v $
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.1  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 */