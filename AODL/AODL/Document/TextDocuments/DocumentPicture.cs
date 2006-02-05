/*
 * $Id: DocumentPicture.cs,v 1.2 2006/02/05 20:03:32 larsbm Exp $
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
using System.IO;
using System.Drawing;

namespace AODL.Document.TextDocuments
{
	/// <summary>
	/// DocumentPicture represent a picture resp. graphic which used within
	/// a file in the OpenDocument format.
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

		private string _imagePath;
		/// <summary>
		/// Gets or sets the path of the image.
		/// </summary>
		/// <value>The path of the image.</value>
		public string ImagePath
		{
			get { return this._imagePath; }
			set { this._imagePath = value; }
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
//				if(!File.Exists(file))
//					throw new Exception("The imagefile "+file+" doesn't exist!");
//				this.Image		= Image.FromFile(file);
				FileInfo fi		= new FileInfo(file);
				this.ImageName	= fi.Name;
				this.ImagePath	= fi.FullName;
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}
