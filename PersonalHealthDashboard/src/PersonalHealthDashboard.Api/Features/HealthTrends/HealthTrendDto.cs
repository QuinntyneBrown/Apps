using PersonalHealthDashboard.Core;

namespace PersonalHealthDashboard.Api.Features.HealthTrends;

public record HealthTrendDto
{
    public Guid HealthTrendId { get; init; }
    public Guid UserId { get; init; }
    public string MetricName { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public double AverageValue { get; init; }
    public double MinValue { get; init; }
    public double MaxValue { get; init; }
    public string TrendDirection { get; init; } = string.Empty;
    public double PercentageChange { get; init; }
    public string? Insights { get; init; }
    public DateTime CreatedAt { get; init; }
    public int PeriodDuration { get; init; }
}

public static class HealthTrendExtensions
{
    public static HealthTrendDto ToDto(this HealthTrend healthTrend)
    {
        return new HealthTrendDto
        {
            HealthTrendId = healthTrend.HealthTrendId,
            UserId = healthTrend.UserId,
            MetricName = healthTrend.MetricName,
            StartDate = healthTrend.StartDate,
            EndDate = healthTrend.EndDate,
            AverageValue = healthTrend.AverageValue,
            MinValue = healthTrend.MinValue,
            MaxValue = healthTrend.MaxValue,
            TrendDirection = healthTrend.TrendDirection,
            PercentageChange = healthTrend.PercentageChange,
            Insights = healthTrend.Insights,
            CreatedAt = healthTrend.CreatedAt,
            PeriodDuration = healthTrend.GetPeriodDuration(),
        };
    }
}
