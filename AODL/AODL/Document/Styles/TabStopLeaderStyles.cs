/*
 * $Id: TabStopLeaderStyles.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
	/// Represent all possible TabStopLeader Styles.
	/// </summary>
	public class TabStopLeaderStyles
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TabStopLeaderStyles"/> class.
		/// </summary>
		public TabStopLeaderStyles()
		{
		}

		/// <summary>
		/// Dotted style
		/// </summary>
		public static readonly string Dotted		= "dotted";
		/// <summary>
		/// Dotted style
		/// </summary>
		public static readonly string Solid			= "solid";
	}

	/// <summary>
	/// Represent all possible TabStopLeader Styles.
	/// </summary>
	public class TabStopTypes
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TabStopLeaderStyles"/> class.
		/// </summary>
		public TabStopTypes()
		{
		}

		/// <summary>
		/// Right
		/// </summary>
		public static readonly string Right			= "right";
		/// <summary>
		/// Center
		/// </summary>
		public static readonly string Center		= "center";
	}
}

/*
 * $Log: TabStopLeaderStyles.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.1  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 */
