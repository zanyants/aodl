/*
 * $Id: FrameBuilder.cs,v 1.1 2006/02/16 18:35:41 larsbm Exp $
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
using System.Xml;
using AODL.Document.Styles;
using AODL.Document.Content.Text;
using AODL.Document.Content.Text.Indexes;
using AODL.Document.Content;
using AODL.Document;

namespace AODL.Document.Content.Draw
{
	/// <summary>
	/// Use the FrameBuilder class to create several types of Frame implementations
	/// like illustration frame, a standard graphic frame, ...
	/// </summary>
	public class FrameBuilder
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FrameBuilder"/> class.
		/// </summary>
		public FrameBuilder()
		{
		}

		/// <summary>
		/// Builds the standard graphic frame.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="frameStyleName">Name of the frame style.</param>
		/// <param name="graphicName">Name of the graphic.</param>
		/// <param name="pathToGraphic">The path to graphic.</param>
		/// <returns>
		/// A new Frame object containing the Graphic object.
		/// </returns>
		public static Frame BuildStandardGraphicFrame(IDocument document, string frameStyleName, 
			string graphicName, string pathToGraphic)
		{
			return new Frame(document, frameStyleName, graphicName, pathToGraphic);
		}

		/// <summary>
		/// Builds the illustration frame.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="frameStyleName">Name of the frame style.</param>
		/// <param name="graphicName">Name of the graphic.</param>
		/// <param name="pathToGraphic">The path to graphic.</param>
		/// <param name="illustrationText">The illustration text.</param>
		/// <param name="illustrationNumber">The illustration number.</param>
		/// <returns>
		/// A new Frame object containing a DrawTextBox which contains the
		/// illustration Graphic object and a text sequence representing
		/// the displayed illustration text.
		/// </returns>
		public static Frame BuildIllustrationFrame(IDocument document, string frameStyleName, string graphicName, 
			string pathToGraphic, string illustrationText, int illustrationNumber)
		{
			DrawTextBox drawTextBox			= new DrawTextBox(document);
			Frame frameTextBox				= new Frame(document, frameStyleName);
			frameTextBox.DrawName			= frameStyleName+"_"+graphicName;
			frameTextBox.ZIndex				= "0"; 

			Paragraph pIllustration			= ParagraphBuilder.CreateStandardTextParagraph(document);
			pIllustration.StyleName			= "Illustration";
			Frame frame						= new Frame(document, "InnerFrame_"+frameStyleName, 
				graphicName, pathToGraphic);
			frame.ZIndex					= "1";

			pIllustration.Content.Add(frame);
			//add Illustration as text
			pIllustration.TextContent.Add(new SimpleText(document, "Illustration"));			
			//add TextSequence
			TextSequence textSequence		= new TextSequence(document);
			textSequence.Name				= "Illustration";
			textSequence.NumFormat			= "1";
			textSequence.RefName			= "refIllustration"+illustrationNumber.ToString();
			textSequence.Formula			= "ooow:Illustration+1";
			textSequence.TextContent.Add(new SimpleText(document, illustrationNumber.ToString()));
			pIllustration.TextContent.Add(textSequence);
			//add the ilustration text
			pIllustration.TextContent.Add(new SimpleText(document, illustrationText));
			//add the Paragraph to the DrawTextBox
			drawTextBox.Content.Add(pIllustration);
			
			frameTextBox.SvgWidth			= frame.SvgWidth;
			drawTextBox.MinWidth			= frame.SvgWidth;
			drawTextBox.MinHeight			= frame.SvgHeight;
			frameTextBox.Content.Add(drawTextBox);

			return frameTextBox;
		}
	}
}

/*
 * $Log: FrameBuilder.cs,v $
 * Revision 1.1  2006/02/16 18:35:41  larsbm
 * - Add FrameBuilder class
 * - TextSequence implementation (Todo loading!)
 * - Free draing postioning via x and y coordinates
 * - Graphic will give access to it's full qualified path
 *   via the GraphicRealPath property
 * - Fixed Bug with CellSpan in Spreadsheetdocuments
 * - Fixed bug graphic of loaded files won't be deleted if they
 *   are removed from the content.
 * - Break-Before property for Paragraph properties for Page Break
 *
 */