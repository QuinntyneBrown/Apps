// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core.Tests;

public class AchievementTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesAchievement()
    {
        var achievement = new Achievement();
        Assert.Multiple(() =>
        {
            Assert.That(achievement.AchievementId, Is.EqualTo(Guid.Empty));
            Assert.That(achievement.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(achievement.Title, Is.EqualTo(string.Empty));
            Assert.That(achievement.Description, Is.EqualTo(string.Empty));
            Assert.That(achievement.AchievementType, Is.EqualTo(AchievementType.Award));
            Assert.That(achievement.AchievedDate, Is.EqualTo(default(DateTime)));
            Assert.That(achievement.Organization, Is.Null);
            Assert.That(achievement.Impact, Is.Null);
            Assert.That(achievement.SkillIds, Is.Not.Null.And.Empty);
            Assert.That(achievement.ProjectIds, Is.Not.Null.And.Empty);
            Assert.That(achievement.IsFeatured, Is.False);
            Assert.That(achievement.Tags, Is.Not.Null.And.Empty);
            Assert.That(achievement.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(achievement.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void ToggleFeatured_FromFalse_SetsTrue()
    {
        var achievement = new Achievement { IsFeatured = false };
        achievement.ToggleFeatured();
        Assert.That(achievement.IsFeatured, Is.True);
    }

    [Test]
    public void AddSkill_NewSkill_Adds()
    {
        var achievement = new Achievement();
        var skillId = Guid.NewGuid();
        achievement.AddSkill(skillId);
        Assert.That(achievement.SkillIds, Contains.Item(skillId));
    }

    [Test]
    public void AddSkill_Duplicate_DoesNotAddDuplicate()
    {
        var achievement = new Achievement();
        var skillId = Guid.NewGuid();
        achievement.AddSkill(skillId);
        achievement.AddSkill(skillId);
        Assert.That(achievement.SkillIds.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddProject_NewProject_Adds()
    {
        var achievement = new Achievement();
        var projectId = Guid.NewGuid();
        achievement.AddProject(projectId);
        Assert.That(achievement.ProjectIds, Contains.Item(projectId));
    }

    [Test]
    public void AddProject_Duplicate_DoesNotAddDuplicate()
    {
        var achievement = new Achievement();
        var projectId = Guid.NewGuid();
        achievement.AddProject(projectId);
        achievement.AddProject(projectId);
        Assert.That(achievement.ProjectIds.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddTag_NewTag_Adds()
    {
        var achievement = new Achievement();
        achievement.AddTag("Important");
        Assert.That(achievement.Tags, Contains.Item("Important"));
    }

    [Test]
    public void AddTag_Duplicate_DoesNotAddDuplicate()
    {
        var achievement = new Achievement();
        achievement.AddTag("Important");
        achievement.AddTag("important");
        Assert.That(achievement.Tags.Count, Is.EqualTo(1));
    }

    [Test]
    public void Achievement_WithAllProperties_SetsCorrectly()
    {
        var achievement = new Achievement
        {
            AchievementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Best Developer Award",
            Description = "Recognized for excellence",
            AchievementType = AchievementType.Award,
            AchievedDate = new DateTime(2024, 5, 1),
            Organization = "Tech Corp",
            Impact = "Motivated the team",
            IsFeatured = true
        };

        Assert.Multiple(() =>
        {
            Assert.That(achievement.Title, Is.EqualTo("Best Developer Award"));
            Assert.That(achievement.Description, Is.EqualTo("Recognized for excellence"));
            Assert.That(achievement.AchievementType, Is.EqualTo(AchievementType.Award));
            Assert.That(achievement.AchievedDate, Is.EqualTo(new DateTime(2024, 5, 1)));
            Assert.That(achievement.Organization, Is.EqualTo("Tech Corp"));
            Assert.That(achievement.Impact, Is.EqualTo("Motivated the team"));
            Assert.That(achievement.IsFeatured, Is.True);
        });
    }

    [Test]
    public void Achievement_DifferentTypes_SetCorrectly()
    {
        var award = new Achievement { Title = "Award", AchievementType = AchievementType.Award };
        var cert = new Achievement { Title = "Cert", AchievementType = AchievementType.Certification };
        var pub = new Achievement { Title = "Pub", AchievementType = AchievementType.Publication };

        Assert.Multiple(() =>
        {
            Assert.That(award.AchievementType, Is.EqualTo(AchievementType.Award));
            Assert.That(cert.AchievementType, Is.EqualTo(AchievementType.Certification));
            Assert.That(pub.AchievementType, Is.EqualTo(AchievementType.Publication));
        });
    }
}
