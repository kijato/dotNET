 
using System; 
using System.Threading; 
class Program 
{ 

class Osszeadas 
{ 
   public int bemeno_a; 
   public int bemeno_b; 
   public int kimeno_c; 
   public volatile bool szal_kesz = false; 
   public Exception kivetel = null; 
   // 
   public Osszeadas(int a,int b) 
   { 
    bemeno_a=a; 
    bemeno_b=b; 
   } 
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
      Osszeadas p = new Osszeadas(10,20); 
      Thread t = new Thread(p.osszeadas); 
      t.Start(); 
      // 
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