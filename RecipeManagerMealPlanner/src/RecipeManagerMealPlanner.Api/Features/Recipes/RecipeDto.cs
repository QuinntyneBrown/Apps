using RecipeManagerMealPlanner.Core;

namespace RecipeManagerMealPlanner.Api.Features.Recipes;

public record RecipeDto
{
    public Guid RecipeId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Cuisine Cuisine { get; init; }
    public DifficultyLevel DifficultyLevel { get; init; }
    public int PrepTimeMinutes { get; init; }
    public int CookTimeMinutes { get; init; }
    public int Servings { get; init; }
    public string Instructions { get; init; } = string.Empty;
    public string? PhotoUrl { get; init; }
    public string? Source { get; init; }
    public int? Rating { get; init; }
    public string? Notes { get; init; }
    public bool IsFavorite { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class RecipeExtensions
{
    public static RecipeDto ToDto(this Recipe recipe)
    {
        return new RecipeDto
        {
            RecipeId = recipe.RecipeId,
            UserId = recipe.UserId,
            Name = recipe.Name,
            Description = recipe.Description,
            Cuisine = recipe.Cuisine,
            DifficultyLevel = recipe.DifficultyLevel,
            PrepTimeMinutes = recipe.PrepTimeMinutes,
            CookTimeMinutes = recipe.CookTimeMinutes,
            Servings = recipe.Servings,
            Instructions = recipe.Instructions,
            PhotoUrl = recipe.PhotoUrl,
            Source = recipe.Source,
            Rating = recipe.Rating,
            Notes = recipe.Notes,
            IsFavorite = recipe.IsFavorite,
            CreatedAt = recipe.CreatedAt,
        };
    }
}
