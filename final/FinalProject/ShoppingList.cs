using System;
using System.Collections.Generic;

public class ShoppingList
{
    protected List<PantryItem> _pantryItems;

    public ShoppingList()
    {
        _pantryItems = new List<PantryItem>();
    }

    public void AddPantryItem(PantryItem item)
    {
        _pantryItems.Add(item);
    }

    // Looks at how much of an ingredient you already have in the pantry.
    // Returns 0 if it's not in the pantry at all.
    protected double GetPantryQuantity(string ingredientName)
    {
        foreach (PantryItem item in _pantryItems)
        {
            if (item.GetName() == ingredientName)
            {
                return item.GetQuantity();
            }
        }

        return 0;
    }

    // Goes through every meal, pulls out every Ingredient (including ones
    // hidden inside a Recipe), and prints how much of each you still need
    // to buy after subtracting what's already in the pantry.
    public void PrintNeededItems(List<Meal> meals)
    {
        Dictionary<string, double> totalsNeeded = new Dictionary<string, double>();

        foreach (Meal meal in meals)
        {
            foreach (FoodItem item in meal.GetItems())
            {
                if (item is Ingredient)
                {
                    Ingredient ingredient = (Ingredient)item;
                    AddToTotals(totalsNeeded, ingredient);
                }
                else if (item is Recipe)
                {
                    Recipe recipe = (Recipe)item;
                    foreach (Ingredient ingredient in recipe.GetIngredients())
                    {
                        AddToTotals(totalsNeeded, ingredient);
                    }
                }
            }
        }

        Console.WriteLine("Shopping List:");

        foreach (string ingredientName in totalsNeeded.Keys)
        {
            double needed = totalsNeeded[ingredientName];
            double haveInPantry = GetPantryQuantity(ingredientName);
            double stillNeeded = needed - haveInPantry;

            if (stillNeeded > 0)
            {
                Console.WriteLine($"  {ingredientName}: {stillNeeded}");
            }
        }
    }

    private void AddToTotals(Dictionary<string, double> totals, Ingredient ingredient)
    {
        string name = ingredient.GetName();
        double quantity = ingredient.GetQuantity();

        if (totals.ContainsKey(name))
        {
            totals[name] = totals[name] + quantity;
        }
        else
        {
            totals[name] = quantity;
        }
    }
}