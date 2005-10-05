using System;
using AODL.TextDocument.Style;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Represent a simple non formated Displaytext.
	/// </summary>
	public class SimpleText : IText
	{
		//Empty defaultconstructor.
		public SimpleText()
		{
		}

		/// <summary>
		/// Overloaded constructor.
		/// </summary>
		/// <param name="content">The IContent object the SimpleText object belongs to.</param>
		/// <param name="text">The Displaytext.</param>
		public SimpleText(IContent content,string text)
		{
			this.Text		= text;
			this.Content	= content;
		}

		#region IText Member

		/// <summary>
		/// This property isn't needed it's only simpletext.
		/// </summary>
		/// <remarks>
		/// Not implemented. Throws a NotSupportedException.
		/// </remarks>
		public System.Xml.XmlNode Node
		{
			get
			{
				// TODO:  Getter-Implementierung für SimpleText.Node hinzufügen
				return null;
			}
			set
			{
				throw new NotSupportedException("SimpleText doesn't support the Property Node!");
			}
		}

		private string _text;
		/// <summary>
		/// The Displaytext.
		/// </summary>
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value.Replace(@"\n", "<text:line-break xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");
			}
		}
		
		/// <summary>
		/// Only the displaytext.
		/// </summary>
		public string Xml
		{
			get
			{
				return this._text;
			}			
		}


		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <remarks>Set throws NotSupportedException.</remarks>
		public IStyle Style
		{
			get
			{
				// TODO:  Getter-Implementierung für SimpleText.Style hinzufügen
				return null;
			}
			set
			{
				throw new NotSupportedException("SimpleText doesn't support the Property IStyle!");
			}
		}

		private IContent _content;
		/// <summary>
		/// The IContent object the SimpleText object belongs to.
		/// </summary>
		public IContent Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		#endregion
	}
}
