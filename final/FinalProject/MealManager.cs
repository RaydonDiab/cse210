using System;
using System.Collections.Generic;

public class MealManager
{
    protected List<Meal> _meals;
    protected List<Recipe> _recipes;
    protected List<Ingredient> _ingredientLibrary;
    protected List<PantryItem> _pantryItems;
    protected List<ConsumptionEntry> _consumptionLog;
    protected NutritionGoal _goal;
    protected FileStorage _storage;
    protected InputHelper _input;
    protected FoodItemBuilder _foodBuilder;

    public MealManager()
    {
        _meals = new List<Meal>();
        _recipes = new List<Recipe>();
        _ingredientLibrary = new List<Ingredient>();
        _pantryItems = new List<PantryItem>();
        _consumptionLog = new List<ConsumptionEntry>();
        _goal = new NutritionGoal(2000);
        _storage = new FileStorage();
        _input = new InputHelper();
        _foodBuilder = new FoodItemBuilder(_input);
    }

    public void Start()
    {
        LoadAllData();
        Console.WriteLine($"Loaded {_ingredientLibrary.Count} library ingredients, {_recipes.Count} recipes, and {_consumptionLog.Count} log entries.");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();

        string choice = "";

        while (choice != "18")
        {
            Console.Clear();
            DisplayMenu();
            choice = Console.ReadLine();
            Console.Clear();

            if (choice == "1")
            {
                CreateMeal();
            }
            else if (choice == "2")
            {
                AddIngredientToMeal();
            }
            else if (choice == "3")
            {
                AddPackagedFoodToMeal();
            }
            else if (choice == "4")
            {
                CreateRecipe();
            }
            else if (choice == "5")
            {
                AddRecipeToMeal();
            }
            else if (choice == "6")
            {
                ViewRecipes();
            }
            else if (choice == "7")
            {
                AddIngredientToLibrary();
            }
            else if (choice == "8")
            {
                ViewIngredientLibrary();
            }
            else if (choice == "9")
            {
                LogMeal();
            }
            else if (choice == "10")
            {
                ViewConsumptionLog();
            }
            else if (choice == "11")
            {
                ViewAllMeals();
            }
            else if (choice == "12")
            {
                SetNutritionGoal();
            }
            else if (choice == "13")
            {
                CheckGoal();
            }
            else if (choice == "14")
            {
                AddPantryItem();
            }
            else if (choice == "15")
            {
                ViewPantry();
            }
            else if (choice == "16")
            {
                ViewShoppingList();
            }
            else if (choice == "17")
            {
                SaveNow();
            }
            else if (choice == "18")
            {
                SaveAllData();
                Console.WriteLine("Saved. Goodbye!");
            }
            else
            {
                Console.WriteLine("That's not a valid choice. Try again.");
            }

            if (choice != "18")
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to return to the menu...");
                Console.ReadLine();
            }
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("=== Calorie Tracker Menu ===");
        Console.WriteLine("1. Create a new meal");
        Console.WriteLine("2. Add an ingredient to a meal");
        Console.WriteLine("3. Add a packaged food to a meal");
        Console.WriteLine("4. Create a recipe");
        Console.WriteLine("5. Add a recipe to a meal");
        Console.WriteLine("6. View recipes");
        Console.WriteLine("7. Add ingredient to library");
        Console.WriteLine("8. View ingredient library");
        Console.WriteLine("9. Log a meal as consumed");
        Console.WriteLine("10. View consumption log");
        Console.WriteLine("11. View all meals and totals");
        Console.WriteLine("12. Set your nutrition goal");
        Console.WriteLine("13. Check today's totals against your goal");
        Console.WriteLine("14. Add a pantry item");
        Console.WriteLine("15. View pantry");
        Console.WriteLine("16. View shopping list");
        Console.WriteLine("17. Save all data now");
        Console.WriteLine("18. Quit (also saves)");
        Console.WriteLine("(Inside any option, type 'exit' at a prompt to cancel and come back here)");
        Console.Write("Choose an option: ");
    }

    private void PrintCancelled()
    {
        Console.WriteLine("Cancelled. Nothing was saved.");
    }

    // ---------- Saving and loading files ----------

    private void LoadAllData()
    {
        _ingredientLibrary = _storage.LoadIngredientLibrary();
        _recipes = _storage.LoadRecipes();
        _consumptionLog = _storage.LoadConsumptionLog();
    }

    private void SaveAllData()
    {
        _storage.SaveIngredientLibrary(_ingredientLibrary);
        _storage.SaveRecipes(_recipes);
        _storage.SaveConsumptionLog(_consumptionLog);
    }

    private void SaveNow()
    {
        Console.WriteLine("=== Save All Data ===");
        Console.WriteLine("Writes your ingredient library, recipes, and consumption log to files.");
        Console.WriteLine();

        SaveAllData();
        Console.WriteLine("Saved.");
    }

    // ---------- Consumption log ----------

    private void LogMeal()
    {
        Console.WriteLine("=== Log a Meal as Consumed ===");
        Console.WriteLine("Records this meal's totals with today's date into your permanent");
        Console.WriteLine("consumption log, which gets saved to a file.");
        Console.WriteLine();

        Meal meal = ChooseMeal();
        if (meal == null)
        {
            PrintCancelled();
            return;
        }

        string date = DateTime.Now.ToString("d");
        NutritionSummary totals = meal.GetTotalNutrition();

        ConsumptionEntry entry = new ConsumptionEntry(date, meal.GetName(), totals);
        _consumptionLog.Add(entry);

        Console.WriteLine($"Logged {meal.GetName()} on {date}.");
    }

    private void ViewConsumptionLog()
    {
        Console.WriteLine("=== Consumption Log ===");
        Console.WriteLine("Every meal you've logged as eaten, across every day you've used this app.");
        Console.WriteLine();

        if (_consumptionLog.Count == 0)
        {
            Console.WriteLine("Your log is empty.");
            return;
        }

        foreach (ConsumptionEntry entry in _consumptionLog)
        {
            Console.WriteLine(entry.GetDetailsString());
        }
    }

    // ---------- Meal and Recipe selection ----------

    private void CreateMeal()
    {
        Console.WriteLine("=== Create a New Meal ===");
        Console.WriteLine("A meal is a container, like a plate - for example \"Breakfast\" or \"Lunch\".");
        Console.WriteLine("It has no calories by itself. Afterward, use the other menu options to add");
        Console.WriteLine("ingredients, packaged foods, or recipes to it.");
        Console.WriteLine();

        string name;
        if (!_input.ReadString("What is the name of the meal? ", out name))
        {
            PrintCancelled();
            return;
        }

        Meal meal = new Meal(name);
        _meals.Add(meal);

        Console.WriteLine($"Created meal: {name}");
    }

    private Meal ChooseMeal()
    {
        if (_meals.Count == 0)
        {
            Console.WriteLine("You don't have any meals yet. Create one first (option 1).");
            return null;
        }

        Console.WriteLine("Which meal?");
        for (int i = 0; i < _meals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_meals[i].GetName()}");
        }

        string input;
        if (!_input.ReadString("Enter a number: ", out input))
        {
            return null;
        }

        int index;
        if (!int.TryParse(input, out index) || index < 1 || index > _meals.Count)
        {
            Console.WriteLine("That's not a valid meal number.");
            return null;
        }

        return _meals[index - 1];
    }

    private Recipe ChooseRecipe()
    {
        if (_recipes.Count == 0)
        {
            Console.WriteLine("You don't have any recipes yet. Create one first (option 4).");
            return null;
        }

        Console.WriteLine("Which recipe?");
        for (int i = 0; i < _recipes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_recipes[i].GetName()}");
        }

        string input;
        if (!_input.ReadString("Enter a number: ", out input))
        {
            return null;
        }

        int index;
        if (!int.TryParse(input, out index) || index < 1 || index > _recipes.Count)
        {
            Console.WriteLine("That's not a valid recipe number.");
            return null;
        }

        return _recipes[index - 1];
    }

    // ---------- Ingredients ----------

    private void AddIngredientToMeal()
    {
        Console.WriteLine("=== Add an Ingredient to a Meal ===");
        Console.WriteLine("An ingredient is a raw food item, like \"2 eggs\" or \"1 cup of rice\".");
        Console.WriteLine();

        Meal meal = ChooseMeal();
        if (meal == null)
        {
            PrintCancelled();
            return;
        }

        bool addedAny = false;
        string addMore = "yes";
        while (addMore == "yes")
        {
            bool cancelled;
            Ingredient ingredient = _foodBuilder.PickIngredient(_ingredientLibrary, out cancelled);
            if (cancelled)
            {
                if (addedAny)
                {
                    Console.WriteLine("Stopped. Earlier ingredients you added are still saved.");
                }
                else
                {
                    PrintCancelled();
                }
                return;
            }

            meal.AddItem(ingredient);
            addedAny = true;
            Console.WriteLine($"Added {ingredient.GetName()} to {meal.GetName()}.");
            Console.WriteLine();

            if (!_input.ReadString("Add another ingredient to this meal? (yes/no): ", out addMore))
            {
                Console.WriteLine("Stopped. Earlier ingredients you added are still saved.");
                return;
            }
        }
    }

    private void AddIngredientToLibrary()
    {
        Console.WriteLine("=== Add Ingredient to Library ===");
        Console.WriteLine("Save an ingredient's nutrition facts (per unit) so you can reuse it");
        Console.WriteLine("later without retyping the numbers every time.");
        Console.WriteLine();

        bool cancelled;
        Ingredient ingredient = _foodBuilder.ReadLibraryIngredient(out cancelled);
        if (cancelled)
        {
            PrintCancelled();
            return;
        }

        _ingredientLibrary.Add(ingredient);
        Console.WriteLine($"Saved {ingredient.GetName()} to your ingredient library.");
    }

    private void ViewIngredientLibrary()
    {
        Console.WriteLine("=== Ingredient Library ===");
        Console.WriteLine("Reusable ingredients you've saved (nutrition shown per unit).");
        Console.WriteLine();

        if (_ingredientLibrary.Count == 0)
        {
            Console.WriteLine("Your ingredient library is empty.");
            return;
        }

        foreach (Ingredient ingredient in _ingredientLibrary)
        {
            Console.WriteLine(ingredient.GetDetailsString());
        }
    }

    // ---------- Packaged Food ----------

    private void AddPackagedFoodToMeal()
    {
        Console.WriteLine("=== Add a Packaged Food to a Meal ===");
        Console.WriteLine("A packaged food is a store-bought item with a nutrition label,");
        Console.WriteLine("like a granola bar or a bag of chips.");
        Console.WriteLine();

        Meal meal = ChooseMeal();
        if (meal == null)
        {
            PrintCancelled();
            return;
        }

        bool cancelled;
        PackagedFood food = _foodBuilder.ReadPackagedFood(out cancelled);
        if (cancelled)
        {
            PrintCancelled();
            return;
        }

        meal.AddItem(food);
        Console.WriteLine($"Added {food.GetName()} to {meal.GetName()}.");
    }

    // ---------- Recipes ----------

    private void CreateRecipe()
    {
        Console.WriteLine("=== Create a Recipe ===");
        Console.WriteLine("A recipe is made up of several ingredients, like \"Chicken Stir Fry\"");
        Console.WriteLine("being rice, chicken, and broccoli. Its nutrition is the sum of all");
        Console.WriteLine("the ingredients you add to it.");
        Console.WriteLine();

        string name;
        if (!_input.ReadString("What is the name of the recipe? ", out name))
        {
            PrintCancelled();
            return;
        }

        Recipe recipe = new Recipe(name);

        string addMore = "yes";
        while (addMore == "yes")
        {
            bool cancelled;
            Ingredient ingredient = _foodBuilder.PickIngredient(_ingredientLibrary, out cancelled);
            if (cancelled)
            {
                PrintCancelled();
                return;
            }

            recipe.AddIngredient(ingredient);

            if (!_input.ReadString("Add another ingredient to this recipe? (yes/no): ", out addMore))
            {
                PrintCancelled();
                return;
            }
        }

        _recipes.Add(recipe);
        Console.WriteLine($"Created recipe: {name}");
    }

    private void AddRecipeToMeal()
    {
        Console.WriteLine("=== Add a Recipe to a Meal ===");
        Console.WriteLine("This takes a recipe you've already built and adds the whole thing");
        Console.WriteLine("to a meal at once.");
        Console.WriteLine();

        Meal meal = ChooseMeal();
        if (meal == null)
        {
            PrintCancelled();
            return;
        }

        Recipe recipe = ChooseRecipe();
        if (recipe == null)
        {
            PrintCancelled();
            return;
        }

        meal.AddItem(recipe);
        Console.WriteLine($"Added {recipe.GetName()} to {meal.GetName()}.");
    }

    private void ViewRecipes()
    {
        Console.WriteLine("=== Your Recipes ===");
        Console.WriteLine("Every recipe you've built, along with its total nutrition.");
        Console.WriteLine();

        if (_recipes.Count == 0)
        {
            Console.WriteLine("You don't have any recipes yet.");
            return;
        }

        foreach (Recipe recipe in _recipes)
        {
            Console.WriteLine(recipe.GetDetailsString());
        }
    }

    // ---------- Viewing & goals ----------

    private void ViewAllMeals()
    {
        Console.WriteLine("=== Your Meals Today ===");
        Console.WriteLine();

        if (_meals.Count == 0)
        {
            Console.WriteLine("You don't have any meals yet.");
            return;
        }

        NutritionSummary dayTotal = new NutritionSummary(0, 0, 0, 0);

        foreach (Meal meal in _meals)
        {
            meal.PrintSummary();
            dayTotal = dayTotal.Add(meal.GetTotalNutrition());
        }

        Console.WriteLine();
        Console.WriteLine("Day total: " + dayTotal.GetDetailsString());
    }

    private void SetNutritionGoal()
    {
        Console.WriteLine("=== Set Your Nutrition Goal ===");
        Console.WriteLine("This is the calorie target the app compares your daily totals against.");
        Console.WriteLine($"Your current goal is {_goal.GetCalorieTarget()} calories.");
        Console.WriteLine();

        double target;
        if (!_input.ReadDouble("New calorie goal: ", out target))
        {
            PrintCancelled();
            return;
        }

        _goal = new NutritionGoal(target);
        Console.WriteLine($"Your goal is now set to {target} calories.");
    }

    private void CheckGoal()
    {
        Console.WriteLine("=== Goal Check ===");
        Console.WriteLine("Compares everything you've eaten today against your calorie goal.");
        Console.WriteLine();

        NutritionSummary dayTotal = new NutritionSummary(0, 0, 0, 0);

        foreach (Meal meal in _meals)
        {
            dayTotal = dayTotal.Add(meal.GetTotalNutrition());
        }

        Console.WriteLine(_goal.CheckGoal(dayTotal));
    }

    // ---------- Pantry & shopping list ----------

    private void AddPantryItem()
    {
        Console.WriteLine("=== Add a Pantry Item ===");
        Console.WriteLine("This is something you already have at home. The shopping list will");
        Console.WriteLine("subtract these amounts so you don't buy what you don't need.");
        Console.WriteLine();

        string name;
        if (!_input.ReadString("Pantry item name: ", out name))
        {
            PrintCancelled();
            return;
        }

        double quantity;
        if (!_input.ReadDouble("Quantity you have: ", out quantity))
        {
            PrintCancelled();
            return;
        }

        PantryItem item = new PantryItem(name, quantity);
        _pantryItems.Add(item);

        Console.WriteLine($"Added {quantity} {name} to your pantry.");
    }

    private void ViewPantry()
    {
        Console.WriteLine("=== Your Pantry ===");
        Console.WriteLine("Everything you've told the app you already have at home.");
        Console.WriteLine();

        if (_pantryItems.Count == 0)
        {
            Console.WriteLine("Your pantry is empty.");
            return;
        }

        foreach (PantryItem item in _pantryItems)
        {
            Console.WriteLine($"  {item.GetName()}: {item.GetQuantity()}");
        }
    }

    private void ViewShoppingList()
    {
        Console.WriteLine("=== Shopping List ===");
        Console.WriteLine("Every ingredient across all your meals (including ones inside");
        Console.WriteLine("recipes), added up and reduced by what's already in your pantry.");
        Console.WriteLine();

        ShoppingList list = new ShoppingList();

        foreach (PantryItem item in _pantryItems)
        {
            list.AddPantryItem(item);
        }

        list.PrintNeededItems(_meals);
    }
}