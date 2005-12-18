using System;
using NUnit.Framework;
using AODL.Import;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style;

namespace AODLTest
{
	[TestFixture]
	public class MetaData
	{
		[Test]
		public void MetaDataDisplay()
		{
			TextDocument document		= new TextDocument();
			document.Load("ProgrammaticControlOfMenuAndToolbarItems.odt");

			Console.WriteLine(document.DocumentMetadata.InitialCreator);
			Console.WriteLine(document.DocumentMetadata.LastModified);
			Console.WriteLine(document.DocumentMetadata.CreationDate);
			Console.WriteLine(document.DocumentMetadata.CharacterCount);
			Console.WriteLine(document.DocumentMetadata.ImageCount);
			Console.WriteLine(document.DocumentMetadata.Keywords);
			Console.WriteLine(document.DocumentMetadata.Language);
			Console.WriteLine(document.DocumentMetadata.ObjectCount);
			Console.WriteLine(document.DocumentMetadata.PageCount);
			Console.WriteLine(document.DocumentMetadata.ParagraphCount);
			Console.WriteLine(document.DocumentMetadata.Subject);
			Console.WriteLine(document.DocumentMetadata.TableCount);
			Console.WriteLine(document.DocumentMetadata.Title);
			Console.WriteLine(document.DocumentMetadata.WordCount);

			document.DocumentMetadata.SetUserDefinedInfo(UserDefinedInfo.Info1, "Nothing");
			Console.WriteLine(document.DocumentMetadata.GetUserDefinedInfo(UserDefinedInfo.Info1));
		}

		public void DisplyMetaData(TextDocument document)
		{
			Console.WriteLine(document.DocumentMetadata.InitialCreator);
			Console.WriteLine(document.DocumentMetadata.LastModified);
			Console.WriteLine(document.DocumentMetadata.CreationDate);
			Console.WriteLine(document.DocumentMetadata.CharacterCount);
			Console.WriteLine(document.DocumentMetadata.ImageCount);
			Console.WriteLine(document.DocumentMetadata.Keywords);
			Console.WriteLine(document.DocumentMetadata.Language);
			Console.WriteLine(document.DocumentMetadata.ObjectCount);
			Console.WriteLine(document.DocumentMetadata.PageCount);
			Console.WriteLine(document.DocumentMetadata.ParagraphCount);
			Console.WriteLine(document.DocumentMetadata.Subject);
			Console.WriteLine(document.DocumentMetadata.TableCount);
			Console.WriteLine(document.DocumentMetadata.Title);
			Console.WriteLine(document.DocumentMetadata.WordCount);
		}
	}
}
