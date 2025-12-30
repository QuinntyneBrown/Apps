using MealPrepPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.Recipes;

public record CreateRecipeCommand : IRequest<RecipeDto>
{
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
}

public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, RecipeDto>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<CreateRecipeCommandHandler> _logger;

    public CreateRecipeCommandHandler(
        IMealPrepPlannerContext context,
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
            MealPlanId = request.MealPlanId,
            Name = request.Name,
            Description = request.Description,
            PrepTimeMinutes = request.PrepTimeMinutes,
            CookTimeMinutes = request.CookTimeMinutes,
            Servings = request.Servings,
            Ingredients = request.Ingredients,
            Instructions = request.Instructions,
            MealType = request.MealType,
            Tags = request.Tags,
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
