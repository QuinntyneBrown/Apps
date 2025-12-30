// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Core.Tests;

public class GoalStatusTests
{
    [Test]
    public void NotStarted_HasValue_Zero()
    {
        // Arrange & Act
        var value = (int)GoalStatus.NotStarted;

        // Assert
        Assert.That(value, Is.EqualTo(0));
    }

    [Test]
    public void InProgress_HasValue_One()
    {
        // Arrange & Act
        var value = (int)GoalStatus.InProgress;

        // Assert
        Assert.That(value, Is.EqualTo(1));
    }

    [Test]
    public void Completed_HasValue_Two()
    {
        // Arrange & Act
        var value = (int)GoalStatus.Completed;

        // Assert
        Assert.That(value, Is.EqualTo(2));
    }

    [Test]
    public void Paused_HasValue_Three()
    {
        // Arrange & Act
        var value = (int)GoalStatus.Paused;

        // Assert
        Assert.That(value, Is.EqualTo(3));
    }

    [Test]
    public void Cancelled_HasValue_Four()
    {
        // Arrange & Act
        var value = (int)GoalStatus.Cancelled;

        // Assert
        Assert.That(value, Is.EqualTo(4));
    }

    [Test]
    public void AllEnumValues_CanBeAssigned()
    {
        // Arrange & Act
        var notStarted = GoalStatus.NotStarted;
        var inProgress = GoalStatus.InProgress;
        var completed = GoalStatus.Completed;
        var paused = GoalStatus.Paused;
        var cancelled = GoalStatus.Cancelled;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(notStarted, Is.EqualTo(GoalStatus.NotStarted));
            Assert.That(inProgress, Is.EqualTo(GoalStatus.InProgress));
            Assert.That(completed, Is.EqualTo(GoalStatus.Completed));
            Assert.That(paused, Is.EqualTo(GoalStatus.Paused));
            Assert.That(cancelled, Is.EqualTo(GoalStatus.Cancelled));
        });
    }

    [Test]
    public void EnumValues_AreDistinct()
    {
        // Arrange
        var values = Enum.GetValues<GoalStatus>();

        // Act
        var distinctValues = values.Distinct().ToList();

        // Assert
        Assert.That(distinctValues.Count, Is.EqualTo(values.Length));
    }
}
