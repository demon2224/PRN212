namespace Demo24_09
{
    internal class Program
    {
        static void Main()
        {
            //MyClass<string> name = new MyClass<string>() { Value="Jack"};
            //Console.WriteLine(name);
            //MyClass<float> verson = new MyClass<float>() { Value=5.5F};
            //Console.WriteLine(verson);
            //dynamic obj = new { Id = 1, Name="David"  };
            //MyClass<dynamic> myClass = new MyClass<dynamic>() { Value=obj};
            //Console.WriteLine(myClass);

            int a = 10, b = 20;
            Bai1 bai1 = new Bai1();
            bai1.DoSwap(ref a, ref b);
            Console.WriteLine($"After Swap method: a = {a}, b = {b}");
            Console.ReadLine();
        }
    }
}
