using SkillDevelopmentTracker.Api.Features.Skills;
using SkillDevelopmentTracker.Core;

namespace SkillDevelopmentTracker.Api.Tests.Features.Skills;

[TestFixture]
public class SkillDtoTests
{
    [Test]
    public void ToDto_MapsAllProperties_Correctly()
    {
        // Arrange
        var skill = new Skill
        {
            SkillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "React",
            Category = "Programming",
            ProficiencyLevel = ProficiencyLevel.Intermediate,
            TargetLevel = ProficiencyLevel.Advanced,
            StartDate = new DateTime(2023, 1, 1),
            TargetDate = new DateTime(2024, 12, 31),
            HoursSpent = 120m,
            Notes = "Building modern web applications",
            CourseIds = new List<Guid> { Guid.NewGuid() },
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = skill.ToDto();

        // Assert
        Assert.That(dto.SkillId, Is.EqualTo(skill.SkillId));
        Assert.That(dto.UserId, Is.EqualTo(skill.UserId));
        Assert.That(dto.Name, Is.EqualTo(skill.Name));
        Assert.That(dto.Category, Is.EqualTo(skill.Category));
        Assert.That(dto.ProficiencyLevel, Is.EqualTo(skill.ProficiencyLevel));
        Assert.That(dto.TargetLevel, Is.EqualTo(skill.TargetLevel));
        Assert.That(dto.StartDate, Is.EqualTo(skill.StartDate));
        Assert.That(dto.TargetDate, Is.EqualTo(skill.TargetDate));
        Assert.That(dto.HoursSpent, Is.EqualTo(skill.HoursSpent));
        Assert.That(dto.Notes, Is.EqualTo(skill.Notes));
        Assert.That(dto.CourseIds, Is.EqualTo(skill.CourseIds));
        Assert.That(dto.CreatedAt, Is.EqualTo(skill.CreatedAt));
        Assert.That(dto.UpdatedAt, Is.EqualTo(skill.UpdatedAt));
    }
}
