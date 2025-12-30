// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Infrastructure.Tests;

/// <summary>
/// Unit tests for the MusicCollectionOrganizerContext.
/// </summary>
[TestFixture]
public class MusicCollectionOrganizerContextTests
{
    private MusicCollectionOrganizerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MusicCollectionOrganizerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new MusicCollectionOrganizerContext(options);
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
    /// Tests that Artists can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Artists_CanAddAndRetrieve()
    {
        // Arrange
        var artist = new Artist
        {
            ArtistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Artist",
            Biography = "Test biography",
            Country = "USA",
            FormedYear = 2000,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Artists.Add(artist);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Artists.FindAsync(artist.ArtistId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Artist"));
        Assert.That(retrieved.Country, Is.EqualTo("USA"));
    }

    /// <summary>
    /// Tests that Albums can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Albums_CanAddAndRetrieve()
    {
        // Arrange
        var artist = new Artist
        {
            ArtistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Artist",
            CreatedAt = DateTime.UtcNow,
        };

        var album = new Album
        {
            AlbumId = Guid.NewGuid(),
            UserId = artist.UserId,
            Title = "Test Album",
            ArtistId = artist.ArtistId,
            Format = Format.Vinyl,
            Genre = Genre.Rock,
            ReleaseYear = 2020,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Artists.Add(artist);
        _context.Albums.Add(album);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Albums.FindAsync(album.AlbumId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Album"));
        Assert.That(retrieved.Format, Is.EqualTo(Format.Vinyl));
        Assert.That(retrieved.Genre, Is.EqualTo(Genre.Rock));
    }

    /// <summary>
    /// Tests that ListeningLogs can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ListeningLogs_CanAddAndRetrieve()
    {
        // Arrange
        var artist = new Artist
        {
            ArtistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Artist",
            CreatedAt = DateTime.UtcNow,
        };

        var album = new Album
        {
            AlbumId = Guid.NewGuid(),
            UserId = artist.UserId,
            Title = "Test Album",
            ArtistId = artist.ArtistId,
            Format = Format.CD,
            Genre = Genre.Jazz,
            CreatedAt = DateTime.UtcNow,
        };

        var listeningLog = new ListeningLog
        {
            ListeningLogId = Guid.NewGuid(),
            UserId = artist.UserId,
            AlbumId = album.AlbumId,
            ListeningDate = DateTime.UtcNow,
            Rating = 5,
            Notes = "Great album!",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Artists.Add(artist);
        _context.Albums.Add(album);
        _context.ListeningLogs.Add(listeningLog);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ListeningLogs.FindAsync(listeningLog.ListeningLogId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Rating, Is.EqualTo(5));
        Assert.That(retrieved.Notes, Is.EqualTo("Great album!"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var artist = new Artist
        {
            ArtistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Artist",
            CreatedAt = DateTime.UtcNow,
        };

        var album = new Album
        {
            AlbumId = Guid.NewGuid(),
            UserId = artist.UserId,
            Title = "Test Album",
            ArtistId = artist.ArtistId,
            Format = Format.CD,
            Genre = Genre.Rock,
            CreatedAt = DateTime.UtcNow,
        };

        var listeningLog = new ListeningLog
        {
            ListeningLogId = Guid.NewGuid(),
            UserId = artist.UserId,
            AlbumId = album.AlbumId,
            ListeningDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Artists.Add(artist);
        _context.Albums.Add(album);
        _context.ListeningLogs.Add(listeningLog);
        await _context.SaveChangesAsync();

        // Act
        _context.Albums.Remove(album);
        await _context.SaveChangesAsync();

        var retrievedLog = await _context.ListeningLogs.FindAsync(listeningLog.ListeningLogId);

        // Assert
        Assert.That(retrievedLog, Is.Null);
    }
}
