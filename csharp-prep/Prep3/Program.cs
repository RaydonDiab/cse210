using System;

class Program
{
    static void Main(string[] args)
    {

        //Console.WriteLine("What is the magic number? ");
        //int magicNumber = int.Parse(Console.ReadLine());

        string play = "y";

    while (play == "y")
        {
            Random randomGenerator = new Random();
            int magicNumber = randomGenerator.Next(1,11);

            int guess = -2;
            int guessAmount = 1;
        
            while (guess != magicNumber)
            {
                Console.WriteLine("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                

                if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                    guessAmount = guessAmount +1;
                }

                else if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                    guessAmount = guessAmount +1;
                }

                else
                {
                    Console.WriteLine("You got it right!");
                    Console.WriteLine($"You got it in {guessAmount} guesses!");
                }
            }

            Console.WriteLine("Do you want to play again?: (y/n)");
            play = Console.ReadLine().ToLower().Trim();

            while (play != "y" && play != "n")
            {
                Console.WriteLine("Please enter y or n: ");
                play = Console.ReadLine().ToLower().Trim();
            }

            if (play != "y")
            {
                Console.WriteLine("Thanks for playing");
            }
        }
    }
}