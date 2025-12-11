using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSingleton29_9
{
    public sealed class Singleton
    {
        private static readonly Singleton Instance;
        private static int totalInstance;

        //prevent new contructor
        private Singleton()
        {
            totalInstance++;
            Console.WriteLine("Private constructor is called");
        }

        static Singleton()
        {
            Console.WriteLine("Static constructor called");
            Instance = new Singleton();
            Console.WriteLine($"Instance created. Total: {totalInstance}");
            Console.WriteLine("Exit from static constructor");
        }

        public static Singleton GetInstance => Instance;
        public int GetTotalInstance() => totalInstance;
        public void Print() => Console.WriteLine("Hello there!");
    }

    public class Sin// : Singleton
    {
        //Singleton sinclass = new Singleton();

        public void Print() { Console.WriteLine("Sin total instance : "/* + GetTotalInstance()*/ ); }
    }
}
