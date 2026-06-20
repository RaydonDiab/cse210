public class BreathingActivity : Activity
{
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            ShowGrowingBar(4, true);

            Console.WriteLine("Hold...");
            ShowHoldBar(4, true);

            Console.WriteLine("Breathe out...");
            ShowGrowingBar(4, false);

            Console.WriteLine("Hold...");
            ShowHoldBar(4, false);
        }

        DisplayEndingMessage();
    }
}