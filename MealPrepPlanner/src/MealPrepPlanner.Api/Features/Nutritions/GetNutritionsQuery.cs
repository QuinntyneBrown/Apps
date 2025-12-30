using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.Nutritions;

public record GetNutritionsQuery : IRequest<IEnumerable<NutritionDto>>
{
    public Guid? UserId { get; init; }
    public Guid? RecipeId { get; init; }
}

public class GetNutritionsQueryHandler : IRequestHandler<GetNutritionsQuery, IEnumerable<NutritionDto>>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<GetNutritionsQueryHandler> _logger;

    public GetNutritionsQueryHandler(
        IMealPrepPlannerContext context,
        ILogger<GetNutritionsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<NutritionDto>> Handle(GetNutritionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting nutritions for user {UserId}", request.UserId);

        var query = _context.Nutritions.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(n => n.UserId == request.UserId.Value);
        }

        if (request.RecipeId.HasValue)
        {
            query = query.Where(n => n.RecipeId == request.RecipeId.Value);
        }

        var nutritions = await query
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync(cancellationToken);

        return nutritions.Select(n => n.ToDto());
    }
}
