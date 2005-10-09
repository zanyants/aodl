/*
 * $Id: ListLevelStyleCollection.cs,v 1.1 2005/10/09 15:52:47 larsbm Exp $
 */

using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument.Style
{
	public class ListLevelStyleCollection : CollectionWithEvents
	{
		public int Add(AODL.TextDocument.Style.ListLevelStyle value)
		{
			return base.List.Add(value as object);
		}

		public void Remove(AODL.TextDocument.Style.ListLevelStyle value)
		{
			base.List.Remove(value as object);
		}

		public void Insert(int index, AODL.TextDocument.Style.ListLevelStyle value)
		{
			base.List.Insert(index, value as object);
		}

		public bool Contains(AODL.TextDocument.Style.ListLevelStyle value)
		{
			return base.List.Contains(value as object);
		}

		public AODL.TextDocument.Style.ListLevelStyle this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Style.ListLevelStyle); }
		}
	}
}

/*
 * $Log: ListLevelStyleCollection.cs,v $
 * Revision 1.1  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 */