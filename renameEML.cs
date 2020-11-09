/*
Ford�t�program:
	https://www.nuget.org/packages/Microsoft.Net.Compilers.Toolset/3.8.0-5.final
Assembly:
	https://www.nuget.org/packages/MsgReader/
Ford�t�s:
	csc.exe -target:exe -platform:x64 -r:MsgReader.dll renameEML.cs
Haszn�lat:	
	renameEML.exe <f�jln�v>.eml
*/

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using MsgReader;

public class Program {

	public static void Main(string[] args) {

		if (args.Length == 0) {
			Console.WriteLine("Param�terk�nt adj meg egy f�jlnevet!");
			return;
		}

		var fileInfo = new FileInfo(args[0]);
		
		if ( ! File.Exists(fileInfo.Name) ) {
			Console.WriteLine("Param�terk�nt megadott f�jl nem l�zetik...!");
			return;
		}
			
		var eml = MsgReader.Mime.Message.Load(fileInfo);

		var subject = eml.Headers.Subject;
		
		if ( eml.Headers == null || eml.Headers.Subject == null ) {
			Console.WriteLine("A lev�l nem tartalmaz 'Fejl�cet', vagy 'T�rgy' mez�t, ez�rt �tugrom!");
		}
		
		// a csere csak egyszer fut le...!
		subject = Regex.Replace(subject, @"[<>\\:/|?*]", "");

		if ( File.Exists(subject+".eml") ) {
			Console.WriteLine("A '"+subject+".eml"+"' nev� f�jl m�r l�tezik, nem �rom fel�l!");
		} else {
			File.Move(fileInfo.Name, subject+".eml");
			Console.WriteLine("'" + fileInfo.Name + "' -> '"+subject+".eml"+"'");
		}

		/*if (eml.Headers != null) {
			if (eml.Headers.To != null) {
				foreach (var recipient in eml.Headers.To) {	var to = recipient.Address; }
			}
		}*/
		//if (eml.TextBody != null) {	var textBody = Encoding.UTF8.GetString(eml.TextBody.Body); }
		//if (eml.HtmlBody != null) {	var htmlBody = Encoding.UTF8.GetString(eml.HtmlBody.Body); }

	}

}