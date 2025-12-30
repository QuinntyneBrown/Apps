using MealPrepPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.MealPlans;

public record CreateMealPlanCommand : IRequest<MealPlanDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int? DailyCalorieTarget { get; init; }
    public string? DietaryPreferences { get; init; }
    public string? Notes { get; init; }
}

public class CreateMealPlanCommandHandler : IRequestHandler<CreateMealPlanCommand, MealPlanDto>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<CreateMealPlanCommandHandler> _logger;

    public CreateMealPlanCommandHandler(
        IMealPrepPlannerContext context,
        ILogger<CreateMealPlanCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MealPlanDto> Handle(CreateMealPlanCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating meal plan for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            DailyCalorieTarget = request.DailyCalorieTarget,
            DietaryPreferences = request.DietaryPreferences,
            IsActive = true,
            Notes = request.Notes,
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
