using MealPrepPlanner.Core;

namespace MealPrepPlanner.Api.Features.MealPlans;

public record MealPlanDto
{
    public Guid MealPlanId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int? DailyCalorieTarget { get; init; }
    public string? DietaryPreferences { get; init; }
    public bool IsActive { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public int Duration { get; init; }
    public bool IsCurrentlyActive { get; init; }
}

public static class MealPlanExtensions
{
    public static MealPlanDto ToDto(this MealPlan mealPlan)
    {
        return new MealPlanDto
        {
            MealPlanId = mealPlan.MealPlanId,
            UserId = mealPlan.UserId,
            Name = mealPlan.Name,
            StartDate = mealPlan.StartDate,
            EndDate = mealPlan.EndDate,
            DailyCalorieTarget = mealPlan.DailyCalorieTarget,
            DietaryPreferences = mealPlan.DietaryPreferences,
            IsActive = mealPlan.IsActive,
            Notes = mealPlan.Notes,
            CreatedAt = mealPlan.CreatedAt,
            Duration = mealPlan.GetDuration(),
            IsCurrentlyActive = mealPlan.IsCurrentlyActive(),
        };
    }
}
