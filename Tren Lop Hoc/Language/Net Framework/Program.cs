using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net_Framework
{
    internal class Program
    {

        static int[] numbers = { 1, 2, 3, 4, 5 };

        static ref int FindNumber(int target)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == target)
                {
                    return ref numbers[i]; // trả về tham chiếu
                }
            }
            throw new Exception("Not found");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Original sequence: " + string.Join(", ", numbers));

            ref int value = ref FindNumber(3); // ref local
            value *= 2; // thay đổi trực tiếp phần tử mảng

            Console.WriteLine("New sequence: " + string.Join(", ", numbers));
        }
    }
}
