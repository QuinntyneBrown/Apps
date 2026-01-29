namespace Runs.Core.Models;

public class Run
{
    public Guid RunId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public decimal Distance { get; set; }
    public int DurationMinutes { get; set; }
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    public decimal? AveragePace { get; set; }
    public int? AverageHeartRate { get; set; }
    public int? ElevationGain { get; set; }
    public int? CaloriesBurned { get; set; }
    public string? Route { get; set; }
    public string? Weather { get; set; }
    public string? Notes { get; set; }
    public int? EffortRating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public decimal CalculatePace() => Distance == 0 ? 0 : DurationMinutes / Distance;
    public bool IsToday() => CompletedAt.Date == DateTime.UtcNow.Date;
}
