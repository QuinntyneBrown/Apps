using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.Recipes;

public record UpdateRecipeCommand : IRequest<RecipeDto?>
{
    public Guid RecipeId { get; init; }
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
}

public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, RecipeDto?>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<UpdateRecipeCommandHandler> _logger;

    public UpdateRecipeCommandHandler(
        IRecipeManagerMealPlannerContext context,
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

        recipe.Name = request.Name;
        recipe.Description = request.Description;
        recipe.Cuisine = request.Cuisine;
        recipe.DifficultyLevel = request.DifficultyLevel;
        recipe.PrepTimeMinutes = request.PrepTimeMinutes;
        recipe.CookTimeMinutes = request.CookTimeMinutes;
        recipe.Servings = request.Servings;
        recipe.Instructions = request.Instructions;
        recipe.PhotoUrl = request.PhotoUrl;
        recipe.Source = request.Source;
        recipe.Rating = request.Rating;
        recipe.Notes = request.Notes;
        recipe.IsFavorite = request.IsFavorite;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated recipe {RecipeId}", request.RecipeId);

        return recipe.ToDto();
    }
}
