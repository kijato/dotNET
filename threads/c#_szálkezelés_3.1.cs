
// http://aries.ektf.hu/~hz/pdf-tamop/pdf-03/html/ch03.html

using System; 
using System.Threading; 
 
class Program 
{ 
   static int[] tomb = new int[1000000]; 
   static int osszeg = 0; 
   static void Main(string[] args) 
   { 
      Random rnd = new Random(); 
      for (int i = 0; i < tomb.Length; i++) 
         tomb[i] = rnd.Next(100, 200); 
      Thread t1 = new Thread(osszeg_1); 
      t1.Start(); 
      Thread t2 = new Thread(osszeg_2); 
      t2.Start(); 
      t1.Join(); 
      t2.Join(); 
      Console.WriteLine("Osszeg␣2␣szalon={0}",osszeg); 
      int norm = 0; 
      foreach(int x in tomb) 
         norm = norm+x; 
      Console.WriteLine("Osszege␣normal={0}", norm); 
      if (norm != osszeg) Console.WriteLine("!!!␣HIBA␣!!!"); 
      Console.ReadLine(); 
   } 
   /*
   static void osszeg_1() 
   { 
      for (int i = 0; i < tomb.Length/2; i++) 
         osszeg = osszeg + tomb[i]; 
   } 
   static void osszeg_2() 
   { 
      for (int i = tomb.Length / 2; i < tomb.Length; i++) 
         osszeg = osszeg + tomb[i]; 
   }
   */
	
	// 3.5
	/*
    static void osszeg_1() 
	{ 
	 lock (tomb) 
	 { 
	   for (int i = 0; i < tomb.Length / 2; i++) 
		  osszeg = osszeg + tomb[i]; 
	 } 
	} 
	 
	static void osszeg_2() 
	{ 
	   lock (tomb) 
	   { 
		  for (int i = tomb.Length / 2; i < tomb.Length; i++) 
			 osszeg = osszeg + tomb[i]; 
	   } 
	}
	*/
	
	// 3.6
	/*
	static void osszeg_1() 
	{ 
	 for (int i = 0; i < tomb.Length / 2; i++) 
	   lock (tomb) 
	   { 
		osszeg = osszeg + tomb[i]; 
	   } 
	} 
	 
	static void osszeg_2() 
	{ 
	 for (int i = tomb.Length / 2; i < tomb.Length; i++) 
	   lock (tomb) 
	   { 
		osszeg = osszeg + tomb[i]; 
	   } 
	}
	*/
		
	// 3.7
	static void osszeg_1() 
	{ 
	 int sum=0; 
	 for (int i = 0; i < tomb.Length / 2; i++) 
		sum = sum + tomb[i]; 
	 // 
	 lock (tomb) 
	 { 
		osszeg = osszeg + sum; 
	 } 
	} 
	 
	static void osszeg_2() 
	{ 
	 int sum=0; 
	 for (int i = tomb.Length / 2; i < tomb.Length; i++) 
		sum = sum + tomb[i]; 
	 // 
	 lock (tomb) 
	 { 
		osszeg = osszeg + sum; 
	 } 
	}		
		
}