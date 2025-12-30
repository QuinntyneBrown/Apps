using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.Nutritions;

public record GetNutritionByIdQuery : IRequest<NutritionDto?>
{
    public Guid NutritionId { get; init; }
}

public class GetNutritionByIdQueryHandler : IRequestHandler<GetNutritionByIdQuery, NutritionDto?>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<GetNutritionByIdQueryHandler> _logger;

    public GetNutritionByIdQueryHandler(
        IMealPrepPlannerContext context,
        ILogger<GetNutritionByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NutritionDto?> Handle(GetNutritionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting nutrition {NutritionId}", request.NutritionId);

        var nutrition = await _context.Nutritions
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.NutritionId == request.NutritionId, cancellationToken);

        if (nutrition == null)
        {
            _logger.LogWarning("Nutrition {NutritionId} not found", request.NutritionId);
            return null;
        }

        return nutrition.ToDto();
    }
}
