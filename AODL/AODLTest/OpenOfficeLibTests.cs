/*
 * $Id: OpenOfficeLibTests.cs,v 1.1 2006/02/06 19:27:22 larsbm Exp $
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
using NUnit.Framework;
using System.Xml;
using AODL.Document.Content.Tables;
using AODL.Document.TextDocuments;
using AODL.Document.Styles;
using AODL.Document.Content.Text;
using OpenOfficeLib.Connection;
using OpenOfficeLib.Document;
using OpenOfficeLib.Printer;
using unoidl.com.sun.star.lang;
using unoidl.com.sun.star.uno;
using unoidl.com.sun.star.bridge;
using unoidl.com.sun.star.frame;

namespace AODLTest
{
	/// <summary>
	/// OpenOfficeLibTests
	/// </summary>
	[TestFixture]
	public class OpenOfficeLibTests
	{
		/// <summary>
		/// Creates the new document and print it out.
		/// </summary>
		/// <remarks>This test is flagged with the explicit
		/// attribute, it won't run automaticaly.</remarks>
		[Test (Description="Simple print test. OpenOffice installation and printer must exist."), Explicit]
		public void CreateNewDocumentAndDoAPrintOut()
		{
			string fileToPrint						= AARunMeFirstAndOnce.outPutFolder+"fileToPrint.odt";

			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a standard paragraph using the ParagraphBuilder
			Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some simple text
			paragraph.TextContent.Add(new SimpleText(document, "Some simple text!"));
			//Add the paragraph to the document
			document.Content.Add(paragraph);
			//Save empty
			document.SaveTo(fileToPrint);

			//Now print the new document via the OpenOfficeLib
			//Get the Component Context
			XComponentContext xComponentContext			= Connector.GetComponentContext();
			//Get a MultiServiceFactory
			XMultiServiceFactory xMultiServiceFactory	= Connector.GetMultiServiceFactory(xComponentContext);
			//Get a Dektop instance		
			XDesktop xDesktop							= Connector.GetDesktop(xMultiServiceFactory);
			//Convert a windows path to an OpenOffice one
			fileToPrint						= Component.PathConverter(fileToPrint);
			//Load the document you want to print
			XComponent xComponent						= Component.LoadDocument(
				(XComponentLoader)xDesktop, fileToPrint, "_blank");
			//Print the XComponent
			Printer.Print(xComponent);
		}
	}
}

/*
 * $Log: OpenOfficeLibTests.cs,v $
 * Revision 1.1  2006/02/06 19:27:22  larsbm
 * - fixed bug in spreadsheet document
 * - added smal OpenOfficeLib for document printing
 *
 */