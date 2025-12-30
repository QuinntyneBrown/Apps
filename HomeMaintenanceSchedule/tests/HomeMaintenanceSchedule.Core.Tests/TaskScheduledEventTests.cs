// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core.Tests;

public class TaskScheduledEventTests
{
    [Test]
    public void TaskScheduledEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var maintenanceTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var dueDate = DateTime.UtcNow.AddDays(30);
        var isRecurring = true;
        var timestamp = DateTime.UtcNow;

        // Act
        var taskEvent = new TaskScheduledEvent
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            DueDate = dueDate,
            IsRecurring = isRecurring,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taskEvent.MaintenanceTaskId, Is.EqualTo(maintenanceTaskId));
            Assert.That(taskEvent.UserId, Is.EqualTo(userId));
            Assert.That(taskEvent.DueDate, Is.EqualTo(dueDate));
            Assert.That(taskEvent.IsRecurring, Is.True);
            Assert.That(taskEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void TaskScheduledEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var taskEvent = new TaskScheduledEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taskEvent.MaintenanceTaskId, Is.EqualTo(Guid.Empty));
            Assert.That(taskEvent.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(taskEvent.DueDate, Is.EqualTo(default(DateTime)));
            Assert.That(taskEvent.IsRecurring, Is.False);
            Assert.That(taskEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void TaskScheduledEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var taskEvent = new TaskScheduledEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DueDate = DateTime.UtcNow.AddDays(15)
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(taskEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void TaskScheduledEvent_IsRecurring_DefaultsToFalse()
    {
        // Act
        var taskEvent = new TaskScheduledEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DueDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(taskEvent.IsRecurring, Is.False);
    }

    [Test]
    public void TaskScheduledEvent_IsRecurring_CanBeTrue()
    {
        // Arrange & Act
        var taskEvent = new TaskScheduledEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DueDate = DateTime.UtcNow,
            IsRecurring = true
        };

        // Assert
        Assert.That(taskEvent.IsRecurring, Is.True);
    }

    [Test]
    public void TaskScheduledEvent_IsImmutable()
    {
        // Arrange
        var maintenanceTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var dueDate = DateTime.UtcNow.AddDays(45);
        var isRecurring = true;

        // Act
        var taskEvent = new TaskScheduledEvent
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            DueDate = dueDate,
            IsRecurring = isRecurring
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(taskEvent.MaintenanceTaskId, Is.EqualTo(maintenanceTaskId));
            Assert.That(taskEvent.UserId, Is.EqualTo(userId));
            Assert.That(taskEvent.DueDate, Is.EqualTo(dueDate));
            Assert.That(taskEvent.IsRecurring, Is.EqualTo(isRecurring));
        });
    }

    [Test]
    public void TaskScheduledEvent_EqualityByValue()
    {
        // Arrange
        var maintenanceTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var dueDate = DateTime.UtcNow.AddDays(20);
        var isRecurring = false;
        var timestamp = DateTime.UtcNow;

        var event1 = new TaskScheduledEvent
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            DueDate = dueDate,
            IsRecurring = isRecurring,
            Timestamp = timestamp
        };

        var event2 = new TaskScheduledEvent
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            DueDate = dueDate,
            IsRecurring = isRecurring,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void TaskScheduledEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new TaskScheduledEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DueDate = DateTime.UtcNow.AddDays(10),
            IsRecurring = true
        };

        var event2 = new TaskScheduledEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DueDate = DateTime.UtcNow.AddDays(20),
            IsRecurring = false
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
