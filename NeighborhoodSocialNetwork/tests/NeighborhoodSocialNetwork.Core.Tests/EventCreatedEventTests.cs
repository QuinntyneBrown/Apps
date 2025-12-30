// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core.Tests;

public class EventCreatedEventTests
{
    [Test]
    public void EventCreatedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var neighborId = Guid.NewGuid();
        var title = "Summer Block Party";
        var eventDateTime = new DateTime(2024, 7, 15, 14, 0, 0);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new EventCreatedEvent
        {
            EventId = eventId,
            CreatedByNeighborId = neighborId,
            Title = title,
            EventDateTime = eventDateTime,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.EventId, Is.EqualTo(eventId));
            Assert.That(evt.CreatedByNeighborId, Is.EqualTo(neighborId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.EventDateTime, Is.EqualTo(eventDateTime));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void EventCreatedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new EventCreatedEvent
        {
            EventId = Guid.NewGuid(),
            CreatedByNeighborId = Guid.NewGuid(),
            Title = "Test Event",
            EventDateTime = DateTime.UtcNow
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void EventCreatedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var neighborId = Guid.NewGuid();
        var eventDateTime = new DateTime(2024, 7, 15, 14, 0, 0);
        var timestamp = DateTime.UtcNow;

        var evt1 = new EventCreatedEvent
        {
            EventId = eventId,
            CreatedByNeighborId = neighborId,
            Title = "Test Event",
            EventDateTime = eventDateTime,
            Timestamp = timestamp
        };

        var evt2 = new EventCreatedEvent
        {
            EventId = eventId,
            CreatedByNeighborId = neighborId,
            Title = "Test Event",
            EventDateTime = eventDateTime,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void EventCreatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new EventCreatedEvent
        {
            EventId = Guid.NewGuid(),
            CreatedByNeighborId = Guid.NewGuid(),
            Title = "Event 1",
            EventDateTime = DateTime.UtcNow
        };

        var evt2 = new EventCreatedEvent
        {
            EventId = Guid.NewGuid(),
            CreatedByNeighborId = Guid.NewGuid(),
            Title = "Event 2",
            EventDateTime = DateTime.UtcNow
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
