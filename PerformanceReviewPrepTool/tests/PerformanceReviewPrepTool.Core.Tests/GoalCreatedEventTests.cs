// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core.Tests;

public class GoalCreatedEventTests
{
    [Test]
    public void GoalCreatedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var reviewPeriodId = Guid.NewGuid();
        var targetDate = new DateTime(2024, 12, 31);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new GoalCreatedEvent
        {
            GoalId = goalId,
            ReviewPeriodId = reviewPeriodId,
            Title = "Complete certification",
            TargetDate = targetDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.ReviewPeriodId, Is.EqualTo(reviewPeriodId));
            Assert.That(evt.Title, Is.EqualTo("Complete certification"));
            Assert.That(evt.TargetDate, Is.EqualTo(targetDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
