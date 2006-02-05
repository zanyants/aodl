/*
 * $Id: AODLException.cs,v 1.2 2006/02/05 20:02:25 larsbm Exp $
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
using System.Xml;
using System.Collections;
using System.Diagnostics;

namespace AODL.Document.Exceptions
{
	/// <summary>
	/// AODLException is a special exception which will let you also
	/// access possible broken XmlNodes.
	/// </summary>
	public class AODLException : Exception
	{
		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node
		{
			get { return this._node; }
			set { this._node = value; }
		}

		private string _inMethod;
		/// <summary>
		/// Gets or sets the in method.
		/// </summary>
		/// <value>The in method.</value>
		public string InMethod
		{
			get { return this._inMethod; }
			set { this._inMethod = value; }
		}

		private Hashtable _calledParams;
		/// <summary>
		/// Gets or sets the called params.
		/// e.g. a param was string aString
		/// -> CalledParams.Add("string", aString as object);
		/// </summary>
		/// <value>The called params.</value>
		public Hashtable CalledParams
		{
			get { return this._calledParams; }
			set { this._calledParams = value; }
		}

		private Exception _originalException;
		/// <summary>
		/// Gets or sets the original exception.
		/// </summary>
		/// <value>The original exception.</value>
		public Exception OriginalException
		{
			get { return this._originalException; }
			set { this._originalException = value; }
		}

		private string _message;
		/// <summary>
		/// The message
		/// </summary>
		/// <value></value>
		public override string Message
		{
			get
			{
				return _message;
			}
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="AODLException"/> class.
		/// </summary>
		public AODLException()
		{
			this.CalledParams			= new Hashtable();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AODLException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public AODLException(string message)
		{
			this._message				= message;
			this.CalledParams			= new Hashtable();
		}

		/// <summary>
		/// Gets the exception source info.
		/// </summary>
		/// <returns></returns>
		public static string GetExceptionSourceInfo(StackFrame callStack)
		{
			string method			= callStack.GetMethod().Name;
			string source			= callStack.GetFileName();
			int sourceLine			= callStack.GetFileLineNumber();

			return source + ", in " + method +", Line: " + sourceLine.ToString();
		} 
	}
}

/*
 * $Log: AODLException.cs,v $
 * Revision 1.2  2006/02/05 20:02:25  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */