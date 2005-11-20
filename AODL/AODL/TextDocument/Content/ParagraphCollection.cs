/*
 * $Id: ParagraphCollection.cs,v 1.3 2005/11/20 17:31:20 larsbm Exp $
 */

using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// A Paragraph collection
	/// </summary>
	public class ParagraphCollection : CollectionWithEvents
	{
		/// <summary>
		/// Adds the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public int Add(AODL.TextDocument.Content.Paragraph value)
		{
			return base.List.Add(value as object);
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Remove(AODL.TextDocument.Content.Paragraph value)
		{
			base.List.Remove(value as object);
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public void Insert(int index, AODL.TextDocument.Content.Paragraph value)
		{
			base.List.Insert(index, value as object);
		}

		/// <summary>
		/// Determines whether [contains] [the specified value].
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(AODL.TextDocument.Content.Paragraph value)
		{
			return base.List.Contains(value as object);
		}

		/// <summary>
		/// Gets the <see cref="Paragraph"/> at the specified index.
		/// </summary>
		/// <value></value>
		public AODL.TextDocument.Content.Paragraph this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Content.Paragraph); }
		}
	}
}

/*
 * $Log: ParagraphCollection.cs,v $
 * Revision 1.3  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */