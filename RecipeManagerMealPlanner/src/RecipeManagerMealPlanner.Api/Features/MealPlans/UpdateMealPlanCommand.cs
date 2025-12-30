using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.MealPlans;

public record UpdateMealPlanCommand : IRequest<MealPlanDto?>
{
    public Guid MealPlanId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime MealDate { get; init; }
    public string MealType { get; init; } = string.Empty;
    public Guid? RecipeId { get; init; }
    public string? Notes { get; init; }
    public bool IsPrepared { get; init; }
}

public class UpdateMealPlanCommandHandler : IRequestHandler<UpdateMealPlanCommand, MealPlanDto?>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<UpdateMealPlanCommandHandler> _logger;

    public UpdateMealPlanCommandHandler(
        IRecipeManagerMealPlannerContext context,
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
        mealPlan.MealDate = request.MealDate;
        mealPlan.MealType = request.MealType;
        mealPlan.RecipeId = request.RecipeId;
        mealPlan.Notes = request.Notes;
        mealPlan.IsPrepared = request.IsPrepared;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated meal plan {MealPlanId}", request.MealPlanId);

        return mealPlan.ToDto();
    }
}
