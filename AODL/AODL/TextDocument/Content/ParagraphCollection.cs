using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument.Content
{
	public class ParagraphCollection : CollectionWithEvents
	{
		public int Add(AODL.TextDocument.Content.Paragraph value)
		{
			return base.List.Add(value as object);
		}

		public void Remove(AODL.TextDocument.Content.Paragraph value)
		{
			base.List.Remove(value as object);
		}

		public void Insert(int index, AODL.TextDocument.Content.Paragraph value)
		{
			base.List.Insert(index, value as object);
		}

		public bool Contains(AODL.TextDocument.Content.Paragraph value)
		{
			return base.List.Contains(value as object);
		}

		public AODL.TextDocument.Content.Paragraph this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Content.Paragraph); }
		}
	}
}