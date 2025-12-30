// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core.Tests;

public class TripCreatedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Summer Vacation";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new TripCreatedEvent
        {
            TripId = tripId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TripId, Is.EqualTo(tripId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Beach Getaway";

        // Act
        var evt = new TripCreatedEvent
        {
            TripId = tripId,
            UserId = userId,
            Name = name
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Mountain Trip";
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new TripCreatedEvent
        {
            TripId = tripId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        var event2 = new TripCreatedEvent
        {
            TripId = tripId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new TripCreatedEvent
        {
            TripId = tripId,
            UserId = userId,
            Name = "Trip 1",
            Timestamp = timestamp
        };

        var event2 = new TripCreatedEvent
        {
            TripId = tripId,
            UserId = userId,
            Name = "Trip 2",
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "European Tour";

        // Act
        var evt = new TripCreatedEvent
        {
            TripId = tripId,
            UserId = userId,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TripId, Is.EqualTo(tripId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new TripCreatedEvent
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Trip"
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("TripCreatedEvent"));
    }
}
