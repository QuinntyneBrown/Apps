// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core.Tests;

public class AchievementCreatedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        var achievementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Best Developer";
        var achievementType = AchievementType.Award;
        var achievedDate = new DateTime(2024, 5, 1);

        var eventData = new AchievementCreatedEvent
        {
            AchievementId = achievementId,
            UserId = userId,
            Title = title,
            AchievementType = achievementType,
            AchievedDate = achievedDate
        };

        Assert.Multiple(() =>
        {
            Assert.That(eventData.AchievementId, Is.EqualTo(achievementId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Title, Is.EqualTo(title));
            Assert.That(eventData.AchievementType, Is.EqualTo(achievementType));
            Assert.That(eventData.AchievedDate, Is.EqualTo(achievedDate));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var achievementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new AchievementCreatedEvent { AchievementId = achievementId, UserId = userId, Title = "Test", AchievementType = AchievementType.Award, AchievedDate = DateTime.Today, Timestamp = timestamp };
        var event2 = new AchievementCreatedEvent { AchievementId = achievementId, UserId = userId, Title = "Test", AchievementType = AchievementType.Award, AchievedDate = DateTime.Today, Timestamp = timestamp };

        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        var event1 = new AchievementCreatedEvent { AchievementId = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Achievement 1", AchievementType = AchievementType.Award, AchievedDate = DateTime.Today };
        var event2 = new AchievementCreatedEvent { AchievementId = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Achievement 2", AchievementType = AchievementType.Certification, AchievedDate = DateTime.Today };

        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
