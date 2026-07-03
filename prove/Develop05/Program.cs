using System;

/*
 * ------------------------------------------------------------------
 * BONUS FEATURES (beyond the base requirements):
 *
 * 1. LEVELS: The player's score now translates into a "Level"
 *    (1 level per 1000 points), displayed at the top of the menu
 *    each time it's shown. See GoalManager.DisplayPlayerInfo().
 *
 * 2. NEGATIVE GOALS: A new goal type, NegativeGoal, was added so
 *    users can track habits they want to AVOID (e.g. "Skipped
 *    Workout", "Ate Junk Food"). Recording one of these subtracts
 *    points instead of adding them. See NegativeGoal.cs.
 * ------------------------------------------------------------------
 */

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Eternal Quest!");
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}