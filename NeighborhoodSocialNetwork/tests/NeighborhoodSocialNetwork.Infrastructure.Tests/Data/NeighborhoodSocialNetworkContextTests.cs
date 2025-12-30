// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Infrastructure.Tests;

/// <summary>
/// Unit tests for the NeighborhoodSocialNetworkContext.
/// </summary>
[TestFixture]
public class NeighborhoodSocialNetworkContextTests
{
    private NeighborhoodSocialNetworkContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<NeighborhoodSocialNetworkContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new NeighborhoodSocialNetworkContext(options);
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
    /// Tests that Neighbors can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Neighbors_CanAddAndRetrieve()
    {
        // Arrange
        var neighbor = new Neighbor
        {
            NeighborId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Neighbor",
            Address = "123 Test Street",
            ContactInfo = "test@example.com",
            IsVerified = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Neighbors.Add(neighbor);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Neighbors.FindAsync(neighbor.NeighborId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Neighbor"));
        Assert.That(retrieved.Address, Is.EqualTo("123 Test Street"));
    }

    /// <summary>
    /// Tests that Events can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Events_CanAddAndRetrieve()
    {
        // Arrange
        var neighbor = new Neighbor
        {
            NeighborId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Neighbor",
            CreatedAt = DateTime.UtcNow,
        };

        var eventItem = new Event
        {
            EventId = Guid.NewGuid(),
            CreatedByNeighborId = neighbor.NeighborId,
            Title = "Test Event",
            Description = "Test Description",
            EventDateTime = DateTime.UtcNow.AddDays(7),
            Location = "Test Location",
            IsPublic = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Neighbors.Add(neighbor);
        _context.Events.Add(eventItem);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Events.FindAsync(eventItem.EventId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Event"));
        Assert.That(retrieved.Location, Is.EqualTo("Test Location"));
    }

    /// <summary>
    /// Tests that Recommendations can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Recommendations_CanAddAndRetrieve()
    {
        // Arrange
        var neighbor = new Neighbor
        {
            NeighborId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Neighbor",
            CreatedAt = DateTime.UtcNow,
        };

        var recommendation = new Recommendation
        {
            RecommendationId = Guid.NewGuid(),
            NeighborId = neighbor.NeighborId,
            Title = "Test Recommendation",
            Description = "Great place!",
            RecommendationType = RecommendationType.Restaurant,
            BusinessName = "Test Restaurant",
            Rating = 5,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Neighbors.Add(neighbor);
        _context.Recommendations.Add(recommendation);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Recommendations.FindAsync(recommendation.RecommendationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Recommendation"));
        Assert.That(retrieved.Rating, Is.EqualTo(5));
    }

    /// <summary>
    /// Tests that Messages can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Messages_CanAddAndRetrieve()
    {
        // Arrange
        var sender = new Neighbor
        {
            NeighborId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Sender",
            CreatedAt = DateTime.UtcNow,
        };

        var recipient = new Neighbor
        {
            NeighborId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Recipient",
            CreatedAt = DateTime.UtcNow,
        };

        var message = new Message
        {
            MessageId = Guid.NewGuid(),
            SenderNeighborId = sender.NeighborId,
            RecipientNeighborId = recipient.NeighborId,
            Subject = "Test Subject",
            Content = "Test Content",
            IsRead = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Neighbors.AddRange(sender, recipient);
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Messages.FindAsync(message.MessageId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Subject, Is.EqualTo("Test Subject"));
        Assert.That(retrieved.IsRead, Is.False);
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var neighbor = new Neighbor
        {
            NeighborId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Neighbor",
            CreatedAt = DateTime.UtcNow,
        };

        var recommendation = new Recommendation
        {
            RecommendationId = Guid.NewGuid(),
            NeighborId = neighbor.NeighborId,
            Title = "Test Recommendation",
            Description = "Test Description",
            RecommendationType = RecommendationType.Service,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Neighbors.Add(neighbor);
        _context.Recommendations.Add(recommendation);
        await _context.SaveChangesAsync();

        // Act
        _context.Neighbors.Remove(neighbor);
        await _context.SaveChangesAsync();

        var retrievedRecommendation = await _context.Recommendations.FindAsync(recommendation.RecommendationId);

        // Assert
        Assert.That(retrievedRecommendation, Is.Null);
    }
}
