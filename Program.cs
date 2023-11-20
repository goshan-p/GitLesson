using System;


namespace HW1
{
     class Program
    {
        static void Main(string[] args)
        {
            double a, b;

            Console.WriteLine("Var 1:");
            a = double.Parse(Console.ReadLine());

            Console.WriteLine("Var 2:");
            b = double.Parse(Console.ReadLine());

            Console.WriteLine("Sum: {0}", a + b);
            Console.WriteLine("Diff: {0}", a - b);
            Console.WriteLine("Mult: {0}", a * b);

            if (b == 0)
            {
                Console.WriteLine("Div: Error");
            }
            else
            {
                Console.WriteLine("Div: {0}", a / b);
            }
            
        }
    }
}
