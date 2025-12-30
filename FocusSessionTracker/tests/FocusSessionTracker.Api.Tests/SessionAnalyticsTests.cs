// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Api.Features.SessionAnalytics;
using FocusSessionTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Tests;

/// <summary>
/// Tests for session analytics commands and queries.
/// </summary>
[TestFixture]
public class SessionAnalyticsTests
{
    private IFocusSessionTrackerContext _context = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<Infrastructure.FocusSessionTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new Infrastructure.FocusSessionTrackerContext(options);
    }

    [Test]
    public async Task GenerateAnalytics_ShouldGenerateAnalyticsForUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var startDate = DateTime.UtcNow.AddDays(-7);
        var endDate = DateTime.UtcNow;

        var session1 = new Core.FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = userId,
            SessionType = SessionType.DeepWork,
            Name = "Session 1",
            PlannedDurationMinutes = 60,
            StartTime = DateTime.UtcNow.AddDays(-5),
            EndTime = DateTime.UtcNow.AddDays(-5).AddHours(1),
            IsCompleted = true,
            FocusScore = 8
        };

        var session2 = new Core.FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = userId,
            SessionType = SessionType.Study,
            Name = "Session 2",
            PlannedDurationMinutes = 90,
            StartTime = DateTime.UtcNow.AddDays(-3),
            EndTime = DateTime.UtcNow.AddDays(-3).AddMinutes(90),
            IsCompleted = true,
            FocusScore = 9
        };

        _context.Sessions.Add(session1);
        _context.Sessions.Add(session2);
        await _context.SaveChangesAsync();

        var handler = new GenerateAnalyticsCommandHandler(_context);
        var command = new GenerateAnalyticsCommand
        {
            UserId = userId,
            PeriodStartDate = startDate,
            PeriodEndDate = endDate
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(userId));
        Assert.That(result.TotalSessions, Is.EqualTo(2));
        Assert.That(result.CompletionRate, Is.EqualTo(100));
        Assert.That(result.AverageFocusScore, Is.EqualTo(8.5));
    }

    [Test]
    public async Task GetAnalytics_ShouldReturnAnalyticsForUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var analytics1 = new Core.SessionAnalytics
        {
            SessionAnalyticsId = Guid.NewGuid(),
            UserId = userId,
            PeriodStartDate = DateTime.UtcNow.AddDays(-14),
            PeriodEndDate = DateTime.UtcNow.AddDays(-7),
            TotalSessions = 5,
            TotalFocusMinutes = 300,
            CompletionRate = 80
        };

        var analytics2 = new Core.SessionAnalytics
        {
            SessionAnalyticsId = Guid.NewGuid(),
            UserId = userId,
            PeriodStartDate = DateTime.UtcNow.AddDays(-7),
            PeriodEndDate = DateTime.UtcNow,
            TotalSessions = 8,
            TotalFocusMinutes = 480,
            CompletionRate = 87.5
        };

        _context.Analytics.Add(analytics1);
        _context.Analytics.Add(analytics2);
        await _context.SaveChangesAsync();

        var handler = new GetAnalyticsQueryHandler(_context);
        var query = new GetAnalyticsQuery { UserId = userId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.All(a => a.UserId == userId), Is.True);
    }

    [Test]
    public async Task GenerateAnalytics_ShouldCalculateCorrectCompletionRate()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var startDate = DateTime.UtcNow.AddDays(-7);
        var endDate = DateTime.UtcNow;

        // 3 completed sessions
        for (int i = 0; i < 3; i++)
        {
            var completedSession = new Core.FocusSession
            {
                FocusSessionId = Guid.NewGuid(),
                UserId = userId,
                SessionType = SessionType.Pomodoro,
                Name = $"Completed Session {i}",
                PlannedDurationMinutes = 25,
                StartTime = DateTime.UtcNow.AddDays(-i - 1),
                EndTime = DateTime.UtcNow.AddDays(-i - 1).AddMinutes(25),
                IsCompleted = true,
                FocusScore = 7 + i
            };
            _context.Sessions.Add(completedSession);
        }

        // 2 incomplete sessions
        for (int i = 0; i < 2; i++)
        {
            var incompleteSession = new Core.FocusSession
            {
                FocusSessionId = Guid.NewGuid(),
                UserId = userId,
                SessionType = SessionType.Study,
                Name = $"Incomplete Session {i}",
                PlannedDurationMinutes = 60,
                StartTime = DateTime.UtcNow.AddDays(-i - 1),
                IsCompleted = false
            };
            _context.Sessions.Add(incompleteSession);
        }

        await _context.SaveChangesAsync();

        var handler = new GenerateAnalyticsCommandHandler(_context);
        var command = new GenerateAnalyticsCommand
        {
            UserId = userId,
            PeriodStartDate = startDate,
            PeriodEndDate = endDate
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TotalSessions, Is.EqualTo(5));
        Assert.That(result.CompletionRate, Is.EqualTo(60)); // 3 out of 5 = 60%
    }

    [TearDown]
    public void TearDown()
    {
        _context?.Dispose();
    }
}
