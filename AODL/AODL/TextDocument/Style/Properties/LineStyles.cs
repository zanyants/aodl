/*
 * $Id: LineStyles.cs,v 1.3 2005/10/22 15:52:10 larsbm Exp $
 */

using System;

namespace AODL.TextDocument.Style.Properties
{
	/// <summary>
	/// Represents the possible line styles used in OpenDocument.
	/// e.g for the FormatedText.Underline
	/// </summary>
	public class LineStyles
	{
		/// <summary>
		/// long-dash
		/// </summary>
		public static readonly string longdash	= "long-dash";
		/// <summary>
		/// dot-dash
		/// </summary>
		public static readonly string dotdash	= "dot-dash";
		/// <summary>
		/// dot-dot-dash
		/// </summary>
		public static readonly string dotdotdash	= "dot-dot-dash";
		/// <summary>
		/// No style
		/// </summary>
		public static readonly string none	= "none";
		/// <summary>
		/// solid
		/// </summary>
		public static readonly string solid	= "solid";
		/// <summary>
		/// dotted
		/// </summary>
		public static readonly string dotted	= "dotted";
		/// <summary>
		/// dash
		/// </summary>
		public static readonly string dash	= "dash";
		/// <summary>
		/// wave
		/// </summary>
		public static readonly string wave	= "wave";
	}
}

/*
 * $Log: LineStyles.cs,v $
 * Revision 1.3  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */