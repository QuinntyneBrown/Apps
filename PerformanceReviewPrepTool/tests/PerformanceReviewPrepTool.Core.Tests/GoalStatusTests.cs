// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core.Tests;

public class GoalStatusTests
{
    [Test]
    public void GoalStatus_AllValues_CanBeAssigned()
    {
        // Arrange
        var statuses = new[]
        {
            GoalStatus.NotStarted,
            GoalStatus.InProgress,
            GoalStatus.OnTrack,
            GoalStatus.AtRisk,
            GoalStatus.Completed,
            GoalStatus.Deferred,
            GoalStatus.Cancelled
        };

        // Act & Assert
        foreach (var status in statuses)
        {
            var goal = new Goal { Status = status };
            Assert.That(goal.Status, Is.EqualTo(status));
        }
    }
}
