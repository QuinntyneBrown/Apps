// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VideoGameCollectionManager.Infrastructure.Tests;

/// <summary>
/// Unit tests for the VideoGameCollectionManagerContext.
/// </summary>
[TestFixture]
public class VideoGameCollectionManagerContextTests
{
    private VideoGameCollectionManagerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<VideoGameCollectionManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new VideoGameCollectionManagerContext(options);
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
    /// Tests that Games can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Games_CanAddAndRetrieve()
    {
        // Arrange
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "The Legend of Zelda: Breath of the Wild",
            Platform = Platform.NintendoSwitch,
            Genre = Genre.Adventure,
            Status = CompletionStatus.Completed,
            Rating = 10,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Games.FindAsync(game.GameId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("The Legend of Zelda: Breath of the Wild"));
        Assert.That(retrieved.Platform, Is.EqualTo(Platform.NintendoSwitch));
        Assert.That(retrieved.Status, Is.EqualTo(CompletionStatus.Completed));
    }

    /// <summary>
    /// Tests that PlaySessions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task PlaySessions_CanAddAndRetrieve()
    {
        // Arrange
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Elden Ring",
            Platform = Platform.PlayStation5,
            Genre = Genre.RPG,
            Status = CompletionStatus.InProgress,
            CreatedAt = DateTime.UtcNow,
        };

        var playSession = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = game.UserId,
            GameId = game.GameId,
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(2),
            DurationMinutes = 120,
            Notes = "Great gaming session",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Games.Add(game);
        _context.PlaySessions.Add(playSession);
        await _context.SaveChangesAsync();

        var retrieved = await _context.PlaySessions.FindAsync(playSession.PlaySessionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.GameId, Is.EqualTo(game.GameId));
        Assert.That(retrieved.DurationMinutes, Is.EqualTo(120));
    }

    /// <summary>
    /// Tests that Wishlists can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Wishlists_CanAddAndRetrieve()
    {
        // Arrange
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Baldur's Gate 3",
            Platform = Platform.PC,
            Genre = Genre.RPG,
            Priority = 1,
            IsAcquired = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Wishlists.Add(wishlist);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Wishlists.FindAsync(wishlist.WishlistId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Baldur's Gate 3"));
        Assert.That(retrieved.Priority, Is.EqualTo(1));
        Assert.That(retrieved.IsAcquired, Is.False);
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Halo Infinite",
            Platform = Platform.XboxSeriesX,
            Genre = Genre.Shooter,
            Status = CompletionStatus.InProgress,
            CreatedAt = DateTime.UtcNow,
        };

        var playSession = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = game.UserId,
            GameId = game.GameId,
            StartTime = DateTime.UtcNow,
            DurationMinutes = 90,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Games.Add(game);
        _context.PlaySessions.Add(playSession);
        await _context.SaveChangesAsync();

        // Act
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();

        var retrievedPlaySession = await _context.PlaySessions.FindAsync(playSession.PlaySessionId);

        // Assert
        Assert.That(retrievedPlaySession, Is.Null);
    }
}
