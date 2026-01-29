using Skills.Core.Models;

namespace Skills.Api.Features;

public record SkillDto(
    Guid SkillId,
    Guid UserId,
    string Name,
    string Category,
    string ProficiencyLevel,
    decimal? YearsOfExperience,
    bool IsFeatured,
    DateTime CreatedAt);

public static class SkillExtensions
{
    public static SkillDto ToDto(this Skill skill)
    {
        return new SkillDto(
            skill.SkillId,
            skill.UserId,
            skill.Name,
            skill.Category,
            skill.ProficiencyLevel,
            skill.YearsOfExperience,
            skill.IsFeatured,
            skill.CreatedAt);
    }
}
