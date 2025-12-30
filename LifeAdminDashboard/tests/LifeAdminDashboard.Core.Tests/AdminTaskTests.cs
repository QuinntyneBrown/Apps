// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core.Tests;

public class AdminTaskTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesAdminTask()
    {
        // Arrange
        var adminTaskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Pay Property Tax";
        var category = TaskCategory.Financial;
        var priority = TaskPriority.High;

        // Act
        var task = new AdminTask
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
            Assert.That(task.AdminTaskId, Is.EqualTo(adminTaskId));
            Assert.That(task.UserId, Is.EqualTo(userId));
            Assert.That(task.Title, Is.EqualTo(title));
            Assert.That(task.Category, Is.EqualTo(category));
            Assert.That(task.Priority, Is.EqualTo(priority));
            Assert.That(task.IsCompleted, Is.False);
            Assert.That(task.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Complete_WhenCalled_SetsIsCompletedTrueAndCompletionDate()
    {
        // Arrange
        var task = new AdminTask { IsCompleted = false };
        var beforeCompletion = DateTime.UtcNow;

        // Act
        task.Complete();

        var afterCompletion = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(task.IsCompleted, Is.True);
            Assert.That(task.CompletionDate, Is.Not.Null);
            Assert.That(task.CompletionDate, Is.GreaterThanOrEqualTo(beforeCompletion));
            Assert.That(task.CompletionDate, Is.LessThanOrEqualTo(afterCompletion));
        });
    }

    [Test]
    public void IsOverdue_WhenNotCompletedAndDueDatePassed_ReturnsTrue()
    {
        // Arrange
        var task = new AdminTask
        {
            IsCompleted = false,
            DueDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = task.IsOverdue();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsOverdue_WhenNotCompletedAndDueDateInFuture_ReturnsFalse()
    {
        // Arrange
        var task = new AdminTask
        {
            IsCompleted = false,
            DueDate = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var result = task.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOverdue_WhenCompletedAndDueDatePassed_ReturnsFalse()
    {
        // Arrange
        var task = new AdminTask
        {
            IsCompleted = true,
            DueDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = task.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOverdue_WhenDueDateIsNull_ReturnsFalse()
    {
        // Arrange
        var task = new AdminTask
        {
            IsCompleted = false,
            DueDate = null
        };

        // Act
        var result = task.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Snooze_WithDueDate_AddsDaysToDate()
    {
        // Arrange
        var originalDueDate = DateTime.UtcNow.AddDays(1);
        var task = new AdminTask { DueDate = originalDueDate };
        var daysToSnooze = 3;

        // Act
        task.Snooze(daysToSnooze);

        // Assert
        Assert.That(task.DueDate, Is.EqualTo(originalDueDate.AddDays(daysToSnooze)));
    }

    [Test]
    public void Snooze_WithNullDueDate_DoesNotThrow()
    {
        // Arrange
        var task = new AdminTask { DueDate = null };

        // Act & Assert
        Assert.DoesNotThrow(() => task.Snooze(3));
        Assert.That(task.DueDate, Is.Null);
    }

    [Test]
    public void IsCompleted_DefaultsToFalse()
    {
        // Arrange & Act
        var task = new AdminTask();

        // Assert
        Assert.That(task.IsCompleted, Is.False);
    }

    [Test]
    public void IsRecurring_DefaultsToFalse()
    {
        // Arrange & Act
        var task = new AdminTask();

        // Assert
        Assert.That(task.IsRecurring, Is.False);
    }

    [Test]
    public void IsRecurring_CanBeSetToTrue()
    {
        // Arrange
        var task = new AdminTask();

        // Act
        task.IsRecurring = true;

        // Assert
        Assert.That(task.IsRecurring, Is.True);
    }

    [Test]
    public void RecurrencePattern_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var task = new AdminTask();
        var expectedPattern = "Monthly";

        // Act
        task.RecurrencePattern = expectedPattern;

        // Assert
        Assert.That(task.RecurrencePattern, Is.EqualTo(expectedPattern));
    }

    [Test]
    public void Description_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var task = new AdminTask();
        var expectedDescription = "Pay quarterly taxes";

        // Act
        task.Description = expectedDescription;

        // Assert
        Assert.That(task.Description, Is.EqualTo(expectedDescription));
    }
}
