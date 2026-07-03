using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        int choice = -1;
        while (choice != 6)
        {
            DisplayPlayerInfo();
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");

            string input = Console.ReadLine();

            if (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Please enter a valid number.\n");
                continue;
            }

            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    CreateGoal();
                    break;
                case 2:
                    ListGoalDetails();
                    break;
                case 3:
                    SaveGoals();
                    break;
                case 4:
                    LoadGoals();
                    break;
                case 5:
                    RecordEvent();
                    break;
                case 6:
                    Console.WriteLine("Farewell, adventurer.");
                    break;
                default:
                    Console.WriteLine("Not a valid option, try again.\n");
                    break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        // Bonus feature: levels up every 1000 points, printed here.
        int level = (_score / 1000) + 1;
        Console.WriteLine($"\nYou have {_score} points. (Level {level})");
    }

    public void ListGoalDetails()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet.\n");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
        Console.WriteLine();
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal (one-time)");
        Console.WriteLine("  2. Eternal Goal (never-ending)");
        Console.WriteLine("  3. Checklist Goal (multiple completions with bonus)");
        Console.WriteLine("  4. Negative Goal (subtracts points, for bad habits)");
        Console.Write("Which type of goal would you like to create? ");
        string typeChoice = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();

        Console.Write("What is the amount of points associated with this goal? ");
        int points = ReadInt();

        switch (typeChoice)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, description, points));
                break;
            case "2":
                _goals.Add(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int target = ReadInt();
                Console.Write("What is the bonus for accomplishing it that many times? ");
                int bonus = ReadInt();
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;
            case "4":
                _goals.Add(new NegativeGoal(name, description, points));
                break;
            default:
                Console.WriteLine("Not a valid goal type. Goal not created.\n");
                return;
        }

        Console.WriteLine("Goal created!\n");
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals to record.\n");
            return;
        }

        ListGoalDetails();
        Console.Write("Which goal did you accomplish? ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int index) && index >= 1 && index <= _goals.Count)
        {
            Goal goal = _goals[index - 1];
            int earned = goal.RecordEvent();
            _score += earned;

            if (earned >= 0)
            {
                Console.WriteLine($"Congratulations! You earned {earned} points!\n");
            }
            else
            {
                Console.WriteLine($"Recorded. You lost {-earned} points.\n");
            }
        }
        else
        {
            Console.WriteLine("Not a valid goal selection.\n");
        }
    }

    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved.\n");
    }

    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("That file does not exist.\n");
            return;
        }

        _goals.Clear();
        string[] lines = File.ReadAllLines(filename);

        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(":");
            string type = parts[0];

            switch (type)
            {
                case "SimpleGoal":
                    _goals.Add(SimpleGoal.CreateFromString(parts));
                    break;
                case "EternalGoal":
                    _goals.Add(EternalGoal.CreateFromString(parts));
                    break;
                case "ChecklistGoal":
                    _goals.Add(ChecklistGoal.CreateFromString(parts));
                    break;
                case "NegativeGoal":
                    _goals.Add(NegativeGoal.CreateFromString(parts));
                    break;
                default:
                    Console.WriteLine($"Unknown goal type in file: {type}");
                    break;
            }
        }

        Console.WriteLine("Goals loaded.\n");
    }

    private int ReadInt()
    {
        int result;
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.Write("Please enter a valid number: ");
        }
        return result;
    }
}