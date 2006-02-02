/*
 * $Id: SimpleText.cs,v 1.2 2006/02/02 21:55:59 larsbm Exp $
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
using AODL.Document.Import.OpenDocument.NodeProcessors;

namespace AODL.Document.Content.Text
{
	/// <summary>
	/// The class SimpleText represent simple unformatted text
	/// that could be used within the spreadsheet cell content.
	/// </summary>
	public class SimpleText : IText, ICloneable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleText"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="simpleText">The simple text.</param>
		public SimpleText(IDocument document, string simpleText)
		{
			this.Document			= document;
			this.NewNode(simpleText);	
		}

		/// <summary>
		/// News the node.
		/// </summary>
		/// <param name="simpleText">The simple text.</param>
		private void NewNode(string simpleText)
		{
			this.Node			= this.Document.XmlDoc.CreateTextNode(simpleText);
		}

		#region IText Member

		private XmlNode _node;
		/// <summary>
		/// The node that represent the text content.
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

		/// <summary>
		/// The text.
		/// </summary>
		/// <value></value>
		public string Text
		{
			get
			{
				return this.Node.InnerText;
			}
			set
			{
				this.Node.InnerText	= value;
			}
		}

		private IDocument _document;
		/// <summary>
		/// The document to which this text content belongs to.
		/// </summary>
		/// <value></value>
		public IDocument Document
		{
			get
			{
				return this._document;
			}
			set
			{
				this._document = value;
			}
		}

		/// <summary>
		/// Is null no style is available.
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get { return null; }
			set { }
		}

		/// <summary>
		/// No style name available
		/// </summary>
		/// <value></value>
		public string StyleName
		{
			get 
			{ 
				return null;
			}
			set
			{
			}
		}

		#endregion

		#region ICloneable Member
		/// <summary>
		/// Create a deep clone of this SimpleText object.
		/// </summary>
		/// <remarks>A possible Attached Style wouldn't be cloned!</remarks>
		/// <returns>
		/// A clone of this object.
		/// </returns>
		public object Clone()
		{
			SimpleText simpleTextClone		= null;

			if(this.Document != null && this.Node != null)
			{
				TextContentProcessor tcp	= new TextContentProcessor();
				simpleTextClone				= (SimpleText)tcp.CreateTextObject(
					this.Document, this.Node.CloneNode(true));
			}

			return simpleTextClone;
		}

		#endregion
	}
}

/*
 * $Log: SimpleText.cs,v $
 * Revision 1.2  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */