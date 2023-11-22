using System;

namespace HW1
{
    class Program
    {
        static void Main(string[] args)
        {
            double a, b;
            bool aValid, bValid;

            while (true)
            {
                do
                {
                    Console.WriteLine("Var 1:");
                    aValid = double.TryParse(Console.ReadLine(), out a);

                    if (!aValid)
                    {
                        Console.WriteLine("Invalid input for variable 1. Please enter a valid number.");
                    }
                } while (!aValid);

                do
                {
                    Console.WriteLine("Var 2:");
                    bValid = double.TryParse(Console.ReadLine(), out b);
                    if (!bValid)
                    {
                        Console.WriteLine("Invalid input for variable 2. Please enter a valid number.");
                    }
                } while (!bValid);

                Console.WriteLine("Sum: {0}", a + b);
                Console.WriteLine("Diff: {0}", a - b);
                Console.WriteLine("Mult: {0}", a * b);

                if (b != 0)
                    Console.WriteLine("Div: {0}", a / b);
                else
                    Console.WriteLine("Div: Error ");

                Console.WriteLine("-----");
            }
        }
    }
}
