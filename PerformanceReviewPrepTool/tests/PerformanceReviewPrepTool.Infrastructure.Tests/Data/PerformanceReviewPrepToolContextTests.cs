// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Infrastructure.Tests;

/// <summary>
/// Unit tests for the PerformanceReviewPrepToolContext.
/// </summary>
[TestFixture]
public class PerformanceReviewPrepToolContextTests
{
    private PerformanceReviewPrepToolContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PerformanceReviewPrepToolContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new PerformanceReviewPrepToolContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that ReviewPeriods can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ReviewPeriods_CanAddAndRetrieve()
    {
        // Arrange
        var reviewPeriod = new ReviewPeriod
        {
            ReviewPeriodId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Q1 2024 Review",
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 3, 31),
            ReviewDueDate = new DateTime(2024, 4, 15),
            ReviewerName = "John Doe",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.ReviewPeriods.Add(reviewPeriod);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ReviewPeriods.FindAsync(reviewPeriod.ReviewPeriodId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Q1 2024 Review"));
        Assert.That(retrieved.ReviewerName, Is.EqualTo("John Doe"));
        Assert.That(retrieved.IsCompleted, Is.False);
    }

    /// <summary>
    /// Tests that Achievements can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Achievements_CanAddAndRetrieve()
    {
        // Arrange
        var reviewPeriod = new ReviewPeriod
        {
            ReviewPeriodId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Review",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(3),
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var achievement = new Achievement
        {
            AchievementId = Guid.NewGuid(),
            UserId = reviewPeriod.UserId,
            ReviewPeriodId = reviewPeriod.ReviewPeriodId,
            Title = "Completed Migration",
            Description = "Successfully migrated legacy system",
            AchievedDate = DateTime.UtcNow,
            Impact = "Improved performance by 50%",
            Category = "Technical",
            IsKeyAchievement = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.ReviewPeriods.Add(reviewPeriod);
        _context.Achievements.Add(achievement);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Achievements.FindAsync(achievement.AchievementId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Completed Migration"));
        Assert.That(retrieved.IsKeyAchievement, Is.True);
        Assert.That(retrieved.ReviewPeriodId, Is.EqualTo(reviewPeriod.ReviewPeriodId));
    }

    /// <summary>
    /// Tests that Goals can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Goals_CanAddAndRetrieve()
    {
        // Arrange
        var reviewPeriod = new ReviewPeriod
        {
            ReviewPeriodId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Review",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(3),
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = reviewPeriod.UserId,
            ReviewPeriodId = reviewPeriod.ReviewPeriodId,
            Title = "Complete Certification",
            Description = "Obtain AWS certification",
            Status = GoalStatus.InProgress,
            TargetDate = DateTime.UtcNow.AddMonths(2),
            ProgressPercentage = 50,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.ReviewPeriods.Add(reviewPeriod);
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Goals.FindAsync(goal.GoalId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Complete Certification"));
        Assert.That(retrieved.Status, Is.EqualTo(GoalStatus.InProgress));
        Assert.That(retrieved.ProgressPercentage, Is.EqualTo(50));
    }

    /// <summary>
    /// Tests that Feedbacks can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Feedbacks_CanAddAndRetrieve()
    {
        // Arrange
        var reviewPeriod = new ReviewPeriod
        {
            ReviewPeriodId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Review",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(3),
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var feedback = new Feedback
        {
            FeedbackId = Guid.NewGuid(),
            UserId = reviewPeriod.UserId,
            ReviewPeriodId = reviewPeriod.ReviewPeriodId,
            Source = "Manager",
            Content = "Great work on the project",
            ReceivedDate = DateTime.UtcNow,
            FeedbackType = "Positive",
            IsKeyFeedback = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.ReviewPeriods.Add(reviewPeriod);
        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Feedbacks.FindAsync(feedback.FeedbackId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Source, Is.EqualTo("Manager"));
        Assert.That(retrieved.Content, Is.EqualTo("Great work on the project"));
        Assert.That(retrieved.IsKeyFeedback, Is.True);
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var reviewPeriod = new ReviewPeriod
        {
            ReviewPeriodId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Review",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(3),
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var achievement = new Achievement
        {
            AchievementId = Guid.NewGuid(),
            UserId = reviewPeriod.UserId,
            ReviewPeriodId = reviewPeriod.ReviewPeriodId,
            Title = "Test Achievement",
            Description = "Test description",
            AchievedDate = DateTime.UtcNow,
            IsKeyAchievement = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.ReviewPeriods.Add(reviewPeriod);
        _context.Achievements.Add(achievement);
        await _context.SaveChangesAsync();

        // Act
        _context.ReviewPeriods.Remove(reviewPeriod);
        await _context.SaveChangesAsync();

        var retrievedAchievement = await _context.Achievements.FindAsync(achievement.AchievementId);

        // Assert
        Assert.That(retrievedAchievement, Is.Null);
    }
}
