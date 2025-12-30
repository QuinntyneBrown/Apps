// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Infrastructure.Tests;

/// <summary>
/// Unit tests for the KnowledgeBaseSecondBrainContext.
/// </summary>
[TestFixture]
public class KnowledgeBaseSecondBrainContextTests
{
    private KnowledgeBaseSecondBrainContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<KnowledgeBaseSecondBrainContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new KnowledgeBaseSecondBrainContext(options);
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
    /// Tests that Notes can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Notes_CanAddAndRetrieve()
    {
        // Arrange
        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Note",
            Content = "This is a test note",
            NoteType = NoteType.Article,
            CreatedAt = DateTime.UtcNow,
            LastModifiedAt = DateTime.UtcNow,
        };

        // Act
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Notes.FindAsync(note.NoteId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Note"));
        Assert.That(retrieved.Content, Is.EqualTo("This is a test note"));
        Assert.That(retrieved.NoteType, Is.EqualTo(NoteType.Article));
    }

    /// <summary>
    /// Tests that Tags can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Tags_CanAddAndRetrieve()
    {
        // Arrange
        var tag = new Tag
        {
            TagId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Programming",
            Color = "#FF0000",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Tags.FindAsync(tag.TagId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Programming"));
        Assert.That(retrieved.Color, Is.EqualTo("#FF0000"));
    }

    /// <summary>
    /// Tests that NoteLinks can be added and retrieved.
    /// </summary>
    [Test]
    public async Task NoteLinks_CanAddAndRetrieve()
    {
        // Arrange
        var sourceNote = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Source Note",
            Content = "Source content",
            NoteType = NoteType.Article,
            CreatedAt = DateTime.UtcNow,
            LastModifiedAt = DateTime.UtcNow,
        };

        var targetNote = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Target Note",
            Content = "Target content",
            NoteType = NoteType.Article,
            CreatedAt = DateTime.UtcNow,
            LastModifiedAt = DateTime.UtcNow,
        };

        var link = new NoteLink
        {
            NoteLinkId = Guid.NewGuid(),
            SourceNoteId = sourceNote.NoteId,
            TargetNoteId = targetNote.NoteId,
            LinkType = "Related",
            Description = "Test link",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Notes.Add(sourceNote);
        _context.Notes.Add(targetNote);
        _context.Links.Add(link);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Links.FindAsync(link.NoteLinkId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.SourceNoteId, Is.EqualTo(sourceNote.NoteId));
        Assert.That(retrieved.TargetNoteId, Is.EqualTo(targetNote.NoteId));
        Assert.That(retrieved.LinkType, Is.EqualTo("Related"));
    }

    /// <summary>
    /// Tests that SearchQueries can be added and retrieved.
    /// </summary>
    [Test]
    public async Task SearchQueries_CanAddAndRetrieve()
    {
        // Arrange
        var searchQuery = new SearchQuery
        {
            SearchQueryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            QueryText = "test query",
            Name = "Test Search",
            IsSaved = true,
            ExecutionCount = 5,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.SearchQueries.Add(searchQuery);
        await _context.SaveChangesAsync();

        var retrieved = await _context.SearchQueries.FindAsync(searchQuery.SearchQueryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.QueryText, Is.EqualTo("test query"));
        Assert.That(retrieved.Name, Is.EqualTo("Test Search"));
        Assert.That(retrieved.ExecutionCount, Is.EqualTo(5));
    }

    /// <summary>
    /// Tests that cascade delete works for outgoing links.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesOutgoingLinks()
    {
        // Arrange
        var sourceNote = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Source Note",
            Content = "Source content",
            NoteType = NoteType.Article,
            CreatedAt = DateTime.UtcNow,
            LastModifiedAt = DateTime.UtcNow,
        };

        var targetNote = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Target Note",
            Content = "Target content",
            NoteType = NoteType.Article,
            CreatedAt = DateTime.UtcNow,
            LastModifiedAt = DateTime.UtcNow,
        };

        var link = new NoteLink
        {
            NoteLinkId = Guid.NewGuid(),
            SourceNoteId = sourceNote.NoteId,
            TargetNoteId = targetNote.NoteId,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Notes.Add(sourceNote);
        _context.Notes.Add(targetNote);
        _context.Links.Add(link);
        await _context.SaveChangesAsync();

        // Act
        _context.Notes.Remove(sourceNote);
        await _context.SaveChangesAsync();

        var retrievedLink = await _context.Links.FindAsync(link.NoteLinkId);

        // Assert
        Assert.That(retrievedLink, Is.Null);
    }
}
