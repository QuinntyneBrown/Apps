using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.Recipes;

public record GetRecipeByIdQuery : IRequest<RecipeDto?>
{
    public Guid RecipeId { get; init; }
}

public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, RecipeDto?>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<GetRecipeByIdQueryHandler> _logger;

    public GetRecipeByIdQueryHandler(
        IMealPrepPlannerContext context,
        ILogger<GetRecipeByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RecipeDto?> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting recipe {RecipeId}", request.RecipeId);

        var recipe = await _context.Recipes
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);

        if (recipe == null)
        {
            _logger.LogWarning("Recipe {RecipeId} not found", request.RecipeId);
            return null;
        }

        return recipe.ToDto();
    }
}
