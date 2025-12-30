// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MealPrepPlanner.Infrastructure.Tests;

/// <summary>
/// Unit tests for the MealPrepPlannerContext.
/// </summary>
[TestFixture]
public class MealPrepPlannerContextTests
{
    private MealPrepPlannerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MealPrepPlannerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new MealPrepPlannerContext(options);
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
    /// Tests that MealPlans can be added and retrieved.
    /// </summary>
    [Test]
    public async Task MealPlans_CanAddAndRetrieve()
    {
        // Arrange
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Meal Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7),
            DailyCalorieTarget = 2000,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.MealPlans.Add(mealPlan);
        await _context.SaveChangesAsync();

        var retrieved = await _context.MealPlans.FindAsync(mealPlan.MealPlanId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Meal Plan"));
        Assert.That(retrieved.DailyCalorieTarget, Is.EqualTo(2000));
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
            Description = "Test Description",
            PrepTimeMinutes = 15,
            CookTimeMinutes = 30,
            Servings = 4,
            Ingredients = "[]",
            Instructions = "Test Instructions",
            MealType = "Dinner",
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Recipes.FindAsync(recipe.RecipeId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Recipe"));
        Assert.That(retrieved.MealType, Is.EqualTo("Dinner"));
    }

    /// <summary>
    /// Tests that GroceryLists can be added and retrieved.
    /// </summary>
    [Test]
    public async Task GroceryLists_CanAddAndRetrieve()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Grocery List",
            Items = "[]",
            IsCompleted = false,
            EstimatedCost = 50.00m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.GroceryLists.Add(groceryList);
        await _context.SaveChangesAsync();

        var retrieved = await _context.GroceryLists.FindAsync(groceryList.GroceryListId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Grocery List"));
        Assert.That(retrieved.EstimatedCost, Is.EqualTo(50.00m));
    }

    /// <summary>
    /// Tests that Nutritions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Nutritions_CanAddAndRetrieve()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Calories = 300,
            Protein = 25.5m,
            Carbohydrates = 30.0m,
            Fat = 10.0m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Nutritions.Add(nutrition);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Nutritions.FindAsync(nutrition.NutritionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Calories, Is.EqualTo(300));
        Assert.That(retrieved.Protein, Is.EqualTo(25.5m));
    }

    /// <summary>
    /// Tests that recipes associated with a meal plan can be retrieved.
    /// </summary>
    [Test]
    public async Task MealPlan_CanRetrieveAssociatedRecipes()
    {
        // Arrange
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = mealPlan.UserId,
            MealPlanId = mealPlan.MealPlanId,
            Name = "Test Recipe",
            PrepTimeMinutes = 10,
            CookTimeMinutes = 20,
            Servings = 2,
            Ingredients = "[]",
            Instructions = "Test",
            MealType = "Lunch",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.MealPlans.Add(mealPlan);
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        var retrievedPlan = await _context.MealPlans
            .Include(mp => mp.Recipes)
            .FirstOrDefaultAsync(mp => mp.MealPlanId == mealPlan.MealPlanId);

        // Assert
        Assert.That(retrievedPlan, Is.Not.Null);
        Assert.That(retrievedPlan!.Recipes, Has.Count.EqualTo(1));
        Assert.That(retrievedPlan.Recipes.First().Name, Is.EqualTo("Test Recipe"));
    }
}
