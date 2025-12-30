using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.Recipes;

public record GetRecipesQuery : IRequest<IEnumerable<RecipeDto>>
{
    public Guid? UserId { get; init; }
    public Cuisine? Cuisine { get; init; }
    public DifficultyLevel? DifficultyLevel { get; init; }
    public bool? FavoritesOnly { get; init; }
}

public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, IEnumerable<RecipeDto>>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<GetRecipesQueryHandler> _logger;

    public GetRecipesQueryHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<GetRecipesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<RecipeDto>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting recipes for user {UserId}", request.UserId);

        var query = _context.Recipes.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        if (request.Cuisine.HasValue)
        {
            query = query.Where(r => r.Cuisine == request.Cuisine.Value);
        }

        if (request.DifficultyLevel.HasValue)
        {
            query = query.Where(r => r.DifficultyLevel == request.DifficultyLevel.Value);
        }

        if (request.FavoritesOnly == true)
        {
            query = query.Where(r => r.IsFavorite);
        }

        var recipes = await query
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        return recipes.Select(r => r.ToDto());
    }
}
