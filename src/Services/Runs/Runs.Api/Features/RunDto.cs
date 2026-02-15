namespace Runs.Api.Features;

public record RunDto
{
    public Guid RunId { get; init; }
    public Guid UserId { get; init; }
    public decimal Distance { get; init; }
    public int DurationMinutes { get; init; }
    public DateTime CompletedAt { get; init; }
    public decimal? AveragePace { get; init; }
    public int? AverageHeartRate { get; init; }
    public int? ElevationGain { get; init; }
    public int? CaloriesBurned { get; init; }
    public string? Route { get; init; }
    public string? Weather { get; init; }
    public string? Notes { get; init; }
    public int? EffortRating { get; init; }
}
