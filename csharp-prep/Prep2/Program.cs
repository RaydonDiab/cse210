using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What was your grade percentage? ");
        string grade = Console.ReadLine();
        int number = int.Parse(grade);

        string letter = "";
        string sign = "";
        int remainder = number % 10;

        if (remainder >= 7)
        {
            sign = "+";
        }

        if (remainder < 3)
        {
            sign = "-";
        }


        if (number >= 90)
        {
            letter = "A";
            if (remainder >= 3)
            {
                sign = "";
            }
        }
        if (number >= 80 && number <90)
        {
            letter = "B";
        }
        if (number >= 70 && number <80)
        {
            letter = "C";
        }
        if (number >= 60 && number <70)
        {
            letter = "D";
        }
        if (number < 60)
        {
            letter = "F";
            if (remainder >= 0)
            {
                sign = "";
            }
        }

        Console.WriteLine($"Your grade is {letter}{sign}");

        if (number >= 70)
        {
            Console.WriteLine("Congratulations. You passed!");
        }
        if (number < 70)
        {
            Console.WriteLine("You didn't make it, but you'll get in next time!");
        }

    }
}