/*
 * $Id: CellCollection.cs,v 1.1 2006/01/29 11:28:22 larsbm Exp $
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
using AODL.Document.Collections;

namespace AODL.Document.Content.Tables
{
	/// <summary>
	/// CellCollection
	/// </summary>
	public class CellCollection : CollectionWithEvents
	{
		/// <summary>
		/// Adds the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public int Add(AODL.Document.Content.Tables.Cell value)
		{
			return base.List.Add(value as object);
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Remove(AODL.Document.Content.Tables.Cell value)
		{
			base.List.Remove(value as object);
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public void Insert(int index, AODL.Document.Content.Tables.Cell value)
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
		public bool Contains(AODL.Document.Content.Tables.Cell value)
		{
			return base.List.Contains(value as object);
		}

		/// <summary>
		/// Gets the <see cref="Cell"/> at the specified index.
		/// </summary>
		/// <value></value>
		public AODL.Document.Content.Tables.Cell this[int index]
		{
			get { return (base.List[index] as AODL.Document.Content.Tables.Cell); }
		}
	}
}

/*
 * $Log: CellCollection.cs,v $
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */