/*
 * $Id: Colors.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
using System.Collections;
using System.Drawing;

namespace AODL.Document.Helper
{
	/// <summary>
	/// Converter class. Convert any enum Color from System.Drawing.Color
	/// into his rgb hex value.
	/// </summary>
	public class Colors
	{

		/// <summary>
		/// Convert any enum Color from System.Drawing.Color
		/// into his rgb hex value.
		/// </summary>
		/// <param name="color">A System.Drawing.Color</param>
		/// <returns>The rgb hex value.</returns>
		public static string GetColor(Color color)
		{
			int argb = color.ToArgb();

			return "#"+argb.ToString("x").Substring(2);
		}
	}
}

/*
 * $Log: Colors.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.2  2005/10/08 07:55:35  larsbm
 * - added cvs tags
 *
 */