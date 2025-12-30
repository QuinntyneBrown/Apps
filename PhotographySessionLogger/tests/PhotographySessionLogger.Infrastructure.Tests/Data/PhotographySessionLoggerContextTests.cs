// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Infrastructure.Tests;

/// <summary>
/// Unit tests for the PhotographySessionLoggerContext.
/// </summary>
[TestFixture]
public class PhotographySessionLoggerContextTests
{
    private PhotographySessionLoggerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PhotographySessionLoggerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new PhotographySessionLoggerContext(options);
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
    /// Tests that Sessions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Sessions_CanAddAndRetrieve()
    {
        // Arrange
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Session",
            SessionType = SessionType.Portrait,
            SessionDate = DateTime.UtcNow,
            Location = "Test Location",
            Client = "Test Client",
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Sessions.FindAsync(session.SessionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Session"));
        Assert.That(retrieved.SessionType, Is.EqualTo(SessionType.Portrait));
    }

    /// <summary>
    /// Tests that Photos can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Photos_CanAddAndRetrieve()
    {
        // Arrange
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Session",
            SessionType = SessionType.Portrait,
            SessionDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var photo = new Photo
        {
            PhotoId = Guid.NewGuid(),
            UserId = session.UserId,
            SessionId = session.SessionId,
            FileName = "test.jpg",
            FilePath = "/test/path",
            CameraSettings = "ISO 400, f/2.8",
            Rating = 5,
            Tags = "test,photo",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Sessions.Add(session);
        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Photos.FindAsync(photo.PhotoId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.FileName, Is.EqualTo("test.jpg"));
        Assert.That(retrieved.Rating, Is.EqualTo(5));
    }

    /// <summary>
    /// Tests that Gears can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Gears_CanAddAndRetrieve()
    {
        // Arrange
        var gear = new Gear
        {
            GearId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Camera",
            GearType = "Camera",
            Brand = "Canon",
            Model = "EOS R5",
            PurchaseDate = DateTime.UtcNow,
            PurchasePrice = 3899.99m,
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Gears.Add(gear);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Gears.FindAsync(gear.GearId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Camera"));
        Assert.That(retrieved.GearType, Is.EqualTo("Camera"));
    }

    /// <summary>
    /// Tests that Projects can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Projects_CanAddAndRetrieve()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Description = "Test description",
            DueDate = DateTime.UtcNow.AddDays(30),
            IsCompleted = false,
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Projects.FindAsync(project.ProjectId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Project"));
        Assert.That(retrieved.IsCompleted, Is.False);
    }

    /// <summary>
    /// Tests that cascade delete works for Photos when Session is deleted.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedPhotos()
    {
        // Arrange
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Session",
            SessionType = SessionType.Portrait,
            SessionDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var photo = new Photo
        {
            PhotoId = Guid.NewGuid(),
            UserId = session.UserId,
            SessionId = session.SessionId,
            FileName = "test.jpg",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Sessions.Add(session);
        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();

        // Act
        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();

        var retrievedPhoto = await _context.Photos.FindAsync(photo.PhotoId);

        // Assert
        Assert.That(retrievedPhoto, Is.Null);
    }
}
