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