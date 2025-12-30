// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core.Tests;

public class TaskCategoryTests
{
    [Test]
    public void TaskCategory_Financial_HasCorrectValue()
    {
        // Arrange & Act
        var category = TaskCategory.Financial;

        // Assert
        Assert.That((int)category, Is.EqualTo(0));
    }

    [Test]
    public void TaskCategory_Health_HasCorrectValue()
    {
        // Arrange & Act
        var category = TaskCategory.Health;

        // Assert
        Assert.That((int)category, Is.EqualTo(1));
    }

    [Test]
    public void TaskCategory_HomeMaintenance_HasCorrectValue()
    {
        // Arrange & Act
        var category = TaskCategory.HomeMaintenance;

        // Assert
        Assert.That((int)category, Is.EqualTo(2));
    }

    [Test]
    public void TaskCategory_Vehicle_HasCorrectValue()
    {
        // Arrange & Act
        var category = TaskCategory.Vehicle;

        // Assert
        Assert.That((int)category, Is.EqualTo(3));
    }

    [Test]
    public void TaskCategory_Insurance_HasCorrectValue()
    {
        // Arrange & Act
        var category = TaskCategory.Insurance;

        // Assert
        Assert.That((int)category, Is.EqualTo(4));
    }

    [Test]
    public void TaskCategory_Legal_HasCorrectValue()
    {
        // Arrange & Act
        var category = TaskCategory.Legal;

        // Assert
        Assert.That((int)category, Is.EqualTo(5));
    }

    [Test]
    public void TaskCategory_Subscriptions_HasCorrectValue()
    {
        // Arrange & Act
        var category = TaskCategory.Subscriptions;

        // Assert
        Assert.That((int)category, Is.EqualTo(6));
    }

    [Test]
    public void TaskCategory_Documents_HasCorrectValue()
    {
        // Arrange & Act
        var category = TaskCategory.Documents;

        // Assert
        Assert.That((int)category, Is.EqualTo(7));
    }

    [Test]
    public void TaskCategory_Other_HasCorrectValue()
    {
        // Arrange & Act
        var category = TaskCategory.Other;

        // Assert
        Assert.That((int)category, Is.EqualTo(8));
    }

    [Test]
    public void TaskCategory_CanBeAssignedToAdminTask()
    {
        // Arrange
        var task = new AdminTask();

        // Act
        task.Category = TaskCategory.Health;

        // Assert
        Assert.That(task.Category, Is.EqualTo(TaskCategory.Health));
    }

    [Test]
    public void TaskCategory_AllValuesCanBeAssigned()
    {
        // Arrange
        var task = new AdminTask();
        var allCategories = new[]
        {
            TaskCategory.Financial,
            TaskCategory.Health,
            TaskCategory.HomeMaintenance,
            TaskCategory.Vehicle,
            TaskCategory.Insurance,
            TaskCategory.Legal,
            TaskCategory.Subscriptions,
            TaskCategory.Documents,
            TaskCategory.Other
        };

        // Act & Assert
        foreach (var category in allCategories)
        {
            task.Category = category;
            Assert.That(task.Category, Is.EqualTo(category));
        }
    }
}
