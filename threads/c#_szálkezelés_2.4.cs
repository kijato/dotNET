using System; 
using System.Threading; 
class Program 
{

	class Osszeadas 
	{ 
	   public static int bemeno_a; 
	   public static int bemeno_b; 
	   public static int kimeno_c; 
	   public static volatile bool szal_kesz; 
	   public static Exception kivetel; 
	   // 
	   public static void osszeadas() 
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
      Osszeadas.bemeno_a = 10; 
      Osszeadas.bemeno_b = 20; 
      Osszeadas.szal_kesz = false; 
      Osszeadas.kivetel = null; 
      // letrehozzuk es inditjuk a szalat 
      Thread t = new Thread(Osszeadas.osszeadas); 
      t.Start(); 
      // 
      // csinalunk valami hasznosat 
      // amig a masik szal szamol 
      // 
      // varunk a szal kesz allapotra 
      t.Join(); 
      // majd kiirjuk az eredmenyt 
      if (Osszeadas.szal_kesz) 
      { 
       Console.WriteLine(Osszeadas.kimeno_c); 
      } 
      else 
      { 
       Console.Write("A␣szal␣kivetel␣miatt␣allt␣le"); 
       Console.WriteLine(Osszeadas.kivetel.Message); 
      } 
      // <enter> leutesere varakozas 
      Console.ReadLine(); 
   } 
}