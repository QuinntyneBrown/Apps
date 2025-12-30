using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.MealPlans;

public record UpdateMealPlanCommand : IRequest<MealPlanDto?>
{
    public Guid MealPlanId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int? DailyCalorieTarget { get; init; }
    public string? DietaryPreferences { get; init; }
    public bool IsActive { get; init; }
    public string? Notes { get; init; }
}

public class UpdateMealPlanCommandHandler : IRequestHandler<UpdateMealPlanCommand, MealPlanDto?>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<UpdateMealPlanCommandHandler> _logger;

    public UpdateMealPlanCommandHandler(
        IMealPrepPlannerContext context,
        ILogger<UpdateMealPlanCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MealPlanDto?> Handle(UpdateMealPlanCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating meal plan {MealPlanId}", request.MealPlanId);

        var mealPlan = await _context.MealPlans
            .FirstOrDefaultAsync(m => m.MealPlanId == request.MealPlanId, cancellationToken);

        if (mealPlan == null)
        {
            _logger.LogWarning("Meal plan {MealPlanId} not found", request.MealPlanId);
            return null;
        }

        mealPlan.Name = request.Name;
        mealPlan.StartDate = request.StartDate;
        mealPlan.EndDate = request.EndDate;
        mealPlan.DailyCalorieTarget = request.DailyCalorieTarget;
        mealPlan.DietaryPreferences = request.DietaryPreferences;
        mealPlan.IsActive = request.IsActive;
        mealPlan.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated meal plan {MealPlanId}", request.MealPlanId);

        return mealPlan.ToDto();
    }
}
