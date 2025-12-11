namespace BaiTap3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            dynamic[] array = new dynamic[n];
            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                array[i] = ParseValue(input);
            }
            dynamic sum = SumArray(array);
            Console.WriteLine($"Sum: {sum}");
        }

        static dynamic ParseValue(string input)
        {
            if (int.TryParse(input, out int intValue))
                return intValue;
            if (double.TryParse(input, out double doubleValue))
                return doubleValue;
            if (bool.TryParse(input, out bool boolValue))
                return boolValue;
            return input;
        }

        static dynamic SumArray(dynamic[] array)
        {
            dynamic sum = 0;

            foreach (var item in array)
            {
                if (item is int || item is double)
                {
                    sum += item;
                }
            }
            return sum;
        }

    }
}
