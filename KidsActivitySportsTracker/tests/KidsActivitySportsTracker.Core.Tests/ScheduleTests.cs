// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Core.Tests;

public class ScheduleTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesSchedule()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        var activityId = Guid.NewGuid();
        var eventType = "Practice";
        var dateTime = DateTime.UtcNow.AddDays(1);

        // Act
        var schedule = new Schedule
        {
            ScheduleId = scheduleId,
            ActivityId = activityId,
            EventType = eventType,
            DateTime = dateTime
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(schedule.ScheduleId, Is.EqualTo(scheduleId));
            Assert.That(schedule.ActivityId, Is.EqualTo(activityId));
            Assert.That(schedule.EventType, Is.EqualTo(eventType));
            Assert.That(schedule.DateTime, Is.EqualTo(dateTime));
            Assert.That(schedule.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ScheduleId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new Schedule();
        var expectedId = Guid.NewGuid();

        // Act
        schedule.ScheduleId = expectedId;

        // Assert
        Assert.That(schedule.ScheduleId, Is.EqualTo(expectedId));
    }

    [Test]
    public void ActivityId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new Schedule();
        var expectedActivityId = Guid.NewGuid();

        // Act
        schedule.ActivityId = expectedActivityId;

        // Assert
        Assert.That(schedule.ActivityId, Is.EqualTo(expectedActivityId));
    }

    [Test]
    public void EventType_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new Schedule();
        var expectedEventType = "Game";

        // Act
        schedule.EventType = expectedEventType;

        // Assert
        Assert.That(schedule.EventType, Is.EqualTo(expectedEventType));
    }

    [Test]
    public void DateTime_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new Schedule();
        var expectedDateTime = DateTime.UtcNow.AddDays(7);

        // Act
        schedule.DateTime = expectedDateTime;

        // Assert
        Assert.That(schedule.DateTime, Is.EqualTo(expectedDateTime));
    }

    [Test]
    public void Location_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new Schedule();
        var expectedLocation = "Central Park Field 3";

        // Act
        schedule.Location = expectedLocation;

        // Assert
        Assert.That(schedule.Location, Is.EqualTo(expectedLocation));
    }

    [Test]
    public void DurationMinutes_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new Schedule();
        var expectedDuration = 90;

        // Act
        schedule.DurationMinutes = expectedDuration;

        // Assert
        Assert.That(schedule.DurationMinutes, Is.EqualTo(expectedDuration));
    }

    [Test]
    public void Notes_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new Schedule();
        var expectedNotes = "Bring soccer ball and water";

        // Act
        schedule.Notes = expectedNotes;

        // Assert
        Assert.That(schedule.Notes, Is.EqualTo(expectedNotes));
    }

    [Test]
    public void IsConfirmed_DefaultsToFalse()
    {
        // Arrange & Act
        var schedule = new Schedule();

        // Assert
        Assert.That(schedule.IsConfirmed, Is.False);
    }

    [Test]
    public void IsConfirmed_CanBeSetToTrue()
    {
        // Arrange
        var schedule = new Schedule();

        // Act
        schedule.IsConfirmed = true;

        // Assert
        Assert.That(schedule.IsConfirmed, Is.True);
    }

    [Test]
    public void Activity_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var schedule = new Schedule();
        var activity = new Activity { ActivityId = Guid.NewGuid() };

        // Act
        schedule.Activity = activity;

        // Assert
        Assert.That(schedule.Activity, Is.EqualTo(activity));
    }

    [Test]
    public void DurationMinutes_CanBeNull()
    {
        // Arrange
        var schedule = new Schedule();

        // Act
        schedule.DurationMinutes = null;

        // Assert
        Assert.That(schedule.DurationMinutes, Is.Null);
    }

    [Test]
    public void Location_CanBeNull()
    {
        // Arrange
        var schedule = new Schedule();

        // Act
        schedule.Location = null;

        // Assert
        Assert.That(schedule.Location, Is.Null);
    }
}
