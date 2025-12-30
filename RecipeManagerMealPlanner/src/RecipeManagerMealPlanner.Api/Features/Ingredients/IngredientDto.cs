using RecipeManagerMealPlanner.Core;

namespace RecipeManagerMealPlanner.Api.Features.Ingredients;

public record IngredientDto
{
    public Guid IngredientId { get; init; }
    public Guid RecipeId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Quantity { get; init; } = string.Empty;
    public string? Unit { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class IngredientExtensions
{
    public static IngredientDto ToDto(this Ingredient ingredient)
    {
        return new IngredientDto
        {
            IngredientId = ingredient.IngredientId,
            RecipeId = ingredient.RecipeId,
            Name = ingredient.Name,
            Quantity = ingredient.Quantity,
            Unit = ingredient.Unit,
            Notes = ingredient.Notes,
            CreatedAt = ingredient.CreatedAt,
        };
    }
}
