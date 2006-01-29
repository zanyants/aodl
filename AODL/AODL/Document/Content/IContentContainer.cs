/*
 * $Id: IContentContainer.cs,v 1.1 2006/01/29 11:29:46 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, Lars Behrmann, lb@OpenDocument4all.com
 * 
 * Last changes:
 * 
 */

using System;

namespace AODL.Document.Content
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
 * Revision 1.1  2006/01/29 11:29:46  larsbm
 * *** empty log message ***
 *
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