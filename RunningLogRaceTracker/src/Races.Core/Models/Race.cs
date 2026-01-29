namespace Races.Core.Models;

public class Race
{
    public Guid RaceId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public RaceType RaceType { get; set; }
    public DateTime RaceDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public decimal Distance { get; set; }
    public int? FinishTimeMinutes { get; set; }
    public int? GoalTimeMinutes { get; set; }
    public int? Placement { get; set; }
    public bool IsCompleted { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool AchievedGoal()
    {
        if (!IsCompleted || !FinishTimeMinutes.HasValue || !GoalTimeMinutes.HasValue)
            return false;
        return FinishTimeMinutes.Value <= GoalTimeMinutes.Value;
    }

    public bool IsUpcoming() => RaceDate > DateTime.UtcNow;
}
