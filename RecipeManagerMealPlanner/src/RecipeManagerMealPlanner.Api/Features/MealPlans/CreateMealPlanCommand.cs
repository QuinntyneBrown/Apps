using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.MealPlans;

public record CreateMealPlanCommand : IRequest<MealPlanDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime MealDate { get; init; }
    public string MealType { get; init; } = string.Empty;
    public Guid? RecipeId { get; init; }
    public string? Notes { get; init; }
}

public class CreateMealPlanCommandHandler : IRequestHandler<CreateMealPlanCommand, MealPlanDto>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<CreateMealPlanCommandHandler> _logger;

    public CreateMealPlanCommandHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<CreateMealPlanCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MealPlanDto> Handle(CreateMealPlanCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating meal plan for user {UserId}, name: {Name}, date: {MealDate}",
            request.UserId,
            request.Name,
            request.MealDate);

        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            MealDate = request.MealDate,
            MealType = request.MealType,
            RecipeId = request.RecipeId,
            Notes = request.Notes,
            IsPrepared = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.MealPlans.Add(mealPlan);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created meal plan {MealPlanId} for user {UserId}",
            mealPlan.MealPlanId,
            request.UserId);

        return mealPlan.ToDto();
    }
}
