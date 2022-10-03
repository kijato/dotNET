using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Element;
using iText.Layout.Borders;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Colorspace;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using iText.IO.Font;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//csc -r:itext.kernel.dll,itext.io.dll,itext.layout.dll GetTextFromPDF.cs &&  GetTextFromPDF.exe "efoldmero_400000106123+.pdf"

namespace GetTextFromPDF
{
    class Program
    {
		public static void Main(string[] args)
		{
			//try
			//{
			
				byte[] byteArray = File.ReadAllBytes(args[0]);
				
				ExtractTextFromPDF e = new ExtractTextFromPDF(byteArray);
				
				Console.WriteLine("-----\n"+e.PageContent+"\n-----\n");

				foreach(string str in e.PageContent.Split(':')) {
					Console.WriteLine(str);
				}
				
				Console.ReadKey();
					
			//}
			//catch (Exception ex)
			//{
			//	Console.WriteLine(ex.Message);
			//}

		}
	}
	
	class ExtractTextFromPDF
	{
		public string PageContent
		{ 
			get; 
			set; 
		} 		

		public ExtractTextFromPDF(byte[] byteArray)
		{
			
			string PageContent = String.Empty;
			
			using (MemoryStream memory = new MemoryStream(byteArray))
			{
				PdfReader pdfReader = new PdfReader(memory);
				PdfDocument pdfDoc = new PdfDocument(pdfReader);
				string pageContent = "";
				for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
				{
					ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
					pageContent += PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
				}
				pdfDoc.Close();
				pdfReader.Close();

				this.PageContent = pageContent;
			}
		}
		
	}
}
