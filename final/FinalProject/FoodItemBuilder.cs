using System;
using System.Collections.Generic;

public class FoodItemBuilder
{
    protected InputHelper _input;

    public FoodItemBuilder(InputHelper input)
    {
        _input = input;
    }

    // Asks whether to reuse a saved ingredient or type a brand new one.
    public Ingredient PickIngredient(List<Ingredient> ingredientLibrary, out bool cancelled)
    {
        Console.WriteLine("1. Choose from your ingredient library");
        Console.WriteLine("2. Enter a brand new ingredient");

        string choice;
        if (!_input.ReadString("Choice: ", out choice))
        {
            cancelled = true;
            return null;
        }

        if (choice == "1")
        {
            return PickFromLibrary(ingredientLibrary, out cancelled);
        }

        return EnterNewIngredient(out cancelled);
    }

    private Ingredient PickFromLibrary(List<Ingredient> ingredientLibrary, out bool cancelled)
    {
        if (ingredientLibrary.Count == 0)
        {
            Console.WriteLine("Your ingredient library is empty. Let's enter a new one instead.");
            return EnterNewIngredient(out cancelled);
        }

        Console.WriteLine("Which ingredient?");
        for (int i = 0; i < ingredientLibrary.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {ingredientLibrary[i].GetName()}");
        }

        string input;
        if (!_input.ReadString("Enter a number: ", out input))
        {
            cancelled = true;
            return null;
        }

        int index;
        if (!int.TryParse(input, out index) || index < 1 || index > ingredientLibrary.Count)
        {
            Console.WriteLine("That's not a valid ingredient number.");
            cancelled = true;
            return null;
        }

        Ingredient template = ingredientLibrary[index - 1];

        double quantity;
        if (!_input.ReadDouble("Quantity: ", out quantity))
        {
            cancelled = true;
            return null;
        }

        cancelled = false;
        return new Ingredient(
            template.GetName(),
            quantity,
            template.GetCaloriesPerUnit(),
            template.GetProteinPerUnit(),
            template.GetCarbsPerUnit(),
            template.GetFatPerUnit()
        );
    }

    public Ingredient EnterNewIngredient(out bool cancelled)
    {
        string name;
        if (!_input.ReadString("Ingredient name: ", out name)) { cancelled = true; return null; }

        double quantity;
        if (!_input.ReadDouble("Quantity: ", out quantity)) { cancelled = true; return null; }

        double calories;
        if (!_input.ReadDouble("Calories per unit: ", out calories)) { cancelled = true; return null; }

        double protein;
        if (!_input.ReadDouble("Protein per unit (g): ", out protein)) { cancelled = true; return null; }

        double carbs;
        if (!_input.ReadDouble("Carbs per unit (g): ", out carbs)) { cancelled = true; return null; }

        double fat;
        if (!_input.ReadDouble("Fat per unit (g): ", out fat)) { cancelled = true; return null; }

        cancelled = false;
        return new Ingredient(name, quantity, calories, protein, carbs, fat);
    }

    // Used only by "Add ingredient to library" - no quantity, since a library
    // entry is a per-unit template, not tied to a specific amount eaten.
    public Ingredient ReadLibraryIngredient(out bool cancelled)
    {
        string name;
        if (!_input.ReadString("Ingredient name: ", out name)) { cancelled = true; return null; }

        double calories;
        if (!_input.ReadDouble("Calories per unit: ", out calories)) { cancelled = true; return null; }

        double protein;
        if (!_input.ReadDouble("Protein per unit (g): ", out protein)) { cancelled = true; return null; }

        double carbs;
        if (!_input.ReadDouble("Carbs per unit (g): ", out carbs)) { cancelled = true; return null; }

        double fat;
        if (!_input.ReadDouble("Fat per unit (g): ", out fat)) { cancelled = true; return null; }

        cancelled = false;
        return new Ingredient(name, 1, calories, protein, carbs, fat);
    }

    public PackagedFood ReadPackagedFood(out bool cancelled)
    {
        string name;
        if (!_input.ReadString("Packaged food name: ", out name)) { cancelled = true; return null; }

        double servings;
        if (!_input.ReadDouble("Servings consumed: ", out servings)) { cancelled = true; return null; }

        double calories;
        if (!_input.ReadDouble("Calories per serving: ", out calories)) { cancelled = true; return null; }

        double protein;
        if (!_input.ReadDouble("Protein per serving (g): ", out protein)) { cancelled = true; return null; }

        double carbs;
        if (!_input.ReadDouble("Carbs per serving (g): ", out carbs)) { cancelled = true; return null; }

        double fat;
        if (!_input.ReadDouble("Fat per serving (g): ", out fat)) { cancelled = true; return null; }

        cancelled = false;
        return new PackagedFood(name, servings, calories, protein, carbs, fat);
    }
}