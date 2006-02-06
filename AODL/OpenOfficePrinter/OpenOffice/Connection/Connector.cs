/*
 * $Id: Connector.cs,v 1.1 2006/02/06 19:27:23 larsbm Exp $
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
using unoidl.com.sun.star.lang;
using unoidl.com.sun.star.uno;
using unoidl.com.sun.star.bridge;
using unoidl.com.sun.star.frame;

namespace OpenOfficeLib.Connection
{
	/// <summary>
	/// All connection relevant methods
	/// </summary>
	public class Connector
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Connector"/> class.
		/// </summary>
		public Connector()
		{
		}

		/// <summary>
		/// Get a the Component Context using default bootstrap
		/// </summary>
		/// <returns>ComponentContext object</returns>
		public static unoidl.com.sun.star.uno.XComponentContext GetComponentContext()
		{
			try
			{
				return uno.util.Bootstrap.bootstrap();
			}
			catch(System.Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Get the MultiServiceFactory
		/// </summary>
		/// <param name="componentcontext">A component context</param>
		/// <returns>MultiServiceFactory object</returns>
		public static unoidl.com.sun.star.lang.XMultiServiceFactory GetMultiServiceFactory(
			unoidl.com.sun.star.uno.XComponentContext componentcontext)
		{
			try
			{
				return (unoidl.com.sun.star.lang.XMultiServiceFactory)componentcontext.getServiceManager();
			}
			catch(System.Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Get the Desktop
		/// </summary>
		/// <param name="multiservicefactory">A multi service factory</param>
		/// <returns>Desktop object</returns>
		public static unoidl.com.sun.star.frame.XDesktop GetDesktop(
			unoidl.com.sun.star.lang.XMultiServiceFactory multiservicefactory)
		{
			try
			{
				return (unoidl.com.sun.star.frame.XDesktop)multiservicefactory.createInstance("com.sun.star.frame.Desktop");
			}
			catch(System.Exception ex)
			{
				throw;
			}
		}
	}
}

/*
 * $Log: Connector.cs,v $
 * Revision 1.1  2006/02/06 19:27:23  larsbm
 * - fixed bug in spreadsheet document
 * - added smal OpenOfficeLib for document printing
 *
 */