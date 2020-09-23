using System; 
using System.Threading; 
class Program 
{ 
   static void Main() 
   { 
      // elokeszitjuk a parametereket 
      bemeno_a = 10; 
      bemeno_b = 20; 
      // letrehozzuk es inditjuk a szalat 
      Thread t = new Thread(osszeadas); 
      t.Start(); 
      // 
      // csinalunk valami hasznosat 
      // amig a masik szal szamol 
      // 
      // kiirjuk az eredmenyt 
      Console.WriteLine(kimeno_c); 
      // <enter> leutesere varakozas 
      Console.ReadLine(); 
   } 
   // 
   static int bemeno_a; 
   static int bemeno_b; 
   static int kimeno_c; 
   // 
   static void osszeadas() 
   { 
      // a szamitasi folyamat kulon szalon 
      kimeno_c = bemeno_a + bemeno_b; 
   } 
}