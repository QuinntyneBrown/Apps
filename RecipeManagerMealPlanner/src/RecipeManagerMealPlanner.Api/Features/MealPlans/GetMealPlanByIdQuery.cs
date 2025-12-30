using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.MealPlans;

public record GetMealPlanByIdQuery : IRequest<MealPlanDto?>
{
    public Guid MealPlanId { get; init; }
}

public class GetMealPlanByIdQueryHandler : IRequestHandler<GetMealPlanByIdQuery, MealPlanDto?>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<GetMealPlanByIdQueryHandler> _logger;

    public GetMealPlanByIdQueryHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<GetMealPlanByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MealPlanDto?> Handle(GetMealPlanByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting meal plan {MealPlanId}", request.MealPlanId);

        var mealPlan = await _context.MealPlans
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MealPlanId == request.MealPlanId, cancellationToken);

        return mealPlan?.ToDto();
    }
}
