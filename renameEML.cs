/*
Fordítóprogram:
	https://www.nuget.org/packages/Microsoft.Net.Compilers.Toolset/3.8.0-5.final
Assembly:
	https://www.nuget.org/packages/MsgReader/
Fordítás:
	csc.exe -target:exe -platform:x64 -r:MsgReader.dll renameEML.cs
Használat:	
	renameEML.exe <fájlnév>.eml
*/

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using MsgReader;

public class Program {

	public static void Main(string[] args) {

		if (args.Length == 0) {
			Console.WriteLine("Paraméterként adj meg egy fájlnevet!");
			return;
		}

		var fileInfo = new FileInfo(args[0]);
		
		if ( ! File.Exists(fileInfo.Name) ) {
			Console.WriteLine("Paraméterként megadott fájl nem lézetik...!");
			return;
		}
			
		var eml = MsgReader.Mime.Message.Load(fileInfo);

		var subject = eml.Headers.Subject;
		
		if ( eml.Headers == null || eml.Headers.Subject == null ) {
			Console.WriteLine("A levél nem tartalmaz 'Fejlécet', vagy 'Tárgy' mezõt, ezért átugrom!");
		}
		
		// a csere csak egyszer fut le...!
		subject = Regex.Replace(subject, @"[<>\\:/|?*]", "");

		if ( File.Exists(subject+".eml") ) {
			Console.WriteLine("A '"+subject+".eml"+"' nevû fájl már létezik, nem írom felül!");
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