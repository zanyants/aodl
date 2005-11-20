/*
 * $Id: TextAlignments.cs,v 1.3 2005/11/20 17:31:20 larsbm Exp $
 */

using System;

namespace AODL.TextDocument.Style.Properties
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
 * Revision 1.3  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */