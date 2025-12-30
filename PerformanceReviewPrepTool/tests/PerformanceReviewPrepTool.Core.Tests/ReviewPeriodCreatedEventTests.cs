// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core.Tests;

public class ReviewPeriodCreatedEventTests
{
    [Test]
    public void ReviewPeriodCreatedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var reviewPeriodId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 6, 30);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ReviewPeriodCreatedEvent
        {
            ReviewPeriodId = reviewPeriodId,
            UserId = userId,
            Title = "H1 2024 Review",
            StartDate = startDate,
            EndDate = endDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReviewPeriodId, Is.EqualTo(reviewPeriodId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo("H1 2024 Review"));
            Assert.That(evt.StartDate, Is.EqualTo(startDate));
            Assert.That(evt.EndDate, Is.EqualTo(endDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
