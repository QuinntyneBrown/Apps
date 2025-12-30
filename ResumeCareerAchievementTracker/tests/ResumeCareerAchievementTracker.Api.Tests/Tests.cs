using ResumeCareerAchievementTracker.Api.Features.Achievements;
using ResumeCareerAchievementTracker.Api.Features.Projects;
using ResumeCareerAchievementTracker.Api.Features.Skills;

namespace ResumeCareerAchievementTracker.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void AchievementDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var achievement = new Core.Achievement
        {
            AchievementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Achievement",
            Description = "Test Description",
            AchievementType = Core.AchievementType.Award,
            AchievedDate = DateTime.UtcNow,
            Organization = "Test Organization",
            Impact = "Test Impact",
            SkillIds = new List<Guid> { Guid.NewGuid() },
            ProjectIds = new List<Guid> { Guid.NewGuid() },
            IsFeatured = true,
            Tags = new List<string> { "tag1", "tag2" },
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = achievement.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.AchievementId, Is.EqualTo(achievement.AchievementId));
            Assert.That(dto.UserId, Is.EqualTo(achievement.UserId));
            Assert.That(dto.Title, Is.EqualTo(achievement.Title));
            Assert.That(dto.Description, Is.EqualTo(achievement.Description));
            Assert.That(dto.AchievementType, Is.EqualTo(achievement.AchievementType));
            Assert.That(dto.AchievedDate, Is.EqualTo(achievement.AchievedDate));
            Assert.That(dto.Organization, Is.EqualTo(achievement.Organization));
            Assert.That(dto.Impact, Is.EqualTo(achievement.Impact));
            Assert.That(dto.SkillIds, Is.EqualTo(achievement.SkillIds));
            Assert.That(dto.ProjectIds, Is.EqualTo(achievement.ProjectIds));
            Assert.That(dto.IsFeatured, Is.EqualTo(achievement.IsFeatured));
            Assert.That(dto.Tags, Is.EqualTo(achievement.Tags));
            Assert.That(dto.CreatedAt, Is.EqualTo(achievement.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(achievement.UpdatedAt));
        });
    }

    [Test]
    public void ProjectDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var project = new Core.Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Description = "Test Description",
            Organization = "Test Organization",
            Role = "Test Role",
            StartDate = DateTime.UtcNow.AddMonths(-6),
            EndDate = DateTime.UtcNow,
            Technologies = new List<string> { "C#", ".NET" },
            Outcomes = new List<string> { "Outcome 1", "Outcome 2" },
            ProjectUrl = "https://example.com",
            IsFeatured = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = project.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ProjectId, Is.EqualTo(project.ProjectId));
            Assert.That(dto.UserId, Is.EqualTo(project.UserId));
            Assert.That(dto.Name, Is.EqualTo(project.Name));
            Assert.That(dto.Description, Is.EqualTo(project.Description));
            Assert.That(dto.Organization, Is.EqualTo(project.Organization));
            Assert.That(dto.Role, Is.EqualTo(project.Role));
            Assert.That(dto.StartDate, Is.EqualTo(project.StartDate));
            Assert.That(dto.EndDate, Is.EqualTo(project.EndDate));
            Assert.That(dto.Technologies, Is.EqualTo(project.Technologies));
            Assert.That(dto.Outcomes, Is.EqualTo(project.Outcomes));
            Assert.That(dto.ProjectUrl, Is.EqualTo(project.ProjectUrl));
            Assert.That(dto.IsFeatured, Is.EqualTo(project.IsFeatured));
            Assert.That(dto.CreatedAt, Is.EqualTo(project.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(project.UpdatedAt));
        });
    }

    [Test]
    public void SkillDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var skill = new Core.Skill
        {
            SkillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "C# Programming",
            Category = "Programming Languages",
            ProficiencyLevel = "Expert",
            YearsOfExperience = 5.5m,
            LastUsedDate = DateTime.UtcNow,
            Notes = "Test Notes",
            IsFeatured = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = skill.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.SkillId, Is.EqualTo(skill.SkillId));
            Assert.That(dto.UserId, Is.EqualTo(skill.UserId));
            Assert.That(dto.Name, Is.EqualTo(skill.Name));
            Assert.That(dto.Category, Is.EqualTo(skill.Category));
            Assert.That(dto.ProficiencyLevel, Is.EqualTo(skill.ProficiencyLevel));
            Assert.That(dto.YearsOfExperience, Is.EqualTo(skill.YearsOfExperience));
            Assert.That(dto.LastUsedDate, Is.EqualTo(skill.LastUsedDate));
            Assert.That(dto.Notes, Is.EqualTo(skill.Notes));
            Assert.That(dto.IsFeatured, Is.EqualTo(skill.IsFeatured));
            Assert.That(dto.CreatedAt, Is.EqualTo(skill.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(skill.UpdatedAt));
        });
    }
}
