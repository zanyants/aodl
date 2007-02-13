using System;

namespace AODL.ExternalExporter.PDF.Document.Helper
{
	/// <summary>
	/// Summary for MeasurementHelper.
	/// </summary>
	public class MeasurementHelper
	{
		/// <summary>
		/// The conversation DPI target with 96 DPI as standard.
		/// </summary>
		public static double TARGET_DPI = 96.0;
		/// <summary>
		/// The standard factor for inch to cm.
		/// </summary>
		public static double INCH_IN_CM = 2.54;
		/// <summary>
		/// DTP point in cm
		/// </summary>
		public static double DTP_POINT_CM = 0.0352;
		/// <summary>
		/// DTP point in inch
		/// </summary>
		public static double DTP_POINT_IN = 0.0139;
		/// <summary>
		/// iText points to cm factor 72points = 2.54cm
		/// </summary>
		public static double ITEXT_POINT_CM = 28.346;
		/// <summary>
		/// iText points to cm factor 72points = 1in
		/// </summary>
		public static double ITEXT_POINT_IN = 72;

		/// <summary>
		/// Initializes a new instance of the <see cref="MeasurementHelper"/> class.
		/// </summary>
		public MeasurementHelper()
		{
		}

		/// <summary>
		/// Inches to points converting.
		/// </summary>
		/// <param name="inch">The inch.</param>
		/// <returns>The points.</returns>
		public static int InchToPoints(double inch)
		{
			try
			{
				return (int)(inch * ITEXT_POINT_IN);
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Cm to points converting
		/// </summary>
		/// <param name="cm">The cm.</param>
		/// <returns>The points.</returns>
		public static int CmToPoints(double cm)
		{
			try
			{
				return (int)(cm * ITEXT_POINT_CM);
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
