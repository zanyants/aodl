/*
 * $Id: MetaData.cs,v 1.3 2006/01/29 19:30:24 larsbm Exp $
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
using AODL.Document.Import;
using AODL.Document.TextDocuments;
using AODL.Document.Exceptions;
using AODL.Document.Content;
using AODL.Document.Styles;

namespace AODLTest
{
	[TestFixture]
	public class MetaData
	{
		[Test]
		public void MetaDataDisplay()
		{
			TextDocument document		= null;
			try
			{
				document				= new TextDocument();
				document.Load(AARunMeFirstAndOnce.inPutFolder+"ProgrammaticControlOfMenuAndToolbarItems.odt");
			}
			catch(Exception ex)
			{
				if(ex is AODLException)
				{
					if(((AODLException)ex).OriginalException != null)
						Console.WriteLine("Org ex: {0}", ((AODLException)ex).OriginalException.Message+"\r\n"
							+((AODLException)ex).OriginalException.StackTrace);
					if(((AODLException)ex).Node != null)
						Console.WriteLine("Node: {0}", ((AODLException)ex).Node.OuterXml);
				}
				else throw ex;
			}

			Console.WriteLine(document.DocumentMetadata.InitialCreator);
			Console.WriteLine(document.DocumentMetadata.LastModified);
			Console.WriteLine(document.DocumentMetadata.CreationDate);
			Console.WriteLine(document.DocumentMetadata.CharacterCount);
			Console.WriteLine(document.DocumentMetadata.ImageCount);
			Console.WriteLine(document.DocumentMetadata.Keywords);
			Console.WriteLine(document.DocumentMetadata.Language);
			Console.WriteLine(document.DocumentMetadata.ObjectCount);
			Console.WriteLine(document.DocumentMetadata.PageCount);
			Console.WriteLine(document.DocumentMetadata.ParagraphCount);
			Console.WriteLine(document.DocumentMetadata.Subject);
			Console.WriteLine(document.DocumentMetadata.TableCount);
			Console.WriteLine(document.DocumentMetadata.Title);
			Console.WriteLine(document.DocumentMetadata.WordCount);

			document.DocumentMetadata.SetUserDefinedInfo(UserDefinedInfo.Info1, "Nothing");
			Console.WriteLine(document.DocumentMetadata.GetUserDefinedInfo(UserDefinedInfo.Info1));
		}

		public void DisplyMetaData(TextDocument document)
		{
			Console.WriteLine(document.DocumentMetadata.InitialCreator);
			Console.WriteLine(document.DocumentMetadata.LastModified);
			Console.WriteLine(document.DocumentMetadata.CreationDate);
			Console.WriteLine(document.DocumentMetadata.CharacterCount);
			Console.WriteLine(document.DocumentMetadata.ImageCount);
			Console.WriteLine(document.DocumentMetadata.Keywords);
			Console.WriteLine(document.DocumentMetadata.Language);
			Console.WriteLine(document.DocumentMetadata.ObjectCount);
			Console.WriteLine(document.DocumentMetadata.PageCount);
			Console.WriteLine(document.DocumentMetadata.ParagraphCount);
			Console.WriteLine(document.DocumentMetadata.Subject);
			Console.WriteLine(document.DocumentMetadata.TableCount);
			Console.WriteLine(document.DocumentMetadata.Title);
			Console.WriteLine(document.DocumentMetadata.WordCount);
		}
	}
}

/*
 * $Log: MetaData.cs,v $
 * Revision 1.3  2006/01/29 19:30:24  larsbm
 * - Added app config support for NUnit tests
 *
 * Revision 1.2  2006/01/29 11:26:02  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */