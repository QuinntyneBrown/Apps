using PerformanceReviewPrepTool.Api.Features.ReviewPeriods;
using PerformanceReviewPrepTool.Api.Features.Achievements;
using PerformanceReviewPrepTool.Api.Features.Goals;
using PerformanceReviewPrepTool.Api.Features.Feedbacks;

namespace PerformanceReviewPrepTool.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void ReviewPeriodDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var reviewPeriod = new Core.ReviewPeriod
        {
            ReviewPeriodId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Q1 2024 Review",
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 3, 31),
            ReviewDueDate = new DateTime(2024, 4, 15),
            ReviewerName = "John Manager",
            IsCompleted = false,
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = reviewPeriod.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ReviewPeriodId, Is.EqualTo(reviewPeriod.ReviewPeriodId));
            Assert.That(dto.UserId, Is.EqualTo(reviewPeriod.UserId));
            Assert.That(dto.Title, Is.EqualTo(reviewPeriod.Title));
            Assert.That(dto.StartDate, Is.EqualTo(reviewPeriod.StartDate));
            Assert.That(dto.EndDate, Is.EqualTo(reviewPeriod.EndDate));
            Assert.That(dto.ReviewDueDate, Is.EqualTo(reviewPeriod.ReviewDueDate));
            Assert.That(dto.ReviewerName, Is.EqualTo(reviewPeriod.ReviewerName));
            Assert.That(dto.IsCompleted, Is.EqualTo(reviewPeriod.IsCompleted));
            Assert.That(dto.Notes, Is.EqualTo(reviewPeriod.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(reviewPeriod.CreatedAt));
        });
    }

    [Test]
    public void AchievementDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var achievement = new Core.Achievement
        {
            AchievementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReviewPeriodId = Guid.NewGuid(),
            Title = "Delivered Project X",
            Description = "Successfully delivered major project",
            AchievedDate = DateTime.UtcNow.AddDays(-10),
            Impact = "Increased revenue by 20%",
            Category = "Technical",
            IsKeyAchievement = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = achievement.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.AchievementId, Is.EqualTo(achievement.AchievementId));
            Assert.That(dto.UserId, Is.EqualTo(achievement.UserId));
            Assert.That(dto.ReviewPeriodId, Is.EqualTo(achievement.ReviewPeriodId));
            Assert.That(dto.Title, Is.EqualTo(achievement.Title));
            Assert.That(dto.Description, Is.EqualTo(achievement.Description));
            Assert.That(dto.AchievedDate, Is.EqualTo(achievement.AchievedDate));
            Assert.That(dto.Impact, Is.EqualTo(achievement.Impact));
            Assert.That(dto.Category, Is.EqualTo(achievement.Category));
            Assert.That(dto.IsKeyAchievement, Is.EqualTo(achievement.IsKeyAchievement));
            Assert.That(dto.CreatedAt, Is.EqualTo(achievement.CreatedAt));
        });
    }

    [Test]
    public void GoalDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var goal = new Core.Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReviewPeriodId = Guid.NewGuid(),
            Title = "Complete Certification",
            Description = "Obtain AWS Solutions Architect certification",
            Status = Core.GoalStatus.InProgress,
            TargetDate = DateTime.UtcNow.AddMonths(2),
            ProgressPercentage = 50,
            SuccessMetrics = "Pass certification exam with 80% or higher",
            Notes = "Study sessions scheduled",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = goal.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.GoalId, Is.EqualTo(goal.GoalId));
            Assert.That(dto.UserId, Is.EqualTo(goal.UserId));
            Assert.That(dto.ReviewPeriodId, Is.EqualTo(goal.ReviewPeriodId));
            Assert.That(dto.Title, Is.EqualTo(goal.Title));
            Assert.That(dto.Description, Is.EqualTo(goal.Description));
            Assert.That(dto.Status, Is.EqualTo(goal.Status));
            Assert.That(dto.TargetDate, Is.EqualTo(goal.TargetDate));
            Assert.That(dto.ProgressPercentage, Is.EqualTo(goal.ProgressPercentage));
            Assert.That(dto.SuccessMetrics, Is.EqualTo(goal.SuccessMetrics));
            Assert.That(dto.Notes, Is.EqualTo(goal.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(goal.CreatedAt));
        });
    }

    [Test]
    public void FeedbackDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var feedback = new Core.Feedback
        {
            FeedbackId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReviewPeriodId = Guid.NewGuid(),
            Source = "Manager",
            Content = "Excellent work on the project",
            ReceivedDate = DateTime.UtcNow.AddDays(-5),
            FeedbackType = "Positive",
            Category = "Technical Skills",
            IsKeyFeedback = true,
            Notes = "Share in review",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = feedback.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.FeedbackId, Is.EqualTo(feedback.FeedbackId));
            Assert.That(dto.UserId, Is.EqualTo(feedback.UserId));
            Assert.That(dto.ReviewPeriodId, Is.EqualTo(feedback.ReviewPeriodId));
            Assert.That(dto.Source, Is.EqualTo(feedback.Source));
            Assert.That(dto.Content, Is.EqualTo(feedback.Content));
            Assert.That(dto.ReceivedDate, Is.EqualTo(feedback.ReceivedDate));
            Assert.That(dto.FeedbackType, Is.EqualTo(feedback.FeedbackType));
            Assert.That(dto.Category, Is.EqualTo(feedback.Category));
            Assert.That(dto.IsKeyFeedback, Is.EqualTo(feedback.IsKeyFeedback));
            Assert.That(dto.Notes, Is.EqualTo(feedback.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(feedback.CreatedAt));
        });
    }
}
