public class MasterySession
{
    private Scripture _scripture;

    public MasterySession(Scripture scripture)
    {
        _scripture = scripture;
    }

    public void Run()
    {
        Console.Clear();
        Console.WriteLine("=== MASTERY MODE ===");
        Console.WriteLine($"Reference: {_scripture.GetReferenceText()}");
        Console.WriteLine();
        Console.WriteLine("Type the scripture from memory as best you can.");
        Console.WriteLine("Press Enter when done.");
        Console.WriteLine();
        Console.Write("> ");

        string input = Console.ReadLine() ?? "";

        string[] typedWords = CleanAndSplit(input);
        string[] actualWords = CleanAndSplit(_scripture.GetRawText());

        int correct = 0;
        int total = actualWords.Length;

        // Count how many words the user got right (in order)
        for (int i = 0; i < Math.Min(typedWords.Length, actualWords.Length); i++)
        {
            if (typedWords[i] == actualWords[i])
                correct++;
        }

        double percent = total > 0 ? (double)correct / total * 100 : 0;

        Console.Clear();
        Console.WriteLine("=== MASTERY RESULTS ===");
        Console.WriteLine($"Reference: {_scripture.GetReferenceText()}");
        Console.WriteLine();
        Console.WriteLine("Correct words highlighted below:");
        Console.WriteLine();

        DisplayComparison(typedWords, actualWords);

        Console.WriteLine();
        Console.WriteLine($"Score: {correct}/{total} words correct ({percent:F0}%)");
        Console.WriteLine();

        if (percent == 100)
            Console.WriteLine("Perfect! You've mastered this scripture!");
        else if (percent >= 80)
            Console.WriteLine("Great job! Almost there.");
        else if (percent >= 50)
            Console.WriteLine("Good effort! Keep practicing.");
        else
            Console.WriteLine("Keep at it — you'll get there!");

        Console.WriteLine();
        Console.Write("Press Enter to return to the menu...");
        Console.ReadLine();
    }

    private void DisplayComparison(string[] typed, string[] actual)
    {
        for (int i = 0; i < actual.Length; i++)
        {
            string actualWord = actual[i];
            bool gotIt = i < typed.Length && typed[i] == actualWord;

            if (gotIt)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(actualWord);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // Show what they typed, or mark it missing
                string display = i < typed.Length ? $"[{typed[i]}]" : "[?]";
                Console.Write(display);
            }

            Console.ResetColor();
            Console.Write(" ");
        }
        Console.WriteLine();
    }

    // Strips punctuation and lowercases for lenient comparison
    private string[] CleanAndSplit(string text)
    {
        return text.ToLower()
                   .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                   .Select(w => new string(w.Where(char.IsLetter).ToArray()))
                   .Where(w => w.Length > 0)
                   .ToArray();
    }
}