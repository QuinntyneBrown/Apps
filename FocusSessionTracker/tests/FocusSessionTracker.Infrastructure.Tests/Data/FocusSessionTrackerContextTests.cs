// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FocusSessionTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the FocusSessionTrackerContext.
/// </summary>
[TestFixture]
public class FocusSessionTrackerContextTests
{
    private FocusSessionTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FocusSessionTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FocusSessionTrackerContext(options);
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
    /// Tests that FocusSessions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Sessions_CanAddAndRetrieve()
    {
        // Arrange
        var session = new FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionType = SessionType.DeepWork,
            Name = "Test Focus Session",
            PlannedDurationMinutes = 60,
            StartTime = DateTime.UtcNow,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Sessions.FindAsync(session.FocusSessionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Focus Session"));
        Assert.That(retrieved.SessionType, Is.EqualTo(SessionType.DeepWork));
        Assert.That(retrieved.PlannedDurationMinutes, Is.EqualTo(60));
    }

    /// <summary>
    /// Tests that Distractions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Distractions_CanAddAndRetrieve()
    {
        // Arrange
        var session = new FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionType = SessionType.Pomodoro,
            Name = "Test Session",
            PlannedDurationMinutes = 25,
            StartTime = DateTime.UtcNow,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var distraction = new Distraction
        {
            DistractionId = Guid.NewGuid(),
            FocusSessionId = session.FocusSessionId,
            Type = "Email notification",
            Description = "Work email",
            OccurredAt = DateTime.UtcNow,
            DurationMinutes = 5,
            IsInternal = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Sessions.Add(session);
        _context.Distractions.Add(distraction);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Distractions.FindAsync(distraction.DistractionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.FocusSessionId, Is.EqualTo(session.FocusSessionId));
        Assert.That(retrieved.Type, Is.EqualTo("Email notification"));
        Assert.That(retrieved.IsInternal, Is.False);
    }

    /// <summary>
    /// Tests that SessionAnalytics can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Analytics_CanAddAndRetrieve()
    {
        // Arrange
        var analytics = new SessionAnalytics
        {
            SessionAnalyticsId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PeriodStartDate = DateTime.UtcNow.AddDays(-7).Date,
            PeriodEndDate = DateTime.UtcNow.Date,
            TotalSessions = 10,
            TotalFocusMinutes = 600,
            AverageFocusScore = 7.5,
            TotalDistractions = 5,
            CompletionRate = 90.0,
            MostProductiveSessionType = SessionType.DeepWork,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Analytics.Add(analytics);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Analytics.FindAsync(analytics.SessionAnalyticsId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.TotalSessions, Is.EqualTo(10));
        Assert.That(retrieved.TotalFocusMinutes, Is.EqualTo(600));
        Assert.That(retrieved.CompletionRate, Is.EqualTo(90.0));
    }

    /// <summary>
    /// Tests that cascade delete works for Session and Distractions.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedDistractions()
    {
        // Arrange
        var session = new FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionType = SessionType.DeepWork,
            Name = "Test Session",
            PlannedDurationMinutes = 60,
            StartTime = DateTime.UtcNow,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var distraction = new Distraction
        {
            DistractionId = Guid.NewGuid(),
            FocusSessionId = session.FocusSessionId,
            Type = "Phone call",
            OccurredAt = DateTime.UtcNow,
            IsInternal = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Sessions.Add(session);
        _context.Distractions.Add(distraction);
        await _context.SaveChangesAsync();

        // Act
        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();

        var retrievedDistraction = await _context.Distractions.FindAsync(distraction.DistractionId);

        // Assert
        Assert.That(retrievedDistraction, Is.Null);
    }

    /// <summary>
    /// Tests that completed sessions can be queried.
    /// </summary>
    [Test]
    public async Task Sessions_CanQueryCompleted()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var completedSession = new FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = userId,
            SessionType = SessionType.DeepWork,
            Name = "Completed Session",
            PlannedDurationMinutes = 60,
            StartTime = DateTime.UtcNow.AddHours(-2),
            EndTime = DateTime.UtcNow.AddHours(-1),
            IsCompleted = true,
            CreatedAt = DateTime.UtcNow,
        };

        var incompleteSession = new FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = userId,
            SessionType = SessionType.Pomodoro,
            Name = "Incomplete Session",
            PlannedDurationMinutes = 25,
            StartTime = DateTime.UtcNow,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Sessions.AddRange(completedSession, incompleteSession);
        await _context.SaveChangesAsync();

        // Act
        var completedSessions = await _context.Sessions
            .Where(s => s.UserId == userId && s.IsCompleted)
            .ToListAsync();

        // Assert
        Assert.That(completedSessions, Has.Count.EqualTo(1));
        Assert.That(completedSessions[0].Name, Is.EqualTo("Completed Session"));
    }
}
