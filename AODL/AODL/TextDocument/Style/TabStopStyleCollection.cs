/*
 * $Id: TabStopStyleCollection.cs,v 1.1 2005/11/20 17:31:20 larsbm Exp $
 */

using System.Collections;
using AODL.TextDocument.Style;
using AODL.TextDocument;
using AODL.Collections;
using System.Xml;

namespace AODL.TextDocument.Style
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

		private TextDocument _document;
		/// <summary>
		/// Gets or sets the document.
		/// </summary>
		/// <value>The document.</value>
		public TextDocument Document
		{
			get { return this._document; }
			set { this._document = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TabStopStyleCollection"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		public TabStopStyleCollection(TextDocument document)
		{
			this.Document		= document;
			this.Node			= this.Document.CreateNode("tab-stops", "style");
		}

		/// <summary>
		/// Adds the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public int Add(AODL.TextDocument.Style.TabStopStyle value)
		{
			this.Node.AppendChild(((TabStopStyle)value).Node);
			return base.List.Add(value as object);
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Remove(AODL.TextDocument.Style.TabStopStyle value)
		{
			this.Node.RemoveChild(((TabStopStyle)value).Node);
			base.List.Remove(value as object);
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public void Insert(int index, AODL.TextDocument.Style.TabStopStyle value)
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
		public bool Contains(AODL.TextDocument.Style.TabStopStyle value)
		{
			return base.List.Contains(value as object);
		}

		/// <summary>
		/// Gets the <see cref="TabStopStyle"/> at the specified index.
		/// </summary>
		/// <value></value>
		public AODL.TextDocument.Style.TabStopStyle this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Style.TabStopStyle); }
		}
	}
}

/*
 * $Log: TabStopStyleCollection.cs,v $
 * Revision 1.1  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 */