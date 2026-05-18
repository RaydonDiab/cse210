using System;

// Exceeding requirements:
// 1. Added mood tracking to each journal entry
// 2. Added a streak tracker to encourage daily journaling
// 3. Saved journal as a proper .csv file with quote escaping, openable in Excel

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();
        string menuChoice = "";

        while (menuChoice != "4")
        {
            Console.WriteLine("\nPlease select one of the following choices:");
            Console.WriteLine("  1. Write");
            Console.WriteLine("  2. Display");
            Console.WriteLine("  3. Save");
            Console.WriteLine("  4. Load");
            Console.WriteLine("  5. Quit");
            Console.Write("What would you like to do? ");
            menuChoice = Console.ReadLine();

            if (menuChoice == "1")
            {
                string prompt = promptGenerator.GetRandomPrompt();
                Console.WriteLine($"\n{prompt}");
                Console.Write("> ");
                string response = Console.ReadLine();
                Console.WriteLine("\nHow are you feeling? (e.g. happy, sad, anxious, grateful)");
                Console.Write("> ");
                string mood = Console.ReadLine();

                Entry newEntry = new Entry();
                newEntry._date = DateTime.Now.ToShortDateString();
                newEntry._prompt = prompt;
                newEntry._response = response;
                newEntry._mood = mood;

                journal.AddEntry(newEntry);
            }
            else if (menuChoice == "2")
            {
                journal.DisplayEntries();
                journal.DisplayStreak();
            }
            else if (menuChoice == "3")
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine();
                journal.SaveToFile(filename);
            }
            else if (menuChoice == "4")
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine();
                journal.LoadFromFile(filename);
            }
            else if (menuChoice == "5")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice, please try again.");
            }
        }
    }
}