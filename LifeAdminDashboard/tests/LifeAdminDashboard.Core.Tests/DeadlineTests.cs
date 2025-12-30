// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core.Tests;

public class DeadlineTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesDeadline()
    {
        // Arrange
        var deadlineId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Submit Tax Return";
        var deadlineDateTime = DateTime.UtcNow.AddMonths(3);

        // Act
        var deadline = new Deadline
        {
            DeadlineId = deadlineId,
            UserId = userId,
            Title = title,
            DeadlineDateTime = deadlineDateTime
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(deadline.DeadlineId, Is.EqualTo(deadlineId));
            Assert.That(deadline.UserId, Is.EqualTo(userId));
            Assert.That(deadline.Title, Is.EqualTo(title));
            Assert.That(deadline.DeadlineDateTime, Is.EqualTo(deadlineDateTime));
            Assert.That(deadline.IsCompleted, Is.False);
            Assert.That(deadline.RemindersEnabled, Is.True);
            Assert.That(deadline.ReminderDaysAdvance, Is.EqualTo(7));
            Assert.That(deadline.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Complete_WhenCalled_SetsIsCompletedTrueAndCompletionDate()
    {
        // Arrange
        var deadline = new Deadline { IsCompleted = false };
        var beforeCompletion = DateTime.UtcNow;

        // Act
        deadline.Complete();

        var afterCompletion = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(deadline.IsCompleted, Is.True);
            Assert.That(deadline.CompletionDate, Is.Not.Null);
            Assert.That(deadline.CompletionDate, Is.GreaterThanOrEqualTo(beforeCompletion));
            Assert.That(deadline.CompletionDate, Is.LessThanOrEqualTo(afterCompletion));
        });
    }

    [Test]
    public void IsOverdue_WhenNotCompletedAndDeadlinePassed_ReturnsTrue()
    {
        // Arrange
        var deadline = new Deadline
        {
            IsCompleted = false,
            DeadlineDateTime = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = deadline.IsOverdue();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsOverdue_WhenNotCompletedAndDeadlineInFuture_ReturnsFalse()
    {
        // Arrange
        var deadline = new Deadline
        {
            IsCompleted = false,
            DeadlineDateTime = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var result = deadline.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOverdue_WhenCompletedAndDeadlinePassed_ReturnsFalse()
    {
        // Arrange
        var deadline = new Deadline
        {
            IsCompleted = true,
            DeadlineDateTime = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = deadline.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldRemind_WhenRemindersEnabledAndWithinReminderWindow_ReturnsTrue()
    {
        // Arrange
        var deadline = new Deadline
        {
            RemindersEnabled = true,
            ReminderDaysAdvance = 7,
            IsCompleted = false,
            DeadlineDateTime = DateTime.UtcNow.AddDays(5)
        };

        // Act
        var result = deadline.ShouldRemind();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ShouldRemind_WhenRemindersDisabled_ReturnsFalse()
    {
        // Arrange
        var deadline = new Deadline
        {
            RemindersEnabled = false,
            ReminderDaysAdvance = 7,
            IsCompleted = false,
            DeadlineDateTime = DateTime.UtcNow.AddDays(5)
        };

        // Act
        var result = deadline.ShouldRemind();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldRemind_WhenCompleted_ReturnsFalse()
    {
        // Arrange
        var deadline = new Deadline
        {
            RemindersEnabled = true,
            ReminderDaysAdvance = 7,
            IsCompleted = true,
            DeadlineDateTime = DateTime.UtcNow.AddDays(5)
        };

        // Act
        var result = deadline.ShouldRemind();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldRemind_WhenOutsideReminderWindow_ReturnsFalse()
    {
        // Arrange
        var deadline = new Deadline
        {
            RemindersEnabled = true,
            ReminderDaysAdvance = 7,
            IsCompleted = false,
            DeadlineDateTime = DateTime.UtcNow.AddDays(10)
        };

        // Act
        var result = deadline.ShouldRemind();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void RemindersEnabled_DefaultsToTrue()
    {
        // Arrange & Act
        var deadline = new Deadline();

        // Assert
        Assert.That(deadline.RemindersEnabled, Is.True);
    }

    [Test]
    public void ReminderDaysAdvance_DefaultsToSeven()
    {
        // Arrange & Act
        var deadline = new Deadline();

        // Assert
        Assert.That(deadline.ReminderDaysAdvance, Is.EqualTo(7));
    }

    [Test]
    public void Category_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var deadline = new Deadline();
        var expectedCategory = "Tax";

        // Act
        deadline.Category = expectedCategory;

        // Assert
        Assert.That(deadline.Category, Is.EqualTo(expectedCategory));
    }
}
