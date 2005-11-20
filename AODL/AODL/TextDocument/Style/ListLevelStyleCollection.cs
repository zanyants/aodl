/*
 * $Id: ListLevelStyleCollection.cs,v 1.2 2005/11/20 17:31:20 larsbm Exp $
 */

using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument.Style
{
	/// <summary>
	/// A ListLevelStyle collection
	/// </summary>
	public class ListLevelStyleCollection : CollectionWithEvents
	{
		/// <summary>
		/// Adds the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public int Add(AODL.TextDocument.Style.ListLevelStyle value)
		{
			return base.List.Add(value as object);
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Remove(AODL.TextDocument.Style.ListLevelStyle value)
		{
			base.List.Remove(value as object);
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public void Insert(int index, AODL.TextDocument.Style.ListLevelStyle value)
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
		public bool Contains(AODL.TextDocument.Style.ListLevelStyle value)
		{
			return base.List.Contains(value as object);
		}

		/// <summary>
		/// Gets the <see cref="ListLevelStyle"/> at the specified index.
		/// </summary>
		/// <value></value>
		public AODL.TextDocument.Style.ListLevelStyle this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Style.ListLevelStyle); }
		}
	}
}

/*
 * $Log: ListLevelStyleCollection.cs,v $
 * Revision 1.2  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.1  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 */