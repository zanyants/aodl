/*
 * $Id: CellCollection.cs,v 1.1 2005/10/15 11:40:31 larsbm Exp $
 */

using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument.Content
{
	public class CellCollection : CollectionWithEvents
	{
		public int Add(AODL.TextDocument.Content.Cell value)
		{
			return base.List.Add(value as object);
		}

		public void Remove(AODL.TextDocument.Content.Cell value)
		{
			base.List.Remove(value as object);
		}

		public void Insert(int index, AODL.TextDocument.Content.Cell value)
		{
			base.List.Insert(index, value as object);
		}

		public bool Contains(AODL.TextDocument.Content.Cell value)
		{
			return base.List.Contains(value as object);
		}

		public AODL.TextDocument.Content.Cell this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Content.Cell); }
		}
	}
}

/*
 * $Log: CellCollection.cs,v $
 * Revision 1.1  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 */