/*
https://www.nuget.org/packages/PdfPig/
wget -nH -nd https://www.nuget.org/api/v2/package/PdfPig/0.1.2
unzip pdfpig.0.1.2.nupkg
rename lib pdfpig
*/
// https://web.archive.org/web/20060427192430/http://partners.adobe.com/public/developer/en/pdf/PDFReference16.pdf
// c:\ProgramData\CS-Script\CSScriptNpp\1.7.24.0\Roslyn\csc.exe
// csc.exe -r:UglyToad.PdfPig.dll,UglyToad.PdfPig.Tokens.dll -debug- -target:exe -platform:x64 ExtractEmbededFilesFromPdf.cs
//
using System;
using System.IO;
using System.Collections.Generic;
//using System.Xml;
using System.Xml.Linq;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.AcroForms;
using UglyToad.PdfPig.AcroForms.Fields;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Outline;
using UglyToad.PdfPig.Tokens;

public static class Program
{
    public static void Main(string[] args)
    {
		//String fn = args[0];
		String fn = @"c:\Temp\pdfpig\35600_5904-1_2019.pdf";
        using (PdfDocument document = PdfDocument.Open(fn))
        {
            Console.WriteLine($"Document has {document.NumberOfPages} pages.");
            Console.WriteLine("DocumentInformationDictionary: "+document.Information.DocumentInformationDictionary);
			Console.WriteLine("CalogDictionary: "+document.Structure.Catalog.CatalogDictionary);
			/*foreach(KeyValuePair<string, IToken> key in document.Structure.Catalog.CatalogDictionary.Data)
			{
				Console.WriteLine("\t"+key.Key+" -> "+key.Value+" ( "+key.Value.GetType().ToString()+" )");
			}*/

			bool hasForm = document.TryGetForm(out AcroForm af);
			foreach(var key in af.Fields)
			{
				Console.WriteLine("\t"+key.Bounds);
				Console.WriteLine("\t"+key.Dictionary);
				Console.WriteLine("\t"+key.FieldFlags);
				Console.WriteLine("\t"+key.FieldType);
				Console.WriteLine("\t"+key.Information);
				Console.WriteLine("\t"+key.PageNumber);
				Console.WriteLine("\t"+key.RawFieldType);
			}
			
			
            if (document.TryGetForm(out AcroForm form))
            {
                foreach (AcroFieldBase field in form.GetFieldsForPage(1))
                {
                    switch (field)
                    {
                        case AcroCheckboxField cb:
                            if (cb.IsChecked)
                            {
                                Console.WriteLine($"Checkbox was checked: {cb.Information.MappingName}.");
                            }
                            break;
                    }
                }
            }
			else
			{
				Console.WriteLine($"Document doesn't contains form.");
			}

            if (document.TryGetXmpMetadata(out XmpMetadata metadata))
            {
                XDocument xmp = metadata.GetXDocument();
				Console.WriteLine('['+xmp.Element("Author").Value+']');
				Console.WriteLine('['+xmp.Element("Title").Value+']');
				Console.WriteLine('['+xmp.Element("Name").Value+']');
				Console.WriteLine('['+xmp.Element("Description").Value+']');
            }
			else
			{
				   Console.WriteLine($"Document doesn't contains metadata... :|");
			}

            if (document.TryGetBookmarks(out Bookmarks bookmarks))
            {
                Console.WriteLine($"Document contained bookmarks with {bookmarks.Roots.Count} root nodes.");
            }
			else
			{
				Console.WriteLine($"Document doesn't contains bookmarks.");
			}


            Console.WriteLine($"Document uses version {document.Version} of the PDF specification.");

            if (document.Advanced.TryGetEmbeddedFiles(out IReadOnlyList<EmbeddedFile> embeddedFiles) && embeddedFiles.Count > 0)
            {
                Console.WriteLine($"Document contains {embeddedFiles.Count} embedded files.");

				foreach(var file in embeddedFiles)
				{
					Console.WriteLine("\t"+file);

					IReadOnlyList<byte> bytes = file.Bytes;

					using (BinaryWriter writer = new BinaryWriter(File.Open(args[0]+"_-_"+file.Name, FileMode.Create)))
					{
						//writer.Write(bytes);
						foreach(var b in bytes)
						{
							writer.Write(b);
						}
					}
					Console.WriteLine("\tThe file saved to: '"+args[0]+"_-_"+file.Name+"'");

				}
            }
        }
    }
}
