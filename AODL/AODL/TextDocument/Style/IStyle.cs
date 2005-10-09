/*
 * $Id: IStyle.cs,v 1.3 2005/10/09 15:52:47 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument.Style.Properties;

namespace AODL.TextDocument.Style
{
	/// <summary>
	/// All classes that act as a style class within the library
	/// must implement this base interface.
	/// </summary>
	public interface IStyle
	{
		/// <summary>
		/// The XmlNode.
		/// </summary>
		XmlNode Node {get; set;}
		/// <summary>
		/// The style name.
		/// </summary>
		string Name {get; set;}
		/// <summary>
		/// The TextDocument to this object belongs.
		/// </summary>
		TextDocument Document {get; set;}
	}
}

/*
 * $Log: IStyle.cs,v $
 * Revision 1.3  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 * Revision 1.2  2005/10/08 07:50:15  larsbm
 * - added cvs tags
 *
 */