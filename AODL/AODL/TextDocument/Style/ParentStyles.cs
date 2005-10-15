/*
 * $Id: ParentStyles.cs,v 1.3 2005/10/15 11:40:31 larsbm Exp $
 */

using System;

namespace AODL.TextDocument.Style
{	
	/// <summary>
	/// Enum that represents th possible values. For a style emlement and his parent-style attribute.
	/// </summary>
	public enum ParentStyles
	{
		Standard,
		Table
	}
}

/*
 * $Log: ParentStyles.cs,v $
 * Revision 1.3  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */