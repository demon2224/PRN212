namespace BaiTap2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine($"Sum: {Sum(10, 20)}");
            Console.WriteLine($"Sum: {Sum("anh", 20)}");
        }
        
        static dynamic Sum(dynamic param1, dynamic param2)
        {
            if (param1 is string || param2 is string)
            {
                return param1.ToString() + param2.ToString();
            }
            else
            {
                return param1 + param2;
            }
        }
    }
}
