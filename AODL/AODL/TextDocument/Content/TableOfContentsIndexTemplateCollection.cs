/*
 * $Id: TableOfContentsIndexTemplateCollection.cs,v 1.1 2006/01/05 10:31:10 larsbm Exp $
 */

using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument.Content
{
	public class TableOfContentsIndexTemplateCollection : CollectionWithEvents
	{
		public int Add(AODL.TextDocument.Content.TableOfContentsIndexTemplate value)
		{
			return base.List.Add(value as object);
		}

		public void Remove(AODL.TextDocument.Content.TableOfContentsIndexTemplate value)
		{
			base.List.Remove(value as object);
		}

		public void Insert(int index, AODL.TextDocument.Content.TableOfContentsIndexTemplate value)
		{
			base.List.Insert(index, value as object);
		}

		public bool Contains(AODL.TextDocument.Content.TableOfContentsIndexTemplate value)
		{
			return base.List.Contains(value as object);
		}

		public AODL.TextDocument.Content.TableOfContentsIndexTemplate this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.Content.TableOfContentsIndexTemplate); }
		}
	}
}

/*
 * $Log: TableOfContentsIndexTemplateCollection.cs,v $
 * Revision 1.1  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 */