class Journal
{
    public List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void DisplayEntries()
    {
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine("Date,Prompt,Response,Mood");
            foreach (Entry entry in _entries)
            {
                string date = $"\"{entry._date}\"";
                string prompt = $"\"{entry._prompt.Replace("\"", "\"\"")}\"";
                string response = $"\"{entry._response.Replace("\"", "\"\"")}\"";
                string mood = $"\"{entry._mood}\"";
                outputFile.WriteLine($"{date},{prompt},{response},{mood}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        _entries.Clear();
        string[] lines = System.IO.File.ReadAllLines(filename);

        // Skip the header line
        for (int i = 1; i < lines.Length; i++)
        {
            // Remove the leading and trailing quote, then split
            string trimmed = lines[i].Trim('"');
            string[] parts = trimmed.Split("\",\"");

            if (parts.Length < 4) continue; // skip malformed lines

            Entry entry = new Entry();
            entry._date = parts[0];
            entry._prompt = parts[1].Replace("\"\"", "\"");
            entry._response = parts[2].Replace("\"\"", "\"");
            entry._mood = parts[3];

            _entries.Add(entry);
        }
    }

    public void DisplayStreak()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries yet!");
            return;
        }

        int streak = 1;
        for (int i = _entries.Count - 1; i > 0; i--)
        {
            DateTime current = DateTime.Parse(_entries[i]._date);
            DateTime previous = DateTime.Parse(_entries[i - 1]._date);

            if ((current - previous).Days == 1)
            {
                streak++;
            }
            else
            {
                break;
            }
        }

        Console.WriteLine($"Current streak: {streak} day(s)!");
    }
}