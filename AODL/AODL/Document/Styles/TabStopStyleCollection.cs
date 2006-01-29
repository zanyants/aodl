/*
 * $Id: TabStopStyleCollection.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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

using System.Collections;
using AODL.Document;
using AODL.Document.Collections;
using System.Xml;

namespace AODL.Document.Styles
{
	/// <summary>
	/// Represent a TabStopStyle collection which could be
	/// used within a ParagraphStyle object.
	/// Notice: A TabStopStyleCollection will not work
	/// within a Standard Paragraph!
	/// </summary>
	public class TabStopStyleCollection : CollectionWithEvents
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

		private IDocument _document;
		/// <summary>
		/// Gets or sets the document.
		/// </summary>
		/// <value>The document.</value>
		public IDocument Document
		{
			get { return this._document; }
			set { this._document = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TabStopStyleCollection"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		public TabStopStyleCollection(IDocument document)
		{
			this.Document		= document;
			this.Node			= this.Document.CreateNode("tab-stops", "style");
		}

		/// <summary>
		/// Adds the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public int Add(AODL.Document.Styles.TabStopStyle value)
		{
			this.Node.AppendChild(((TabStopStyle)value).Node);
			return base.List.Add(value as object);
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Remove(AODL.Document.Styles.TabStopStyle value)
		{
			this.Node.RemoveChild(((TabStopStyle)value).Node);
			base.List.Remove(value as object);
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public void Insert(int index, AODL.Document.Styles.TabStopStyle value)
		{
			//It's not necessary to know the postion of the child node
			this.Node.AppendChild(((TabStopStyle)value).Node);
			base.List.Insert(index, value as object);
		}

		/// <summary>
		/// Determines whether [contains] [the specified value].
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(AODL.Document.Styles.TabStopStyle value)
		{
			return base.List.Contains(value as object);
		}

		/// <summary>
		/// Gets the <see cref="TabStopStyle"/> at the specified index.
		/// </summary>
		/// <value></value>
		public AODL.Document.Styles.TabStopStyle this[int index]
		{
			get { return (base.List[index] as AODL.Document.Styles.TabStopStyle); }
		}
	}
}

/*
 * $Log: TabStopStyleCollection.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.1  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 */