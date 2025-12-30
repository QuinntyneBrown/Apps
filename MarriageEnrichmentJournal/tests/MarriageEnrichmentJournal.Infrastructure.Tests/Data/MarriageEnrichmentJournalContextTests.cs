// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Infrastructure.Tests;

/// <summary>
/// Unit tests for the MarriageEnrichmentJournalContext.
/// </summary>
[TestFixture]
public class MarriageEnrichmentJournalContextTests
{
    private MarriageEnrichmentJournalContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MarriageEnrichmentJournalContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new MarriageEnrichmentJournalContext(options);
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
    /// Tests that JournalEntries can be added and retrieved.
    /// </summary>
    [Test]
    public async Task JournalEntries_CanAddAndRetrieve()
    {
        // Arrange
        var entry = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Entry",
            Content = "This is a test journal entry",
            EntryType = EntryType.General,
            EntryDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.JournalEntries.Add(entry);
        await _context.SaveChangesAsync();

        var retrieved = await _context.JournalEntries.FindAsync(entry.JournalEntryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Entry"));
        Assert.That(retrieved.Content, Is.EqualTo("This is a test journal entry"));
        Assert.That(retrieved.EntryType, Is.EqualTo(EntryType.General));
    }

    /// <summary>
    /// Tests that Gratitudes can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Gratitudes_CanAddAndRetrieve()
    {
        // Arrange
        var gratitude = new Gratitude
        {
            GratitudeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Text = "I am grateful for today",
            GratitudeDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Gratitudes.Add(gratitude);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Gratitudes.FindAsync(gratitude.GratitudeId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Text, Is.EqualTo("I am grateful for today"));
    }

    /// <summary>
    /// Tests that Reflections can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Reflections_CanAddAndRetrieve()
    {
        // Arrange
        var reflection = new Reflection
        {
            ReflectionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Text = "Today I learned something new",
            Topic = "Growth",
            ReflectionDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Reflections.Add(reflection);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Reflections.FindAsync(reflection.ReflectionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Text, Is.EqualTo("Today I learned something new"));
        Assert.That(retrieved.Topic, Is.EqualTo("Growth"));
    }

    /// <summary>
    /// Tests that cascade delete works for gratitudes.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesGratitudes()
    {
        // Arrange
        var entry = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Entry",
            Content = "Content",
            EntryType = EntryType.General,
            EntryDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var gratitude = new Gratitude
        {
            GratitudeId = Guid.NewGuid(),
            JournalEntryId = entry.JournalEntryId,
            UserId = entry.UserId,
            Text = "Test gratitude",
            GratitudeDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.JournalEntries.Add(entry);
        _context.Gratitudes.Add(gratitude);
        await _context.SaveChangesAsync();

        // Act
        _context.JournalEntries.Remove(entry);
        await _context.SaveChangesAsync();

        var retrievedGratitude = await _context.Gratitudes.FindAsync(gratitude.GratitudeId);

        // Assert
        Assert.That(retrievedGratitude, Is.Null);
    }

    /// <summary>
    /// Tests that cascade delete works for reflections.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesReflections()
    {
        // Arrange
        var entry = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Entry",
            Content = "Content",
            EntryType = EntryType.General,
            EntryDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var reflection = new Reflection
        {
            ReflectionId = Guid.NewGuid(),
            JournalEntryId = entry.JournalEntryId,
            UserId = entry.UserId,
            Text = "Test reflection",
            ReflectionDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.JournalEntries.Add(entry);
        _context.Reflections.Add(reflection);
        await _context.SaveChangesAsync();

        // Act
        _context.JournalEntries.Remove(entry);
        await _context.SaveChangesAsync();

        var retrievedReflection = await _context.Reflections.FindAsync(reflection.ReflectionId);

        // Assert
        Assert.That(retrievedReflection, Is.Null);
    }
}
