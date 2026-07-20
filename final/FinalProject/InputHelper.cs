using System;

public class InputHelper
{
    // Reads a line of text. Returns false (and no value) if the user typed "exit".
    public bool ReadString(string prompt, out string value)
    {
        Console.Write(prompt);
        value = Console.ReadLine();

        if (value != null && value.Trim().ToLower() == "exit")
        {
            return false;
        }

        return true;
    }

    // Reads a number, re-asking on a bad value. Returns false if the user typed "exit".
    public bool ReadDouble(string prompt, out double value)
    {
        while (true)
        {
            string input;
            if (!ReadString(prompt, out input))
            {
                value = 0;
                return false;
            }

            if (double.TryParse(input, out value))
            {
                return true;
            }

            Console.WriteLine("That's not a valid number. Try again, or type 'exit' to cancel.");
        }
    }
}