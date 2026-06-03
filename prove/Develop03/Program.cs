// Scripture Memorizer - CSE 210 Unit 03
//
// Exceeds requirements:
// 1. STRETCH CHALLENGE: Only visible words are selected when hiding.
// 2. SCRIPTURE LIBRARY + MENU: User picks from a list of scriptures.
// 3. GO BACK FEATURE: User can reveal a few words at a time if they forget.
// 4. MASTERY MODE: User types the scripture from memory; correct words are shown in green, wrong/missing in red, with a final score.
// 5. WORD-LENGTH UNDERSCORES: Blanks match the hidden word's length.

using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Scripture> _library = new List<Scripture>
    {
        new Scripture(
            new ScriptureReference("John", 3, 16),
            "For God so loved the world that he gave his only begotten Son that whosoever believeth in him should not perish but have everlasting life"
        ),
        new Scripture(
            new ScriptureReference("Proverbs", 3, 5, 6),
            "Trust in the Lord with all thine heart and lean not unto thine own understanding In all thy ways acknowledge him and he shall direct thy paths"
        ),
        new Scripture(
            new ScriptureReference("Joshua", 1, 9),
            "Have not I commanded thee Be strong and of a good courage be not afraid neither be thou dismayed for the Lord thy God is with thee whithersoever thou goest"
        ),
        new Scripture(
            new ScriptureReference("Philippians", 4, 13),
            "I can do all things through Christ which strengtheneth me"
        ),
        new Scripture(
            new ScriptureReference("2 Nephi", 2, 25),
            "Adam fell that men might be and men are that they might have joy"
        ),
    };

    static void Main(string[] args)
    {
        while (true)
        {
            Scripture chosen = ShowSelectionMenu();
            if (chosen == null) break;

            chosen.Reset();
            ShowScriptureMenu(chosen);
        }

        Console.Clear();
        Console.WriteLine("Goodbye!");
    }

    static Scripture ShowSelectionMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== SCRIPTURE MEMORIZER ===");
            Console.WriteLine("Choose a scripture to practice:\n");

            for (int i = 0; i < _library.Count; i++)
                Console.WriteLine($"  {i + 1}. {_library[i].GetReferenceText()}");

            Console.WriteLine($"  {_library.Count + 1}. Quit");
            Console.WriteLine();
            Console.Write("Enter a number: ");

            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input.Trim(), out int choice))
            {
                if (choice >= 1 && choice <= _library.Count)
                    return _library[choice - 1];
                else if (choice == _library.Count + 1)
                    return null;
            }

            Console.WriteLine("Invalid choice. Press Enter to try again.");
            Console.ReadLine();
        }
    }

    static void ShowScriptureMenu(Scripture scripture)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("All words are hidden!");
                Console.WriteLine();
                Console.WriteLine("  1. Try Mastery Mode");
                Console.WriteLine("  2. Reveal a few words");
                Console.WriteLine("  3. Back to scripture selection");
                Console.WriteLine();
                Console.Write("Enter a number: ");

                string input = Console.ReadLine() ?? "";
                if (input.Trim() == "1")
                    new MasterySession(scripture).Run();
                else if (input.Trim() == "2")
                    scripture.RevealRandomWords(3);
                else if (input.Trim() == "3")
                    return;

                continue;
            }

            Console.WriteLine("  Enter  - Hide a few words");
            if (scripture.HasHiddenWords())
            {
                Console.WriteLine("  r      - Reveal a few words");
                Console.WriteLine("  m      - Mastery mode (type it from memory)");
            }
            Console.WriteLine("  q      - Back to scripture selection");
            Console.WriteLine();
            Console.Write("> ");

            string choice = Console.ReadLine() ?? "";

            switch (choice.Trim().ToLower())
            {
                case "":
                    scripture.HideRandomWords(3);
                    break;
                case "r":
                    if (scripture.HasHiddenWords())
                        scripture.RevealRandomWords(3);
                    break;
                case "m":
                    if (scripture.HasHiddenWords())
                        new MasterySession(scripture).Run();
                    break;
                case "q":
                    return;
            }
        }
    }
}