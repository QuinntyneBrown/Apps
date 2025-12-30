// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HabitFormationApp.Core.Tests;

public class EventTests
{
    [Test]
    public void StreakUpdatedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var streakId = Guid.NewGuid();
        var habitId = Guid.NewGuid();
        var currentStreak = 10;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new StreakUpdatedEvent
        {
            StreakId = streakId,
            HabitId = habitId,
            CurrentStreak = currentStreak,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.StreakId, Is.EqualTo(streakId));
            Assert.That(evt.HabitId, Is.EqualTo(habitId));
            Assert.That(evt.CurrentStreak, Is.EqualTo(currentStreak));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void StreakUpdatedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new StreakUpdatedEvent
        {
            StreakId = Guid.NewGuid(),
            HabitId = Guid.NewGuid(),
            CurrentStreak = 5
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void HabitCreatedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var habitId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Exercise Daily";
        var frequency = HabitFrequency.Daily;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new HabitCreatedEvent
        {
            HabitId = habitId,
            UserId = userId,
            Name = name,
            Frequency = frequency,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.HabitId, Is.EqualTo(habitId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Frequency, Is.EqualTo(frequency));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void HabitCreatedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new HabitCreatedEvent
        {
            HabitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Read",
            Frequency = HabitFrequency.Daily
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void ReminderScheduledEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var habitId = Guid.NewGuid();
        var reminderTime = new TimeSpan(8, 0, 0);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ReminderScheduledEvent
        {
            ReminderId = reminderId,
            HabitId = habitId,
            ReminderTime = reminderTime,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReminderId, Is.EqualTo(reminderId));
            Assert.That(evt.HabitId, Is.EqualTo(habitId));
            Assert.That(evt.ReminderTime, Is.EqualTo(reminderTime));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ReminderScheduledEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new ReminderScheduledEvent
        {
            ReminderId = Guid.NewGuid(),
            HabitId = Guid.NewGuid(),
            ReminderTime = new TimeSpan(9, 30, 0)
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Events_AreRecords_AndSupportValueEquality()
    {
        // Arrange
        var id = Guid.NewGuid();
        var habitId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new StreakUpdatedEvent
        {
            StreakId = id,
            HabitId = habitId,
            CurrentStreak = 7,
            Timestamp = timestamp
        };

        var event2 = new StreakUpdatedEvent
        {
            StreakId = id,
            HabitId = habitId,
            CurrentStreak = 7,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void HabitCreatedEvent_SupportsAllFrequencies()
    {
        // Arrange & Act
        var dailyEvent = new HabitCreatedEvent { Frequency = HabitFrequency.Daily };
        var weeklyEvent = new HabitCreatedEvent { Frequency = HabitFrequency.Weekly };
        var customEvent = new HabitCreatedEvent { Frequency = HabitFrequency.Custom };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dailyEvent.Frequency, Is.EqualTo(HabitFrequency.Daily));
            Assert.That(weeklyEvent.Frequency, Is.EqualTo(HabitFrequency.Weekly));
            Assert.That(customEvent.Frequency, Is.EqualTo(HabitFrequency.Custom));
        });
    }
}
