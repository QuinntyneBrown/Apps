// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core.Tests;

public class SkillAddedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        var skillId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "C# Programming";
        var category = "Programming";
        var proficiencyLevel = "Expert";

        var eventData = new SkillAddedEvent
        {
            SkillId = skillId,
            UserId = userId,
            Name = name,
            Category = category,
            ProficiencyLevel = proficiencyLevel
        };

        Assert.Multiple(() =>
        {
            Assert.That(eventData.SkillId, Is.EqualTo(skillId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Name, Is.EqualTo(name));
            Assert.That(eventData.Category, Is.EqualTo(category));
            Assert.That(eventData.ProficiencyLevel, Is.EqualTo(proficiencyLevel));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var skillId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new SkillAddedEvent { SkillId = skillId, UserId = userId, Name = "Python", Category = "Programming", ProficiencyLevel = "Advanced", Timestamp = timestamp };
        var event2 = new SkillAddedEvent { SkillId = skillId, UserId = userId, Name = "Python", Category = "Programming", ProficiencyLevel = "Advanced", Timestamp = timestamp };

        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        var event1 = new SkillAddedEvent { SkillId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Java", Category = "Programming", ProficiencyLevel = "Intermediate" };
        var event2 = new SkillAddedEvent { SkillId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Leadership", Category = "Soft Skills", ProficiencyLevel = "Advanced" };

        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
