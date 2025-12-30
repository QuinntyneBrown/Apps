// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Api.Features.Distraction;
using FocusSessionTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Tests;

/// <summary>
/// Tests for distraction commands and queries.
/// </summary>
[TestFixture]
public class DistractionTests
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
    public async Task CreateDistraction_ShouldCreateDistraction()
    {
        // Arrange
        var session = new Core.FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionType = SessionType.DeepWork,
            Name = "Test Session",
            PlannedDurationMinutes = 60,
            StartTime = DateTime.UtcNow
        };

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        var handler = new CreateDistractionCommandHandler(_context);
        var command = new CreateDistractionCommand
        {
            FocusSessionId = session.FocusSessionId,
            Type = "Phone",
            Description = "Received a call",
            OccurredAt = DateTime.UtcNow,
            DurationMinutes = 5,
            IsInternal = false
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Type, Is.EqualTo("Phone"));
        Assert.That(result.DurationMinutes, Is.EqualTo(5));
        Assert.That(result.IsInternal, Is.False);
    }

    [Test]
    public async Task GetDistractions_ShouldReturnDistractionsForSession()
    {
        // Arrange
        var session = new Core.FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionType = SessionType.Study,
            Name = "Study Session",
            PlannedDurationMinutes = 90,
            StartTime = DateTime.UtcNow
        };

        var distraction1 = new Core.Distraction
        {
            DistractionId = Guid.NewGuid(),
            FocusSessionId = session.FocusSessionId,
            Type = "Email",
            OccurredAt = DateTime.UtcNow,
            IsInternal = false
        };

        var distraction2 = new Core.Distraction
        {
            DistractionId = Guid.NewGuid(),
            FocusSessionId = session.FocusSessionId,
            Type = "Mind wandering",
            OccurredAt = DateTime.UtcNow,
            IsInternal = true
        };

        _context.Sessions.Add(session);
        _context.Distractions.Add(distraction1);
        _context.Distractions.Add(distraction2);
        await _context.SaveChangesAsync();

        var handler = new GetDistractionsQueryHandler(_context);
        var query = new GetDistractionsQuery { FocusSessionId = session.FocusSessionId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task DeleteDistraction_ShouldDeleteDistraction()
    {
        // Arrange
        var session = new Core.FocusSession
        {
            FocusSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionType = SessionType.Pomodoro,
            Name = "Pomodoro Session",
            PlannedDurationMinutes = 25,
            StartTime = DateTime.UtcNow
        };

        var distraction = new Core.Distraction
        {
            DistractionId = Guid.NewGuid(),
            FocusSessionId = session.FocusSessionId,
            Type = "Social Media",
            OccurredAt = DateTime.UtcNow,
            IsInternal = false
        };

        _context.Sessions.Add(session);
        _context.Distractions.Add(distraction);
        await _context.SaveChangesAsync();

        var handler = new DeleteDistractionCommandHandler(_context);
        var command = new DeleteDistractionCommand { DistractionId = distraction.DistractionId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        var deletedDistraction = await _context.Distractions.FindAsync(distraction.DistractionId);
        Assert.That(deletedDistraction, Is.Null);
    }

    [TearDown]
    public void TearDown()
    {
        _context?.Dispose();
    }
}
