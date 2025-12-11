namespace BaiTap4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap gia tri ban dau: ");
            int number = int.Parse(Console.ReadLine());
            Reward(ref number, out int bnumber);

            Console.WriteLine($"\nsau Reward: {number}");
            Console.WriteLine($"bnumber (out): {bnumber}");
        }

        static void Reward(ref int num, out int bnumber)
        {
            while (true)
            {
                Console.Write("Nhap so can cong them (N de thoat): ");
                string input = Console.ReadLine();

                if (input.Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (int.TryParse(input, out int addValue))
                {
                    num += addValue;
                }
                
            }

            bnumber = num;
        }
    }
}
