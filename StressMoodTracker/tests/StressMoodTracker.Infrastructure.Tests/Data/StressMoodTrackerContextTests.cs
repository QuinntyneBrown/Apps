// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace StressMoodTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the StressMoodTrackerContext.
/// </summary>
[TestFixture]
public class StressMoodTrackerContextTests
{
    private StressMoodTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<StressMoodTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new StressMoodTrackerContext(options);
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
    /// Tests that MoodEntries can be added and retrieved.
    /// </summary>
    [Test]
    public async Task MoodEntries_CanAddAndRetrieve()
    {
        // Arrange
        var moodEntry = new MoodEntry
        {
            MoodEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MoodLevel = MoodLevel.Good,
            StressLevel = StressLevel.Low,
            EntryTime = DateTime.UtcNow,
            Notes = "Test mood entry",
            Activities = "Test activities",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.MoodEntries.Add(moodEntry);
        await _context.SaveChangesAsync();

        var retrieved = await _context.MoodEntries.FindAsync(moodEntry.MoodEntryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Notes, Is.EqualTo("Test mood entry"));
        Assert.That(retrieved.MoodLevel, Is.EqualTo(MoodLevel.Good));
        Assert.That(retrieved.StressLevel, Is.EqualTo(StressLevel.Low));
    }

    /// <summary>
    /// Tests that Triggers can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Triggers_CanAddAndRetrieve()
    {
        // Arrange
        var trigger = new Trigger
        {
            TriggerId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Trigger",
            Description = "Test description",
            TriggerType = "Work",
            ImpactLevel = 4,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Triggers.Add(trigger);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Triggers.FindAsync(trigger.TriggerId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Trigger"));
        Assert.That(retrieved.TriggerType, Is.EqualTo("Work"));
        Assert.That(retrieved.ImpactLevel, Is.EqualTo(4));
    }

    /// <summary>
    /// Tests that Journals can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Journals_CanAddAndRetrieve()
    {
        // Arrange
        var journal = new Journal
        {
            JournalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Journal",
            Content = "Test content for the journal entry",
            EntryDate = DateTime.UtcNow,
            Tags = "test, journal",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Journals.Add(journal);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Journals.FindAsync(journal.JournalId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Journal"));
        Assert.That(retrieved.Content, Is.EqualTo("Test content for the journal entry"));
        Assert.That(retrieved.Tags, Is.EqualTo("test, journal"));
    }

    /// <summary>
    /// Tests that multiple MoodEntries can be queried by user.
    /// </summary>
    [Test]
    public async Task MoodEntries_CanQueryByUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var moodEntry1 = new MoodEntry
        {
            MoodEntryId = Guid.NewGuid(),
            UserId = userId,
            MoodLevel = MoodLevel.Good,
            StressLevel = StressLevel.Low,
            EntryTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var moodEntry2 = new MoodEntry
        {
            MoodEntryId = Guid.NewGuid(),
            UserId = userId,
            MoodLevel = MoodLevel.Excellent,
            StressLevel = StressLevel.Low,
            EntryTime = DateTime.UtcNow.AddHours(-1),
            CreatedAt = DateTime.UtcNow.AddHours(-1),
        };

        // Act
        _context.MoodEntries.AddRange(moodEntry1, moodEntry2);
        await _context.SaveChangesAsync();

        var userEntries = await _context.MoodEntries
            .Where(m => m.UserId == userId)
            .ToListAsync();

        // Assert
        Assert.That(userEntries, Has.Count.EqualTo(2));
        Assert.That(userEntries.All(m => m.UserId == userId), Is.True);
    }

    /// <summary>
    /// Tests that MoodEntries can be updated.
    /// </summary>
    [Test]
    public async Task MoodEntries_CanUpdate()
    {
        // Arrange
        var moodEntry = new MoodEntry
        {
            MoodEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MoodLevel = MoodLevel.Neutral,
            StressLevel = StressLevel.Moderate,
            EntryTime = DateTime.UtcNow,
            Notes = "Initial notes",
            CreatedAt = DateTime.UtcNow,
        };

        _context.MoodEntries.Add(moodEntry);
        await _context.SaveChangesAsync();

        // Act
        moodEntry.Notes = "Updated notes";
        moodEntry.MoodLevel = MoodLevel.Good;
        await _context.SaveChangesAsync();

        var retrieved = await _context.MoodEntries.FindAsync(moodEntry.MoodEntryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Notes, Is.EqualTo("Updated notes"));
        Assert.That(retrieved.MoodLevel, Is.EqualTo(MoodLevel.Good));
    }

    /// <summary>
    /// Tests that MoodEntries can be deleted.
    /// </summary>
    [Test]
    public async Task MoodEntries_CanDelete()
    {
        // Arrange
        var moodEntry = new MoodEntry
        {
            MoodEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MoodLevel = MoodLevel.Good,
            StressLevel = StressLevel.Low,
            EntryTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.MoodEntries.Add(moodEntry);
        await _context.SaveChangesAsync();

        // Act
        _context.MoodEntries.Remove(moodEntry);
        await _context.SaveChangesAsync();

        var retrieved = await _context.MoodEntries.FindAsync(moodEntry.MoodEntryId);

        // Assert
        Assert.That(retrieved, Is.Null);
    }
}
