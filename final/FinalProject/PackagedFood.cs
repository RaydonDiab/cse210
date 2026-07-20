using System;

public class PackagedFood : FoodItem
{
    protected double _servingsConsumed;
    protected double _caloriesPerServing;
    protected double _proteinPerServing;
    protected double _carbsPerServing;
    protected double _fatPerServing;

    public PackagedFood(string name, double servingsConsumed, double caloriesPerServing, double proteinPerServing, double carbsPerServing, double fatPerServing)
        : base(name)
    {
        _servingsConsumed = servingsConsumed;
        _caloriesPerServing = caloriesPerServing;
        _proteinPerServing = proteinPerServing;
        _carbsPerServing = carbsPerServing;
        _fatPerServing = fatPerServing;
    }

    public override NutritionSummary GetNutritionSummary()
    {
        double totalCalories = _caloriesPerServing * _servingsConsumed;
        double totalProtein = _proteinPerServing * _servingsConsumed;
        double totalCarbs = _carbsPerServing * _servingsConsumed;
        double totalFat = _fatPerServing * _servingsConsumed;

        return new NutritionSummary(totalCalories, totalProtein, totalCarbs, totalFat);
    }
}