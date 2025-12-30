// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Core.Tests;

public class ReminderSentEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesReminderSentEvent()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ReminderSentEvent
        {
            ReminderId = reminderId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReminderId, Is.EqualTo(reminderId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        // Act
        var evt = new ReminderSentEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReminderId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = new DateTime(2025, 1, 1, 10, 0, 0);

        var evt1 = new ReminderSentEvent
        {
            ReminderId = reminderId,
            UserId = userId,
            Timestamp = timestamp
        };

        var evt2 = new ReminderSentEvent
        {
            ReminderId = reminderId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var evt1 = new ReminderSentEvent
        {
            ReminderId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow
        };

        var evt2 = new ReminderSentEvent
        {
            ReminderId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow
        };

        // Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        // Arrange
        var original = new ReminderSentEvent
        {
            ReminderId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow
        };

        var newTimestamp = new DateTime(2025, 12, 31);

        // Act
        var modified = original with { Timestamp = newTimestamp };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modified.ReminderId, Is.EqualTo(original.ReminderId));
            Assert.That(modified.UserId, Is.EqualTo(original.UserId));
            Assert.That(modified.Timestamp, Is.EqualTo(newTimestamp));
            Assert.That(modified, Is.Not.SameAs(original));
        });
    }

    [Test]
    public void ReminderId_CanBeSetAndRetrieved()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var evt = new ReminderSentEvent { ReminderId = reminderId };

        // Assert
        Assert.That(evt.ReminderId, Is.EqualTo(reminderId));
    }

    [Test]
    public void UserId_CanBeSetAndRetrieved()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var evt = new ReminderSentEvent { UserId = userId };

        // Assert
        Assert.That(evt.UserId, Is.EqualTo(userId));
    }

    [Test]
    public void Timestamp_CanBeSetToSpecificTime()
    {
        // Arrange
        var specificTime = new DateTime(2025, 8, 15, 16, 45, 30);
        var evt = new ReminderSentEvent { Timestamp = specificTime };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(specificTime));
    }

    [Test]
    public void Timestamp_DefaultValue_IsNearCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new ReminderSentEvent();

        // Arrange
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Record_WithMultipleProperties_Equality()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var evt1 = new ReminderSentEvent
        {
            ReminderId = reminderId,
            UserId = userId
        };

        var evt2 = new ReminderSentEvent
        {
            ReminderId = reminderId,
            UserId = userId
        };

        // Assert - Should not be equal because Timestamp is auto-set
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
