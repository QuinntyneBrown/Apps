// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Api.Features.Events;
using ConferenceEventManager.Core;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Tests.Features.Events;

[TestFixture]
public class GetEventsTests
{
    private IConferenceEventManagerContext _context = null!;
    private GetEvents.Handler _handler = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<Infrastructure.Data.ConferenceEventManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new Infrastructure.Data.ConferenceEventManagerContext(options);
        _handler = new GetEvents.Handler(_context);
    }

    [Test]
    public async Task Handle_ReturnsAllEvents_WhenNoFilterProvided()
    {
        // Arrange
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();

        _context.Events.Add(new Event
        {
            EventId = Guid.NewGuid(),
            UserId = userId1,
            Name = "Event 1",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow.AddDays(30),
            EndDate = DateTime.UtcNow.AddDays(32)
        });

        _context.Events.Add(new Event
        {
            EventId = Guid.NewGuid(),
            UserId = userId2,
            Name = "Event 2",
            EventType = EventType.Workshop,
            StartDate = DateTime.UtcNow.AddDays(40),
            EndDate = DateTime.UtcNow.AddDays(42)
        });

        await _context.SaveChangesAsync();

        var query = new GetEvents.Query();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
    }

    [Test]
    public async Task Handle_ReturnsFilteredEvents_WhenUserIdProvided()
    {
        // Arrange
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();

        _context.Events.Add(new Event
        {
            EventId = Guid.NewGuid(),
            UserId = userId1,
            Name = "Event 1",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow.AddDays(30),
            EndDate = DateTime.UtcNow.AddDays(32)
        });

        _context.Events.Add(new Event
        {
            EventId = Guid.NewGuid(),
            UserId = userId2,
            Name = "Event 2",
            EventType = EventType.Workshop,
            StartDate = DateTime.UtcNow.AddDays(40),
            EndDate = DateTime.UtcNow.AddDays(42)
        });

        await _context.SaveChangesAsync();

        var query = new GetEvents.Query { UserId = userId1 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().HaveCount(1);
        result.First().UserId.Should().Be(userId1);
    }
}
