using System;
using System.IO;
using System.Collections.Generic;

public class FileStorage
{
    private const string IngredientsFile = "ingredients.txt";
    private const string RecipesFile = "recipes.txt";
    private const string LogFile = "log.txt";

    // ---------- Ingredient library ----------

    // Each ingredient becomes one line: name,caloriesPerUnit,proteinPerUnit,carbsPerUnit,fatPerUnit
    public void SaveIngredientLibrary(List<Ingredient> ingredientLibrary)
    {
        List<string> lines = new List<string>();

        foreach (Ingredient ingredient in ingredientLibrary)
        {
            string line = ingredient.GetName() + "," +
                ingredient.GetCaloriesPerUnit() + "," +
                ingredient.GetProteinPerUnit() + "," +
                ingredient.GetCarbsPerUnit() + "," +
                ingredient.GetFatPerUnit();

            lines.Add(line);
        }

        File.WriteAllLines(IngredientsFile, lines);
    }

    public List<Ingredient> LoadIngredientLibrary()
    {
        List<Ingredient> ingredientLibrary = new List<Ingredient>();

        if (!File.Exists(IngredientsFile))
        {
            return ingredientLibrary;
        }

        string[] lines = File.ReadAllLines(IngredientsFile);

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');

            string name = parts[0];
            double calories = double.Parse(parts[1]);
            double protein = double.Parse(parts[2]);
            double carbs = double.Parse(parts[3]);
            double fat = double.Parse(parts[4]);

            Ingredient ingredient = new Ingredient(name, 1, calories, protein, carbs, fat);
            ingredientLibrary.Add(ingredient);
        }

        return ingredientLibrary;
    }

    // ---------- Recipes ----------

    // Each recipe becomes one line: name|ing1Name,qty,cal,pro,carb,fat;ing2Name,qty,cal,pro,carb,fat;...
    public void SaveRecipes(List<Recipe> recipes)
    {
        List<string> lines = new List<string>();

        foreach (Recipe recipe in recipes)
        {
            List<string> ingredientParts = new List<string>();

            foreach (Ingredient ingredient in recipe.GetIngredients())
            {
                string part = ingredient.GetName() + "," +
                    ingredient.GetQuantity() + "," +
                    ingredient.GetCaloriesPerUnit() + "," +
                    ingredient.GetProteinPerUnit() + "," +
                    ingredient.GetCarbsPerUnit() + "," +
                    ingredient.GetFatPerUnit();

                ingredientParts.Add(part);
            }

            string line = recipe.GetName() + "|" + string.Join(";", ingredientParts);
            lines.Add(line);
        }

        File.WriteAllLines(RecipesFile, lines);
    }

    public List<Recipe> LoadRecipes()
    {
        List<Recipe> recipes = new List<Recipe>();

        if (!File.Exists(RecipesFile))
        {
            return recipes;
        }

        string[] lines = File.ReadAllLines(RecipesFile);

        foreach (string line in lines)
        {
            string[] mainParts = line.Split('|');
            string recipeName = mainParts[0];

            Recipe recipe = new Recipe(recipeName);

            if (mainParts.Length > 1 && mainParts[1] != "")
            {
                string[] ingredientChunks = mainParts[1].Split(';');

                foreach (string chunk in ingredientChunks)
                {
                    string[] fields = chunk.Split(',');

                    string ingName = fields[0];
                    double quantity = double.Parse(fields[1]);
                    double calories = double.Parse(fields[2]);
                    double protein = double.Parse(fields[3]);
                    double carbs = double.Parse(fields[4]);
                    double fat = double.Parse(fields[5]);

                    Ingredient ingredient = new Ingredient(ingName, quantity, calories, protein, carbs, fat);
                    recipe.AddIngredient(ingredient);
                }
            }

            recipes.Add(recipe);
        }

        return recipes;
    }

    // ---------- Consumption log ----------

    // Each log entry becomes one line: date,mealName,calories,protein,carbs,fat
    public void SaveConsumptionLog(List<ConsumptionEntry> consumptionLog)
    {
        List<string> lines = new List<string>();

        foreach (ConsumptionEntry entry in consumptionLog)
        {
            NutritionSummary totals = entry.GetTotals();

            string line = entry.GetDate() + "," +
                entry.GetMealName() + "," +
                totals.GetCalories() + "," +
                totals.GetProtein() + "," +
                totals.GetCarbs() + "," +
                totals.GetFat();

            lines.Add(line);
        }

        File.WriteAllLines(LogFile, lines);
    }

    public List<ConsumptionEntry> LoadConsumptionLog()
    {
        List<ConsumptionEntry> consumptionLog = new List<ConsumptionEntry>();

        if (!File.Exists(LogFile))
        {
            return consumptionLog;
        }

        string[] lines = File.ReadAllLines(LogFile);

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');

            string date = parts[0];
            string mealName = parts[1];
            double calories = double.Parse(parts[2]);
            double protein = double.Parse(parts[3]);
            double carbs = double.Parse(parts[4]);
            double fat = double.Parse(parts[5]);

            NutritionSummary totals = new NutritionSummary(calories, protein, carbs, fat);
            ConsumptionEntry entry = new ConsumptionEntry(date, mealName, totals);
            consumptionLog.Add(entry);
        }

        return consumptionLog;
    }
}