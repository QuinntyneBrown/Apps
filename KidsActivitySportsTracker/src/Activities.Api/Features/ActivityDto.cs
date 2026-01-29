using Activities.Core.Models;

namespace Activities.Api.Features;

public record ActivityDto(
    Guid ActivityId,
    Guid UserId,
    string Name,
    ActivityType Type,
    string? Description,
    string? Location,
    string? CoachName,
    string? ContactInfo,
    decimal? Cost,
    bool IsActive,
    DateTime CreatedAt);

public static class ActivityExtensions
{
    public static ActivityDto ToDto(this Activity activity)
    {
        return new ActivityDto(
            activity.ActivityId,
            activity.UserId,
            activity.Name,
            activity.Type,
            activity.Description,
            activity.Location,
            activity.CoachName,
            activity.ContactInfo,
            activity.Cost,
            activity.IsActive,
            activity.CreatedAt);
    }
}
