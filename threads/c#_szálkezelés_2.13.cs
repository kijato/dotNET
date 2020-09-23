using System; 
using System.Threading; 
 
class Program 
{ 
   static Random rnd = new Random(); 
   static void Main(string[] args) 
   { 
      Thread t1 = new Thread(Kiir_1); 
      t1.Start(); 
      Thread t2 = new Thread(Kiir_2); 
      t2.Start(); 
      t1.Join(); 
      t2.Join(); 
      Console.ForegroundColor = ConsoleColor.Gray; 
      Console.WriteLine("Mindket␣szal␣kesz!"); 
      Console.ReadLine(); 
   } 
 
   static void Kiir_1() 
   { 
      for (int i = 0; i < 10; i++) 
      { 
         Console.ForegroundColor = ConsoleColor.Yellow; 
         Console.WriteLine("1-es␣szal␣{0}",i+1); 
         Thread.Sleep(rnd.Next(500, 1200)); 
      } 
   } 
   static void Kiir_2() 
   { 
      for (int i = 0; i < 10; i++) 
      { 
         Console.ForegroundColor = ConsoleColor.Green; 
         Console.WriteLine("2-es␣szal␣{0}", i + 1); 
         Thread.Sleep( rnd.Next(500,1200)); 
      } 
   } 
}