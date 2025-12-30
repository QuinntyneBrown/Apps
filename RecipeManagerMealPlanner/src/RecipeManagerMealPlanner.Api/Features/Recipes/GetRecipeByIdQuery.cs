using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.Recipes;

public record GetRecipeByIdQuery : IRequest<RecipeDto?>
{
    public Guid RecipeId { get; init; }
}

public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, RecipeDto?>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<GetRecipeByIdQueryHandler> _logger;

    public GetRecipeByIdQueryHandler(
        IRecipeManagerMealPlannerContext context,
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

        return recipe?.ToDto();
    }
}
