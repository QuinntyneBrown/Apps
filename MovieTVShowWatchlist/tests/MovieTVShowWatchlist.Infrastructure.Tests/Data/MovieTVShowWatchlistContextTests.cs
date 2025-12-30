// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MovieTVShowWatchlist.Infrastructure.Tests;

/// <summary>
/// Unit tests for the MovieTVShowWatchlistContext.
/// </summary>
[TestFixture]
public class MovieTVShowWatchlistContextTests
{
    private MovieTVShowWatchlistContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MovieTVShowWatchlistContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new MovieTVShowWatchlistContext(options);
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
    /// Tests that Users can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Users_CanAddAndRetrieve()
    {
        // Arrange
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            DisplayName = "Test User",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Users.FindAsync(user.UserId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Username, Is.EqualTo("testuser"));
        Assert.That(retrieved.Email, Is.EqualTo("test@example.com"));
    }

    /// <summary>
    /// Tests that Movies can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Movies_CanAddAndRetrieve()
    {
        // Arrange
        var movie = new Movie
        {
            MovieId = Guid.NewGuid(),
            Title = "Test Movie",
            ReleaseYear = 2024,
            Director = "Test Director",
            Runtime = 120,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Movies.FindAsync(movie.MovieId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Movie"));
        Assert.That(retrieved.Director, Is.EqualTo("Test Director"));
    }

    /// <summary>
    /// Tests that TVShows can be added and retrieved.
    /// </summary>
    [Test]
    public async Task TVShows_CanAddAndRetrieve()
    {
        // Arrange
        var tvShow = new TVShow
        {
            TVShowId = Guid.NewGuid(),
            Title = "Test Show",
            PremiereYear = 2024,
            NumberOfSeasons = 3,
            Status = ShowStatus.Ongoing,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        _context.TVShows.Add(tvShow);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TVShows.FindAsync(tvShow.TVShowId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Show"));
        Assert.That(retrieved.Status, Is.EqualTo(ShowStatus.Ongoing));
    }

    /// <summary>
    /// Tests that WatchlistItems can be added and retrieved.
    /// </summary>
    [Test]
    public async Task WatchlistItems_CanAddAndRetrieve()
    {
        // Arrange
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            DisplayName = "Test User",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var watchlistItem = new WatchlistItem
        {
            WatchlistItemId = Guid.NewGuid(),
            UserId = user.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            Title = "Test Movie",
            AddedDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Users.Add(user);
        _context.WatchlistItems.Add(watchlistItem);
        await _context.SaveChangesAsync();

        var retrieved = await _context.WatchlistItems.FindAsync(watchlistItem.WatchlistItemId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Movie"));
        Assert.That(retrieved.ContentType, Is.EqualTo(ContentType.Movie));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            DisplayName = "Test User",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var watchlistItem = new WatchlistItem
        {
            WatchlistItemId = Guid.NewGuid(),
            UserId = user.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            Title = "Test Movie",
            AddedDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _context.Users.Add(user);
        _context.WatchlistItems.Add(watchlistItem);
        await _context.SaveChangesAsync();

        // Act
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        var retrievedWatchlistItem = await _context.WatchlistItems.FindAsync(watchlistItem.WatchlistItemId);

        // Assert
        Assert.That(retrievedWatchlistItem, Is.Null);
    }
}
