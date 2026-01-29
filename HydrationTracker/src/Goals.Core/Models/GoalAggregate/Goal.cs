namespace Goals.Core.Models;

public class Goal
{
    public Guid GoalId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public decimal DailyGoalMl { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public decimal GetDailyGoalInOz() => DailyGoalMl * 0.033814m;
}
