// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core.Tests;

public class SkillProficiencyUpdatedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        var skillId = Guid.NewGuid();
        var oldLevel = "Beginner";
        var newLevel = "Intermediate";

        var eventData = new SkillProficiencyUpdatedEvent
        {
            SkillId = skillId,
            OldLevel = oldLevel,
            NewLevel = newLevel
        };

        Assert.Multiple(() =>
        {
            Assert.That(eventData.SkillId, Is.EqualTo(skillId));
            Assert.That(eventData.OldLevel, Is.EqualTo(oldLevel));
            Assert.That(eventData.NewLevel, Is.EqualTo(newLevel));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var skillId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new SkillProficiencyUpdatedEvent { SkillId = skillId, OldLevel = "Beginner", NewLevel = "Intermediate", Timestamp = timestamp };
        var event2 = new SkillProficiencyUpdatedEvent { SkillId = skillId, OldLevel = "Beginner", NewLevel = "Intermediate", Timestamp = timestamp };

        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        var event1 = new SkillProficiencyUpdatedEvent { SkillId = Guid.NewGuid(), OldLevel = "Beginner", NewLevel = "Intermediate" };
        var event2 = new SkillProficiencyUpdatedEvent { SkillId = Guid.NewGuid(), OldLevel = "Intermediate", NewLevel = "Advanced" };

        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
