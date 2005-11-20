/*
 * $Id: TabStopLeaderStyles.cs,v 1.1 2005/11/20 17:31:20 larsbm Exp $
 */

using System;

namespace AODL.TextDocument.Style
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
 * Revision 1.1  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 */
