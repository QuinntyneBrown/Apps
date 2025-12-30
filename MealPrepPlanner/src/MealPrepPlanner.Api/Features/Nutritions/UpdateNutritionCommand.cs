using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.Nutritions;

public record UpdateNutritionCommand : IRequest<NutritionDto?>
{
    public Guid NutritionId { get; init; }
    public Guid? RecipeId { get; init; }
    public int Calories { get; init; }
    public decimal Protein { get; init; }
    public decimal Carbohydrates { get; init; }
    public decimal Fat { get; init; }
    public decimal? Fiber { get; init; }
    public decimal? Sugar { get; init; }
    public decimal? Sodium { get; init; }
    public string? AdditionalNutrients { get; init; }
}

public class UpdateNutritionCommandHandler : IRequestHandler<UpdateNutritionCommand, NutritionDto?>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<UpdateNutritionCommandHandler> _logger;

    public UpdateNutritionCommandHandler(
        IMealPrepPlannerContext context,
        ILogger<UpdateNutritionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NutritionDto?> Handle(UpdateNutritionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating nutrition {NutritionId}", request.NutritionId);

        var nutrition = await _context.Nutritions
            .FirstOrDefaultAsync(n => n.NutritionId == request.NutritionId, cancellationToken);

        if (nutrition == null)
        {
            _logger.LogWarning("Nutrition {NutritionId} not found", request.NutritionId);
            return null;
        }

        nutrition.RecipeId = request.RecipeId;
        nutrition.Calories = request.Calories;
        nutrition.Protein = request.Protein;
        nutrition.Carbohydrates = request.Carbohydrates;
        nutrition.Fat = request.Fat;
        nutrition.Fiber = request.Fiber;
        nutrition.Sugar = request.Sugar;
        nutrition.Sodium = request.Sodium;
        nutrition.AdditionalNutrients = request.AdditionalNutrients;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated nutrition {NutritionId}", request.NutritionId);

        return nutrition.ToDto();
    }
}
