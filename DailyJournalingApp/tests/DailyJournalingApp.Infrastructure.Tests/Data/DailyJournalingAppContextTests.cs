// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DailyJournalingApp.Infrastructure.Tests;

/// <summary>
/// Unit tests for the DailyJournalingAppContext.
/// </summary>
[TestFixture]
public class DailyJournalingAppContextTests
{
    private DailyJournalingAppContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DailyJournalingAppContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DailyJournalingAppContext(options);
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
            Content = "This is a test journal entry.",
            EntryDate = DateTime.UtcNow,
            Mood = Mood.Happy,
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.JournalEntries.Add(entry);
        await _context.SaveChangesAsync();

        var retrieved = await _context.JournalEntries.FindAsync(entry.JournalEntryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Entry"));
        Assert.That(retrieved.Mood, Is.EqualTo(Mood.Happy));
    }

    /// <summary>
    /// Tests that Prompts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Prompts_CanAddAndRetrieve()
    {
        // Arrange
        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            Text = "What are you grateful for today?",
            Category = "Gratitude",
            IsSystemPrompt = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Prompts.Add(prompt);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Prompts.FindAsync(prompt.PromptId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Text, Is.EqualTo("What are you grateful for today?"));
        Assert.That(retrieved.Category, Is.EqualTo("Gratitude"));
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
            Name = "Work",
            Color = "#3498db",
            UsageCount = 5,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Tags.FindAsync(tag.TagId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Work"));
        Assert.That(retrieved.Color, Is.EqualTo("#3498db"));
    }

    /// <summary>
    /// Tests that a journal entry can be associated with a prompt.
    /// </summary>
    [Test]
    public async Task JournalEntry_CanBeAssociatedWithPrompt()
    {
        // Arrange
        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            Text = "How are you feeling today?",
            Category = "Reflection",
            IsSystemPrompt = true,
            CreatedAt = DateTime.UtcNow,
        };

        var entry = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Today's Reflection",
            Content = "I'm feeling grateful and energized.",
            EntryDate = DateTime.UtcNow,
            Mood = Mood.VeryHappy,
            PromptId = prompt.PromptId,
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Prompts.Add(prompt);
        _context.JournalEntries.Add(entry);
        await _context.SaveChangesAsync();

        var retrieved = await _context.JournalEntries
            .Include(e => e.Prompt)
            .FirstOrDefaultAsync(e => e.JournalEntryId == entry.JournalEntryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Prompt, Is.Not.Null);
        Assert.That(retrieved.Prompt!.Text, Is.EqualTo("How are you feeling today?"));
    }

    /// <summary>
    /// Tests that deleting a prompt sets journal entries' PromptId to null.
    /// </summary>
    [Test]
    public async Task DeletePrompt_SetsJournalEntryPromptIdToNull()
    {
        // Arrange
        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            Text = "Test Prompt",
            IsSystemPrompt = true,
            CreatedAt = DateTime.UtcNow,
        };

        var entry = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Entry",
            Content = "Test content",
            EntryDate = DateTime.UtcNow,
            Mood = Mood.Neutral,
            PromptId = prompt.PromptId,
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Prompts.Add(prompt);
        _context.JournalEntries.Add(entry);
        await _context.SaveChangesAsync();

        // Act
        _context.Prompts.Remove(prompt);
        await _context.SaveChangesAsync();

        var retrievedEntry = await _context.JournalEntries.FindAsync(entry.JournalEntryId);

        // Assert
        Assert.That(retrievedEntry, Is.Not.Null);
        Assert.That(retrievedEntry!.PromptId, Is.Null);
    }

    /// <summary>
    /// Tests that favorite journal entries can be queried.
    /// </summary>
    [Test]
    public async Task JournalEntries_CanQueryFavorites()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var entry1 = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            UserId = userId,
            Title = "Favorite Entry",
            Content = "This is a favorite.",
            EntryDate = DateTime.UtcNow,
            Mood = Mood.Happy,
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
        };

        var entry2 = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            UserId = userId,
            Title = "Regular Entry",
            Content = "This is not a favorite.",
            EntryDate = DateTime.UtcNow,
            Mood = Mood.Neutral,
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.JournalEntries.AddRange(entry1, entry2);
        await _context.SaveChangesAsync();

        // Act
        var favorites = await _context.JournalEntries
            .Where(e => e.UserId == userId && e.IsFavorite)
            .ToListAsync();

        // Assert
        Assert.That(favorites, Has.Count.EqualTo(1));
        Assert.That(favorites[0].Title, Is.EqualTo("Favorite Entry"));
    }
}
