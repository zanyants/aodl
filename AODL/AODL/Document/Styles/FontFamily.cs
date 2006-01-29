/*
 * $Id: FontFamily.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
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

namespace AODL.Document.Styles
{	
	/// <summary>
	/// Class reprsent all 102 available fonts in OpenOffice.
	/// </summary>
	public class FontFamilies
	{
		/// <summary>
		/// Pts to px.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns>The font size in pixel</returns>
		internal static string PtToPx(string point)
		{
			try
			{
				int size	= Convert.ToInt32(point.Replace("pt",""));
				if(size < 12)
					size++;
				return size.ToString()+"pt";
			}
			catch(Exception ex)
			{
				//unhandled
			}
			return point;
		}

		/// <summary>
		/// Replace some numberings of OO fonts 
		/// </summary>
		/// <param name="ooFont">The oo font.</param>
		/// <returns>The html font</returns>
		internal static string HtmlFont(string ooFont)
		{
			try
			{
				string[] number		= new string[] {"0","1","2","3"};
				foreach(string n in number)
					ooFont			= ooFont.Replace(n, "");
			}
			catch(Exception ex)
			{
				//unhandled
			}
			return ooFont;
		}

		///<summary>
		///Marlett
		///</summary>
		public static readonly string Marlett = "Marlett";
		///<summary>
		///OpenSymbol
		///</summary>
		public static readonly string OpenSymbol = "OpenSymbol";
		///<summary>
		///VisualUI
		///</summary>
		public static readonly string VisualUI = "VisualUI";
		///<summary>
		///Wingdings
		///</summary>
		public static readonly string Wingdings = "Wingdings";
		///<summary>
		///MT Extra
		///</summary>
		public static readonly string MTExtra = "MT Extra";
		///<summary>
		///Symbol
		///</summary>
		public static readonly string Symbol = "Symbol";
		///<summary>
		///Webdings
		///</summary>
		public static readonly string Webdings = "Webdings";
		///<summary>
		///Wingdings 2
		///</summary>
		public static readonly string Wingdings2 = "Wingdings 2";
		///<summary>
		///Wingdings 3
		///</summary>
		public static readonly string Wingdings3 = "Wingdings 3";
		///<summary>
		///HolidayPi BT
		///</summary>
		public static readonly string HolidayPiBT = "HolidayPi BT";
		///<summary>
		///Tahoma2
		///</summary>
		public static readonly string Tahoma2 = "Tahoma2";
		///<summary>
		///Bitstream Vera Sans Mono
		///</summary>
		public static readonly string BitstreamVeraSansMono = "Bitstream Vera Sans Mono";
		///<summary>
		///Courier New
		///</summary>
		public static readonly string CourierNew = "Courier New";
		///<summary>
		///Lucida Console
		///</summary>
		public static readonly string LucidaConsole = "Lucida Console";
		///<summary>
		///Academy Engraved LET
		///</summary>
		public static readonly string AcademyEngravedLET = "Academy Engraved LET";
		///<summary>
		///Gautami
		///</summary>
		public static readonly string Gautami = "Gautami";
		///<summary>
		///Highlight LET
		///</summary>
		public static readonly string HighlightLET = "Highlight LET";
		///<summary>
		///John Handy LET
		///</summary>
		public static readonly string JohnHandyLET = "John Handy LET";
		///<summary>
		///Jokerman Alts LET
		///</summary>
		public static readonly string JokermanAltsLET = "Jokerman Alts LET";
		///<summary>
		///La Bamba LET
		///</summary>
		public static readonly string LaBambaLET = "La Bamba LET";
		///<summary>
		///Latha
		///</summary>
		public static readonly string Latha = "Latha";
		///<summary>
		///Lucida Sans Unicode1
		///</summary>
		public static readonly string LucidaSansUnicode1 = "Lucida Sans Unicode1";
		///<summary>
		///MV Boli
		///</summary>
		public static readonly string MVBoli = "MV Boli";
		///<summary>
		///Mangal
		///</summary>
		public static readonly string Mangal = "Mangal";
		///<summary>
		///Mekanik LET
		///</summary>
		public static readonly string MekanikLET = "Mekanik LET";
		///<summary>
		///Milano LET
		///</summary>
		public static readonly string MilanoLET = "Milano LET";
		///<summary>
		///Odessa LET
		///</summary>
		public static readonly string OdessaLET = "Odessa LET";
		///<summary>
		///One Stroke Script LET
		///</summary>
		public static readonly string OneStrokeScriptLET = "One Stroke Script LET";
		///<summary>
		///Orange LET
		///</summary>
		public static readonly string OrangeLET = "Orange LET";
		///<summary>
		///Pump Demi Bold LET
		///</summary>
		public static readonly string PumpDemiBoldLET = "Pump Demi Bold LET";
		///<summary>
		///Quixley LET
		///</summary>
		public static readonly string QuixleyLET = "Quixley LET";
		///<summary>
		///Raavi
		///</summary>
		public static readonly string Raavi = "Raavi";
		///<summary>
		///Rage Italic LET
		///</summary>
		public static readonly string RageItalicLET = "Rage Italic LET";
		///<summary>
		///Ruach LET
		///</summary>
		public static readonly string RuachLET = "Ruach LET";
		///<summary>
		///Scruff LET
		///</summary>
		public static readonly string ScruffLET = "Scruff LET";
		///<summary>
		///Shruti
		///</summary>
		public static readonly string Shruti = "Shruti";
		///<summary>
		///Smudger Alts LET
		///</summary>
		public static readonly string SmudgerAltsLET = "Smudger Alts LET";
		///<summary>
		///Tahoma1
		///</summary>
		public static readonly string Tahoma1 = "Tahoma1";
		///<summary>
		///Tiranti Solid LET
		///</summary>
		public static readonly string TirantiSolidLET = "Tiranti Solid LET";
		///<summary>
		///Tunga
		///</summary>
		public static readonly string Tunga = "Tunga";
		///<summary>
		///University Roman Alts LET
		///</summary>
		public static readonly string UniversityRomanAltsLET = "University Roman Alts LET";
		///<summary>
		///Victorian LET
		///</summary>
		public static readonly string VictorianLET = "Victorian LET";
		///<summary>
		///Westwood LET
		///</summary>
		public static readonly string WestwoodLET = "Westwood LET";
		///<summary>
		///Blackadder ITC
		///</summary>
		public static readonly string BlackadderITC = "Blackadder ITC";
		///<summary>
		///Broadway BT
		///</summary>
		public static readonly string BroadwayBT = "Broadway BT";
		///<summary>
		///Curlz MT
		///</summary>
		public static readonly string CurlzMT = "Curlz MT";
		///<summary>
		///Felix Titling
		///</summary>
		public static readonly string FelixTitling = "Felix Titling";
		///<summary>
		///Matisse ITC
		///</summary>
		public static readonly string MatisseITC = "Matisse ITC";
		///<summary>
		///OldDreadfulNo7 BT
		///</summary>
		public static readonly string OldDreadfulNo7BT = "OldDreadfulNo7 BT";
		///<summary>
		///Tempus Sans ITC
		///</summary>
		public static readonly string TempusSansITC = "Tempus Sans ITC";
		///<summary>
		///Modern
		///</summary>
		public static readonly string Modern = "Modern";
		///<summary>
		///Bitstream Vera Serif
		///</summary>
		public static readonly string BitstreamVeraSerif = "Bitstream Vera Serif";
		///<summary>
		///Book Antiqua
		///</summary>
		public static readonly string BookAntiqua = "Book Antiqua";
		///<summary>
		///Bookman Old Style
		///</summary>
		public static readonly string BookmanOldStyle = "Bookman Old Style";
		///<summary>
		///Engravers MT
		///</summary>
		public static readonly string EngraversMT = "Engravers MT";
		///<summary>
		///Garamond
		///</summary>
		public static readonly string Garamond = "Garamond";
		///<summary>
		///Georgia
		///</summary>
		public static readonly string Georgia = "Georgia";
		///<summary>
		///Palatino Linotype
		///</summary>
		public static readonly string PalatinoLinotype = "Palatino Linotype";
		///<summary>
		///Perpetua
		///</summary>
		public static readonly string Perpetua = "Perpetua";
		///<summary>
		///Rockwell Extra Bold
		///</summary>
		public static readonly string RockwellExtraBold = "Rockwell Extra Bold";
		///<summary>
		///Roman
		///</summary>
		public static readonly string Roman = "Roman";
		///<summary>
		///Sylfaen
		///</summary>
		public static readonly string Sylfaen = "Sylfaen";
		///<summary>
		///Times New Roman
		///</summary>
		public static readonly string TimesNewRoman = "Times New Roman";
		///<summary>
		///Blackletter686 BT
		///</summary>
		public static readonly string Blackletter686BT = "Blackletter686 BT";
		///<summary>
		///Bradley Hand ITC
		///</summary>
		public static readonly string BradleyHandITC = "Bradley Hand ITC";
		///<summary>
		///Calligraph421 BT
		///</summary>
		public static readonly string Calligraph421BT = "Calligraph421 BT";
		///<summary>
		///Cataneo BT
		///</summary>
		public static readonly string CataneoBT = "Cataneo BT";
		///<summary>
		///Comic Sans MS
		///</summary>
		public static readonly string ComicSansMS = "Comic Sans MS";
		///<summary>
		///Edwardian Script ITC
		///</summary>
		public static readonly string EdwardianScriptITC = "Edwardian Script ITC";
		///<summary>
		///Estrangelo Edessa
		///</summary>
		public static readonly string EstrangeloEdessa = "Estrangelo Edessa";
		///<summary>
		///French Script MT
		///</summary>
		public static readonly string FrenchScriptMT = "French Script MT";
		///<summary>
		///Kristen ITC
		///</summary>
		public static readonly string KristenITC = "Kristen ITC";
		///<summary>
		///MisterEarl BT
		///</summary>
		public static readonly string MisterEarlBT = "MisterEarl BT";
		///<summary>
		///Mistral
		///</summary>
		public static readonly string Mistral = "Mistral";
		///<summary>
		///Papyrus
		///</summary>
		public static readonly string Papyrus = "Papyrus";
		///<summary>
		///ParkAvenue BT
		///</summary>
		public static readonly string ParkAvenueBT = "ParkAvenue BT";
		///<summary>
		///Script
		///</summary>
		public static readonly string Script = "Script";
		///<summary>
		///Staccato222 BT
		///</summary>
		public static readonly string Staccato222BT = "Staccato222 BT";
		///<summary>
		///Vivaldi
		///</summary>
		public static readonly string Vivaldi = "Vivaldi";
		///<summary>
		///Arial
		///</summary>
		public static readonly string Arial = "Arial";
		///<summary>
		///Arial Black
		///</summary>
		public static readonly string ArialBlack = "Arial Black";
		///<summary>
		///Arial Narrow
		///</summary>
		public static readonly string ArialNarrow = "Arial Narrow";
		///<summary>
		///Bitstream Vera Sans
		///</summary>
		public static readonly string BitstreamVeraSans = "Bitstream Vera Sans";
		///<summary>
		///Century Gothic
		///</summary>
		public static readonly string CenturyGothic = "Century Gothic";
		///<summary>
		///Copperplate Gothic Bold
		///</summary>
		public static readonly string CopperplateGothicBold = "Copperplate Gothic Bold";
		///<summary>
		///Copperplate Gothic Light
		///</summary>
		public static readonly string CopperplateGothicLight = "Copperplate Gothic Light";
		///<summary>
		///Eras Demi ITC
		///</summary>
		public static readonly string ErasDemiITC = "Eras Demi ITC";
		///<summary>
		///Eras Light ITC
		///</summary>
		public static readonly string ErasLightITC = "Eras Light ITC";
		///<summary>
		///Eurostile
		///</summary>
		public static readonly string Eurostile = "Eurostile";
		///<summary>
		///Franklin Gothic Book
		///</summary>
		public static readonly string FranklinGothicBook = "Franklin Gothic Book";
		///<summary>
		///Franklin Gothic Demi
		///</summary>
		public static readonly string FranklinGothicDemi = "Franklin Gothic Demi";
		///<summary>
		///Franklin Gothic Medium
		///</summary>
		public static readonly string FranklinGothicMedium = "Franklin Gothic Medium";
		///<summary>
		///Franklin Gothic Medium Cond
		///</summary>
		public static readonly string FranklinGothicMediumCond = "Franklin Gothic Medium Cond";
		///<summary>
		///Impact
		///</summary>
		public static readonly string Impact = "Impact";
		///<summary>
		///Lucida Sans
		///</summary>
		public static readonly string LucidaSans = "Lucida Sans";
		///<summary>
		///Lucida Sans Unicode
		///</summary>
		public static readonly string LucidaSansUnicode = "Lucida Sans Unicode";
		///<summary>
		///Maiandra GD
		///</summary>
		public static readonly string MaiandraGD = "Maiandra GD";
		///<summary>
		///Microsoft Sans Serif
		///</summary>
		public static readonly string MicrosoftSansSerif = "Microsoft Sans Serif";
		///<summary>
		///Square721 BT
		///</summary>
		public static readonly string Square721BT = "Square721 BT";
		///<summary>
		///Tahoma
		///</summary>
		public static readonly string Tahoma = "Tahoma";
		///<summary>
		///Trebuchet MS
		///</summary>
		public static readonly string TrebuchetMS = "Trebuchet MS";
		///<summary>
		///Verdana
		///</summary>
		public static readonly string Verdana = "Verdana";
	}
}

/*
 * $Log: FontFamily.cs,v $
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.4  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.3  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.2  2005/10/08 07:50:15  larsbm
 * - added cvs tags
 *
 */