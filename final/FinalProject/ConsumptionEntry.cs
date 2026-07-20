using System;

public class ConsumptionEntry
{
    protected string _date;
    protected string _mealName;
    protected NutritionSummary _totals;

    public ConsumptionEntry(string date, string mealName, NutritionSummary totals)
    {
        _date = date;
        _mealName = mealName;
        _totals = totals;
    }

    public string GetDate()
    {
        return _date;
    }

    public string GetMealName()
    {
        return _mealName;
    }

    public NutritionSummary GetTotals()
    {
        return _totals;
    }

    public string GetDetailsString()
    {
        return $"{_date} - {_mealName}: {_totals.GetDetailsString()}";
    }
}