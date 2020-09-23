using System; 
using System.Threading; 
class Program 
{ 


	class Osszeadas 
	{ 
	   public int bemeno_a; 
	   public int bemeno_b; 
	   public int kimeno_c; 
	   public volatile bool szal_kesz; 
	   public Exception kivetel; 
	   // 
	   public void osszeadas() 
	   { 
		  try 
		  { 
			 // a szamitasi folyamat kulon szalon 
			 kimeno_c = bemeno_a + bemeno_b; 
			 // szal kesz allapotba lepunk 
			 szal_kesz = true; 
		  } 
		  catch (Exception e) 
		  { 
			 // kimenekitjuk a kivetel leirot 
			 kivetel = e; 
		  } 
	   } 
	}

   static void Main() 
   { 
      // elokeszitjuk a parametereket 
      Osszeadas p = new Osszeadas(); 
      p.bemeno_a = 10; 
      p.bemeno_b = 20; 
      p.szal_kesz = false; 
      p.kivetel = null; 
      // letrehozzuk es inditjuk a szalat 
      Thread t = new Thread(p.osszeadas); 
      t.Start(); 
      // 
      // csinalunk valami hasznosat 
      // amig a masik szal szamol 
      // 
      // varunk a szal kesz allapotra 
      t.Join(); 
      // majd kiirjuk az eredmenyt 
      if (p.szal_kesz) 
      { 
       Console.WriteLine(p.kimeno_c); 
      } 
      else 
      { 
       Console.Write("A␣szal␣kivetel␣miatt␣allt␣le"); 
       Console.WriteLine(p.kivetel.Message); 
      } 
      // <enter> leutesere varakozas 
      Console.ReadLine(); 
   } 
}