// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Infrastructure.Tests;

/// <summary>
/// Unit tests for the HomeInventoryManagerContext.
/// </summary>
[TestFixture]
public class HomeInventoryManagerContextTests
{
    private HomeInventoryManagerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HomeInventoryManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new HomeInventoryManagerContext(options);
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
    /// Tests that Items can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Items_CanAddAndRetrieve()
    {
        // Arrange
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Item",
            Description = "Test Description",
            Category = Category.Electronics,
            Room = Room.LivingRoom,
            PurchasePrice = 500m,
            CurrentValue = 400m,
            Quantity = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Items.FindAsync(item.ItemId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Item"));
        Assert.That(retrieved.Category, Is.EqualTo(Category.Electronics));
    }

    /// <summary>
    /// Tests that ValueEstimates can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ValueEstimates_CanAddAndRetrieve()
    {
        // Arrange
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Item",
            Category = Category.Furniture,
            Room = Room.Bedroom,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var valueEstimate = new ValueEstimate
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = item.ItemId,
            EstimatedValue = 1200m,
            EstimationDate = DateTime.UtcNow,
            Source = "Professional Appraisal",
            Notes = "Test estimate",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Items.Add(item);
        _context.ValueEstimates.Add(valueEstimate);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ValueEstimates.FindAsync(valueEstimate.ValueEstimateId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.EstimatedValue, Is.EqualTo(1200m));
        Assert.That(retrieved.Source, Is.EqualTo("Professional Appraisal"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedValueEstimates()
    {
        // Arrange
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Item",
            Category = Category.Tools,
            Room = Room.Garage,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var valueEstimate = new ValueEstimate
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = item.ItemId,
            EstimatedValue = 500m,
            EstimationDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Items.Add(item);
        _context.ValueEstimates.Add(valueEstimate);
        await _context.SaveChangesAsync();

        // Act
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();

        var retrievedValueEstimate = await _context.ValueEstimates.FindAsync(valueEstimate.ValueEstimateId);

        // Assert
        Assert.That(retrievedValueEstimate, Is.Null);
    }
}
