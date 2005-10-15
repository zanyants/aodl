/*
 * $Id: RowCollection.cs,v 1.1 2005/10/15 11:40:31 larsbm Exp $
 */

using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument.Content
{
	public class RowCollection : CollectionWithEvents
	{
		public int Add(AODL.TextDocument.Content.Row value)
		{
			return base.List.Add(value as object);
		}

		public void Remove(AODL.TextDocument.Content.Row value)
		{
			base.List.Remove(value as object);
		}

		public void Insert(int index, AODL.TextDocument.Content.Row value)
		{
			base.List.Insert(index, value as object);
		}

		public bool Contains(AODL.TextDocument.Content.Row value)
		{
			return base.List.Contains(value as object);
		}

		public AODL.TextDocument.Content.Row this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Content.Row); }
		}
	}
}

/*
 * $Id: RowCollection.cs,v 1.1 2005/10/15 11:40:31 larsbm Exp $
 */