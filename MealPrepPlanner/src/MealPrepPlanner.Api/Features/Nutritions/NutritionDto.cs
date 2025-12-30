using MealPrepPlanner.Core;

namespace MealPrepPlanner.Api.Features.Nutritions;

public record NutritionDto
{
    public Guid NutritionId { get; init; }
    public Guid UserId { get; init; }
    public Guid? RecipeId { get; init; }
    public int Calories { get; init; }
    public decimal Protein { get; init; }
    public decimal Carbohydrates { get; init; }
    public decimal Fat { get; init; }
    public decimal? Fiber { get; init; }
    public decimal? Sugar { get; init; }
    public decimal? Sodium { get; init; }
    public string? AdditionalNutrients { get; init; }
    public DateTime CreatedAt { get; init; }
    public decimal ProteinCaloriesPercentage { get; init; }
    public bool IsHighProtein { get; init; }
}

public static class NutritionExtensions
{
    public static NutritionDto ToDto(this Nutrition nutrition)
    {
        return new NutritionDto
        {
            NutritionId = nutrition.NutritionId,
            UserId = nutrition.UserId,
            RecipeId = nutrition.RecipeId,
            Calories = nutrition.Calories,
            Protein = nutrition.Protein,
            Carbohydrates = nutrition.Carbohydrates,
            Fat = nutrition.Fat,
            Fiber = nutrition.Fiber,
            Sugar = nutrition.Sugar,
            Sodium = nutrition.Sodium,
            AdditionalNutrients = nutrition.AdditionalNutrients,
            CreatedAt = nutrition.CreatedAt,
            ProteinCaloriesPercentage = nutrition.GetProteinCaloriesPercentage(),
            IsHighProtein = nutrition.IsHighProtein(),
        };
    }
}
