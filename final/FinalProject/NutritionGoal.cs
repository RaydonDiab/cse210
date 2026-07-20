using System;

public class NutritionGoal
{
    protected double _calorieTarget;

    public NutritionGoal(double calorieTarget)
    {
        _calorieTarget = calorieTarget;
    }

    public double GetCalorieTarget()
    {
        return _calorieTarget;
    }

    public string CheckGoal(NutritionSummary actual)
    {
        double actualCalories = actual.GetCalories();

        if (actualCalories <= _calorieTarget)
        {
            double remaining = _calorieTarget - actualCalories;
            return $"You are {remaining} calories under your goal of {_calorieTarget}.";
        }
        else
        {
            double over = actualCalories - _calorieTarget;
            return $"You are {over} calories over your goal of {_calorieTarget}.";
        }
    }
}