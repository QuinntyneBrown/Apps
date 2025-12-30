using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.Recipes;

public record CreateRecipeCommand : IRequest<RecipeDto>
{
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
    public string? Notes { get; init; }
}

public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, RecipeDto>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<CreateRecipeCommandHandler> _logger;

    public CreateRecipeCommandHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<CreateRecipeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RecipeDto> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating recipe for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            Cuisine = request.Cuisine,
            DifficultyLevel = request.DifficultyLevel,
            PrepTimeMinutes = request.PrepTimeMinutes,
            CookTimeMinutes = request.CookTimeMinutes,
            Servings = request.Servings,
            Instructions = request.Instructions,
            PhotoUrl = request.PhotoUrl,
            Source = request.Source,
            Notes = request.Notes,
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created recipe {RecipeId} for user {UserId}",
            recipe.RecipeId,
            request.UserId);

        return recipe.ToDto();
    }
}
