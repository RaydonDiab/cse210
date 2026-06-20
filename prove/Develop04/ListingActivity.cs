public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private List<string> _remainingPrompts = new List<string>();

    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine(GetNextPrompt());
        ShowThinkingAnimation(5);

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string item = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(item))
            {
                continue;
            }

            bool alreadyListed = items.Exists(existing =>
                existing.Equals(item, StringComparison.OrdinalIgnoreCase));

            if (alreadyListed)
            {
                Console.WriteLine("  (already listed - try something new)");
            }
            else
            {
                items.Add(item);
            }
        }

        Console.WriteLine($"You listed {items.Count} items:");
        foreach (string item in items)
        {
            Console.WriteLine($"  - {item}");
        }

        DisplayEndingMessage();
    }

    private string GetNextPrompt()
    {
        if (_remainingPrompts.Count == 0)
        {
            _remainingPrompts = new List<string>(_prompts);
            Shuffle(_remainingPrompts);
        }

        string next = _remainingPrompts[0];
        _remainingPrompts.RemoveAt(0);
        return next;
    }

    private void ShowThinkingAnimation(int seconds)
    {
        string baseText = "Get ready to think";
        string[] frames = { baseText, baseText + ".", baseText + "..", baseText + "..." };
        int maxLength = frames[3].Length;
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < endTime)
        {
            string frame = frames[i % frames.Length].PadRight(maxLength);
            Console.Write(frame);
            Thread.Sleep(500);
            Console.Write(new string('\b', frame.Length));
            i++;
        }

        Console.Write(new string(' ', maxLength));
        Console.Write(new string('\b', maxLength));
    }
}