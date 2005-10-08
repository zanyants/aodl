/*
 * $Id: IFamilyStyle.cs,v 1.2 2005/10/08 07:50:15 larsbm Exp $
 */

using System;

namespace AODL.TextDocument.Style
{	
	/// <summary>
	/// All classes that need a family style attribute must implement
	/// this interface.
	/// </summary>
	public interface IFamilyStyle
	{
		/// <summary>
		/// The family style.
		/// </summary>
		string Family
		{
			get; 
			set; 
		}
	}
}

/*
 * $Log: IFamilyStyle.cs,v $
 * Revision 1.2  2005/10/08 07:50:15  larsbm
 * - added cvs tags
 *
 */