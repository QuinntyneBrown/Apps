using Screenings.Core.Models;

namespace Screenings.Api.Features;

public record ScreeningDto(
    Guid ScreeningId,
    Guid UserId,
    string ScreeningType,
    DateTime ScheduledDate,
    DateTime? CompletedDate,
    string? Result,
    string? Notes,
    int FrequencyMonths,
    bool IsCompleted,
    DateTime CreatedAt);

public static class ScreeningExtensions
{
    public static ScreeningDto ToDto(this Screening screening)
    {
        return new ScreeningDto(
            screening.ScreeningId,
            screening.UserId,
            screening.ScreeningType,
            screening.ScheduledDate,
            screening.CompletedDate,
            screening.Result,
            screening.Notes,
            screening.FrequencyMonths,
            screening.IsCompleted,
            screening.CreatedAt);
    }
}
