using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
/*
	https://developpaper.com/how-to-store-authentication-information-safely-in-net-c/
*/

//csc -r:itext.kernel.dll,itext.io.dll,itext.layout.dll GetTextFromPDF.cs &&  GetTextFromPDF.exe "efoldmero_400000106123+.pdf"

namespace Crypt
{
	class Program
	{
		public static void Main(string[] args)
		{

			//try {
				
				
			//Data that needs to be protected. Use encoding UTF8. Getbytes() converts a string into a byte array.
			byte[] plaintext = Encoding.UTF8.GetBytes("Here is a unicode characters string. Pi (\u03a0)");

			//Generate an additional entropy (for vector initialization)
			byte[] entropy = new byte[20];
			using(RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider()){
				rng.GetBytes(entropy);
			}	
			byte[] ciphertext = ProtectedData.Protect(plaintext, entropy, DataProtectionScope.CurrentUser);
			
			plaintext = ProtectedData.Unprotect(ciphertext, entropy, DataProtectionScope.CurrentUser);
			
			Console.WriteLine(Encoding.UTF8.GetString(ciphertext)+"\n");	
			Console.WriteLine(Encoding.UTF8.GetString(plaintext)+"\n");	
			Console.WriteLine();	
			
			//} catch (Exception e) {	Console.WriteLine(e.Message); }
			//Console.Read();


		}
	}
}
