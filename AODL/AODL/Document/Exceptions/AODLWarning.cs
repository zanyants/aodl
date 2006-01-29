/*
 * $Id: AODLWarning.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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

namespace AODL.Document.Exceptions
{
	/// <summary>
	/// You can use an AODLWarning instead of an AODLException
	/// if the whole result isn't really in danger.
	/// </summary>
	public class AODLWarning : AODLException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AODLWarning"/> class.
		/// </summary>
		public AODLWarning() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AODLWarning"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public AODLWarning(string message) : base(message)
		{
		}
	}
}

/*
 * $Log: AODLWarning.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */