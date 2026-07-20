using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to your Calorie Tracker!");
        MealManager manager = new MealManager();
        manager.Start();
    }
}