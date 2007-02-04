using System;

namespace AODL.ExternalExporter.PDF.Document.StyleConverter
{
	/// <summary>
	/// Summary for RGBColorConverter.
	/// </summary>
	public class RGBColorConverter
	{
		public RGBColorConverter()
		{
		}

		/// <summary>
		/// Toes the color of the hex.
		/// </summary>
		/// <param name="rgbColor">Color of the RGB.</param>
		/// <returns></returns>
		public static string ToHexColor(string rgbColor)
		{
			try
			{
				//need leading zeros ?
				if(rgbColor.Length < 9)
					//fill up with leading zeros
					rgbColor      = rgbColor.PadLeft(9, '0');
				//our rgb values
				int rValue      = Convert.ToInt32(rgbColor.Substring(0,3));
				int gValue      = Convert.ToInt32(rgbColor.Substring(3,3));
				int bValue      = Convert.ToInt32(rgbColor.Substring(6));
				//convert into hex
				string srValue   = ((rValue!=0)?rValue.ToString("x"):"00");
				string sgValue   = ((gValue!=0)?gValue.ToString("x"):"00");
				string sbValue   = ((bValue!=0)?bValue.ToString("x"):"00");
				//build hex color
				string hexColor   = "#"
					//fill with leading zeros, if needed
					+ srValue.PadLeft(2, '0')
					+ sgValue.PadLeft(2, '0')
					+ sbValue.PadLeft(2, '0');
      
				return hexColor;
			}
			catch(Exception)
			{
				return "#000000"; // black as default,
			}
		} 

		/// <summary>
		/// Gets the color from hex.
		/// </summary>
		/// <param name="rgbColor">Color of the RGB.</param>
		/// <returns></returns>
		public static iTextSharp.text.Color GetColorFromHex(string rgbColor)
		{
			iTextSharp.text.Color color = new iTextSharp.text.Color(0,0,0);
			try
			{
				if(rgbColor != null && rgbColor.StartsWith("#"))
					color = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml(rgbColor));
			}
			catch(Exception)
			{}
			return color; // default black
		}
	}
}
