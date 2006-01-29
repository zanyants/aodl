/*
 * $Id: UnknownProperty.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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
using AODL.Document;
using AODL.Document.Styles;

namespace AODL.Document.Styles.Properties
{
	/// <summary>
	/// UnknownProperty represent an unknown element.
	/// </summary>
	public class UnknownProperty : IProperty
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UnknownProperty"/> class.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="node">The node.</param>
		public UnknownProperty(IStyle style, XmlNode node)
		{
			this.Style				= style;
			this.Node				= node;
		}

		#region IProperty Member

		private XmlNode _node;
		/// <summary>
		/// The XmlNode which represent the property element.
		/// </summary>
		/// <value></value>
		public XmlNode Node
		{
			get
			{
				return this._node;
			}
			set
			{
				this._node = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// The style object to which this property object belongs
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this._style = value;
			}
		}

		#endregion
	}
}

/*
 * $Log: UnknownProperty.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */