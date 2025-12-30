// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Infrastructure.Tests;

/// <summary>
/// Unit tests for the BBQGrillingRecipeBookContext.
/// </summary>
[TestFixture]
public class BBQGrillingRecipeBookContextTests
{
    private BBQGrillingRecipeBookContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<BBQGrillingRecipeBookContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new BBQGrillingRecipeBookContext(options);
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
            Name = "BBQ Chicken",
            Description = "Delicious BBQ chicken",
            MeatType = MeatType.Chicken,
            CookingMethod = CookingMethod.DirectGrilling,
            PrepTimeMinutes = 15,
            CookTimeMinutes = 30,
            Ingredients = "Chicken, BBQ sauce",
            Instructions = "Grill and baste",
            Servings = 4,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Recipes.FindAsync(recipe.RecipeId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("BBQ Chicken"));
        Assert.That(retrieved.MeatType, Is.EqualTo(MeatType.Chicken));
    }

    /// <summary>
    /// Tests that Techniques can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Techniques_CanAddAndRetrieve()
    {
        // Arrange
        var technique = new Technique
        {
            TechniqueId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Reverse Searing",
            Description = "Perfect for thick steaks",
            Category = "Grilling",
            DifficultyLevel = 3,
            Instructions = "Low and slow, then sear",
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Techniques.Add(technique);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Techniques.FindAsync(technique.TechniqueId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Reverse Searing"));
        Assert.That(retrieved.DifficultyLevel, Is.EqualTo(3));
    }

    /// <summary>
    /// Tests that CookSessions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task CookSessions_CanAddAndRetrieve()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            Description = "Test",
            MeatType = MeatType.Beef,
            CookingMethod = CookingMethod.Smoking,
            PrepTimeMinutes = 30,
            CookTimeMinutes = 240,
            Ingredients = "Beef, rub",
            Instructions = "Smoke low and slow",
            Servings = 6,
            CreatedAt = DateTime.UtcNow,
        };

        var cookSession = new CookSession
        {
            CookSessionId = Guid.NewGuid(),
            UserId = recipe.UserId,
            RecipeId = recipe.RecipeId,
            CookDate = DateTime.UtcNow,
            ActualCookTimeMinutes = 250,
            TemperatureUsed = 225,
            Rating = 5,
            Notes = "Perfect cook!",
            WasSuccessful = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Recipes.Add(recipe);
        _context.CookSessions.Add(cookSession);
        await _context.SaveChangesAsync();

        var retrieved = await _context.CookSessions.FindAsync(cookSession.CookSessionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Rating, Is.EqualTo(5));
        Assert.That(retrieved.Notes, Is.EqualTo("Perfect cook!"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedCookSessions()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            Description = "Test",
            MeatType = MeatType.Pork,
            CookingMethod = CookingMethod.SlowAndLow,
            PrepTimeMinutes = 30,
            CookTimeMinutes = 180,
            Ingredients = "Pork, rub",
            Instructions = "Cook low and slow",
            Servings = 4,
            CreatedAt = DateTime.UtcNow,
        };

        var cookSession = new CookSession
        {
            CookSessionId = Guid.NewGuid(),
            UserId = recipe.UserId,
            RecipeId = recipe.RecipeId,
            CookDate = DateTime.UtcNow,
            ActualCookTimeMinutes = 190,
            WasSuccessful = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Recipes.Add(recipe);
        _context.CookSessions.Add(cookSession);
        await _context.SaveChangesAsync();

        // Act
        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();

        var retrievedSession = await _context.CookSessions.FindAsync(cookSession.CookSessionId);

        // Assert
        Assert.That(retrievedSession, Is.Null);
    }
}
