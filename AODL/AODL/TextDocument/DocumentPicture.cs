/*
 * $Id: DocumentPicture.cs,v 1.2 2005/12/12 19:39:17 larsbm Exp $
 */

using System;
using System.IO;
using System.Drawing;

namespace AODL.TextDocument
{
	/// <summary>
	/// Zusammenfassung für DocumentPicture.
	/// </summary>
	public class DocumentPicture 
	{
		private Image _image;
		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The image.</value>
		public Image Image
		{
			get { return this._image; }
			set { this._image = value; }
		}

		private string _imageName;
		/// <summary>
		/// Gets or sets the name of the image.
		/// </summary>
		/// <value>The name of the image.</value>
		public string ImageName
		{
			get { return this._imageName; }
			set { this._imageName = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentPicture"/> class.
		/// </summary>
		public DocumentPicture()
		{			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentPicture"/> class.
		/// </summary>
		/// <param name="file">The file.</param>
		public DocumentPicture(string file)
		{
			try
			{
				if(!File.Exists(file))
					throw new Exception("The imagefile "+file+" doesn't exist!");
				this.Image		= Image.FromFile(file);
				FileInfo fi		= new FileInfo(file);
				this.ImageName	= fi.Name;
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}
