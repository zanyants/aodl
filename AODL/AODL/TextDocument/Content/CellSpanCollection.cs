/*
 * $Id: CellSpanCollection.cs,v 1.1 2006/01/05 10:31:10 larsbm Exp $
 */

using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument.Content
{
	public class CellSpanCollection : CollectionWithEvents
	{
		public int Add(AODL.TextDocument.Content.CellSpan value)
		{
			return base.List.Add(value as object);
		}

		public void Remove(AODL.TextDocument.Content.CellSpan value)
		{
			base.List.Remove(value as object);
		}

		public void Insert(int index, AODL.TextDocument.Content.CellSpan value)
		{
			base.List.Insert(index, value as object);
		}

		public bool Contains(AODL.TextDocument.Content.CellSpan value)
		{
			return base.List.Contains(value as object);
		}

		public AODL.TextDocument.Content.CellSpan this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Content.CellSpan); }
		}
	}
}

/*
 * $Log: CellSpanCollection.cs,v $
 * Revision 1.1  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 */