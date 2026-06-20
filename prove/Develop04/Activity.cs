public class Activity
{
    private string _name;
    private string _description;
    private int _duration;
    private Random _random = new Random();

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"--- {_name} ---");
        Console.WriteLine(_description);
        Console.Write("How long, in seconds, would you like for your session? ");
        _duration = int.Parse(Console.ReadLine());

        Console.WriteLine("Get ready...");
        ShowSpinner(3);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine("Well done!");
        ShowSpinner(2);
        Console.WriteLine($"You have completed the {_name} for {_duration} seconds.");
        ShowSpinner(3);
    }

    protected int GetDuration() => _duration;

    // General-purpose pause animation (used by start/end messages, Reflection)
    protected void ShowSpinner(int seconds)
    {
        string[] frames = { "⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;
        while (DateTime.Now < endTime)
        {
            Console.Write(frames[i % frames.Length]);
            Thread.Sleep(100);
            Console.Write("\b \b");
            i++;
        }
    }

    // Simple numeric countdown, available if any activity wants it
    protected void ShowCountDown(int seconds)
    {
        for (int s = seconds; s > 0; s--)
        {
            Console.Write(s);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    // Used by Breathing for the "breathe in" / "breathe out" phases
    protected void ShowGrowingBar(int seconds, bool growing)
    {
        int barWidth = 20;
        int delay = (seconds * 1000) / barWidth;

        for (int filled = 0; filled <= barWidth; filled++)
        {
            int shown = growing ? filled : barWidth - filled;
            string bar = "[" + new string('■', shown) + new string(' ', barWidth - shown) + "]";

            Console.Write(bar);
            Thread.Sleep(delay);
            Console.Write(new string('\b', bar.Length));
        }

        Console.Write(new string(' ', barWidth + 2));
        Console.Write(new string('\b', barWidth + 2));
    }

    // Used by Breathing for the "hold" phases, with a ticking countdown
    protected void ShowHoldBar(int seconds, bool filled)
    {
        int barWidth = 20;
        int shown = filled ? barWidth : 0;
        string bar = "[" + new string('■', shown) + new string(' ', barWidth - shown) + "]";

        for (int s = seconds; s > 0; s--)
        {
            string frame = bar + " (" + s + ")";
            Console.Write(frame);
            Thread.Sleep(1000);
            Console.Write(new string('\b', frame.Length));
            Console.Write(new string(' ', frame.Length));
            Console.Write(new string('\b', frame.Length));
        }
    }

    // Shared by Reflection (questions) and Listing (prompts)
    protected void Shuffle(List<string> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}