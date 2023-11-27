using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите длину массива: ");
        int length;

        while (!int.TryParse(Console.ReadLine(), out length) || length <= 0)
        {
            Console.WriteLine("Некорректный ввод. Введите целое положительное число:");
        }

        int[] array = new int[length];

        for (int i = 0; i < length; i++)
        {
            Console.WriteLine("Введите число номер {0}: ", i + 1);

            while (!int.TryParse(Console.ReadLine(), out array[i]))
            {
                Console.WriteLine("Некорректный ввод. Введите целое число:");
            }
        }

        Console.Write("Итоговый массив: ");
        foreach (var element in array)
        {
            Console.Write(element + " ");
        }

        Console.ReadLine();
    }
}
