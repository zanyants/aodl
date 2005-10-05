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
