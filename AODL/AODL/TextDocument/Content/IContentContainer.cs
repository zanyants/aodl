/*
 * $Id: IContentContainer.cs,v 1.2 2005/11/20 17:31:20 larsbm Exp $
 */

using System;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// All classes that want to act as a content conatiner
	/// have to implement this interface.
	/// </summary>
	public interface IContentContainer
	{
		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>The content.</value>
		IContentCollection Content {get; set;}
	}
}

/*
 * $Log: IContentContainer.cs,v $
 * Revision 1.2  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.1  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 */