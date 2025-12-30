// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core.Tests;

public class ReviewPeriodTests
{
    [Test]
    public void Constructor_CreatesReviewPeriod_WithDefaultValues()
    {
        // Arrange & Act
        var reviewPeriod = new ReviewPeriod();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reviewPeriod.ReviewPeriodId, Is.EqualTo(Guid.Empty));
            Assert.That(reviewPeriod.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(reviewPeriod.Title, Is.EqualTo(string.Empty));
            Assert.That(reviewPeriod.StartDate, Is.EqualTo(default(DateTime)));
            Assert.That(reviewPeriod.EndDate, Is.EqualTo(default(DateTime)));
            Assert.That(reviewPeriod.ReviewDueDate, Is.Null);
            Assert.That(reviewPeriod.ReviewerName, Is.Null);
            Assert.That(reviewPeriod.IsCompleted, Is.False);
            Assert.That(reviewPeriod.CompletedDate, Is.Null);
            Assert.That(reviewPeriod.Notes, Is.Null);
            Assert.That(reviewPeriod.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(reviewPeriod.UpdatedAt, Is.Null);
            Assert.That(reviewPeriod.Achievements, Is.Not.Null);
            Assert.That(reviewPeriod.Achievements, Is.Empty);
            Assert.That(reviewPeriod.Goals, Is.Not.Null);
            Assert.That(reviewPeriod.Goals, Is.Empty);
            Assert.That(reviewPeriod.Feedbacks, Is.Not.Null);
            Assert.That(reviewPeriod.Feedbacks, Is.Empty);
        });
    }

    [Test]
    public void Complete_SetsIsCompletedToTrue()
    {
        // Arrange
        var reviewPeriod = new ReviewPeriod { IsCompleted = false };

        // Act
        reviewPeriod.Complete();

        // Assert
        Assert.That(reviewPeriod.IsCompleted, Is.True);
    }

    [Test]
    public void Complete_SetsCompletedDate()
    {
        // Arrange
        var reviewPeriod = new ReviewPeriod();
        var beforeComplete = DateTime.UtcNow;

        // Act
        reviewPeriod.Complete();
        var afterComplete = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reviewPeriod.CompletedDate, Is.Not.Null);
            Assert.That(reviewPeriod.CompletedDate!.Value, Is.GreaterThanOrEqualTo(beforeComplete));
            Assert.That(reviewPeriod.CompletedDate!.Value, Is.LessThanOrEqualTo(afterComplete));
        });
    }

    [Test]
    public void Complete_UpdatesUpdatedAt()
    {
        // Arrange
        var reviewPeriod = new ReviewPeriod();

        // Act
        reviewPeriod.Complete();

        // Assert
        Assert.That(reviewPeriod.UpdatedAt, Is.Not.Null);
    }

    [Test]
    public void ReviewPeriod_WithCollections_CanBePopulated()
    {
        // Arrange
        var reviewPeriod = new ReviewPeriod();
        var achievement = new Achievement { AchievementId = Guid.NewGuid() };
        var goal = new Goal { GoalId = Guid.NewGuid() };
        var feedback = new Feedback { FeedbackId = Guid.NewGuid() };

        // Act
        reviewPeriod.Achievements.Add(achievement);
        reviewPeriod.Goals.Add(goal);
        reviewPeriod.Feedbacks.Add(feedback);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reviewPeriod.Achievements.Count, Is.EqualTo(1));
            Assert.That(reviewPeriod.Goals.Count, Is.EqualTo(1));
            Assert.That(reviewPeriod.Feedbacks.Count, Is.EqualTo(1));
        });
    }
}
