// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the HomeBrewingTrackerContext.
/// </summary>
[TestFixture]
public class HomeBrewingTrackerContextTests
{
    private HomeBrewingTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HomeBrewingTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new HomeBrewingTrackerContext(options);
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
            Name = "Test IPA",
            BeerStyle = BeerStyle.IPA,
            Description = "A test IPA recipe",
            OriginalGravity = 1.065m,
            FinalGravity = 1.012m,
            ABV = 7.0m,
            IBU = 65,
            BatchSize = 5.0m,
            Ingredients = "Test ingredients",
            Instructions = "Test instructions",
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Recipes.FindAsync(recipe.RecipeId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test IPA"));
        Assert.That(retrieved.BeerStyle, Is.EqualTo(BeerStyle.IPA));
        Assert.That(retrieved.ABV, Is.EqualTo(7.0m));
    }

    /// <summary>
    /// Tests that Batches can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Batches_CanAddAndRetrieve()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            BeerStyle = BeerStyle.IPA,
            Description = "Test",
            BatchSize = 5.0m,
            Ingredients = "Test",
            Instructions = "Test",
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        var batch = new Batch
        {
            BatchId = Guid.NewGuid(),
            UserId = recipe.UserId,
            RecipeId = recipe.RecipeId,
            BatchNumber = "IPA-001",
            Status = BatchStatus.Fermenting,
            BrewDate = DateTime.UtcNow,
            ActualOriginalGravity = 1.066m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Recipes.Add(recipe);
        _context.Batches.Add(batch);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Batches.FindAsync(batch.BatchId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.BatchNumber, Is.EqualTo("IPA-001"));
        Assert.That(retrieved.Status, Is.EqualTo(BatchStatus.Fermenting));
        Assert.That(retrieved.ActualOriginalGravity, Is.EqualTo(1.066m));
    }

    /// <summary>
    /// Tests that TastingNotes can be added and retrieved.
    /// </summary>
    [Test]
    public async Task TastingNotes_CanAddAndRetrieve()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            BeerStyle = BeerStyle.IPA,
            Description = "Test",
            BatchSize = 5.0m,
            Ingredients = "Test",
            Instructions = "Test",
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        var batch = new Batch
        {
            BatchId = Guid.NewGuid(),
            UserId = recipe.UserId,
            RecipeId = recipe.RecipeId,
            BatchNumber = "IPA-001",
            Status = BatchStatus.Completed,
            BrewDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = recipe.UserId,
            BatchId = batch.BatchId,
            TastingDate = DateTime.UtcNow,
            Rating = 5,
            Appearance = "Clear golden",
            Aroma = "Citrus and pine",
            Flavor = "Hoppy and balanced",
            Mouthfeel = "Medium body",
            OverallImpression = "Excellent!",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Recipes.Add(recipe);
        _context.Batches.Add(batch);
        _context.TastingNotes.Add(tastingNote);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TastingNotes.FindAsync(tastingNote.TastingNoteId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Rating, Is.EqualTo(5));
        Assert.That(retrieved.Appearance, Is.EqualTo("Clear golden"));
        Assert.That(retrieved.OverallImpression, Is.EqualTo("Excellent!"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedBatches()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            BeerStyle = BeerStyle.IPA,
            Description = "Test",
            BatchSize = 5.0m,
            Ingredients = "Test",
            Instructions = "Test",
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        var batch = new Batch
        {
            BatchId = Guid.NewGuid(),
            UserId = recipe.UserId,
            RecipeId = recipe.RecipeId,
            BatchNumber = "IPA-001",
            Status = BatchStatus.Fermenting,
            BrewDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Recipes.Add(recipe);
        _context.Batches.Add(batch);
        await _context.SaveChangesAsync();

        // Act
        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();

        var retrievedBatch = await _context.Batches.FindAsync(batch.BatchId);

        // Assert
        Assert.That(retrievedBatch, Is.Null);
    }

    /// <summary>
    /// Tests that Recipes can be updated.
    /// </summary>
    [Test]
    public async Task Recipes_CanUpdate()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test IPA",
            BeerStyle = BeerStyle.IPA,
            Description = "Test",
            BatchSize = 5.0m,
            Ingredients = "Test",
            Instructions = "Test",
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        // Act
        recipe.IsFavorite = true;
        recipe.ABV = 7.5m;
        await _context.SaveChangesAsync();

        var retrieved = await _context.Recipes.FindAsync(recipe.RecipeId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.IsFavorite, Is.True);
        Assert.That(retrieved.ABV, Is.EqualTo(7.5m));
    }
}
