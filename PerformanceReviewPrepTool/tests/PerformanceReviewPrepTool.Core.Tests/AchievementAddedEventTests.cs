// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core.Tests;

public class AchievementAddedEventTests
{
    [Test]
    public void AchievementAddedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var achievementId = Guid.NewGuid();
        var reviewPeriodId = Guid.NewGuid();
        var achievedDate = new DateTime(2024, 6, 15);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new AchievementAddedEvent
        {
            AchievementId = achievementId,
            ReviewPeriodId = reviewPeriodId,
            Title = "Major milestone achieved",
            AchievedDate = achievedDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AchievementId, Is.EqualTo(achievementId));
            Assert.That(evt.ReviewPeriodId, Is.EqualTo(reviewPeriodId));
            Assert.That(evt.Title, Is.EqualTo("Major milestone achieved"));
            Assert.That(evt.AchievedDate, Is.EqualTo(achievedDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
