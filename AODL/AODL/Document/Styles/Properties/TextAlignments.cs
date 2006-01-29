/*
 * $Id: TextAlignments.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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

namespace AODL.Document.Styles.Properties
{
	/// <summary>
	/// Possible text alignments.
	/// </summary>
	public enum TextAlignments
	{
		/// <summary>
		/// Start
		/// </summary>
		start,
		/// <summary>
		/// End
		/// </summary>
		end,
		/// <summary>
		/// Left
		/// </summary>
		left,
		/// <summary>
		/// Right
		/// </summary>
		right,
		/// <summary>
		/// center
		/// </summary>
		center,
		/// <summary>
		/// Justify
		/// </summary>
		justify
	}
}

/*
 * $Log: TextAlignments.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.3  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */