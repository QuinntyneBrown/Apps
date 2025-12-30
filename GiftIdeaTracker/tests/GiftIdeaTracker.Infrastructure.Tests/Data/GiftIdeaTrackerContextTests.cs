// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GiftIdeaTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the GiftIdeaTrackerContext.
/// </summary>
[TestFixture]
public class GiftIdeaTrackerContextTests
{
    private GiftIdeaTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<GiftIdeaTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new GiftIdeaTrackerContext(options);
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
    /// Tests that GiftIdeas can be added and retrieved.
    /// </summary>
    [Test]
    public async Task GiftIdeas_CanAddAndRetrieve()
    {
        // Arrange
        var giftIdea = new GiftIdea
        {
            GiftIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Gift",
            Occasion = Occasion.Birthday,
            EstimatedPrice = 50.00m,
            IsPurchased = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.GiftIdeas.Add(giftIdea);
        await _context.SaveChangesAsync();

        var retrieved = await _context.GiftIdeas.FindAsync(giftIdea.GiftIdeaId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Gift"));
        Assert.That(retrieved.Occasion, Is.EqualTo(Occasion.Birthday));
        Assert.That(retrieved.EstimatedPrice, Is.EqualTo(50.00m));
    }

    /// <summary>
    /// Tests that Recipients can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Recipients_CanAddAndRetrieve()
    {
        // Arrange
        var recipient = new Recipient
        {
            RecipientId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "John Doe",
            Relationship = "Friend",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Recipients.Add(recipient);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Recipients.FindAsync(recipient.RecipientId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("John Doe"));
        Assert.That(retrieved.Relationship, Is.EqualTo("Friend"));
    }

    /// <summary>
    /// Tests that Purchases can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Purchases_CanAddAndRetrieve()
    {
        // Arrange
        var giftIdea = new GiftIdea
        {
            GiftIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Gift",
            Occasion = Occasion.Birthday,
            EstimatedPrice = 50.00m,
            IsPurchased = true,
            CreatedAt = DateTime.UtcNow,
        };

        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            GiftIdeaId = giftIdea.GiftIdeaId,
            PurchaseDate = DateTime.UtcNow,
            ActualPrice = 48.99m,
            Store = "Amazon",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.GiftIdeas.Add(giftIdea);
        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Purchases.FindAsync(purchase.PurchaseId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ActualPrice, Is.EqualTo(48.99m));
        Assert.That(retrieved.Store, Is.EqualTo("Amazon"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedPurchases()
    {
        // Arrange
        var giftIdea = new GiftIdea
        {
            GiftIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Gift",
            Occasion = Occasion.Birthday,
            EstimatedPrice = 50.00m,
            IsPurchased = true,
            CreatedAt = DateTime.UtcNow,
        };

        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            GiftIdeaId = giftIdea.GiftIdeaId,
            PurchaseDate = DateTime.UtcNow,
            ActualPrice = 48.99m,
            Store = "Amazon",
            CreatedAt = DateTime.UtcNow,
        };

        _context.GiftIdeas.Add(giftIdea);
        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync();

        // Act
        _context.GiftIdeas.Remove(giftIdea);
        await _context.SaveChangesAsync();

        var retrievedPurchase = await _context.Purchases.FindAsync(purchase.PurchaseId);

        // Assert
        Assert.That(retrievedPurchase, Is.Null);
    }

    /// <summary>
    /// Tests that GiftIdeas can be updated.
    /// </summary>
    [Test]
    public async Task GiftIdeas_CanUpdate()
    {
        // Arrange
        var giftIdea = new GiftIdea
        {
            GiftIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Gift",
            Occasion = Occasion.Birthday,
            EstimatedPrice = 50.00m,
            IsPurchased = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.GiftIdeas.Add(giftIdea);
        await _context.SaveChangesAsync();

        // Act
        giftIdea.IsPurchased = true;
        giftIdea.EstimatedPrice = 45.00m;
        await _context.SaveChangesAsync();

        var retrieved = await _context.GiftIdeas.FindAsync(giftIdea.GiftIdeaId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.IsPurchased, Is.True);
        Assert.That(retrieved.EstimatedPrice, Is.EqualTo(45.00m));
    }
}
