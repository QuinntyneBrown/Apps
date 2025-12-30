// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Infrastructure.Tests;

/// <summary>
/// Unit tests for the ProfessionalReadingListContext.
/// </summary>
[TestFixture]
public class ProfessionalReadingListContextTests
{
    private ProfessionalReadingListContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ProfessionalReadingListContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ProfessionalReadingListContext(options);
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
    /// Tests that Resources can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Resources_CanAddAndRetrieve()
    {
        // Arrange
        var resource = new Resource
        {
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Book",
            ResourceType = ResourceType.Book,
            Author = "Test Author",
            Publisher = "Test Publisher",
            TotalPages = 300,
            Topics = new List<string> { "test", "book" },
            DateAdded = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Resources.Add(resource);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Resources.FindAsync(resource.ResourceId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Book"));
        Assert.That(retrieved.ResourceType, Is.EqualTo(ResourceType.Book));
    }

    /// <summary>
    /// Tests that ReadingProgress can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ReadingProgress_CanAddAndRetrieve()
    {
        // Arrange
        var resource = new Resource
        {
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Book",
            ResourceType = ResourceType.Book,
            DateAdded = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var progress = new ReadingProgress
        {
            ReadingProgressId = Guid.NewGuid(),
            UserId = resource.UserId,
            ResourceId = resource.ResourceId,
            Status = "Reading",
            CurrentPage = 100,
            ProgressPercentage = 33,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Resources.Add(resource);
        _context.ReadingProgress.Add(progress);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ReadingProgress.FindAsync(progress.ReadingProgressId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Status, Is.EqualTo("Reading"));
        Assert.That(retrieved.ProgressPercentage, Is.EqualTo(33));
    }

    /// <summary>
    /// Tests that Notes can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Notes_CanAddAndRetrieve()
    {
        // Arrange
        var resource = new Resource
        {
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Book",
            ResourceType = ResourceType.Book,
            DateAdded = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = resource.UserId,
            ResourceId = resource.ResourceId,
            Content = "This is a test note",
            PageReference = "Chapter 5",
            Quote = "This is a test quote",
            Tags = new List<string> { "important", "test" },
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Resources.Add(resource);
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Notes.FindAsync(note.NoteId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Content, Is.EqualTo("This is a test note"));
        Assert.That(retrieved.PageReference, Is.EqualTo("Chapter 5"));
    }

    /// <summary>
    /// Tests that cascade delete works for Notes when Resource is deleted.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedNotes()
    {
        // Arrange
        var resource = new Resource
        {
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Book",
            ResourceType = ResourceType.Book,
            DateAdded = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = resource.UserId,
            ResourceId = resource.ResourceId,
            Content = "Test note",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Resources.Add(resource);
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        // Act
        _context.Resources.Remove(resource);
        await _context.SaveChangesAsync();

        var retrievedNote = await _context.Notes.FindAsync(note.NoteId);

        // Assert
        Assert.That(retrievedNote, Is.Null);
    }

    /// <summary>
    /// Tests that cascade delete works for ReadingProgress when Resource is deleted.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedReadingProgress()
    {
        // Arrange
        var resource = new Resource
        {
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Book",
            ResourceType = ResourceType.Book,
            DateAdded = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var progress = new ReadingProgress
        {
            ReadingProgressId = Guid.NewGuid(),
            UserId = resource.UserId,
            ResourceId = resource.ResourceId,
            Status = "Reading",
            ProgressPercentage = 50,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Resources.Add(resource);
        _context.ReadingProgress.Add(progress);
        await _context.SaveChangesAsync();

        // Act
        _context.Resources.Remove(resource);
        await _context.SaveChangesAsync();

        var retrievedProgress = await _context.ReadingProgress.FindAsync(progress.ReadingProgressId);

        // Assert
        Assert.That(retrievedProgress, Is.Null);
    }
}
