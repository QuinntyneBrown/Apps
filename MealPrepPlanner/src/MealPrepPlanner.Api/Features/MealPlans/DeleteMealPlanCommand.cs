using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.MealPlans;

public record DeleteMealPlanCommand : IRequest<bool>
{
    public Guid MealPlanId { get; init; }
}

public class DeleteMealPlanCommandHandler : IRequestHandler<DeleteMealPlanCommand, bool>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<DeleteMealPlanCommandHandler> _logger;

    public DeleteMealPlanCommandHandler(
        IMealPrepPlannerContext context,
        ILogger<DeleteMealPlanCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMealPlanCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting meal plan {MealPlanId}", request.MealPlanId);

        var mealPlan = await _context.MealPlans
            .FirstOrDefaultAsync(m => m.MealPlanId == request.MealPlanId, cancellationToken);

        if (mealPlan == null)
        {
            _logger.LogWarning("Meal plan {MealPlanId} not found", request.MealPlanId);
            return false;
        }

        _context.MealPlans.Remove(mealPlan);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted meal plan {MealPlanId}", request.MealPlanId);

        return true;
    }
}
