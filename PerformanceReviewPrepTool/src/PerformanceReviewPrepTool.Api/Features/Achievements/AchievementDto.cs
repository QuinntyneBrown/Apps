using PerformanceReviewPrepTool.Core;

namespace PerformanceReviewPrepTool.Api.Features.Achievements;

public record AchievementDto
{
    public Guid AchievementId { get; init; }
    public Guid UserId { get; init; }
    public Guid ReviewPeriodId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime AchievedDate { get; init; }
    public string? Impact { get; init; }
    public string? Category { get; init; }
    public bool IsKeyAchievement { get; init; }
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
            ReviewPeriodId = achievement.ReviewPeriodId,
            Title = achievement.Title,
            Description = achievement.Description,
            AchievedDate = achievement.AchievedDate,
            Impact = achievement.Impact,
            Category = achievement.Category,
            IsKeyAchievement = achievement.IsKeyAchievement,
            CreatedAt = achievement.CreatedAt,
            UpdatedAt = achievement.UpdatedAt,
        };
    }
}
