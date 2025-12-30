// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core.Tests;

public class GoalTests
{
    [Test]
    public void Constructor_CreatesGoal_WithDefaultValues()
    {
        // Arrange & Act
        var goal = new Goal();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.GoalId, Is.EqualTo(Guid.Empty));
            Assert.That(goal.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(goal.ReviewPeriodId, Is.EqualTo(Guid.Empty));
            Assert.That(goal.Title, Is.EqualTo(string.Empty));
            Assert.That(goal.Description, Is.EqualTo(string.Empty));
            Assert.That(goal.Status, Is.EqualTo(GoalStatus.NotStarted));
            Assert.That(goal.TargetDate, Is.Null);
            Assert.That(goal.CompletedDate, Is.Null);
            Assert.That(goal.ProgressPercentage, Is.EqualTo(0));
            Assert.That(goal.SuccessMetrics, Is.Null);
            Assert.That(goal.Notes, Is.Null);
            Assert.That(goal.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(goal.UpdatedAt, Is.Null);
            Assert.That(goal.ReviewPeriod, Is.Null);
        });
    }

    [Test]
    public void UpdateProgress_UpdatesProgressPercentage()
    {
        // Arrange
        var goal = new Goal();

        // Act
        goal.UpdateProgress(75);

        // Assert
        Assert.That(goal.ProgressPercentage, Is.EqualTo(75));
    }

    [Test]
    public void UpdateProgress_ClampsToMaximum100()
    {
        // Arrange
        var goal = new Goal();

        // Act
        goal.UpdateProgress(150);

        // Assert
        Assert.That(goal.ProgressPercentage, Is.EqualTo(100));
    }

    [Test]
    public void UpdateProgress_ClampsToMinimum0()
    {
        // Arrange
        var goal = new Goal();

        // Act
        goal.UpdateProgress(-50);

        // Assert
        Assert.That(goal.ProgressPercentage, Is.EqualTo(0));
    }

    [Test]
    public void UpdateProgress_AutoCompletesGoal_WhenProgressIs100()
    {
        // Arrange
        var goal = new Goal { Status = GoalStatus.InProgress };

        // Act
        goal.UpdateProgress(100);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.Status, Is.EqualTo(GoalStatus.Completed));
            Assert.That(goal.CompletedDate, Is.Not.Null);
        });
    }

    [Test]
    public void UpdateProgress_UpdatesUpdatedAt()
    {
        // Arrange
        var goal = new Goal();

        // Act
        goal.UpdateProgress(50);

        // Assert
        Assert.That(goal.UpdatedAt, Is.Not.Null);
    }

    [Test]
    public void Complete_SetsStatusToCompleted()
    {
        // Arrange
        var goal = new Goal { Status = GoalStatus.InProgress };

        // Act
        goal.Complete();

        // Assert
        Assert.That(goal.Status, Is.EqualTo(GoalStatus.Completed));
    }

    [Test]
    public void Complete_SetsCompletedDate()
    {
        // Arrange
        var goal = new Goal();
        var beforeComplete = DateTime.UtcNow;

        // Act
        goal.Complete();
        var afterComplete = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.CompletedDate, Is.Not.Null);
            Assert.That(goal.CompletedDate!.Value, Is.GreaterThanOrEqualTo(beforeComplete));
            Assert.That(goal.CompletedDate!.Value, Is.LessThanOrEqualTo(afterComplete));
        });
    }

    [Test]
    public void Complete_SetsProgressTo100()
    {
        // Arrange
        var goal = new Goal { ProgressPercentage = 80 };

        // Act
        goal.Complete();

        // Assert
        Assert.That(goal.ProgressPercentage, Is.EqualTo(100));
    }

    [Test]
    public void UpdateStatus_UpdatesStatus()
    {
        // Arrange
        var goal = new Goal { Status = GoalStatus.NotStarted };

        // Act
        goal.UpdateStatus(GoalStatus.InProgress);

        // Assert
        Assert.That(goal.Status, Is.EqualTo(GoalStatus.InProgress));
    }

    [Test]
    public void UpdateStatus_ToCompleted_SetsCompletedDateAndProgress()
    {
        // Arrange
        var goal = new Goal { Status = GoalStatus.InProgress };

        // Act
        goal.UpdateStatus(GoalStatus.Completed);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.Status, Is.EqualTo(GoalStatus.Completed));
            Assert.That(goal.CompletedDate, Is.Not.Null);
            Assert.That(goal.ProgressPercentage, Is.EqualTo(100));
        });
    }

    [Test]
    public void UpdateStatus_UpdatesUpdatedAt()
    {
        // Arrange
        var goal = new Goal();

        // Act
        goal.UpdateStatus(GoalStatus.InProgress);

        // Assert
        Assert.That(goal.UpdatedAt, Is.Not.Null);
    }
}
