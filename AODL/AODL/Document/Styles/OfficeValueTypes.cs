/*
 * $Id: OfficeValueTypes.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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

namespace AODL.Document.SpreadsheetDocuments.Tables.Style
{
	/// <summary>
	/// OfficeValueTypes represent possible values
	/// for office values that are used within
	/// spreadsheet table cells
	/// </summary>
	public class OfficeValueTypes
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OfficeValueTypes"/> class.
		/// </summary>
		public OfficeValueTypes()
		{		
		}

		/// <summary>
		/// Type float (floating point number)
		/// </summary>
		public static string Float			= "float";
		/// <summary>
		/// Type string (text)
		/// </summary>
		public static string String			= "string";
	}
}

/*
 * $Log: OfficeValueTypes.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */