using Achievements.Core.Models;

namespace Achievements.Api.Features;

public record AchievementDto(
    Guid AchievementId,
    Guid UserId,
    string Title,
    string Description,
    AchievementType AchievementType,
    DateTime AchievedDate,
    string? Organization,
    string? Impact,
    bool IsFeatured,
    DateTime CreatedAt);

public static class AchievementExtensions
{
    public static AchievementDto ToDto(this Achievement achievement)
    {
        return new AchievementDto(
            achievement.AchievementId,
            achievement.UserId,
            achievement.Title,
            achievement.Description,
            achievement.AchievementType,
            achievement.AchievedDate,
            achievement.Organization,
            achievement.Impact,
            achievement.IsFeatured,
            achievement.CreatedAt);
    }
}
