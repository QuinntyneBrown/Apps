namespace Rounds.Core.Models;

public class Round
{
    public Guid RoundId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime PlayedDate { get; set; }
    public int TotalScore { get; set; }
    public int? FrontNineScore { get; set; }
    public int? BackNineScore { get; set; }
    public string? WeatherConditions { get; set; }
    public string? Notes { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
