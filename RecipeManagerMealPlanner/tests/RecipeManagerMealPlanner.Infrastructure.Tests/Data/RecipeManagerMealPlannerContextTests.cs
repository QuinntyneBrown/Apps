// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Infrastructure.Tests;

/// <summary>
/// Unit tests for the RecipeManagerMealPlannerContext.
/// </summary>
[TestFixture]
public class RecipeManagerMealPlannerContextTests
{
    private RecipeManagerMealPlannerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RecipeManagerMealPlannerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new RecipeManagerMealPlannerContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Recipes can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Recipes_CanAddAndRetrieve()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            Cuisine = Cuisine.Italian,
            DifficultyLevel = DifficultyLevel.Easy,
            PrepTimeMinutes = 10,
            CookTimeMinutes = 20,
            Servings = 4,
            Instructions = "Test instructions",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Recipes.FindAsync(recipe.RecipeId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Recipe"));
        Assert.That(retrieved.Cuisine, Is.EqualTo(Cuisine.Italian));
    }

    /// <summary>
    /// Tests that Ingredients can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Ingredients_CanAddAndRetrieve()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            Cuisine = Cuisine.Italian,
            DifficultyLevel = DifficultyLevel.Easy,
            PrepTimeMinutes = 10,
            CookTimeMinutes = 20,
            Servings = 4,
            Instructions = "Test instructions",
            CreatedAt = DateTime.UtcNow,
        };

        var ingredient = new Ingredient
        {
            IngredientId = Guid.NewGuid(),
            RecipeId = recipe.RecipeId,
            Name = "Tomato",
            Quantity = "2",
            Unit = "pieces",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Recipes.Add(recipe);
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Ingredients.FindAsync(ingredient.IngredientId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Tomato"));
        Assert.That(retrieved.Quantity, Is.EqualTo("2"));
    }

    /// <summary>
    /// Tests that MealPlans can be added and retrieved.
    /// </summary>
    [Test]
    public async Task MealPlans_CanAddAndRetrieve()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            Cuisine = Cuisine.Italian,
            DifficultyLevel = DifficultyLevel.Easy,
            PrepTimeMinutes = 10,
            CookTimeMinutes = 20,
            Servings = 4,
            Instructions = "Test instructions",
            CreatedAt = DateTime.UtcNow,
        };

        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Monday Dinner",
            MealDate = DateTime.UtcNow.Date,
            MealType = "Dinner",
            RecipeId = recipe.RecipeId,
            IsPrepared = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Recipes.Add(recipe);
        _context.MealPlans.Add(mealPlan);
        await _context.SaveChangesAsync();

        var retrieved = await _context.MealPlans.FindAsync(mealPlan.MealPlanId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Monday Dinner"));
        Assert.That(retrieved.MealType, Is.EqualTo("Dinner"));
    }

    /// <summary>
    /// Tests that ShoppingLists can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ShoppingLists_CanAddAndRetrieve()
    {
        // Arrange
        var shoppingList = new ShoppingList
        {
            ShoppingListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Weekly Groceries",
            Items = "Milk, Eggs, Bread",
            CreatedDate = DateTime.UtcNow.Date,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.ShoppingLists.Add(shoppingList);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ShoppingLists.FindAsync(shoppingList.ShoppingListId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Weekly Groceries"));
        Assert.That(retrieved.Items, Is.EqualTo("Milk, Eggs, Bread"));
    }

    /// <summary>
    /// Tests that cascade delete works for ingredients when recipe is deleted.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesIngredients()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            Cuisine = Cuisine.Italian,
            DifficultyLevel = DifficultyLevel.Easy,
            PrepTimeMinutes = 10,
            CookTimeMinutes = 20,
            Servings = 4,
            Instructions = "Test instructions",
            CreatedAt = DateTime.UtcNow,
        };

        var ingredient = new Ingredient
        {
            IngredientId = Guid.NewGuid(),
            RecipeId = recipe.RecipeId,
            Name = "Tomato",
            Quantity = "2",
            Unit = "pieces",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Recipes.Add(recipe);
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();

        // Act
        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();

        var retrievedIngredient = await _context.Ingredients.FindAsync(ingredient.IngredientId);

        // Assert
        Assert.That(retrievedIngredient, Is.Null);
    }
}
