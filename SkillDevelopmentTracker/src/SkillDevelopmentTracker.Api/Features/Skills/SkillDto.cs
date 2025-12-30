using SkillDevelopmentTracker.Core;

namespace SkillDevelopmentTracker.Api.Features.Skills;

public record SkillDto
{
    public Guid SkillId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public ProficiencyLevel ProficiencyLevel { get; init; }
    public ProficiencyLevel? TargetLevel { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? TargetDate { get; init; }
    public decimal HoursSpent { get; init; }
    public string? Notes { get; init; }
    public List<Guid> CourseIds { get; init; } = new List<Guid>();
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
            TargetLevel = skill.TargetLevel,
            StartDate = skill.StartDate,
            TargetDate = skill.TargetDate,
            HoursSpent = skill.HoursSpent,
            Notes = skill.Notes,
            CourseIds = skill.CourseIds,
            CreatedAt = skill.CreatedAt,
            UpdatedAt = skill.UpdatedAt,
        };
    }
}
