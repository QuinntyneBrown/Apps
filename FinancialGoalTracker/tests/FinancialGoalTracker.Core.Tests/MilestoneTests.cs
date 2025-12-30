// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Core.Tests;

public class MilestoneTests
{
    [Test]
    public void Constructor_CreatesMilestone_WithDefaultValues()
    {
        // Arrange & Act
        var milestone = new Milestone();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.MilestoneId, Is.EqualTo(Guid.Empty));
            Assert.That(milestone.GoalId, Is.EqualTo(Guid.Empty));
            Assert.That(milestone.Name, Is.EqualTo(string.Empty));
            Assert.That(milestone.TargetAmount, Is.EqualTo(0m));
            Assert.That(milestone.TargetDate, Is.EqualTo(default(DateTime)));
            Assert.That(milestone.IsCompleted, Is.False);
            Assert.That(milestone.CompletedDate, Is.Null);
            Assert.That(milestone.Notes, Is.Null);
            Assert.That(milestone.Goal, Is.Null);
        });
    }

    [Test]
    public void MilestoneId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var milestone = new Milestone();
        var expectedId = Guid.NewGuid();

        // Act
        milestone.MilestoneId = expectedId;

        // Assert
        Assert.That(milestone.MilestoneId, Is.EqualTo(expectedId));
    }

    [Test]
    public void Name_CanBeSet_AndRetrieved()
    {
        // Arrange
        var milestone = new Milestone();
        var expectedName = "25% Complete";

        // Act
        milestone.Name = expectedName;

        // Assert
        Assert.That(milestone.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void TargetAmount_CanBeSet_AndRetrieved()
    {
        // Arrange
        var milestone = new Milestone();
        var expectedAmount = 2500m;

        // Act
        milestone.TargetAmount = expectedAmount;

        // Assert
        Assert.That(milestone.TargetAmount, Is.EqualTo(expectedAmount));
    }

    [Test]
    public void MarkAsCompleted_SetsIsCompletedToTrue()
    {
        // Arrange
        var milestone = new Milestone();

        // Act
        milestone.MarkAsCompleted();

        // Assert
        Assert.That(milestone.IsCompleted, Is.True);
    }

    [Test]
    public void MarkAsCompleted_SetsCompletedDateToUtcNow()
    {
        // Arrange
        var milestone = new Milestone();
        var beforeCompletion = DateTime.UtcNow;

        // Act
        milestone.MarkAsCompleted();
        var afterCompletion = DateTime.UtcNow;

        // Assert
        Assert.That(milestone.CompletedDate, Is.Not.Null);
        Assert.That(milestone.CompletedDate, Is.GreaterThanOrEqualTo(beforeCompletion));
        Assert.That(milestone.CompletedDate, Is.LessThanOrEqualTo(afterCompletion));
    }

    [Test]
    public void MarkAsCompleted_MultipleTimes_UpdatesCompletedDate()
    {
        // Arrange
        var milestone = new Milestone();

        // Act
        milestone.MarkAsCompleted();
        var firstCompletedDate = milestone.CompletedDate;

        System.Threading.Thread.Sleep(10); // Small delay
        milestone.MarkAsCompleted();
        var secondCompletedDate = milestone.CompletedDate;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.IsCompleted, Is.True);
            Assert.That(secondCompletedDate, Is.GreaterThanOrEqualTo(firstCompletedDate));
        });
    }

    [Test]
    public void Milestone_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var name = "50% Complete";
        var targetAmount = 5000m;
        var targetDate = new DateTime(2024, 9, 1);
        var isCompleted = true;
        var completedDate = new DateTime(2024, 8, 15);
        var notes = "Halfway there!";

        // Act
        var milestone = new Milestone
        {
            MilestoneId = milestoneId,
            GoalId = goalId,
            Name = name,
            TargetAmount = targetAmount,
            TargetDate = targetDate,
            IsCompleted = isCompleted,
            CompletedDate = completedDate,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.MilestoneId, Is.EqualTo(milestoneId));
            Assert.That(milestone.GoalId, Is.EqualTo(goalId));
            Assert.That(milestone.Name, Is.EqualTo(name));
            Assert.That(milestone.TargetAmount, Is.EqualTo(targetAmount));
            Assert.That(milestone.TargetDate, Is.EqualTo(targetDate));
            Assert.That(milestone.IsCompleted, Is.EqualTo(isCompleted));
            Assert.That(milestone.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(milestone.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void IsCompleted_DefaultsToFalse()
    {
        // Arrange & Act
        var milestone = new Milestone();

        // Assert
        Assert.That(milestone.IsCompleted, Is.False);
    }
}
