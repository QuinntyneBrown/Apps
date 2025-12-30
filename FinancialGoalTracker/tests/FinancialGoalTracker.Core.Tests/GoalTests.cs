// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Core.Tests;

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
            Assert.That(goal.Name, Is.EqualTo(string.Empty));
            Assert.That(goal.Description, Is.EqualTo(string.Empty));
            Assert.That(goal.GoalType, Is.EqualTo(GoalType.Savings));
            Assert.That(goal.TargetAmount, Is.EqualTo(0m));
            Assert.That(goal.CurrentAmount, Is.EqualTo(0m));
            Assert.That(goal.TargetDate, Is.EqualTo(default(DateTime)));
            Assert.That(goal.Status, Is.EqualTo(GoalStatus.NotStarted));
            Assert.That(goal.Notes, Is.Null);
            Assert.That(goal.Milestones, Is.Not.Null);
            Assert.That(goal.Milestones, Is.Empty);
        });
    }

    [Test]
    public void CalculateProgress_WithZeroTarget_ReturnsZero()
    {
        // Arrange
        var goal = new Goal
        {
            TargetAmount = 0,
            CurrentAmount = 100
        };

        // Act
        var progress = goal.CalculateProgress();

        // Assert
        Assert.That(progress, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateProgress_WithHalfProgress_ReturnsFiftyPercent()
    {
        // Arrange
        var goal = new Goal
        {
            TargetAmount = 1000,
            CurrentAmount = 500
        };

        // Act
        var progress = goal.CalculateProgress();

        // Assert
        Assert.That(progress, Is.EqualTo(50m));
    }

    [Test]
    public void CalculateProgress_WithFullProgress_ReturnsOneHundredPercent()
    {
        // Arrange
        var goal = new Goal
        {
            TargetAmount = 1000,
            CurrentAmount = 1000
        };

        // Act
        var progress = goal.CalculateProgress();

        // Assert
        Assert.That(progress, Is.EqualTo(100m));
    }

    [Test]
    public void CalculateProgress_WithOverProgress_ReturnsOverOneHundredPercent()
    {
        // Arrange
        var goal = new Goal
        {
            TargetAmount = 1000,
            CurrentAmount = 1500
        };

        // Act
        var progress = goal.CalculateProgress();

        // Assert
        Assert.That(progress, Is.EqualTo(150m));
    }

    [Test]
    public void UpdateProgress_IncreasesCurrentAmount()
    {
        // Arrange
        var goal = new Goal
        {
            CurrentAmount = 100,
            TargetAmount = 1000
        };

        // Act
        goal.UpdateProgress(50);

        // Assert
        Assert.That(goal.CurrentAmount, Is.EqualTo(150m));
    }

    [Test]
    public void UpdateProgress_WhenReachingTarget_SetsStatusToCompleted()
    {
        // Arrange
        var goal = new Goal
        {
            CurrentAmount = 900,
            TargetAmount = 1000,
            Status = GoalStatus.InProgress
        };

        // Act
        goal.UpdateProgress(100);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.CurrentAmount, Is.EqualTo(1000m));
            Assert.That(goal.Status, Is.EqualTo(GoalStatus.Completed));
        });
    }

    [Test]
    public void UpdateProgress_WhenExceedingTarget_SetsStatusToCompleted()
    {
        // Arrange
        var goal = new Goal
        {
            CurrentAmount = 900,
            TargetAmount = 1000,
            Status = GoalStatus.InProgress
        };

        // Act
        goal.UpdateProgress(200);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.CurrentAmount, Is.EqualTo(1100m));
            Assert.That(goal.Status, Is.EqualTo(GoalStatus.Completed));
        });
    }

    [Test]
    public void Goal_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var name = "Emergency Fund";
        var description = "Save for emergencies";
        var goalType = GoalType.Emergency;
        var targetAmount = 10000m;
        var currentAmount = 2500m;
        var targetDate = new DateTime(2025, 12, 31);
        var status = GoalStatus.InProgress;
        var notes = "Save $500/month";

        // Act
        var goal = new Goal
        {
            GoalId = goalId,
            Name = name,
            Description = description,
            GoalType = goalType,
            TargetAmount = targetAmount,
            CurrentAmount = currentAmount,
            TargetDate = targetDate,
            Status = status,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.GoalId, Is.EqualTo(goalId));
            Assert.That(goal.Name, Is.EqualTo(name));
            Assert.That(goal.Description, Is.EqualTo(description));
            Assert.That(goal.GoalType, Is.EqualTo(goalType));
            Assert.That(goal.TargetAmount, Is.EqualTo(targetAmount));
            Assert.That(goal.CurrentAmount, Is.EqualTo(currentAmount));
            Assert.That(goal.TargetDate, Is.EqualTo(targetDate));
            Assert.That(goal.Status, Is.EqualTo(status));
            Assert.That(goal.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Milestones_CanBeAdded()
    {
        // Arrange
        var goal = new Goal();
        var milestone = new Milestone { MilestoneId = Guid.NewGuid(), Name = "25% Complete" };

        // Act
        goal.Milestones.Add(milestone);

        // Assert
        Assert.That(goal.Milestones, Has.Count.EqualTo(1));
    }
}
