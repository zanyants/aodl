/*
 * $Id: TextMasterPageCollection.cs,v 1.1 2007/02/13 17:58:55 larsbm Exp $
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
using System.Collections;
using AODL.Document.Collections;

namespace AODL.Document.Styles.MasterStyles
{
	/// <summary>
	/// Summary for TextMasterPageCollection.
	/// </summary>
	public class TextMasterPageCollection : CollectionWithEvents
	{
		/// <summary>
		/// Adds the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public int Add(AODL.Document.Styles.MasterStyles.TextMasterPage value)
		{
			return base.List.Add(value as object);
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Remove(AODL.Document.Styles.MasterStyles.TextMasterPage value)
		{
			base.List.Remove(value as object);
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public void Insert(int index, AODL.Document.Styles.MasterStyles.TextMasterPage value)
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
		public bool Contains(AODL.Document.Styles.MasterStyles.TextMasterPage value)
		{
			return base.List.Contains(value as object);
		}

		/// <summary>
		/// Gets the <see cref="Cell"/> at the specified index.
		/// </summary>
		/// <value></value>
		public AODL.Document.Styles.MasterStyles.TextMasterPage this[int index]
		{
			get { return (base.List[index] as AODL.Document.Styles.MasterStyles.TextMasterPage); }
		}

		/// <summary>
		/// Get a text master page by his style name.
		/// </summary>
		/// <returns>The TextMasterPage or null if no master page was found for this name.</returns>
		public AODL.Document.Styles.MasterStyles.TextMasterPage GetByStyleName(string styleName)
		{
			try
			{
				foreach(AODL.Document.Styles.MasterStyles.TextMasterPage txtMP in base.List)
				{
					if(txtMP.StyleName.ToLower().Equals(styleName.ToLower()))
						return txtMP;
				}
				return null;
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Gets the default master page for this text document.
		/// </summary>
		/// <returns>The default master page or null if no one was found.</returns>
		public AODL.Document.Styles.MasterStyles.TextMasterPage GetDefaultMasterPage()
		{
			try
			{
				return this.GetByStyleName("Standard");
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
