using System;
using System.Collections.Generic;

public class Meal
{
    protected string _name;
    protected List<FoodItem> _items;

    public Meal(string name)
    {
        _name = name;
        _items = new List<FoodItem>();
    }

    public string GetName()
    {
        return _name;
    }

    public void AddItem(FoodItem item)
    {
        _items.Add(item);
    }

    public List<FoodItem> GetItems()
    {
        return _items;
    }

    public NutritionSummary GetTotalNutrition()
    {
        NutritionSummary total = new NutritionSummary(0, 0, 0, 0);

        foreach (FoodItem item in _items)
        {
            total = total.Add(item.GetNutritionSummary());
        }

        return total;
    }

    public void PrintSummary()
    {
        Console.WriteLine($"Meal: {_name}");

        foreach (FoodItem item in _items)
        {
            Console.WriteLine("  " + item.GetDetailsString());
        }

        NutritionSummary total = GetTotalNutrition();
        Console.WriteLine("  Total: " + total.GetDetailsString());
    }
}