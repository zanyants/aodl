/*
 * $Id: IContentContainer.cs,v 1.1 2005/10/09 15:52:47 larsbm Exp $
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
		IContentCollection Content {get; set;}
	}
}

/*
 * $Log: IContentContainer.cs,v $
 * Revision 1.1  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 */