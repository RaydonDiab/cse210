using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<float> numbers = new List<float>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        // Collect numbers from user
        while (true)
        {
            Console.Write("Enter number: ");
            float input = float.Parse(Console.ReadLine());

            if (input == 0)
                break;

            numbers.Add(input);
        }

        // Core Requirement 1: Sum
        float sum = 0;
        foreach (float num in numbers)
        {
            sum += num;
        }
        Console.WriteLine($"The sum is: {sum}");

        // Core Requirement 2: Average
        float average = sum / numbers.Count;
        Console.WriteLine($"The average is: {average}");

        // Core Requirement 3: Maximum
        float max = numbers[0];
        foreach (float num in numbers)
        {
            if (num > max)
                max = num;
        }
        Console.WriteLine($"The largest number is: {max}");

        // Stretch Challenge 1: Smallest positive number
        float smallestPositive = float.MaxValue;
        foreach (float num in numbers)
        {
            if (num > 0 && num < smallestPositive)
                smallestPositive = num;
        }

        if (smallestPositive == float.MaxValue)
            Console.WriteLine("There were no positive numbers.");
        else
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");

        // Stretch Challenge 2: Sort and display the list
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (float num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}