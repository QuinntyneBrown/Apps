using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.Recipes;

public record GetRecipesQuery : IRequest<IEnumerable<RecipeDto>>
{
    public Guid? UserId { get; init; }
    public Guid? MealPlanId { get; init; }
    public string? MealType { get; init; }
    public bool? IsFavorite { get; init; }
}

public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, IEnumerable<RecipeDto>>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<GetRecipesQueryHandler> _logger;

    public GetRecipesQueryHandler(
        IMealPrepPlannerContext context,
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

        if (request.MealPlanId.HasValue)
        {
            query = query.Where(r => r.MealPlanId == request.MealPlanId.Value);
        }

        if (!string.IsNullOrEmpty(request.MealType))
        {
            query = query.Where(r => r.MealType == request.MealType);
        }

        if (request.IsFavorite.HasValue)
        {
            query = query.Where(r => r.IsFavorite == request.IsFavorite.Value);
        }

        var recipes = await query
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        return recipes.Select(r => r.ToDto());
    }
}
