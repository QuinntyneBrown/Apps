// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core.Tests;

public class TaskCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var adminTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Pay Bills";
        var category = TaskCategory.Financial;
        var priority = TaskPriority.High;

        // Act
        var evt = new TaskCreatedEvent
        {
            AdminTaskId = adminTaskId,
            UserId = userId,
            Title = title,
            Category = category,
            Priority = priority
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AdminTaskId, Is.EqualTo(adminTaskId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.Category, Is.EqualTo(category));
            Assert.That(evt.Priority, Is.EqualTo(priority));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Timestamp_IsSetAutomatically()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new TaskCreatedEvent
        {
            AdminTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void AdminTaskId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var evt = new TaskCreatedEvent { AdminTaskId = expectedId };

        // Assert
        Assert.That(evt.AdminTaskId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();

        // Act
        var evt = new TaskCreatedEvent { UserId = expectedUserId };

        // Assert
        Assert.That(evt.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Title_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedTitle = "Renew License";

        // Act
        var evt = new TaskCreatedEvent { Title = expectedTitle };

        // Assert
        Assert.That(evt.Title, Is.EqualTo(expectedTitle));
    }

    [Test]
    public void Category_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedCategory = TaskCategory.Legal;

        // Act
        var evt = new TaskCreatedEvent { Category = expectedCategory };

        // Assert
        Assert.That(evt.Category, Is.EqualTo(expectedCategory));
    }

    [Test]
    public void Priority_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedPriority = TaskPriority.Medium;

        // Act
        var evt = new TaskCreatedEvent { Priority = expectedPriority };

        // Assert
        Assert.That(evt.Priority, Is.EqualTo(expectedPriority));
    }
}
