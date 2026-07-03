using System;

public abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    public string GetName()
    {
        return _shortName;
    }

    public abstract int RecordEvent();

    public abstract bool IsComplete();

    public virtual string GetDetailsString()
    {
        string check = IsComplete() ? "X" : " ";
        return $"[{check}] {_shortName} ({_description})";
    }

    // Used for saving to file. Each subclass adds its own type + extra fields.
    public abstract string GetStringRepresentation();
}