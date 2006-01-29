/*
 * $Id: FamiliyStyles.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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

namespace AODL.Document.Styles
{	
	/// <summary>
	/// Class that represents th possible values. For a style emlement and his family-style attribute.
	/// </summary>
	public class FamiliyStyles
	{
		/// <summary>
		/// table
		/// </summary>
		public static readonly string Table			= "table";
		/// <summary>
		/// column
		/// </summary>
		public static readonly string TableColumn	= "table-column";
		/// <summary>
		/// cell
		/// </summary>
		public static readonly string TableCell		= "table-cell";
		/// <summary>
		/// row
		/// </summary>
		public static readonly string TableRow		= "table-row";
		/// <summary>
		/// paragraph
		/// </summary>
		public static readonly string Paragraph		= "paragraph";
		/// <summary>
		/// text
		/// </summary>
		public static readonly string Text			= "text";
		/// <summary>
		/// graphic
		/// </summary>
		public static readonly string Graphic		= "graphic";
	}
}

/*
 * $Log: FamiliyStyles.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.3  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.2  2005/10/08 07:50:15  larsbm
 * - added cvs tags
 *
 */