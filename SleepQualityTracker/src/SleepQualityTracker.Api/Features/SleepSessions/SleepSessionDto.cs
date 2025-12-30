using SleepQualityTracker.Core;

namespace SleepQualityTracker.Api.Features.SleepSessions;

public record SleepSessionDto
{
    public Guid SleepSessionId { get; init; }
    public Guid UserId { get; init; }
    public DateTime Bedtime { get; init; }
    public DateTime WakeTime { get; init; }
    public int TotalSleepMinutes { get; init; }
    public SleepQuality SleepQuality { get; init; }
    public int? TimesAwakened { get; init; }
    public int? DeepSleepMinutes { get; init; }
    public int? RemSleepMinutes { get; init; }
    public decimal? SleepEfficiency { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public int TimeInBedMinutes { get; init; }
    public bool MeetsRecommendedDuration { get; init; }
    public bool IsGoodQuality { get; init; }
}

public static class SleepSessionExtensions
{
    public static SleepSessionDto ToDto(this SleepSession sleepSession)
    {
        return new SleepSessionDto
        {
            SleepSessionId = sleepSession.SleepSessionId,
            UserId = sleepSession.UserId,
            Bedtime = sleepSession.Bedtime,
            WakeTime = sleepSession.WakeTime,
            TotalSleepMinutes = sleepSession.TotalSleepMinutes,
            SleepQuality = sleepSession.SleepQuality,
            TimesAwakened = sleepSession.TimesAwakened,
            DeepSleepMinutes = sleepSession.DeepSleepMinutes,
            RemSleepMinutes = sleepSession.RemSleepMinutes,
            SleepEfficiency = sleepSession.SleepEfficiency,
            Notes = sleepSession.Notes,
            CreatedAt = sleepSession.CreatedAt,
            TimeInBedMinutes = sleepSession.GetTimeInBed(),
            MeetsRecommendedDuration = sleepSession.MeetsRecommendedDuration(),
            IsGoodQuality = sleepSession.IsGoodQuality(),
        };
    }
}
