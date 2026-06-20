// ---------------------------------------------------------------
// Exceeding Requirements:
// 1. Breathing Activity uses box breathing (in - hold - out - hold,
//    4 seconds each) instead of simple in/out, with an animated
//    growing/shrinking bar for breathing and a held bar with a live
//    countdown for the hold phases.
// 2. Reflection Activity uses a shuffle-bag algorithm so no question
//    repeats until all 9 have been shown once per session.
// 3. Listing Activity uses the same shuffle-bag for its prompts, so
//    no prompt repeats until all 5 have been used.
// 4. Listing Activity rejects duplicate entries (case-insensitive)
//    and displays the full list of items back to the user at the
//    end, not just the count.
// 5. Added a 4th activity, Body Scan, which guides the user through
//    progressively tensing and releasing each muscle group head to toe.
// ---------------------------------------------------------------


class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        bool running = true;
        while (running)
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflection activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Start body scan activity");
            Console.WriteLine("  5. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    new BreathingActivity().Run();
                    break;
                case "2":
                    new ReflectionActivity().Run();
                    break;
                case "3":
                    new ListingActivity().Run();
                    break;
                case "4":
                    new BodyScanActivity().Run();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}