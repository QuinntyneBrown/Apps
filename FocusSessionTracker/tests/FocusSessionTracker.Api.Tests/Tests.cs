// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Api.Features.FocusSession;
using FocusSessionTracker.Core;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FocusSessionTracker.Api.Tests;

/// <summary>
/// Tests for focus session commands and queries.
/// </summary>
[TestFixture]
public class FocusSessionTests
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
    public async Task CreateFocusSession_ShouldCreateSession()
    {
        // Arrange
        var handler = new CreateFocusSessionCommandHandler(_context);
        var command = new CreateFocusSessionCommand
        {
            UserId = Guid.NewGuid(),
            SessionType = SessionType.DeepWork,
            Name = "Test Session",
            PlannedDurationMinutes = 60,
            StartTime = DateTime.UtcNow,
            Notes = "Test notes"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Test Session"));
        Assert.That(result.PlannedDurationMinutes, Is.EqualTo(60));
        Assert.That(result.SessionType, Is.EqualTo(SessionType.DeepWork));
    }

    [Test]
    public async Task GetFocusSessions_ShouldReturnAllSessions()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var session1 = new Core.FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = userId,
            SessionType = SessionType.Pomodoro,
            Name = "Session 1",
            PlannedDurationMinutes = 25,
            StartTime = DateTime.UtcNow
        };
        var session2 = new Core.FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = userId,
            SessionType = SessionType.Study,
            Name = "Session 2",
            PlannedDurationMinutes = 50,
            StartTime = DateTime.UtcNow
        };

        _context.Sessions.Add(session1);
        _context.Sessions.Add(session2);
        await _context.SaveChangesAsync();

        var handler = new GetFocusSessionsQueryHandler(_context);
        var query = new GetFocusSessionsQuery { UserId = userId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task CompleteFocusSession_ShouldCompleteSession()
    {
        // Arrange
        var session = new Core.FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionType = SessionType.DeepWork,
            Name = "Test Session",
            PlannedDurationMinutes = 60,
            StartTime = DateTime.UtcNow.AddHours(-1)
        };

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        var handler = new CompleteFocusSessionCommandHandler(_context);
        var command = new CompleteFocusSessionCommand
        {
            FocusSessionId = session.FocusSessionId,
            EndTime = DateTime.UtcNow,
            FocusScore = 8
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.IsCompleted, Is.True);
        Assert.That(result.FocusScore, Is.EqualTo(8));
        Assert.That(result.EndTime, Is.Not.Null);
    }

    [Test]
    public async Task DeleteFocusSession_ShouldDeleteSession()
    {
        // Arrange
        var session = new Core.FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionType = SessionType.Planning,
            Name = "To Delete",
            PlannedDurationMinutes = 30,
            StartTime = DateTime.UtcNow
        };

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        var handler = new DeleteFocusSessionCommandHandler(_context);
        var command = new DeleteFocusSessionCommand { FocusSessionId = session.FocusSessionId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        var deletedSession = await _context.Sessions.FindAsync(session.FocusSessionId);
        Assert.That(deletedSession, Is.Null);
    }

    [TearDown]
    public void TearDown()
    {
        _context?.Dispose();
    }
}
