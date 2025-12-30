// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core.Tests;

public class GoalCompletedEventTests
{
    [Test]
    public void GoalCompletedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var completedDate = new DateTime(2024, 11, 15);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new GoalCompletedEvent
        {
            GoalId = goalId,
            CompletedDate = completedDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
