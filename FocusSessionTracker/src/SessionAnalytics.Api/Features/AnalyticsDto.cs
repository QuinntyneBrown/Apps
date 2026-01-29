namespace SessionAnalytics.Api.Features;

public record AnalyticsDto
{
    public Guid AnalyticsId { get; init; }
    public Guid UserId { get; init; }
    public DateTime Date { get; init; }
    public int TotalSessions { get; init; }
    public int TotalFocusMinutes { get; init; }
    public int TotalDistractions { get; init; }
    public double AverageSessionLength { get; init; }
    public DateTime UpdatedAt { get; init; }
}
