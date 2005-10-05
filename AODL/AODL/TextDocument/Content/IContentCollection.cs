using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// A typed IContent Collection.
	/// </summary>
	public class IContentCollection : CollectionWithEvents
	{
		public int Add(AODL.TextDocument.Content.IContent value)
		{
			return base.List.Add(value as object);
		}

		public void Remove(AODL.TextDocument.Content.IContent value)
		{
			base.List.Remove(value as object);
		}

		public void Insert(int index, AODL.TextDocument.Content.IContent value)
		{
			base.List.Insert(index, value as object);
		}

		public bool Contains(AODL.TextDocument.Content.IContent value)
		{
			return base.List.Contains(value as object);
		}

		public AODL.TextDocument.Content.IContent this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Content.IContent); }
		}
	}
}