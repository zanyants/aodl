using System;
using AODL.ExternalExporter.PDF.Document.Helper;
using AODL.Document.Content.Draw;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;

namespace AODL.ExternalExporter.PDF.Document.ContentConverter
{
	/// <summary>
	/// Summary for ImageConverter.
	/// </summary>
	public class ImageConverter
	{
		public ImageConverter()
		{
		}

		/// <summary>
		/// Converts the specified graphic.
		/// </summary>
		/// <param name="graphic">The graphic.</param>
		/// <returns></returns>
		public iTextSharp.text.Image Convert(Graphic graphic)
		{
			try
			{
				iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(graphic.GraphicRealPath);
				img.SetDpi(graphic.Frame.DPI_X, graphic.Frame.DPI_X);
				img = this.ScaleIfNessarry(img, graphic.Frame);	
				img = this.SetImageProperties(img, graphic.Frame);
				//img.Alignment = iTextSharp.text.Image.TEXTWRAP;
//				img.ScalePercent(50.0f);
//				img.RotationDegrees = -45.0f;
				//img.ScaleAbsolute(100, 100);
				return img;
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Scales the pdf image if nessarry by percent.
		/// </summary>
		/// <param name="img">The img.</param>
		/// <param name="frame">The frame.</param>
		/// <returns>The scaled image.</returns>
		private iTextSharp.text.Image ScaleIfNessarry(iTextSharp.text.Image img, Frame frame)
		{
			try
			{
				double scalingPrescision = 0.25;
				double scaledWidthPercent = 0;
				double scaledHeightPercent = 0;
				double odfScaledWidth = AODL.Document.Helper.SizeConverter.GetDoubleFromAnOfficeSizeValue(frame.SvgWidth);
				double odfScaledHeight = AODL.Document.Helper.SizeConverter.GetDoubleFromAnOfficeSizeValue(frame.SvgHeight);
				
				if((frame.Height - odfScaledHeight) > scalingPrescision 
					|| (frame.Height - odfScaledHeight) < scalingPrescision)
				{
					scaledHeightPercent = ((100.0/frame.Height) * odfScaledHeight);
					Console.WriteLine("ScaledHeightPerc {0} , frame {1}, odfScaledHeight {2}", scaledHeightPercent, frame.Height, odfScaledHeight);
				}

				if((frame.Width - odfScaledWidth) > scalingPrescision
					|| (frame.Width - odfScaledWidth) < scalingPrescision)
				{
					scaledWidthPercent = ((100.0/frame.Width) * odfScaledWidth);
				}
				
				if(scaledHeightPercent != 0 || scaledWidthPercent != 0)
				{
					img.ScalePercent((float) scaledWidthPercent, (float) scaledHeightPercent);
				}

				return img;
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Sets the image properties.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <param name="graphic">The graphic.</param>
		/// <returns>The pdf image with the converted odf graphic properties.</returns>
		private iTextSharp.text.Image SetImageProperties(iTextSharp.text.Image image, Frame frame)
		{
			try
			{
				if(frame.Style is FrameStyle)
				{
					if(((FrameStyle)frame.Style).GraphicProperties != null)
					{
						if(((FrameStyle)frame.Style).GraphicProperties.HorizontalPosition != null)
						{
							string pos = ((FrameStyle)frame.Style).GraphicProperties.HorizontalPosition;
							switch(pos)
							{
								case "center":
									image.Alignment = iTextSharp.text.Image.TEXTWRAP | iTextSharp.text.Image.ALIGN_CENTER;
									break;
								case "left":
									image.Alignment = iTextSharp.text.Image.TEXTWRAP | iTextSharp.text.Image.ALIGN_LEFT;
									break;
								case "right":
									image.Alignment = iTextSharp.text.Image.TEXTWRAP | iTextSharp.text.Image.ALIGN_RIGHT;
									break;
								default:
									break;
							}
						}

						if(((FrameStyle)frame.Style).GraphicProperties.MarginLeft != null)
						{
							string marginLeft = ((FrameStyle)frame.Style).GraphicProperties.MarginLeft;
							if(marginLeft != null)
							{
								double mLeft = AODL.Document.Helper.SizeConverter.GetDoubleFromAnOfficeSizeValue(marginLeft);
								if(AODL.Document.Helper.SizeConverter.IsCm(marginLeft))
								{
									int pLeft = MeasurementHelper.CmToPoints(mLeft);
									image.IndentationLeft = (float) pLeft;
								}
								else
								{
									int pLeft = MeasurementHelper.CmToPoints(mLeft);
									image.IndentationLeft = (float) pLeft;
								}
							}
						}

						if(((FrameStyle)frame.Style).GraphicProperties.MarginRight != null)
						{
							string marginRight = ((FrameStyle)frame.Style).GraphicProperties.MarginRight;
							if(marginRight != null)
							{
								double mRight = AODL.Document.Helper.SizeConverter.GetDoubleFromAnOfficeSizeValue(marginRight);
								if(AODL.Document.Helper.SizeConverter.IsCm(marginRight))
								{
									int pRight = MeasurementHelper.CmToPoints(mRight);
									image.IndentationRight = (float) pRight;
								}
								else
								{
									int pRight = MeasurementHelper.InchToPoints(mRight);
									image.IndentationRight = (float) pRight;
								}
							}
						}
					}
				}
				return image;
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
