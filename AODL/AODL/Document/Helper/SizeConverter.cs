/*
 * $Id: SizeConverter.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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

namespace AODL.Document.Helper
{
	/// <summary>
	/// The SizeConverter class offer different methods for size 
	/// type conversation.
	/// </summary>
	public class SizeConverter
	{
		/// <summary>
		/// Pixel to cm factor
		/// </summary>
		public static readonly double pxtocm		= 37.7928;
		/// <summary>
		/// Inch to cm factor
		/// </summary>
		public static readonly double intocm		= 2.41;

		/// <summary>
		/// Initializes a new instance of the <see cref="SizeConverter"/> class.
		/// </summary>
		public SizeConverter()
		{
		}

		/// <summary>
		/// Cms to inch.
		/// </summary>
		/// <param name="cm">The cm.</param>
		/// <returns></returns>
		public static double CmToInch(double cm)
		{
			return intocm*cm;
		}

		/// <summary>
		/// Cms to pixel.
		/// </summary>
		/// <param name="cm">The cm.</param>
		/// <returns></returns>
		public static double CmToPixel(double cm)
		{
			return pxtocm*cm;
		}

		/// <summary>
		/// Inches to cm.
		/// </summary>
		/// <param name="inch">The inch.</param>
		/// <returns></returns>
		public static double InchToCm(double inch)
		{
			return inch/intocm;
		}

		/// <summary>
		/// Inches to pixel.
		/// </summary>
		/// <param name="inch">The inch.</param>
		/// <returns></returns>
		public static double InchToPixel(double inch)
		{
			return inch*pxtocm*intocm;
		}

		/// <summary>
		/// Inches to pixel as string.
		/// </summary>
		/// <param name="inch">The inch.</param>
		/// <returns></returns>
		public static string InchToPixelAsString(double inch)
		{
			return InchToPixel(inch).ToString(System.Globalization.NumberFormatInfo.InvariantInfo)+"px";
		}

		/// <summary>
		/// Inches to cm as string.
		/// </summary>
		/// <param name="inch">The inch.</param>
		/// <returns></returns>
		public static string InchToCmAsString(double inch)
		{
			return InchToCm(inch).ToString(System.Globalization.NumberFormatInfo.InvariantInfo)+"cm";
		}

		/// <summary>
		/// Cms to inch as string.
		/// </summary>
		/// <param name="cm">The cm.</param>
		/// <returns></returns>
		public static string CmToInchAsString(double cm)
		{
			//return CmToInch(cm).ToString(System.Globalization.NumberFormatInfo.InvariantInfo)+"in";
			int inch			= (int)CmToInch(cm);
			return inch.ToString()+"in";
		}

		/// <summary>
		/// Cms to pixel as string.
		/// </summary>
		/// <param name="cm">The cm.</param>
		/// <returns></returns>
		public static string CmToPixelAsString(double cm)
		{
			//return CmToPixel(cm).ToString(System.Globalization.NumberFormatInfo.InvariantInfo)+"px";
			int px			= (int)CmToPixel(cm);
			return px.ToString()+"px";
		}

		/// <summary>
		/// Gets the pixel from an office size value.
		/// e.g pass 1.54cm and get back 58
		/// </summary>
		/// <param name="aSizeValue">A size value.</param>
		/// <returns>If something goes wrong it will return 0.</returns>
		public static int GetPixelFromAnOfficeSizeValue(string aSizeValue)
		{
			if(aSizeValue == null)
				return 0;

			try
			{
				if(aSizeValue.EndsWith("cm"))
				{
					aSizeValue		= aSizeValue.Replace("cm", "");

					return (int)CmToPixel(Convert.ToDouble(aSizeValue, System.Globalization.NumberFormatInfo.InvariantInfo));
				}
				else if(aSizeValue.EndsWith("in"))
				{
					aSizeValue		= aSizeValue.Replace("in", "");

					return (int)InchToPixel(Convert.ToDouble(aSizeValue, System.Globalization.NumberFormatInfo.InvariantInfo));
				}

				return 0;
			}
			catch(Exception ex)
			{
				return 0;
			}
		}
	}
}

/*
 * $Log: SizeConverter.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */