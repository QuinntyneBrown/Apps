using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.Ingredients;

public record GetIngredientsQuery : IRequest<IEnumerable<IngredientDto>>
{
    public Guid? RecipeId { get; init; }
}

public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, IEnumerable<IngredientDto>>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<GetIngredientsQueryHandler> _logger;

    public GetIngredientsQueryHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<GetIngredientsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<IngredientDto>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting ingredients for recipe {RecipeId}", request.RecipeId);

        var query = _context.Ingredients.AsNoTracking();

        if (request.RecipeId.HasValue)
        {
            query = query.Where(i => i.RecipeId == request.RecipeId.Value);
        }

        var ingredients = await query
            .OrderBy(i => i.Name)
            .ToListAsync(cancellationToken);

        return ingredients.Select(i => i.ToDto());
    }
}
