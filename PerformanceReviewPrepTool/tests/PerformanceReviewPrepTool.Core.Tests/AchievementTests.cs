// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core.Tests;

public class AchievementTests
{
    [Test]
    public void Constructor_CreatesAchievement_WithDefaultValues()
    {
        // Arrange & Act
        var achievement = new Achievement();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(achievement.AchievementId, Is.EqualTo(Guid.Empty));
            Assert.That(achievement.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(achievement.ReviewPeriodId, Is.EqualTo(Guid.Empty));
            Assert.That(achievement.Title, Is.EqualTo(string.Empty));
            Assert.That(achievement.Description, Is.EqualTo(string.Empty));
            Assert.That(achievement.AchievedDate, Is.EqualTo(default(DateTime)));
            Assert.That(achievement.Impact, Is.Null);
            Assert.That(achievement.Category, Is.Null);
            Assert.That(achievement.IsKeyAchievement, Is.False);
            Assert.That(achievement.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(achievement.UpdatedAt, Is.Null);
            Assert.That(achievement.ReviewPeriod, Is.Null);
        });
    }

    [Test]
    public void MarkAsKey_SetsIsKeyAchievementToTrue()
    {
        // Arrange
        var achievement = new Achievement { IsKeyAchievement = false };

        // Act
        achievement.MarkAsKey();

        // Assert
        Assert.That(achievement.IsKeyAchievement, Is.True);
    }

    [Test]
    public void MarkAsKey_UpdatesUpdatedAt()
    {
        // Arrange
        var achievement = new Achievement();
        var beforeMark = DateTime.UtcNow;

        // Act
        achievement.MarkAsKey();
        var afterMark = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(achievement.UpdatedAt, Is.Not.Null);
            Assert.That(achievement.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeMark));
            Assert.That(achievement.UpdatedAt!.Value, Is.LessThanOrEqualTo(afterMark));
        });
    }

    [Test]
    public void Achievement_WithAllPropertiesSet_IsValid()
    {
        // Arrange
        var achievementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var reviewPeriodId = Guid.NewGuid();
        var achievedDate = new DateTime(2024, 6, 15);

        // Act
        var achievement = new Achievement
        {
            AchievementId = achievementId,
            UserId = userId,
            ReviewPeriodId = reviewPeriodId,
            Title = "Led successful migration project",
            Description = "Migrated legacy system to cloud",
            AchievedDate = achievedDate,
            Impact = "Reduced costs by 30%",
            Category = "Technical Leadership",
            IsKeyAchievement = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(achievement.AchievementId, Is.EqualTo(achievementId));
            Assert.That(achievement.UserId, Is.EqualTo(userId));
            Assert.That(achievement.ReviewPeriodId, Is.EqualTo(reviewPeriodId));
            Assert.That(achievement.Title, Is.EqualTo("Led successful migration project"));
            Assert.That(achievement.Description, Is.EqualTo("Migrated legacy system to cloud"));
            Assert.That(achievement.AchievedDate, Is.EqualTo(achievedDate));
            Assert.That(achievement.Impact, Is.EqualTo("Reduced costs by 30%"));
            Assert.That(achievement.Category, Is.EqualTo("Technical Leadership"));
            Assert.That(achievement.IsKeyAchievement, Is.True);
        });
    }
}
