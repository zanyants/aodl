/*
 * $Id: DocumentPictureCollection.cs,v 1.1 2005/11/06 14:55:25 larsbm Exp $
 */

using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument
{
	public class DocumentPictureCollection : CollectionWithEvents
	{
		public int Add(AODL.TextDocument.DocumentPicture value)
		{
			return base.List.Add(value as object);
		}

		public void Remove(AODL.TextDocument.DocumentPicture value)
		{
			base.List.Remove(value as object);
		}

		public void Insert(int index, AODL.TextDocument.DocumentPicture value)
		{
			base.List.Insert(index, value as object);
		}

		public bool Contains(AODL.TextDocument.DocumentPicture value)
		{
			return base.List.Contains(value as object);
		}

		public AODL.TextDocument.DocumentPicture this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.DocumentPicture); }
		}
	}
}

/*
 * $Log: DocumentPictureCollection.cs,v $
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */