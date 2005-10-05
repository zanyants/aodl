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