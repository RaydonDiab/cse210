public class Scripture
{
    private ScriptureReference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    public Scripture(ScriptureReference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void HideRandomWords(int count = 3)
    {
        List<Word> visible = _words.Where(w => !w.IsHidden()).ToList();
        int toHide = Math.Min(count, visible.Count);

        for (int i = 0; i < toHide; i++)
        {
            int index = _random.Next(visible.Count);
            visible[index].Hide();
            visible.RemoveAt(index);
        }
    }

    public void RevealRandomWords(int count = 3)
    {
        List<Word> hidden = _words.Where(w => w.IsHidden()).ToList();
        int toReveal = Math.Min(count, hidden.Count);

        for (int i = 0; i < toReveal; i++)
        {
            int index = _random.Next(hidden.Count);
            hidden[index].Reveal();
            hidden.RemoveAt(index);
        }
    }

    public void Reset()
    {
        foreach (Word word in _words)
            word.Reveal();
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }

    public bool HasHiddenWords()
    {
        return _words.Any(w => w.IsHidden());
    }

    public string GetDisplayText()
    {
        string reference = _reference.GetDisplayText();
        string text = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{reference}\n{text}";
    }

    public string GetRawText()
    {
        return string.Join(" ", _words.Select(w => w.GetRawText()));
    }

    public string GetReferenceText()
    {
        return _reference.GetDisplayText();
    }

    public int GetWordCount()
    {
        return _words.Count;
    }
}