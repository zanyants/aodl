/*
 * $Id: ITextCollection.cs,v 1.2 2005/10/08 08:19:25 larsbm Exp $
 */

using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// A typed IText collection.
	/// </summary>
	public class ITextCollection : CollectionWithEvents
	{
		public int Add(AODL.TextDocument.Content.IText value)
		{
			return base.List.Add(value as object);
		}

		public void Remove(AODL.TextDocument.Content.IText value)
		{
			base.List.Remove(value as object);
		}

		public void Insert(int index, AODL.TextDocument.Content.IText value)
		{
			base.List.Insert(index, value as object);
		}

		public bool Contains(AODL.TextDocument.Content.IText value)
		{
			return base.List.Contains(value as object);
		}

		public AODL.TextDocument.Content.IText this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Content.IText); }
		}
	}
}

/*
 * $Log: ITextCollection.cs,v $
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */