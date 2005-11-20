/*
 * $Id: CellCollection.cs,v 1.2 2005/11/20 17:31:20 larsbm Exp $
 */

using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// A Cell collection
	/// </summary>
	public class CellCollection : CollectionWithEvents
	{
		/// <summary>
		/// Adds the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public int Add(AODL.TextDocument.Content.Cell value)
		{
			return base.List.Add(value as object);
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Remove(AODL.TextDocument.Content.Cell value)
		{
			base.List.Remove(value as object);
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public void Insert(int index, AODL.TextDocument.Content.Cell value)
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
		public bool Contains(AODL.TextDocument.Content.Cell value)
		{
			return base.List.Contains(value as object);
		}

		/// <summary>
		/// Gets the <see cref="Cell"/> at the specified index.
		/// </summary>
		/// <value></value>
		public AODL.TextDocument.Content.Cell this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Content.Cell); }
		}
	}
}

/*
 * $Log: CellCollection.cs,v $
 * Revision 1.2  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.1  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 */