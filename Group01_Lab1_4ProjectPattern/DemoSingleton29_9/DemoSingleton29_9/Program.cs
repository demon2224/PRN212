namespace DemoSingleton29_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Try to get Singleton instance, called firstInstance");
            Singleton firstInstance = Singleton.GetInstance;
            Console.WriteLine("Invoke Print() method");
            firstInstance.Print();
            Console.WriteLine();

            Console.WriteLine("2. Try to get Singleton instance, called secondInstance");
            Singleton secondInstance = Singleton.GetInstance;

            Console.WriteLine($"Total instance: {secondInstance.GetTotalInstance()} ");
            Console.WriteLine("Invoke Print() method");
            secondInstance.Print();

            if (firstInstance != secondInstance)
            {
                Console.WriteLine("2 instance Diff");
            }
            else
            {
                Console.WriteLine("2 instance Same");
            }
            Console.WriteLine();

            // Sin sin = new Sin();
            //  sin.Print();


            //Console.WriteLine("3. Try to get Singleton instance, called secondInstance");
            //Singleton newsing = new Singleton();
            //Console.WriteLine($"Total instance: {newsing.GetTotalInstance()} ");
            //Console.WriteLine("Invoke Print() method");
            //newsing.Print();
            //Console.WriteLine(firstInstance != newsing ? "2 instance diff😱" : "2 instance same");

            Console.ReadKey();

        }
    }
}
