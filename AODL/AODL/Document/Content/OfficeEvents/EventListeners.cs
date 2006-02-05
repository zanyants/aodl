/*
 * $Id: EventListeners.cs,v 1.3 2006/02/05 20:02:25 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, Kristy Saunders, ksaunders@eduworks.com
 * 
 * Last changes:
 * 2006-29-01 Merged into the new library version. (Lars Behrman)
 * 2006-01-02 Implementation of IClonable. (Lars Behrman)
 */

using System;
//using System.Collections.Generic;
using System.Xml;
using AODL.Document.Styles;
using AODL.Document.Content.Text;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Content;
using AODL.Document;

namespace AODL.Document.Content.OfficeEvents
{
	/// <summary>
	/// Summary for EventListeners
	/// </summary>
	/// <example>
	///		<code>
	///		<pre>
	///        &lt;office:event-listeners&gt;
	///            &lt;script:event-listener
	///                script:language="JavaScript"
	///               script:event-name="dom:onclick"
	///                xlink:href="changeImage(‘dim’)"/&gt;
	///            &lt;script:event-listener
	///                script:language="JavaScript"
	///                script:event-name="dom:onmouseover"
	///                xlink:href="setCursor('hand')"/&gt;
	///            &lt;script:event-listener
	///                script:language="JavaScript"
	///                script:event-name="dom:onmouseout"
	///                xlink:href="setCursor('arrow')"/&gt;
	///        &lt;/office:event-listeners&gt;
	///      </pre>
	///      </code>
	/// </example>
	public class EventListeners : IContent, IContentContainer, ICloneable
	{
		
		/// <summary>
		/// Initializes a new instance of the <see cref="EventListeners"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public EventListeners(IDocument document, XmlNode node)
		{
			this.Document			= document;
			this.InitStandards();
			this.Node				= node;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EventListeners"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="EventListeners">Array of EventListeners.</param>
		public EventListeners(IDocument document, EventListener[] EventListeners)
		{
			this.Document			= document;
			this.InitStandards();
			this.NewXmlNode();

			if (EventListeners != null)
			{
				foreach (EventListener d in EventListeners)
				{
					_content.Add(d);
				}
			}
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards()
		{
			this.Content				= new IContentCollection();
			this.Content.Inserted		+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(Content_Inserted);
			this.Content.Removed		+=new AODL.Document.Collections.CollectionWithEvents.CollectionChange(Content_Removed);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		private void NewXmlNode()
		{			
			this.Node		= this.Document.CreateNode("event-listeners", "office");
		}

		/// <summary>
		/// Content_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Inserted(int index, object value)
		{
			if(this.Node != null)
				this.Node.AppendChild(((IContent)value).Node);
		}

		/// <summary>
		/// Content_s the removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IContent)value).Node);
		}

		#region IContentCollection Member

		private IContentCollection _content;
		/// <summary>
		/// Gets or sets the content collection.
		/// </summary>
		/// <value>The content collection.</value>
		public IContentCollection Content
		{
			get { return this._content; }
			set { this._content = value; }
		}

		#endregion 

		#region IContent Member		

		private IDocument _document;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his document.
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
		/// A office:event-listeners doesn't have a style-name.
		/// </summary>
		/// <value>The name</value>
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

		/// <summary>
		/// A office:event-listeners doesn't have a style.
		/// </summary>
		public IStyle Style
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

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

		#endregion

		#region ICloneable Member

		/// <summary>
		/// Create a deep clone of this EventListeners object.
		/// </summary>
		/// <remarks>A possible Attached Style wouldn't be cloned!</remarks>
		/// <returns>
		/// A clone of this object.
		/// </returns>
		public object Clone()
		{
			EventListeners eventListenersClone	= null;

			if(this.Document != null && this.Node != null)
			{
				MainContentProcessor mcp		= new MainContentProcessor(this.Document);
				eventListenersClone				= mcp.CreateEventListeners(this.Node.CloneNode(true));
			}

			return eventListenersClone;
		}

		#endregion
	}
}
