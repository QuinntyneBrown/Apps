// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WineCellarInventory.Infrastructure.Tests;

/// <summary>
/// Unit tests for the WineCellarInventoryContext.
/// </summary>
[TestFixture]
public class WineCellarInventoryContextTests
{
    private WineCellarInventoryContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<WineCellarInventoryContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new WineCellarInventoryContext(options);
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
    /// Tests that Wines can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Wines_CanAddAndRetrieve()
    {
        // Arrange
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Wine",
            WineType = WineType.Red,
            Region = Region.Bordeaux,
            Vintage = 2020,
            Producer = "Test Producer",
            PurchasePrice = 50.00m,
            BottleCount = 1,
            StorageLocation = "Test Location",
            Notes = "Test Notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Wines.Add(wine);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Wines.FindAsync(wine.WineId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Wine"));
        Assert.That(retrieved.WineType, Is.EqualTo(WineType.Red));
        Assert.That(retrieved.Region, Is.EqualTo(Region.Bordeaux));
    }

    /// <summary>
    /// Tests that TastingNotes can be added and retrieved.
    /// </summary>
    [Test]
    public async Task TastingNotes_CanAddAndRetrieve()
    {
        // Arrange
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Wine",
            WineType = WineType.Red,
            Region = Region.Bordeaux,
            BottleCount = 1,
            CreatedAt = DateTime.UtcNow,
        };

        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = wine.UserId,
            WineId = wine.WineId,
            TastingDate = DateTime.UtcNow,
            Rating = 90,
            Appearance = "Deep red",
            Aroma = "Berry notes",
            Taste = "Rich and complex",
            Finish = "Long finish",
            OverallImpression = "Excellent wine",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Wines.Add(wine);
        _context.TastingNotes.Add(tastingNote);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TastingNotes.FindAsync(tastingNote.TastingNoteId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Rating, Is.EqualTo(90));
        Assert.That(retrieved.Appearance, Is.EqualTo("Deep red"));
        Assert.That(retrieved.WineId, Is.EqualTo(wine.WineId));
    }

    /// <summary>
    /// Tests that DrinkingWindows can be added and retrieved.
    /// </summary>
    [Test]
    public async Task DrinkingWindows_CanAddAndRetrieve()
    {
        // Arrange
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Wine",
            WineType = WineType.Red,
            Region = Region.Bordeaux,
            BottleCount = 1,
            CreatedAt = DateTime.UtcNow,
        };

        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = wine.UserId,
            WineId = wine.WineId,
            StartDate = DateTime.UtcNow.AddYears(2),
            EndDate = DateTime.UtcNow.AddYears(10),
            Notes = "Optimal drinking period",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Wines.Add(wine);
        _context.DrinkingWindows.Add(drinkingWindow);
        await _context.SaveChangesAsync();

        var retrieved = await _context.DrinkingWindows.FindAsync(drinkingWindow.DrinkingWindowId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.WineId, Is.EqualTo(wine.WineId));
        Assert.That(retrieved.Notes, Is.EqualTo("Optimal drinking period"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Wine",
            WineType = WineType.Red,
            Region = Region.Bordeaux,
            BottleCount = 1,
            CreatedAt = DateTime.UtcNow,
        };

        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = wine.UserId,
            WineId = wine.WineId,
            TastingDate = DateTime.UtcNow,
            Rating = 90,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Wines.Add(wine);
        _context.TastingNotes.Add(tastingNote);
        await _context.SaveChangesAsync();

        // Act
        _context.Wines.Remove(wine);
        await _context.SaveChangesAsync();

        var retrievedTastingNote = await _context.TastingNotes.FindAsync(tastingNote.TastingNoteId);

        // Assert
        Assert.That(retrievedTastingNote, Is.Null);
    }
}
