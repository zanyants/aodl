using System;
using System.Collections;
using System.Drawing;

namespace AODL.TextDocument.Style.Properties
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
