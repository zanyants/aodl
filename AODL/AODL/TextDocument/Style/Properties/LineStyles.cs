/*
 * $Id: LineStyles.cs,v 1.2 2005/10/08 07:55:35 larsbm Exp $
 */

using System;

namespace AODL.TextDocument.Style.Properties
{
	/// <summary>
	/// Represents the possible line styles used in OpenDocument.
	/// e.g for the FormatedText.Underline
	/// </summary>
	public enum LineStyles
	{
		/* Set by hand, because of -
		 * long-dash
		 * dot-dash 
		 * dot-dot-dash
		 */
		/// <summary>
		/// No style
		/// </summary>
		none,
		/// <summary>
		/// solid
		/// </summary>
		solid,
		/// <summary>
		/// dotted
		/// </summary>
		dotted,
		/// <summary>
		/// dash
		/// </summary>
		dash,
		/// <summary>
		/// wave
		/// </summary>
		wave
	}
}

/*
 * $Log: LineStyles.cs,v $
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */