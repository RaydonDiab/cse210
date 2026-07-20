using System;

public class Ingredient : FoodItem
{
    protected double _quantity;
    protected double _caloriesPerUnit;
    protected double _proteinPerUnit;
    protected double _carbsPerUnit;
    protected double _fatPerUnit;

    public Ingredient(string name, double quantity, double caloriesPerUnit, double proteinPerUnit, double carbsPerUnit, double fatPerUnit)
        : base(name)
    {
        _quantity = quantity;
        _caloriesPerUnit = caloriesPerUnit;
        _proteinPerUnit = proteinPerUnit;
        _carbsPerUnit = carbsPerUnit;
        _fatPerUnit = fatPerUnit;
    }

    public double GetQuantity()
    {
        return _quantity;
    }

    public double GetCaloriesPerUnit()
    {
        return _caloriesPerUnit;
    }

    public double GetProteinPerUnit()
    {
        return _proteinPerUnit;
    }

    public double GetCarbsPerUnit()
    {
        return _carbsPerUnit;
    }

    public double GetFatPerUnit()
    {
        return _fatPerUnit;
    }

    public override NutritionSummary GetNutritionSummary()
    {
        double totalCalories = _caloriesPerUnit * _quantity;
        double totalProtein = _proteinPerUnit * _quantity;
        double totalCarbs = _carbsPerUnit * _quantity;
        double totalFat = _fatPerUnit * _quantity;

        return new NutritionSummary(totalCalories, totalProtein, totalCarbs, totalFat);
    }
}