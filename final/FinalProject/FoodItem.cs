using System;

public abstract class FoodItem
{
    protected string _name;

    public FoodItem(string name)
    {
        _name = name;
    }

    public string GetName()
    {
        return _name;
    }

    // Every subclass has to figure out its own nutrition numbers.
    public abstract NutritionSummary GetNutritionSummary();

    public virtual string GetDetailsString()
    {
        NutritionSummary summary = GetNutritionSummary();
        return $"{_name}: {summary.GetDetailsString()}";
    }
}