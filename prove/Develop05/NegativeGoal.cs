using System;

// Bonus feature: a goal that SUBTRACTS points when recorded (e.g. "Skipped Workout",
// "Ate Junk Food"). Lets users track habits they're trying to avoid.
public class NegativeGoal : Goal
{
    public NegativeGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return -_points;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal:{_shortName}:{_description}:{_points}";
    }

    public static NegativeGoal CreateFromString(string[] parts)
    {
        string name = parts[1];
        string description = parts[2];
        int points = int.Parse(parts[3]);
        return new NegativeGoal(name, description, points);
    }
}