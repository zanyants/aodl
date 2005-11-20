/*
 * $Id: DocumentPictureCollection.cs,v 1.2 2005/11/20 17:31:20 larsbm Exp $
 */

using System.Collections;
using AODL.Collections;

namespace AODL.TextDocument
{
	/// <summary>
	/// A DocumentPicture collection.
	/// </summary>
	public class DocumentPictureCollection : CollectionWithEvents
	{
		/// <summary>
		/// Adds the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public int Add(AODL.TextDocument.DocumentPicture value)
		{
			return base.List.Add(value as object);
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Remove(AODL.TextDocument.DocumentPicture value)
		{
			base.List.Remove(value as object);
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public void Insert(int index, AODL.TextDocument.DocumentPicture value)
		{
			base.List.Insert(index, value as object);
		}

		/// <summary>
		/// Determines whether [contains] [the specified value].
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(AODL.TextDocument.DocumentPicture value)
		{
			return base.List.Contains(value as object);
		}

		/// <summary>
		/// Gets the <see cref="DocumentPicture"/> at the specified index.
		/// </summary>
		/// <value></value>
		public AODL.TextDocument.DocumentPicture this[int index]
		{
			get { return (base.List[index] as AODL.TextDocument.DocumentPicture); }
		}
	}
}

/*
 * $Log: DocumentPictureCollection.cs,v $
 * Revision 1.2  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */