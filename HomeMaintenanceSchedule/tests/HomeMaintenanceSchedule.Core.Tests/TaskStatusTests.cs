// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core.Tests;

public class TaskStatusTests
{
    [Test]
    public void TaskStatus_Scheduled_HasCorrectValue()
    {
        // Arrange & Act
        var status = TaskStatus.Scheduled;

        // Assert
        Assert.That((int)status, Is.EqualTo(0));
    }

    [Test]
    public void TaskStatus_InProgress_HasCorrectValue()
    {
        // Arrange & Act
        var status = TaskStatus.InProgress;

        // Assert
        Assert.That((int)status, Is.EqualTo(1));
    }

    [Test]
    public void TaskStatus_Completed_HasCorrectValue()
    {
        // Arrange & Act
        var status = TaskStatus.Completed;

        // Assert
        Assert.That((int)status, Is.EqualTo(2));
    }

    [Test]
    public void TaskStatus_Postponed_HasCorrectValue()
    {
        // Arrange & Act
        var status = TaskStatus.Postponed;

        // Assert
        Assert.That((int)status, Is.EqualTo(3));
    }

    [Test]
    public void TaskStatus_Cancelled_HasCorrectValue()
    {
        // Arrange & Act
        var status = TaskStatus.Cancelled;

        // Assert
        Assert.That((int)status, Is.EqualTo(4));
    }

    [Test]
    public void TaskStatus_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var scheduled = TaskStatus.Scheduled;
        var inProgress = TaskStatus.InProgress;
        var completed = TaskStatus.Completed;
        var postponed = TaskStatus.Postponed;
        var cancelled = TaskStatus.Cancelled;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(scheduled, Is.EqualTo(TaskStatus.Scheduled));
            Assert.That(inProgress, Is.EqualTo(TaskStatus.InProgress));
            Assert.That(completed, Is.EqualTo(TaskStatus.Completed));
            Assert.That(postponed, Is.EqualTo(TaskStatus.Postponed));
            Assert.That(cancelled, Is.EqualTo(TaskStatus.Cancelled));
        });
    }

    [Test]
    public void TaskStatus_ToString_ReturnsCorrectName()
    {
        // Arrange & Act
        var scheduledName = TaskStatus.Scheduled.ToString();
        var inProgressName = TaskStatus.InProgress.ToString();
        var completedName = TaskStatus.Completed.ToString();
        var postponedName = TaskStatus.Postponed.ToString();
        var cancelledName = TaskStatus.Cancelled.ToString();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(scheduledName, Is.EqualTo("Scheduled"));
            Assert.That(inProgressName, Is.EqualTo("InProgress"));
            Assert.That(completedName, Is.EqualTo("Completed"));
            Assert.That(postponedName, Is.EqualTo("Postponed"));
            Assert.That(cancelledName, Is.EqualTo("Cancelled"));
        });
    }

    [Test]
    public void TaskStatus_CanBeCompared()
    {
        // Arrange
        var status1 = TaskStatus.Scheduled;
        var status2 = TaskStatus.Scheduled;
        var status3 = TaskStatus.InProgress;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(status1, Is.EqualTo(status2));
            Assert.That(status1, Is.Not.EqualTo(status3));
        });
    }

    [Test]
    public void TaskStatus_CanBeUsedInSwitch()
    {
        // Arrange
        var status = TaskStatus.Completed;
        string result;

        // Act
        result = status switch
        {
            TaskStatus.Scheduled => "Scheduled",
            TaskStatus.InProgress => "In Progress",
            TaskStatus.Completed => "Completed",
            TaskStatus.Postponed => "Postponed",
            TaskStatus.Cancelled => "Cancelled",
            _ => "Unknown"
        };

        // Assert
        Assert.That(result, Is.EqualTo("Completed"));
    }

    [Test]
    public void TaskStatus_EnumParse_WorksCorrectly()
    {
        // Arrange
        var statusName = "InProgress";

        // Act
        var parsed = Enum.Parse<TaskStatus>(statusName);

        // Assert
        Assert.That(parsed, Is.EqualTo(TaskStatus.InProgress));
    }
}
