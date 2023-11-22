using System;

namespace HW1
{
    class Program
    {
        static void Main(string[] args)
        {
            double a, b;
            bool validInput;

            do
            {
                Console.WriteLine("Var 1:");
                validInput = double.TryParse(Console.ReadLine(), out a);
                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    Console.WriteLine("-----");

                    continue;
                }

                Console.WriteLine("Var 2:");
                validInput = double.TryParse(Console.ReadLine(), out b);
                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    Console.WriteLine("-----");

                    continue;
                }

                Console.WriteLine("Sum: {0}", a + b);
                Console.WriteLine("Diff: {0}", a - b);
                Console.WriteLine("Mult: {0}", a * b);

                if (b != 0)
                    Console.WriteLine("Div: {0}", a / b);
                else
                    Console.WriteLine("Div: Error");

                Console.WriteLine("-----");
            } while (true);
        }
    }
}
