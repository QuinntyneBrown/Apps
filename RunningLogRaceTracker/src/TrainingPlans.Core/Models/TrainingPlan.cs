namespace TrainingPlans.Core.Models;

public class TrainingPlan
{
    public Guid TrainingPlanId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? RaceId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? WeeklyMileageGoal { get; set; }
    public string? PlanDetails { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int GetDurationInWeeks() => (int)((EndDate - StartDate).TotalDays / 7);
    public bool IsInProgress()
    {
        var today = DateTime.UtcNow.Date;
        return IsActive && today >= StartDate.Date && today <= EndDate.Date;
    }
}
