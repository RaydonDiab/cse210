class Entry
{
    // attributes
    public string _date;
    public string _response;
    public string _prompt;

    public string _mood;

    // behaviors
    public void Display()
    {
        Console.WriteLine($"{_date} -- {_prompt} \n {_response} \n Mood: {_mood}");
    }
}