using MealPrepPlanner.Core;

namespace MealPrepPlanner.Api.Features.Recipes;

public record RecipeDto
{
    public Guid RecipeId { get; init; }
    public Guid UserId { get; init; }
    public Guid? MealPlanId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int PrepTimeMinutes { get; init; }
    public int CookTimeMinutes { get; init; }
    public int Servings { get; init; }
    public string Ingredients { get; init; } = string.Empty;
    public string Instructions { get; init; } = string.Empty;
    public string MealType { get; init; } = string.Empty;
    public string? Tags { get; init; }
    public bool IsFavorite { get; init; }
    public DateTime CreatedAt { get; init; }
    public int TotalTime { get; init; }
    public bool IsQuickMeal { get; init; }
}

public static class RecipeExtensions
{
    public static RecipeDto ToDto(this Recipe recipe)
    {
        return new RecipeDto
        {
            RecipeId = recipe.RecipeId,
            UserId = recipe.UserId,
            MealPlanId = recipe.MealPlanId,
            Name = recipe.Name,
            Description = recipe.Description,
            PrepTimeMinutes = recipe.PrepTimeMinutes,
            CookTimeMinutes = recipe.CookTimeMinutes,
            Servings = recipe.Servings,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions,
            MealType = recipe.MealType,
            Tags = recipe.Tags,
            IsFavorite = recipe.IsFavorite,
            CreatedAt = recipe.CreatedAt,
            TotalTime = recipe.GetTotalTime(),
            IsQuickMeal = recipe.IsQuickMeal(),
        };
    }
}
