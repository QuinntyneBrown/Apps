// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core.Tests;

public class MaintenanceTaskTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMaintenanceTask()
    {
        // Arrange
        var maintenanceTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "HVAC Inspection";
        var description = "Annual HVAC system inspection";
        var maintenanceType = MaintenanceType.Preventive;
        var status = TaskStatus.Scheduled;
        var dueDate = DateTime.UtcNow.AddDays(30);
        var estimatedCost = 150m;
        var priority = 2;
        var location = "Basement";

        // Act
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            Name = name,
            Description = description,
            MaintenanceType = maintenanceType,
            Status = status,
            DueDate = dueDate,
            EstimatedCost = estimatedCost,
            Priority = priority,
            Location = location
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(task.MaintenanceTaskId, Is.EqualTo(maintenanceTaskId));
            Assert.That(task.UserId, Is.EqualTo(userId));
            Assert.That(task.Name, Is.EqualTo(name));
            Assert.That(task.Description, Is.EqualTo(description));
            Assert.That(task.MaintenanceType, Is.EqualTo(maintenanceType));
            Assert.That(task.Status, Is.EqualTo(status));
            Assert.That(task.DueDate, Is.EqualTo(dueDate));
            Assert.That(task.EstimatedCost, Is.EqualTo(estimatedCost));
            Assert.That(task.Priority, Is.EqualTo(priority));
            Assert.That(task.Location, Is.EqualTo(location));
        });
    }

    [Test]
    public void MaintenanceTask_DefaultValues_AreSetCorrectly()
    {
        // Act
        var task = new MaintenanceTask();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(task.Name, Is.EqualTo(string.Empty));
            Assert.That(task.Description, Is.Null);
            Assert.That(task.MaintenanceType, Is.EqualTo(MaintenanceType.Preventive));
            Assert.That(task.Status, Is.EqualTo(TaskStatus.Scheduled));
            Assert.That(task.DueDate, Is.Null);
            Assert.That(task.CompletedDate, Is.Null);
            Assert.That(task.RecurrenceFrequencyDays, Is.Null);
            Assert.That(task.EstimatedCost, Is.Null);
            Assert.That(task.ActualCost, Is.Null);
            Assert.That(task.Priority, Is.EqualTo(3));
            Assert.That(task.Location, Is.Null);
            Assert.That(task.ContractorId, Is.Null);
            Assert.That(task.ServiceLogs, Is.Not.Null);
        });
    }

    [Test]
    public void Complete_ChangesStatusAndSetsCompletedDate()
    {
        // Arrange
        var beforeCompletion = DateTime.UtcNow;
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Task",
            Status = TaskStatus.InProgress
        };

        // Act
        task.Complete();
        var afterCompletion = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(task.Status, Is.EqualTo(TaskStatus.Completed));
            Assert.That(task.CompletedDate, Is.Not.Null);
            Assert.That(task.CompletedDate, Is.GreaterThanOrEqualTo(beforeCompletion).And.LessThanOrEqualTo(afterCompletion));
            Assert.That(task.UpdatedAt, Is.GreaterThanOrEqualTo(beforeCompletion).And.LessThanOrEqualTo(afterCompletion));
        });
    }

    [Test]
    public void IsOverdue_WithOverdueTask_ReturnsTrue()
    {
        // Arrange
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Overdue Task",
            Status = TaskStatus.Scheduled,
            DueDate = DateTime.UtcNow.AddDays(-5)
        };

        // Act
        var isOverdue = task.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.True);
    }

    [Test]
    public void IsOverdue_WithUpcomingTask_ReturnsFalse()
    {
        // Arrange
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Future Task",
            Status = TaskStatus.Scheduled,
            DueDate = DateTime.UtcNow.AddDays(10)
        };

        // Act
        var isOverdue = task.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void IsOverdue_WithCompletedTask_ReturnsFalse()
    {
        // Arrange
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Completed Overdue Task",
            Status = TaskStatus.Completed,
            DueDate = DateTime.UtcNow.AddDays(-10)
        };

        // Act
        var isOverdue = task.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void IsOverdue_WithoutDueDate_ReturnsFalse()
    {
        // Arrange
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "No Due Date Task",
            Status = TaskStatus.Scheduled
        };

        // Act
        var isOverdue = task.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void ScheduleNextOccurrence_WithRecurrenceAndCompletion_SetsNextDueDate()
    {
        // Arrange
        var completedDate = DateTime.UtcNow;
        var recurrenceDays = 30;
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Recurring Task",
            Status = TaskStatus.Completed,
            CompletedDate = completedDate,
            RecurrenceFrequencyDays = recurrenceDays
        };

        // Act
        var nextDueDate = task.ScheduleNextOccurrence();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(nextDueDate, Is.Not.Null);
            Assert.That(task.DueDate, Is.EqualTo(completedDate.AddDays(recurrenceDays)));
            Assert.That(task.Status, Is.EqualTo(TaskStatus.Scheduled));
        });
    }

    [Test]
    public void ScheduleNextOccurrence_WithoutRecurrenceFrequency_ReturnsNull()
    {
        // Arrange
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "One-time Task",
            Status = TaskStatus.Completed,
            CompletedDate = DateTime.UtcNow
        };

        // Act
        var nextDueDate = task.ScheduleNextOccurrence();

        // Assert
        Assert.That(nextDueDate, Is.Null);
    }

    [Test]
    public void ScheduleNextOccurrence_WithoutCompletedDate_ReturnsNull()
    {
        // Arrange
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Incomplete Task",
            Status = TaskStatus.InProgress,
            RecurrenceFrequencyDays = 30
        };

        // Act
        var nextDueDate = task.ScheduleNextOccurrence();

        // Assert
        Assert.That(nextDueDate, Is.Null);
    }

    [Test]
    public void ScheduleNextOccurrence_UpdatesUpdatedAt()
    {
        // Arrange
        var beforeSchedule = DateTime.UtcNow;
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Recurring Task",
            Status = TaskStatus.Completed,
            CompletedDate = DateTime.UtcNow,
            RecurrenceFrequencyDays = 7
        };

        // Act
        task.ScheduleNextOccurrence();
        var afterSchedule = DateTime.UtcNow;

        // Assert
        Assert.That(task.UpdatedAt, Is.GreaterThanOrEqualTo(beforeSchedule).And.LessThanOrEqualTo(afterSchedule));
    }

    [Test]
    public void MaintenanceTask_Priority_DefaultsToThree()
    {
        // Act
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Task"
        };

        // Assert
        Assert.That(task.Priority, Is.EqualTo(3));
    }

    [Test]
    public void MaintenanceTask_AllMaintenanceTypes_CanBeAssigned()
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
            var task = new MaintenanceTask
            {
                MaintenanceTaskId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Task",
                MaintenanceType = type
            };

            Assert.That(task.MaintenanceType, Is.EqualTo(type));
        }
    }

    [Test]
    public void MaintenanceTask_AllTaskStatuses_CanBeAssigned()
    {
        // Arrange
        var statuses = new[]
        {
            TaskStatus.Scheduled,
            TaskStatus.InProgress,
            TaskStatus.Completed,
            TaskStatus.Postponed,
            TaskStatus.Cancelled
        };

        // Act & Assert
        foreach (var status in statuses)
        {
            var task = new MaintenanceTask
            {
                MaintenanceTaskId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Task",
                Status = status
            };

            Assert.That(task.Status, Is.EqualTo(status));
        }
    }

    [Test]
    public void MaintenanceTask_ServiceLogs_InitializesAsEmptyList()
    {
        // Act
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Task"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(task.ServiceLogs, Is.Not.Null);
            Assert.That(task.ServiceLogs, Is.Empty);
        });
    }

    [Test]
    public void MaintenanceTask_AllProperties_CanBeSet()
    {
        // Arrange
        var maintenanceTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Complete Task";
        var description = "Full description";
        var maintenanceType = MaintenanceType.Seasonal;
        var status = TaskStatus.InProgress;
        var dueDate = new DateTime(2024, 6, 15);
        var completedDate = new DateTime(2024, 6, 10);
        var recurrenceFrequencyDays = 90;
        var estimatedCost = 500m;
        var actualCost = 475m;
        var priority = 1;
        var location = "Roof";
        var contractorId = Guid.NewGuid();
        var createdAt = DateTime.UtcNow.AddDays(-60);
        var updatedAt = DateTime.UtcNow.AddDays(-1);

        // Act
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = maintenanceTaskId,
            UserId = userId,
            Name = name,
            Description = description,
            MaintenanceType = maintenanceType,
            Status = status,
            DueDate = dueDate,
            CompletedDate = completedDate,
            RecurrenceFrequencyDays = recurrenceFrequencyDays,
            EstimatedCost = estimatedCost,
            ActualCost = actualCost,
            Priority = priority,
            Location = location,
            ContractorId = contractorId,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(task.MaintenanceTaskId, Is.EqualTo(maintenanceTaskId));
            Assert.That(task.UserId, Is.EqualTo(userId));
            Assert.That(task.Name, Is.EqualTo(name));
            Assert.That(task.Description, Is.EqualTo(description));
            Assert.That(task.MaintenanceType, Is.EqualTo(maintenanceType));
            Assert.That(task.Status, Is.EqualTo(status));
            Assert.That(task.DueDate, Is.EqualTo(dueDate));
            Assert.That(task.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(task.RecurrenceFrequencyDays, Is.EqualTo(recurrenceFrequencyDays));
            Assert.That(task.EstimatedCost, Is.EqualTo(estimatedCost));
            Assert.That(task.ActualCost, Is.EqualTo(actualCost));
            Assert.That(task.Priority, Is.EqualTo(priority));
            Assert.That(task.Location, Is.EqualTo(location));
            Assert.That(task.ContractorId, Is.EqualTo(contractorId));
            Assert.That(task.CreatedAt, Is.EqualTo(createdAt));
            Assert.That(task.UpdatedAt, Is.EqualTo(updatedAt));
        });
    }
}
