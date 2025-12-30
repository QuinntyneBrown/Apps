// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core.Tests;

public class TaskCompletedEventTests
{
    [Test]
    public void TaskCompletedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var maintenanceTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var completedDate = DateTime.UtcNow;
        var actualCost = 175.50m;
        var timestamp = DateTime.UtcNow;

        // Act
        var taskEvent = new TaskCompletedEvent
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            CompletedDate = completedDate,
            ActualCost = actualCost,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taskEvent.MaintenanceTaskId, Is.EqualTo(maintenanceTaskId));
            Assert.That(taskEvent.UserId, Is.EqualTo(userId));
            Assert.That(taskEvent.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(taskEvent.ActualCost, Is.EqualTo(actualCost));
            Assert.That(taskEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void TaskCompletedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var taskEvent = new TaskCompletedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taskEvent.MaintenanceTaskId, Is.EqualTo(Guid.Empty));
            Assert.That(taskEvent.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(taskEvent.CompletedDate, Is.EqualTo(default(DateTime)));
            Assert.That(taskEvent.ActualCost, Is.Null);
            Assert.That(taskEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void TaskCompletedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var taskEvent = new TaskCompletedEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletedDate = DateTime.UtcNow
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(taskEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void TaskCompletedEvent_WithoutActualCost_IsValid()
    {
        // Arrange & Act
        var taskEvent = new TaskCompletedEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletedDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(taskEvent.ActualCost, Is.Null);
    }

    [Test]
    public void TaskCompletedEvent_IsImmutable()
    {
        // Arrange
        var maintenanceTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var completedDate = DateTime.UtcNow;
        var actualCost = 250m;

        // Act
        var taskEvent = new TaskCompletedEvent
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            CompletedDate = completedDate,
            ActualCost = actualCost
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(taskEvent.MaintenanceTaskId, Is.EqualTo(maintenanceTaskId));
            Assert.That(taskEvent.UserId, Is.EqualTo(userId));
            Assert.That(taskEvent.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(taskEvent.ActualCost, Is.EqualTo(actualCost));
        });
    }

    [Test]
    public void TaskCompletedEvent_EqualityByValue()
    {
        // Arrange
        var maintenanceTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var completedDate = DateTime.UtcNow;
        var actualCost = 300m;
        var timestamp = DateTime.UtcNow;

        var event1 = new TaskCompletedEvent
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            CompletedDate = completedDate,
            ActualCost = actualCost,
            Timestamp = timestamp
        };

        var event2 = new TaskCompletedEvent
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            CompletedDate = completedDate,
            ActualCost = actualCost,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void TaskCompletedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new TaskCompletedEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletedDate = DateTime.UtcNow,
            ActualCost = 100m
        };

        var event2 = new TaskCompletedEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletedDate = DateTime.UtcNow,
            ActualCost = 200m
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
