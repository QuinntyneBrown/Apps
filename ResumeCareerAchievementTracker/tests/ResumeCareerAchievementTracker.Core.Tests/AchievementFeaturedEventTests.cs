// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core.Tests;

public class AchievementFeaturedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        var achievementId = Guid.NewGuid();
        var isFeatured = true;

        var eventData = new AchievementFeaturedEvent
        {
            AchievementId = achievementId,
            IsFeatured = isFeatured
        };

        Assert.Multiple(() =>
        {
            Assert.That(eventData.AchievementId, Is.EqualTo(achievementId));
            Assert.That(eventData.IsFeatured, Is.EqualTo(isFeatured));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var achievementId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new AchievementFeaturedEvent { AchievementId = achievementId, IsFeatured = true, Timestamp = timestamp };
        var event2 = new AchievementFeaturedEvent { AchievementId = achievementId, IsFeatured = true, Timestamp = timestamp };

        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        var event1 = new AchievementFeaturedEvent { AchievementId = Guid.NewGuid(), IsFeatured = true };
        var event2 = new AchievementFeaturedEvent { AchievementId = Guid.NewGuid(), IsFeatured = false };

        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
