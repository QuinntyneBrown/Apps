using ResumeCareerAchievementTracker.Core;

namespace ResumeCareerAchievementTracker.Api.Features.Achievements;

public record AchievementDto
{
    public Guid AchievementId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public AchievementType AchievementType { get; init; }
    public DateTime AchievedDate { get; init; }
    public string? Organization { get; init; }
    public string? Impact { get; init; }
    public List<Guid> SkillIds { get; init; } = new();
    public List<Guid> ProjectIds { get; init; } = new();
    public bool IsFeatured { get; init; }
    public List<string> Tags { get; init; } = new();
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class AchievementExtensions
{
    public static AchievementDto ToDto(this Achievement achievement)
    {
        return new AchievementDto
        {
            AchievementId = achievement.AchievementId,
            UserId = achievement.UserId,
            Title = achievement.Title,
            Description = achievement.Description,
            AchievementType = achievement.AchievementType,
            AchievedDate = achievement.AchievedDate,
            Organization = achievement.Organization,
            Impact = achievement.Impact,
            SkillIds = achievement.SkillIds,
            ProjectIds = achievement.ProjectIds,
            IsFeatured = achievement.IsFeatured,
            Tags = achievement.Tags,
            CreatedAt = achievement.CreatedAt,
            UpdatedAt = achievement.UpdatedAt,
        };
    }
}
