/*
https://www.nuget.org/packages/PdfPig/
wget -nH -nd https://www.nuget.org/api/v2/package/PdfPig/0.1.2
unzip pdfpig.0.1.2.nupkg
rename lib pdfpig
*/

// c:\ProgramData\CS-Script\CSScriptNpp\1.7.24.0\Roslyn\csc.exe
// csc.exe -r:UglyToad.PdfPig.dll,UglyToad.PdfPig.Tokens.dll -debug- -target:exe -platform:x64 c#_ExtractEmbededFilesFromPdf_v1.cs
//
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using UglyToad.PdfPig;
using UglyToad.PdfPig.AcroForms;
using UglyToad.PdfPig.AcroForms.Fields;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Outline;
using UglyToad.PdfPig.Tokens;

public static class Program
{
    public static void Main(string[] args)
    {
        string fileName = "";
		try
		{
			if ( args.Length!=1 )
			{
				Console.WriteLine(@"Missing parameter...");
				return;
			}
			if ( !File.Exists(args[0]) )
			{
				Console.WriteLine(@"The '"+args[0]+"' file not exists...");
				return;
			}
			
			fileName = args[0];
			using (PdfDocument document = PdfDocument.Open(fileName))
			{
				Console.WriteLine($"Document uses version {document.Version} of the PDF specification.");
				Console.WriteLine($"Document has {document.NumberOfPages} pages.");
				//Console.WriteLine("\tCreator: "+document.Information.Creator);
				//Console.WriteLine("\tAuthor:  "+document.Information.Author);
				//Console.WriteLine("\tTitle:   "+document.Information.Title);
				Console.WriteLine("Document properties: "+document.Information.DocumentInformationDictionary);

				if (document.TryGetForm(out AcroForm form))
				{
					foreach (AcroFieldBase field in form.GetFieldsForPage(1))
					{
						Console.WriteLine("Document fields: "+field.Dictionary);
						Console.WriteLine("Document field informations: "+field.Information);
						// switch (field)
						// {
							// case AcroCheckboxField cb:
								// if (cb.IsChecked)
								// {
									// Console.WriteLine(@"Checkbox was checked: {cb.Information.MappingName}.");
								// }
								// break;
						// }
					}
				}
				else
				{
					Console.WriteLine(@"Document doesn't contains form.");
				}
				
				// if (document.TryGetXmpMetadata(out XmpMetadata metadata))
				// {
					// XDocument xmp = metadata.GetXDocument();
					// Console.WriteLine('['+xmp.Element("Author").Value+']');
					// Console.WriteLine('['+xmp.Element("Title").Value+']');
					// Console.WriteLine('['+xmp.Element("Name").Value+']');
					// Console.WriteLine('['+xmp.Element("Description").Value+']');
				// }
				// else
				// {
					   // Console.WriteLine(@"Document doesn't contains metadata... :|");
				// }
				
				// if (document.TryGetBookmarks(out Bookmarks bookmarks))
				// {
					// Console.WriteLine(@"Document contained bookmarks with {bookmarks.Roots.Count} root nodes.");
				// }
				// else
				// {
					// Console.WriteLine(@"Document doesn't contains bookmarks.");
				// }

				string fileNamePrefix = fileName.Replace(".pdf","_-_");
				if (document.Advanced.TryGetEmbeddedFiles(out IReadOnlyList<EmbeddedFile> embeddedFiles) && embeddedFiles.Count > 0)
				{
					Console.WriteLine($"Document contains {embeddedFiles.Count} embedded files.");
					
					foreach(var file in embeddedFiles)
					{
						Console.WriteLine("\t"+file);
						/* file.Name -> Leírás, file.FileSpecification -> Név */
						fileName = fileNamePrefix + file.FileSpecification;
						IReadOnlyList<byte> bytes = file.Bytes;
						using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
						{
							//writer.Write(bytes);
							foreach(var b in bytes)
								writer.Write(b);
						}

						Console.WriteLine("\t-> The file saved to: '"+fileName+"' (Description: '"+file.Name+"')");

					}
					
				}
				else
				{
					Console.WriteLine("Document has not enbedded file.");
				}
				
			}
		}
		catch ( Exception e )
		{
				Console.WriteLine(e.Message);
		}
    }
}
