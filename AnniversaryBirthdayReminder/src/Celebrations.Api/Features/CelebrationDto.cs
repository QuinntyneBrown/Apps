using Celebrations.Core.Models;

namespace Celebrations.Api.Features;

public record CelebrationDto(
    Guid CelebrationId,
    Guid UserId,
    string Name,
    string CelebrationType,
    DateTime Date,
    string? Notes,
    bool IsRecurring,
    DateTime CreatedAt);

public static class CelebrationExtensions
{
    public static CelebrationDto ToDto(this Celebration celebration)
    {
        return new CelebrationDto(
            celebration.CelebrationId,
            celebration.UserId,
            celebration.Name,
            celebration.CelebrationType,
            celebration.Date,
            celebration.Notes,
            celebration.IsRecurring,
            celebration.CreatedAt);
    }
}
