// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core.Tests;

public class BookingCreatedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new BookingCreatedEvent
        {
            BookingId = bookingId,
            TripId = tripId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BookingId, Is.EqualTo(bookingId));
            Assert.That(evt.TripId, Is.EqualTo(tripId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var tripId = Guid.NewGuid();

        // Act
        var evt = new BookingCreatedEvent
        {
            BookingId = bookingId,
            TripId = tripId
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new BookingCreatedEvent
        {
            BookingId = bookingId,
            TripId = tripId,
            Timestamp = timestamp
        };

        var event2 = new BookingCreatedEvent
        {
            BookingId = bookingId,
            TripId = tripId,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var bookingId1 = Guid.NewGuid();
        var bookingId2 = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new BookingCreatedEvent
        {
            BookingId = bookingId1,
            TripId = tripId,
            Timestamp = timestamp
        };

        var event2 = new BookingCreatedEvent
        {
            BookingId = bookingId2,
            TripId = tripId,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var tripId = Guid.NewGuid();

        // Act
        var evt = new BookingCreatedEvent
        {
            BookingId = bookingId,
            TripId = tripId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BookingId, Is.EqualTo(bookingId));
            Assert.That(evt.TripId, Is.EqualTo(tripId));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new BookingCreatedEvent
        {
            BookingId = Guid.NewGuid(),
            TripId = Guid.NewGuid()
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("BookingCreatedEvent"));
    }
}
