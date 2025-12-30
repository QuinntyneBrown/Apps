using SportsTeamFollowingTracker.Core;

namespace SportsTeamFollowingTracker.Api.Features.Statistics;

public record StatisticDto
{
    public Guid StatisticId { get; init; }
    public Guid UserId { get; init; }
    public Guid TeamId { get; init; }
    public string StatName { get; init; } = string.Empty;
    public decimal Value { get; init; }
    public DateTime RecordedDate { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class StatisticExtensions
{
    public static StatisticDto ToDto(this Statistic statistic)
    {
        return new StatisticDto
        {
            StatisticId = statistic.StatisticId,
            UserId = statistic.UserId,
            TeamId = statistic.TeamId,
            StatName = statistic.StatName,
            Value = statistic.Value,
            RecordedDate = statistic.RecordedDate,
            CreatedAt = statistic.CreatedAt,
        };
    }
}
