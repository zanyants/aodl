/*
 * $Id: Printer.cs,v 1.2 2006/02/06 20:17:07 larsbm Exp $
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
using unoidl.com.sun.star.beans;
using unoidl.com.sun.star.bridge;
using unoidl.com.sun.star.frame;

namespace OpenOfficeLib.Printer
{
	/// <summary>
	/// Simple OpenOffice Printer implementation
	/// </summary>
	public class Printer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Printer"/> class.
		/// </summary>
		/// <example>
		/// <code>
		/// //Get the Component Context
		/// XComponentContext xComponentContext			= Connector.GetComponentContext();
		//	//Get a MultiServiceFactory
		//	XMultiServiceFactory xMultiServiceFactory	= Connector.GetMultiServiceFactory(xComponentContext);
		//	//Get a Dektop instance		
		//	XDesktop xDesktop							= Connector.GetDesktop(xMultiServiceFactory);
		//  //Convert a windows path to an OpenOffice one
		//  string myFileToPrint						= Component.PathConverter(@"D:\myFileToPrint.odt");
		//	//Load the document you want to print
		//	XComponent xComponent						= Component.LoadDocument(
		//				(XComponentLoader)xDesktop, myFileToPrint, "_blank");
		//  //Print the XComponent
		//  Printer.Print(xComponent);
		/// </code>
		/// </example>
		public Printer()
		{
		}

		/// <summary>
		/// Prints the specified XComponent that could be any loaded
		/// OpenOffice document e.g text document, spreadsheet document, ..
		/// </summary>
		/// <param name="xComponent">The x component.</param>
		public static void Print(XComponent xComponent)
		{
			if(xComponent is unoidl.com.sun.star.view.XPrintable)
				((unoidl.com.sun.star.view.XPrintable)xComponent).print(
					new PropertyValue[] {}); 
			else
				throw new NotSupportedException("The given XComponent doesn't implement the XPrintable interface.");
		}
	}
}

/*
 * $Log: Printer.cs,v $
 * Revision 1.2  2006/02/06 20:17:07  larsbm
 * *** empty log message ***
 *
 * Revision 1.1  2006/02/06 19:27:23  larsbm
 * - fixed bug in spreadsheet document
 * - added smal OpenOfficeLib for document printing
 *
 */