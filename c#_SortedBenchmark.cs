using System;
using System.IO;
using System.Collections; // For Hashtable.
using System.Collections.Generic;
using System.Collections.Specialized; // For ListDictionary.
using System.Diagnostics;

/*
	https://www.dotnetperls.com/listdictionary
	https://www.geeksforgeeks.org/c-sharp-sortedlist-with-examples/
	https://docs.microsoft.com/en-us/dotnet/standard/collections/
	https://docs.microsoft.com/en-us/dotnet/standard/collections/sorted-collection-types
	
*/

//csc -r:itext.kernel.dll,itext.io.dll,itext.layout.dll GetTextFromPDF.cs &&  GetTextFromPDF.exe "efoldmero_400000106123+.pdf"

namespace SortedBenchmark
{
	class Program
	{
		public static void Main(string[] args)
		{

			int max = 100000;
			Random rnd = new Random();
			
			//try {
				SortedSet < int > sortedSet = new SortedSet < int > ();
				var s1 = Stopwatch.StartNew();
				for(int i=0; i<max; i++)
					sortedSet.Add(rnd.Next());
				s1.Stop();
				//foreach(var x in sortedSet) { Console.WriteLine(x); }

				SortedList < int, string > sortedList = new SortedList < int, string > ();
				var s2 = Stopwatch.StartNew();
				for(int i=0; i<max; i++)
					sortedList.Add(i, rnd.Next().ToString());
				s2.Stop();
				//foreach (KeyValuePair<int, string> pair in sortedList) { Console.WriteLine("Key: {0}\tValue: {1}", pair.Key, pair.Value); }

				SortedDictionary < int, string > sortedDictionary  = new SortedDictionary < int, string > ();
				var s3 = Stopwatch.StartNew();
				for(int i=0; i<max; i++)
					sortedDictionary.Add(i, rnd.Next().ToString());
				s3.Stop();
				//foreach (KeyValuePair<int, int> pair in sortedDictionary) { Console.WriteLine("Key: {0}\tValue: {1}", pair.Key, pair.Value); }			

				ListDictionary listDictionary  = new ListDictionary ();
				var s4 = Stopwatch.StartNew();
				for(int i=0; i<max; i++)
					listDictionary.Add(i, rnd.Next());
				s4.Stop();
				//foreach (KeyValuePair<int, int> pair in listDictionary) { Console.WriteLine("Key: {0}\tValue: {1}", pair.Key, pair.Value); }			

				Hashtable hashTable  = new Hashtable ();
				var s5 = Stopwatch.StartNew();
				for(int i=0; i<max; i++)
					hashTable.Add(i,rnd.Next());
				s5.Stop();
				//foreach (KeyValuePair<int, int> pair in hashTable) { Console.WriteLine("Key: {0}\tValue: {1}", pair.Key, pair.Value); }
			
				Console.WriteLine(((s1.Elapsed.TotalMilliseconds * 1000000) / max).ToString("SortedSet: 0.00 ns"));	
				Console.WriteLine(((s2.Elapsed.TotalMilliseconds * 1000000) / max).ToString("SortedList: 0.00 ns"));	
				Console.WriteLine(((s3.Elapsed.TotalMilliseconds * 1000000) / max).ToString("SortedDictionary: 0.00 ns"));	
				Console.WriteLine(((s4.Elapsed.TotalMilliseconds * 1000000) / max).ToString("ListDictionary: 0.00 ns"));	
				Console.WriteLine(((s5.Elapsed.TotalMilliseconds * 1000000) / max).ToString("Hashtable: 0.00 ns"));	
			
			//} catch (Exception e) {	Console.WriteLine(e.Message); }
			//Console.Read();


		}
	}
}
