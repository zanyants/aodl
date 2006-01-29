/*
 * $Id: AARunMeFirstAndOnce.cs,v 1.1 2006/01/29 11:26:02 larsbm Exp $
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
using System.IO;

namespace AODLTest
{
	[TestFixture]
	public class AARunMeFirstAndOnce
	{
		public static string outPutFolder		= Environment.CurrentDirectory+"\\GeneratedFiles\\";

		[Test]
		public void AARunMeFirstAndOnceDir()
		{
			if(Directory.Exists(outPutFolder))
				Directory.Delete(outPutFolder, true);
			Directory.CreateDirectory(outPutFolder);
		}
	}
}


/*
 * $Log: AARunMeFirstAndOnce.cs,v $
 * Revision 1.1  2006/01/29 11:26:02  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */