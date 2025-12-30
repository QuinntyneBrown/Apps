// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core.Tests;

public class FeedbackTests
{
    [Test]
    public void Constructor_CreatesFeedback_WithDefaultValues()
    {
        // Arrange & Act
        var feedback = new Feedback();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(feedback.FeedbackId, Is.EqualTo(Guid.Empty));
            Assert.That(feedback.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(feedback.ReviewPeriodId, Is.EqualTo(Guid.Empty));
            Assert.That(feedback.Source, Is.EqualTo(string.Empty));
            Assert.That(feedback.Content, Is.EqualTo(string.Empty));
            Assert.That(feedback.ReceivedDate, Is.EqualTo(default(DateTime)));
            Assert.That(feedback.FeedbackType, Is.Null);
            Assert.That(feedback.Category, Is.Null);
            Assert.That(feedback.IsKeyFeedback, Is.False);
            Assert.That(feedback.Notes, Is.Null);
            Assert.That(feedback.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(feedback.UpdatedAt, Is.Null);
            Assert.That(feedback.ReviewPeriod, Is.Null);
        });
    }

    [Test]
    public void MarkAsKey_SetsIsKeyFeedbackToTrue()
    {
        // Arrange
        var feedback = new Feedback { IsKeyFeedback = false };

        // Act
        feedback.MarkAsKey();

        // Assert
        Assert.That(feedback.IsKeyFeedback, Is.True);
    }

    [Test]
    public void MarkAsKey_UpdatesUpdatedAt()
    {
        // Arrange
        var feedback = new Feedback();
        var beforeMark = DateTime.UtcNow;

        // Act
        feedback.MarkAsKey();
        var afterMark = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(feedback.UpdatedAt, Is.Not.Null);
            Assert.That(feedback.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeMark));
            Assert.That(feedback.UpdatedAt!.Value, Is.LessThanOrEqualTo(afterMark));
        });
    }

    [Test]
    public void Feedback_WithAllPropertiesSet_IsValid()
    {
        // Arrange
        var feedbackId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var reviewPeriodId = Guid.NewGuid();
        var receivedDate = new DateTime(2024, 7, 1);

        // Act
        var feedback = new Feedback
        {
            FeedbackId = feedbackId,
            UserId = userId,
            ReviewPeriodId = reviewPeriodId,
            Source = "Manager",
            Content = "Great teamwork and communication skills",
            ReceivedDate = receivedDate,
            FeedbackType = "Positive",
            Category = "Soft Skills",
            IsKeyFeedback = true,
            Notes = "Highlighted in team meeting"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(feedback.FeedbackId, Is.EqualTo(feedbackId));
            Assert.That(feedback.UserId, Is.EqualTo(userId));
            Assert.That(feedback.ReviewPeriodId, Is.EqualTo(reviewPeriodId));
            Assert.That(feedback.Source, Is.EqualTo("Manager"));
            Assert.That(feedback.Content, Is.EqualTo("Great teamwork and communication skills"));
            Assert.That(feedback.ReceivedDate, Is.EqualTo(receivedDate));
            Assert.That(feedback.FeedbackType, Is.EqualTo("Positive"));
            Assert.That(feedback.Category, Is.EqualTo("Soft Skills"));
            Assert.That(feedback.IsKeyFeedback, Is.True);
            Assert.That(feedback.Notes, Is.EqualTo("Highlighted in team meeting"));
        });
    }
}
