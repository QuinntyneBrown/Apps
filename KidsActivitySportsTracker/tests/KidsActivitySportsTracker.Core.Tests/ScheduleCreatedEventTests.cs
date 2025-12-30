// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Core.Tests;

public class ScheduleCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        var activityId = Guid.NewGuid();
        var eventType = "Practice";
        var dateTime = DateTime.UtcNow.AddDays(1);

        // Act
        var evt = new ScheduleCreatedEvent
        {
            ScheduleId = scheduleId,
            ActivityId = activityId,
            EventType = eventType,
            DateTime = dateTime
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ScheduleId, Is.EqualTo(scheduleId));
            Assert.That(evt.ActivityId, Is.EqualTo(activityId));
            Assert.That(evt.EventType, Is.EqualTo(eventType));
            Assert.That(evt.DateTime, Is.EqualTo(dateTime));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Timestamp_IsSetAutomatically()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new ScheduleCreatedEvent
        {
            ScheduleId = Guid.NewGuid(),
            ActivityId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void ScheduleId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var evt = new ScheduleCreatedEvent { ScheduleId = expectedId };

        // Assert
        Assert.That(evt.ScheduleId, Is.EqualTo(expectedId));
    }

    [Test]
    public void ActivityId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedActivityId = Guid.NewGuid();

        // Act
        var evt = new ScheduleCreatedEvent { ActivityId = expectedActivityId };

        // Assert
        Assert.That(evt.ActivityId, Is.EqualTo(expectedActivityId));
    }

    [Test]
    public void EventType_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedEventType = "Game";

        // Act
        var evt = new ScheduleCreatedEvent { EventType = expectedEventType };

        // Assert
        Assert.That(evt.EventType, Is.EqualTo(expectedEventType));
    }

    [Test]
    public void DateTime_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedDateTime = DateTime.UtcNow.AddDays(7);

        // Act
        var evt = new ScheduleCreatedEvent { DateTime = expectedDateTime };

        // Assert
        Assert.That(evt.DateTime, Is.EqualTo(expectedDateTime));
    }
}
