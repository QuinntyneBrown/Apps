using ResumeCareerAchievementTracker.Core;

namespace ResumeCareerAchievementTracker.Api.Features.Skills;

public record SkillDto
{
    public Guid SkillId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string ProficiencyLevel { get; init; } = string.Empty;
    public decimal? YearsOfExperience { get; init; }
    public DateTime? LastUsedDate { get; init; }
    public string? Notes { get; init; }
    public bool IsFeatured { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class SkillExtensions
{
    public static SkillDto ToDto(this Skill skill)
    {
        return new SkillDto
        {
            SkillId = skill.SkillId,
            UserId = skill.UserId,
            Name = skill.Name,
            Category = skill.Category,
            ProficiencyLevel = skill.ProficiencyLevel,
            YearsOfExperience = skill.YearsOfExperience,
            LastUsedDate = skill.LastUsedDate,
            Notes = skill.Notes,
            IsFeatured = skill.IsFeatured,
            CreatedAt = skill.CreatedAt,
            UpdatedAt = skill.UpdatedAt,
        };
    }
}
