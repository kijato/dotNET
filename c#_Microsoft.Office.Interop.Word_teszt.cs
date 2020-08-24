/*

set csc=c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe 

%csc% Program.cs /r:c:\Windows\assembly\GAC\Microsoft.Office.Interop.Word\11.0.0.0__71e9bce111e9429c\Microsoft.Office.Interop.Word.dll
                    c:\Windows\assembly\GAC_MSIL\Microsoft.Office.Interop.Word\15.0.0.0__71e9bce111e9429c\Microsoft.Office.Interop.Word.dll
*/
using System;
using Microsoft.Office.Interop.Word;

class Program
{
    static void Main()
    {
        // Open a doc file.
        Application application = new Application();
        object missing = System.Reflection.Missing.Value;
        Document document;
		try
		{
			document = application.Documents.Open("c:\\temp\\Program.doc");
		}
		catch ( Exception e )
		{
			document = application.Documents.Add(ref missing, ref missing, ref missing, ref missing);
		}

        // Loop through all words in the document.
        int count = document.Words.Count+100000;
		
		//document.BuiltInDocumentProperties["Keywords"].Value = count.ToString();
		
        for (int i = 1; i <= count; i++)
        {
            // Write the word.
            string text = document.Words[i].Text;
            Console.WriteLine("Word {0} = {1}", i, text);
        }
        // Close word.
		document.SaveAs2("c:\\temp\\Program.doc");
        application.Quit();
    }
}
