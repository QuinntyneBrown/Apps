using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.Recipes;

public record UpdateRecipeCommand : IRequest<RecipeDto?>
{
    public Guid RecipeId { get; init; }
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
}

public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, RecipeDto?>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<UpdateRecipeCommandHandler> _logger;

    public UpdateRecipeCommandHandler(
        IMealPrepPlannerContext context,
        ILogger<UpdateRecipeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RecipeDto?> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating recipe {RecipeId}", request.RecipeId);

        var recipe = await _context.Recipes
            .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);

        if (recipe == null)
        {
            _logger.LogWarning("Recipe {RecipeId} not found", request.RecipeId);
            return null;
        }

        recipe.MealPlanId = request.MealPlanId;
        recipe.Name = request.Name;
        recipe.Description = request.Description;
        recipe.PrepTimeMinutes = request.PrepTimeMinutes;
        recipe.CookTimeMinutes = request.CookTimeMinutes;
        recipe.Servings = request.Servings;
        recipe.Ingredients = request.Ingredients;
        recipe.Instructions = request.Instructions;
        recipe.MealType = request.MealType;
        recipe.Tags = request.Tags;
        recipe.IsFavorite = request.IsFavorite;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated recipe {RecipeId}", request.RecipeId);

        return recipe.ToDto();
    }
}
