// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core.Tests;

public class TaskCreatedEventTests
{
    [Test]
    public void TaskCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var maintenanceTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "HVAC Inspection";
        var maintenanceType = MaintenanceType.Preventive;
        var dueDate = DateTime.UtcNow.AddDays(30);
        var timestamp = DateTime.UtcNow;

        // Act
        var taskEvent = new TaskCreatedEvent
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            Name = name,
            MaintenanceType = maintenanceType,
            DueDate = dueDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taskEvent.MaintenanceTaskId, Is.EqualTo(maintenanceTaskId));
            Assert.That(taskEvent.UserId, Is.EqualTo(userId));
            Assert.That(taskEvent.Name, Is.EqualTo(name));
            Assert.That(taskEvent.MaintenanceType, Is.EqualTo(maintenanceType));
            Assert.That(taskEvent.DueDate, Is.EqualTo(dueDate));
            Assert.That(taskEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void TaskCreatedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var taskEvent = new TaskCreatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taskEvent.MaintenanceTaskId, Is.EqualTo(Guid.Empty));
            Assert.That(taskEvent.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(taskEvent.Name, Is.EqualTo(string.Empty));
            Assert.That(taskEvent.MaintenanceType, Is.EqualTo(MaintenanceType.Preventive));
            Assert.That(taskEvent.DueDate, Is.Null);
            Assert.That(taskEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void TaskCreatedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var taskEvent = new TaskCreatedEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Task",
            MaintenanceType = MaintenanceType.Corrective
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(taskEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void TaskCreatedEvent_AllMaintenanceTypes_CanBeSet()
    {
        // Arrange
        var types = new[]
        {
            MaintenanceType.Preventive,
            MaintenanceType.Corrective,
            MaintenanceType.Seasonal,
            MaintenanceType.Emergency,
            MaintenanceType.Inspection
        };

        // Act & Assert
        foreach (var type in types)
        {
            var taskEvent = new TaskCreatedEvent
            {
                MaintenanceTaskId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Task",
                MaintenanceType = type
            };

            Assert.That(taskEvent.MaintenanceType, Is.EqualTo(type));
        }
    }

    [Test]
    public void TaskCreatedEvent_WithoutDueDate_IsValid()
    {
        // Arrange & Act
        var taskEvent = new TaskCreatedEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Flexible Task",
            MaintenanceType = MaintenanceType.Inspection
        };

        // Assert
        Assert.That(taskEvent.DueDate, Is.Null);
    }

    [Test]
    public void TaskCreatedEvent_IsImmutable()
    {
        // Arrange
        var maintenanceTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Test Task";
        var maintenanceType = MaintenanceType.Seasonal;
        var dueDate = DateTime.UtcNow.AddDays(60);

        // Act
        var taskEvent = new TaskCreatedEvent
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            Name = name,
            MaintenanceType = maintenanceType,
            DueDate = dueDate
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(taskEvent.MaintenanceTaskId, Is.EqualTo(maintenanceTaskId));
            Assert.That(taskEvent.UserId, Is.EqualTo(userId));
            Assert.That(taskEvent.Name, Is.EqualTo(name));
            Assert.That(taskEvent.MaintenanceType, Is.EqualTo(maintenanceType));
            Assert.That(taskEvent.DueDate, Is.EqualTo(dueDate));
        });
    }

    [Test]
    public void TaskCreatedEvent_EqualityByValue()
    {
        // Arrange
        var maintenanceTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Task";
        var maintenanceType = MaintenanceType.Emergency;
        var dueDate = DateTime.UtcNow.AddDays(1);
        var timestamp = DateTime.UtcNow;

        var event1 = new TaskCreatedEvent
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            Name = name,
            MaintenanceType = maintenanceType,
            DueDate = dueDate,
            Timestamp = timestamp
        };

        var event2 = new TaskCreatedEvent
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            Name = name,
            MaintenanceType = maintenanceType,
            DueDate = dueDate,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void TaskCreatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new TaskCreatedEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Task 1",
            MaintenanceType = MaintenanceType.Preventive
        };

        var event2 = new TaskCreatedEvent
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Task 2",
            MaintenanceType = MaintenanceType.Corrective
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
