//css_args  hello.pdf hello+.pdf "hello_+_embedded_file.pdf" "hello_+_embedded_files.pdf"
//css_reference d:\GetEmailsFromGroupwise\itext.kernel.dll;
//css_reference d:\GetEmailsFromGroupwise\itext.io.dll;
//css_reference d:\GetEmailsFromGroupwise\itext.layout.dll;
using System;
using System.IO;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Filespec;
//using System.Security.Principal;
//using System.DirectoryServices.AccountManagement;
//using iText.Layout.Element;

// csc -r:itext.kernel.dll,itext.io.dll,itext.layout.dll addembeddedfiles+.cs

// https://kb.itextpdf.com/home/it7kb/examples/embedded-files

namespace iText
{
    public class AddEmbeddedFiles
    {

        public static String SRC = String.Empty;
        public static String DEST = String.Empty;

        public static void Main(String[] args)
        {
			if ( args.Length<3)
			{
				Console.WriteLine("\nA program különféle fájlok PDF-be ágyazására szolgál.\nA futásához legalább 3 fájlnevet meg kell adni a parancssorban:\n\t1. paraméter a 'forrás', mely a beágyazás alapja.\n\t2. paraméter a 'cél', melybe a beágyazandó állományok kerülnek.\n\tA további, paraméterként átadott állományokat, a program a 'cél'-ba, mellékletként beágyazza.\n\n"+System.AppDomain.CurrentDomain.FriendlyName+" <eredeti pdf> <új pdf> [fájlok, szóközzel elválasztva]\n\nNyomj meg egy gombot a kilépéshez...!"
				);
				Console.ReadKey();
				return;
			};
            new AddEmbeddedFiles().ManipulatePdf(ref args);
        }

        protected void ManipulatePdf(ref String[] files)
        {

			SRC = files[0];
			DEST = files[1];

            FileInfo dir = new FileInfo(DEST);
            dir.Directory.Create();
			
			PdfDocument pdfDoc = new PdfDocument(new PdfReader(SRC), new PdfWriter(DEST));

            //foreach (String text in ATTACHMENTS)
			for ( int i=2; i<files.Length; i++ )
            {
				FileInfo file = new FileInfo(files[i]);
				String embeddedFileName = file.Name;
                //String embeddedFileDescription = Environment.GetEnvironmentVariable("USERNAME")+"@"+Environment.GetEnvironmentVariable("COMPUTERNAME");
                //String embeddedFileDescription = System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName+"@"+Environment.GetEnvironmentVariable("COMPUTERNAME");
				String embeddedFileDescription = file.CreationTime.ToString();
                byte[] embeddedFileContentBytes = File.ReadAllBytes(files[i]);

                // the 5th argument is the mime-type of the embedded file;
                // the 6th argument is the PdfDictionary containing embedded file's parameters;
                // the 7th argument is the AFRelationship key value.
                PdfFileSpec spec = PdfFileSpec.CreateEmbeddedFileSpec( pdfDoc, embeddedFileContentBytes, embeddedFileDescription, embeddedFileName, null, null, null);

                pdfDoc.AddFileAttachment(i.ToString(), spec);
				
            }

			// https://stackoverflow.com/questions/37795151/how-to-extract-attached-files-from-pdf-with-itext7
			PdfObject obj;
			for (int j = 0; j < pdfDoc.GetNumberOfPdfObjects(); j++)
			{
				obj = pdfDoc.GetPdfObject(j);

				//PdfDictionary documentnames = pdfDoc.GetCatalog().GetPdfObject().GetAsDictionary(PdfName.Names);
				//PdfDictionary embeddedfiles = pdfDoc.GetCatalog().GetPdfObject().GetAsDictionary(PdfName.EmbeddedFiles);
				//PdfArray filespecs          = pdfDoc.GetCatalog().GetPdfObject().GetAsArray(PdfName.Names);

				//if ( obj != null && obj.IsStream() )
				if ( obj != null )
				{
					//Console.WriteLine(j + ". " + obj.GetType().ToString());
					
					if ( pdfDoc.GetPdfObject(j+1) != null && ! pdfDoc.GetPdfObject(j+1).IsDictionary() ) continue;
					
					if ( obj.IsStream() && ((PdfDictionary) pdfDoc.GetPdfObject(j+1) ).Get(PdfName.F) != null )
					{
						Console.WriteLine(j + ".");
						//Console.WriteLine("\t"+obj.ToString());
						Console.WriteLine("\tStream size : "+((PdfStream) obj).GetBytes().Length+" byte(s)");
						Console.WriteLine("\tName        : "+((PdfDictionary) pdfDoc.GetPdfObject(j+1) ).Get(PdfName.F));
						Console.WriteLine("\tDescription : "+((PdfDictionary) pdfDoc.GetPdfObject(j+1) ).Get(PdfName.Desc));
					}
					/*if ( obj.IsDictionary() )
					{
						Console.WriteLine("\t"+obj.ToString());
						foreach ( var v in ((PdfDictionary) obj).KeySet() )
						{
							Console.WriteLine("\t\t"+v+"_\t"+((PdfDictionary) obj).Get(v));
						}
					}*/
				}

			}
			
			pdfDoc.Close();

        }

	}
}
