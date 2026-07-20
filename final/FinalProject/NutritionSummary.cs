using System;

public class NutritionSummary
{
    private double _calories;
    private double _protein;
    private double _carbs;
    private double _fat;

    public NutritionSummary(double calories, double protein, double carbs, double fat)
    {
        _calories = calories;
        _protein = protein;
        _carbs = carbs;
        _fat = fat;
    }

    public double GetCalories()
    {
        return _calories;
    }

    public double GetProtein()
    {
        return _protein;
    }

    public double GetCarbs()
    {
        return _carbs;
    }

    public double GetFat()
    {
        return _fat;
    }

    // Adds another NutritionSummary to this one and gives back a new combined total.
    public NutritionSummary Add(NutritionSummary other)
    {
        double totalCalories = _calories + other.GetCalories();
        double totalProtein = _protein + other.GetProtein();
        double totalCarbs = _carbs + other.GetCarbs();
        double totalFat = _fat + other.GetFat();

        return new NutritionSummary(totalCalories, totalProtein, totalCarbs, totalFat);
    }

    public string GetDetailsString()
    {
        return $"{_calories} calories, {_protein}g protein, {_carbs}g carbs, {_fat}g fat";
    }
}