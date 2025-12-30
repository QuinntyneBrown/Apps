// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core.Tests;

public class TaskPriorityTests
{
    [Test]
    public void TaskPriority_Low_HasCorrectValue()
    {
        // Arrange & Act
        var priority = TaskPriority.Low;

        // Assert
        Assert.That((int)priority, Is.EqualTo(0));
    }

    [Test]
    public void TaskPriority_Medium_HasCorrectValue()
    {
        // Arrange & Act
        var priority = TaskPriority.Medium;

        // Assert
        Assert.That((int)priority, Is.EqualTo(1));
    }

    [Test]
    public void TaskPriority_High_HasCorrectValue()
    {
        // Arrange & Act
        var priority = TaskPriority.High;

        // Assert
        Assert.That((int)priority, Is.EqualTo(2));
    }

    [Test]
    public void TaskPriority_Urgent_HasCorrectValue()
    {
        // Arrange & Act
        var priority = TaskPriority.Urgent;

        // Assert
        Assert.That((int)priority, Is.EqualTo(3));
    }

    [Test]
    public void TaskPriority_CanBeAssignedToAdminTask()
    {
        // Arrange
        var task = new AdminTask();

        // Act
        task.Priority = TaskPriority.Urgent;

        // Assert
        Assert.That(task.Priority, Is.EqualTo(TaskPriority.Urgent));
    }

    [Test]
    public void TaskPriority_AllValuesCanBeAssigned()
    {
        // Arrange
        var task = new AdminTask();
        var allPriorities = new[]
        {
            TaskPriority.Low,
            TaskPriority.Medium,
            TaskPriority.High,
            TaskPriority.Urgent
        };

        // Act & Assert
        foreach (var priority in allPriorities)
        {
            task.Priority = priority;
            Assert.That(task.Priority, Is.EqualTo(priority));
        }
    }
}
