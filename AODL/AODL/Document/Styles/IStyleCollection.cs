/*
 * $Id: IStyleCollection.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
using System.Xml;
using AODL.Document.Collections;

namespace AODL.Document.Styles
{
	/// <summary>
	/// IStyleCollection
	/// </summary>
	public class IStyleCollection : CollectionWithEvents
	{
		/// <summary>
		/// Adds the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public int Add(AODL.Document.Styles.IStyle value)
		{
			return base.List.Add(value as object);
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Remove(AODL.Document.Styles.IStyle value)
		{
			base.List.Remove(value as object);
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public void Insert(int index, AODL.Document.Styles.IStyle value)
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
		public bool Contains(AODL.Document.Styles.IStyle value)
		{
			return base.List.Contains(value as object);
		}

		/// <summary>
		/// Gets the <see cref="IStyle"/> at the specified index.
		/// </summary>
		/// <value></value>
		public AODL.Document.Styles.IStyle this[int index]
		{
			get { return (base.List[index] as AODL.Document.Styles.IStyle); }
		}

		/// <summary>
		/// Gets the name of the style by.
		/// </summary>
		/// <param name="styleName">Name of the style.</param>
		/// <returns></returns>
		public AODL.Document.Styles.IStyle GetStyleByName(string styleName)
		{
			foreach(IStyle style in base.List)
			{
				if(style.StyleName != null && styleName != null)
				{
					if(style.StyleName.ToLower().Equals(styleName.ToLower()))
						return style;
				}
			}

			return null;
		}
	}
}

/*
 * $Log: IStyleCollection.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */