using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.Nutritions;

public record DeleteNutritionCommand : IRequest<bool>
{
    public Guid NutritionId { get; init; }
}

public class DeleteNutritionCommandHandler : IRequestHandler<DeleteNutritionCommand, bool>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<DeleteNutritionCommandHandler> _logger;

    public DeleteNutritionCommandHandler(
        IMealPrepPlannerContext context,
        ILogger<DeleteNutritionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteNutritionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting nutrition {NutritionId}", request.NutritionId);

        var nutrition = await _context.Nutritions
            .FirstOrDefaultAsync(n => n.NutritionId == request.NutritionId, cancellationToken);

        if (nutrition == null)
        {
            _logger.LogWarning("Nutrition {NutritionId} not found", request.NutritionId);
            return false;
        }

        _context.Nutritions.Remove(nutrition);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted nutrition {NutritionId}", request.NutritionId);

        return true;
    }
}
