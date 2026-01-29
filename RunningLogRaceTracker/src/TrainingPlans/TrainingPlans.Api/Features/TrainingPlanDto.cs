namespace TrainingPlans.Api.Features;

public record TrainingPlanDto
{
    public Guid TrainingPlanId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid? RaceId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal? WeeklyMileageGoal { get; init; }
    public string? PlanDetails { get; init; }
    public bool IsActive { get; init; }
    public string? Notes { get; init; }
}
