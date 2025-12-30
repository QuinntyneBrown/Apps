// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TravelDestinationWishlist.Core.Tests;

public class EventTests
{
    [Test]
    public void DestinationAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var destinationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Paris";
        var type = DestinationType.City;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new DestinationAddedEvent
        {
            DestinationId = destinationId,
            UserId = userId,
            Name = name,
            DestinationType = type,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.DestinationId, Is.EqualTo(destinationId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Name, Is.EqualTo(name));
            Assert.That(eventData.DestinationType, Is.EqualTo(type));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DestinationVisitedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var destinationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var visitedDate = new DateTime(2024, 6, 15);
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new DestinationVisitedEvent
        {
            DestinationId = destinationId,
            UserId = userId,
            VisitedDate = visitedDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.DestinationId, Is.EqualTo(destinationId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.VisitedDate, Is.EqualTo(visitedDate));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void TripPlannedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var destinationId = Guid.NewGuid();
        var startDate = new DateTime(2024, 6, 1);
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new TripPlannedEvent
        {
            TripId = tripId,
            UserId = userId,
            DestinationId = destinationId,
            StartDate = startDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.TripId, Is.EqualTo(tripId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.DestinationId, Is.EqualTo(destinationId));
            Assert.That(eventData.StartDate, Is.EqualTo(startDate));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void TripCompletedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var destinationId = Guid.NewGuid();
        var endDate = new DateTime(2024, 6, 15);
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new TripCompletedEvent
        {
            TripId = tripId,
            UserId = userId,
            DestinationId = destinationId,
            EndDate = endDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.TripId, Is.EqualTo(tripId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.DestinationId, Is.EqualTo(destinationId));
            Assert.That(eventData.EndDate, Is.EqualTo(endDate));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MemoryCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var memoryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var title = "Eiffel Tower";
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new MemoryCreatedEvent
        {
            MemoryId = memoryId,
            UserId = userId,
            TripId = tripId,
            Title = title,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.MemoryId, Is.EqualTo(memoryId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.TripId, Is.EqualTo(tripId));
            Assert.That(eventData.Title, Is.EqualTo(title));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MemoryUpdatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var memoryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new MemoryUpdatedEvent
        {
            MemoryId = memoryId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.MemoryId, Is.EqualTo(memoryId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Events_Timestamp_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var event1 = new DestinationAddedEvent { DestinationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Test", DestinationType = DestinationType.City };
        var event2 = new TripPlannedEvent { TripId = Guid.NewGuid(), UserId = Guid.NewGuid(), DestinationId = Guid.NewGuid(), StartDate = DateTime.UtcNow };
        var event3 = new MemoryCreatedEvent { MemoryId = Guid.NewGuid(), UserId = Guid.NewGuid(), TripId = Guid.NewGuid(), Title = "Test" };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(event1.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
            Assert.That(event2.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
            Assert.That(event3.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Events_AreRecords_SupportValueEquality()
    {
        // Arrange
        var destinationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new DestinationAddedEvent
        {
            DestinationId = destinationId,
            UserId = userId,
            Name = "Paris",
            DestinationType = DestinationType.City,
            Timestamp = timestamp
        };

        var event2 = new DestinationAddedEvent
        {
            DestinationId = destinationId,
            UserId = userId,
            Name = "Paris",
            DestinationType = DestinationType.City,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }
}
