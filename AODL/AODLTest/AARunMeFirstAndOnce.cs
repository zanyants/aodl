/*
 * $Id: AARunMeFirstAndOnce.cs,v 1.2 2006/01/29 19:30:24 larsbm Exp $
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
		private static string generatedFolder	= System.Configuration.ConfigurationSettings.AppSettings["writefiles"];
		private static string readFromFolder	= System.Configuration.ConfigurationSettings.AppSettings["readfiles"];
		public static string outPutFolder		= Environment.CurrentDirectory+generatedFolder;
		public static string inPutFolder		= Environment.CurrentDirectory+readFromFolder;

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
 * Revision 1.2  2006/01/29 19:30:24  larsbm
 * - Added app config support for NUnit tests
 *
 * Revision 1.1  2006/01/29 11:26:02  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */