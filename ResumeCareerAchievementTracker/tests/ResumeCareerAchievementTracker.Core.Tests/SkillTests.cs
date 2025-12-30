// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core.Tests;

public class SkillTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesSkill()
    {
        var skill = new Skill();
        Assert.Multiple(() =>
        {
            Assert.That(skill.SkillId, Is.EqualTo(Guid.Empty));
            Assert.That(skill.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(skill.Name, Is.EqualTo(string.Empty));
            Assert.That(skill.Category, Is.EqualTo(string.Empty));
            Assert.That(skill.ProficiencyLevel, Is.EqualTo(string.Empty));
            Assert.That(skill.YearsOfExperience, Is.Null);
            Assert.That(skill.LastUsedDate, Is.Null);
            Assert.That(skill.Notes, Is.Null);
            Assert.That(skill.IsFeatured, Is.False);
            Assert.That(skill.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(skill.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void UpdateProficiency_ChangesLevel()
    {
        var skill = new Skill { ProficiencyLevel = "Beginner" };
        skill.UpdateProficiency("Intermediate");
        Assert.Multiple(() =>
        {
            Assert.That(skill.ProficiencyLevel, Is.EqualTo("Intermediate"));
            Assert.That(skill.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void ToggleFeatured_FromFalse_SetsTrue()
    {
        var skill = new Skill { IsFeatured = false };
        skill.ToggleFeatured();
        Assert.That(skill.IsFeatured, Is.True);
    }

    [Test]
    public void UpdateLastUsed_SetsDate()
    {
        var skill = new Skill();
        var date = new DateTime(2024, 6, 1);
        skill.UpdateLastUsed(date);
        Assert.Multiple(() =>
        {
            Assert.That(skill.LastUsedDate, Is.EqualTo(date));
            Assert.That(skill.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void Skill_WithAllProperties_SetsCorrectly()
    {
        var skill = new Skill
        {
            SkillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "C# Programming",
            Category = "Programming",
            ProficiencyLevel = "Expert",
            YearsOfExperience = 5.5m,
            Notes = "Primary language",
            IsFeatured = true
        };

        Assert.Multiple(() =>
        {
            Assert.That(skill.Name, Is.EqualTo("C# Programming"));
            Assert.That(skill.Category, Is.EqualTo("Programming"));
            Assert.That(skill.ProficiencyLevel, Is.EqualTo("Expert"));
            Assert.That(skill.YearsOfExperience, Is.EqualTo(5.5m));
            Assert.That(skill.Notes, Is.EqualTo("Primary language"));
            Assert.That(skill.IsFeatured, Is.True);
        });
    }

    [Test]
    public void Skill_DifferentCategories_SetCorrectly()
    {
        var programming = new Skill { Name = "Python", Category = "Programming" };
        var leadership = new Skill { Name = "Team Management", Category = "Leadership" };
        var design = new Skill { Name = "UI/UX", Category = "Design" };

        Assert.Multiple(() =>
        {
            Assert.That(programming.Category, Is.EqualTo("Programming"));
            Assert.That(leadership.Category, Is.EqualTo("Leadership"));
            Assert.That(design.Category, Is.EqualTo("Design"));
        });
    }

    [Test]
    public void Skill_DifferentProficiencyLevels_SetCorrectly()
    {
        var beginner = new Skill { Name = "Go", ProficiencyLevel = "Beginner" };
        var intermediate = new Skill { Name = "Rust", ProficiencyLevel = "Intermediate" };
        var advanced = new Skill { Name = "JavaScript", ProficiencyLevel = "Advanced" };
        var expert = new Skill { Name = "C#", ProficiencyLevel = "Expert" };

        Assert.Multiple(() =>
        {
            Assert.That(beginner.ProficiencyLevel, Is.EqualTo("Beginner"));
            Assert.That(intermediate.ProficiencyLevel, Is.EqualTo("Intermediate"));
            Assert.That(advanced.ProficiencyLevel, Is.EqualTo("Advanced"));
            Assert.That(expert.ProficiencyLevel, Is.EqualTo("Expert"));
        });
    }
}
