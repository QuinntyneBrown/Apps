// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core.Tests;

public class FeedbackReceivedEventTests
{
    [Test]
    public void FeedbackReceivedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var feedbackId = Guid.NewGuid();
        var reviewPeriodId = Guid.NewGuid();
        var receivedDate = new DateTime(2024, 7, 1);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new FeedbackReceivedEvent
        {
            FeedbackId = feedbackId,
            ReviewPeriodId = reviewPeriodId,
            Source = "Manager",
            ReceivedDate = receivedDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.FeedbackId, Is.EqualTo(feedbackId));
            Assert.That(evt.ReviewPeriodId, Is.EqualTo(reviewPeriodId));
            Assert.That(evt.Source, Is.EqualTo("Manager"));
            Assert.That(evt.ReceivedDate, Is.EqualTo(receivedDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
