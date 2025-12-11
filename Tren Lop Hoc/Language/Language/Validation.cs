using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
    internal class Validation
    {
        public static int CheckInputInt()
        {
            int number = 0;
            bool isValid = false;

            do
            {
                Console.Write("Nhap so nguyen to: ");
                string input = Console.ReadLine();
                try
                {
                    number = Convert.ToInt32(input);
                    if (number > 0)
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Vui long nhap so nguyen to > 0!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                
             } while (!isValid);

            return number;
        }
    }
}
