/*
 * $Id: IFontStyle.cs,v 1.2 2005/10/08 07:50:15 larsbm Exp $
 */

using System;

namespace AODL.TextDocument.Style
{	
	/// <summary>
	/// All classes that represent a font class must implement
	/// this interface.
	/// </summary>
	public interface IFontStyle
	{
		/// <summary>
		/// The font.
		/// </summary>
		FontFamily Font
		{
			get; 
			set; 
		}
	}
}

/*
 * $Log: IFontStyle.cs,v $
 * Revision 1.2  2005/10/08 07:50:15  larsbm
 * - added cvs tags
 *
 */