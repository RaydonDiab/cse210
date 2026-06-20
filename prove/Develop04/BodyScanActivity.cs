public class BodyScanActivity : Activity
{
    private List<string> _bodyParts = new List<string>
    {
        "feet",
        "calves",
        "thighs",
        "stomach",
        "chest",
        "shoulders",
        "arms",
        "hands",
        "neck and jaw",
        "face and forehead"
    };

    public BodyScanActivity() : base(
        "Body Scan Activity",
        "This activity will help you relax by guiding you to systematically tense and release each muscle group in your body, releasing physical tension and promoting deep relaxation.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
        int index = 0;
        while (DateTime.Now < endTime)
        {
            string part = _bodyParts[index % _bodyParts.Count];

            Console.WriteLine($"Tense your {part}...");
            ShowHoldBar(3, true);

            Console.WriteLine($"Now release your {part}...");
            ShowHoldBar(3, false);

            index++;
        }

        DisplayEndingMessage();
    }
}