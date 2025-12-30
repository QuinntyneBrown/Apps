// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SleepQualityTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the SleepQualityTrackerContext.
/// </summary>
[TestFixture]
public class SleepQualityTrackerContextTests
{
    private SleepQualityTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SleepQualityTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SleepQualityTrackerContext(options);
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
    /// Tests that SleepSessions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task SleepSessions_CanAddAndRetrieve()
    {
        // Arrange
        var sleepSession = new SleepSession
        {
            SleepSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Bedtime = new DateTime(2024, 3, 1, 23, 0, 0),
            WakeTime = new DateTime(2024, 3, 2, 7, 0, 0),
            TotalSleepMinutes = 480,
            SleepQuality = SleepQuality.Good,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.SleepSessions.Add(sleepSession);
        await _context.SaveChangesAsync();

        var retrieved = await _context.SleepSessions.FindAsync(sleepSession.SleepSessionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.TotalSleepMinutes, Is.EqualTo(480));
        Assert.That(retrieved.SleepQuality, Is.EqualTo(SleepQuality.Good));
    }

    /// <summary>
    /// Tests that Habits can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Habits_CanAddAndRetrieve()
    {
        // Arrange
        var habit = new Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Evening Exercise",
            HabitType = "Exercise",
            IsPositive = true,
            ImpactLevel = 4,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Habits.FindAsync(habit.HabitId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Evening Exercise"));
        Assert.That(retrieved.IsPositive, Is.True);
        Assert.That(retrieved.ImpactLevel, Is.EqualTo(4));
    }

    /// <summary>
    /// Tests that Patterns can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Patterns_CanAddAndRetrieve()
    {
        // Arrange
        var pattern = new Pattern
        {
            PatternId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Sleep Consistency",
            Description = "Better sleep with consistent schedule",
            PatternType = "Weekly",
            StartDate = new DateTime(2024, 2, 1),
            EndDate = new DateTime(2024, 3, 1),
            ConfidenceLevel = 85,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Patterns.Add(pattern);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Patterns.FindAsync(pattern.PatternId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Sleep Consistency"));
        Assert.That(retrieved.ConfidenceLevel, Is.EqualTo(85));
    }

    /// <summary>
    /// Tests that sleep sessions can be updated.
    /// </summary>
    [Test]
    public async Task SleepSessions_CanUpdate()
    {
        // Arrange
        var sleepSession = new SleepSession
        {
            SleepSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Bedtime = new DateTime(2024, 3, 1, 23, 0, 0),
            WakeTime = new DateTime(2024, 3, 2, 7, 0, 0),
            TotalSleepMinutes = 480,
            SleepQuality = SleepQuality.Fair,
            CreatedAt = DateTime.UtcNow,
        };

        _context.SleepSessions.Add(sleepSession);
        await _context.SaveChangesAsync();

        // Act
        sleepSession.SleepQuality = SleepQuality.Good;
        sleepSession.Notes = "Actually slept better than expected";
        await _context.SaveChangesAsync();

        var retrieved = await _context.SleepSessions.FindAsync(sleepSession.SleepSessionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.SleepQuality, Is.EqualTo(SleepQuality.Good));
        Assert.That(retrieved.Notes, Is.EqualTo("Actually slept better than expected"));
    }

    /// <summary>
    /// Tests that sleep sessions can be deleted.
    /// </summary>
    [Test]
    public async Task SleepSessions_CanDelete()
    {
        // Arrange
        var sleepSession = new SleepSession
        {
            SleepSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Bedtime = new DateTime(2024, 3, 1, 23, 0, 0),
            WakeTime = new DateTime(2024, 3, 2, 7, 0, 0),
            TotalSleepMinutes = 480,
            SleepQuality = SleepQuality.Good,
            CreatedAt = DateTime.UtcNow,
        };

        _context.SleepSessions.Add(sleepSession);
        await _context.SaveChangesAsync();

        // Act
        _context.SleepSessions.Remove(sleepSession);
        await _context.SaveChangesAsync();

        var retrieved = await _context.SleepSessions.FindAsync(sleepSession.SleepSessionId);

        // Assert
        Assert.That(retrieved, Is.Null);
    }
}
