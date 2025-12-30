using RecipeManagerMealPlanner.Core;

namespace RecipeManagerMealPlanner.Api.Features.MealPlans;

public record MealPlanDto
{
    public Guid MealPlanId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime MealDate { get; init; }
    public string MealType { get; init; } = string.Empty;
    public Guid? RecipeId { get; init; }
    public string? Notes { get; init; }
    public bool IsPrepared { get; init; }
    public DateTime CreatedAt { get; init; }
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
            MealDate = mealPlan.MealDate,
            MealType = mealPlan.MealType,
            RecipeId = mealPlan.RecipeId,
            Notes = mealPlan.Notes,
            IsPrepared = mealPlan.IsPrepared,
            CreatedAt = mealPlan.CreatedAt,
        };
    }
}
