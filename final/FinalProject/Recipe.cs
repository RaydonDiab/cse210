using System;
using System.Collections.Generic;

public class Recipe : FoodItem
{
    protected List<Ingredient> _ingredients;

    public Recipe(string name) : base(name)
    {
        _ingredients = new List<Ingredient>();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
    }

    public List<Ingredient> GetIngredients()
    {
        return _ingredients;
    }

    public override NutritionSummary GetNutritionSummary()
    {
        NutritionSummary total = new NutritionSummary(0, 0, 0, 0);

        foreach (Ingredient ingredient in _ingredients)
        {
            total = total.Add(ingredient.GetNutritionSummary());
        }

        return total;
    }
}