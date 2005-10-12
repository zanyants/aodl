using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument.Content
{
	public class ColumnCollection : CollectionWithEvents
	{
		public int Add(AODL.TextDocument.Content.Column value)
		{
			return base.List.Add(value as object);
		}

		public void Remove(AODL.TextDocument.Content.Column value)
		{
			base.List.Remove(value as object);
		}

		public void Insert(int index, AODL.TextDocument.Content.Column value)
		{
			base.List.Insert(index, value as object);
		}

		public bool Contains(AODL.TextDocument.Content.Column value)
		{
			return base.List.Contains(value as object);
		}

		public AODL.TextDocument.Content.Column this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Content.Column); }
		}
	}
}