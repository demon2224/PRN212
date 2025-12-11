namespace AnhTuan
{
    internal class Program
    {
        static (string first, string middle, string last, string full) SplitNames(string fullName)
        {
            var strArray = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string first = strArray[0];
            string last = strArray[^1]; // lấy phần tử cuối
            string middle = strArray.Length > 2
                ? string.Join(" ", strArray, 1, strArray.Length - 2)
                : ""; // nếu không có middle thì để rỗng
            string full = fullName;

            return (first, middle, last, full);
        }

        static void Main(string[] args)
        {
            var (first, middle, last, full) = SplitNames(Console.ReadLine());

            Console.WriteLine($"First: {first}");
            Console.WriteLine($"Middle: {middle}");
            Console.WriteLine($"Last: {last}");
            Console.WriteLine($"Full: {full}");

            Console.ReadLine();
        }
    }
}
