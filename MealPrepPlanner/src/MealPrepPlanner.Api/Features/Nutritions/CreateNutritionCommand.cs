using MealPrepPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.Nutritions;

public record CreateNutritionCommand : IRequest<NutritionDto>
{
    public Guid UserId { get; init; }
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

public class CreateNutritionCommandHandler : IRequestHandler<CreateNutritionCommand, NutritionDto>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<CreateNutritionCommandHandler> _logger;

    public CreateNutritionCommandHandler(
        IMealPrepPlannerContext context,
        ILogger<CreateNutritionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NutritionDto> Handle(CreateNutritionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating nutrition record for user {UserId}",
            request.UserId);

        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = request.UserId,
            RecipeId = request.RecipeId,
            Calories = request.Calories,
            Protein = request.Protein,
            Carbohydrates = request.Carbohydrates,
            Fat = request.Fat,
            Fiber = request.Fiber,
            Sugar = request.Sugar,
            Sodium = request.Sodium,
            AdditionalNutrients = request.AdditionalNutrients,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Nutritions.Add(nutrition);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created nutrition record {NutritionId} for user {UserId}",
            nutrition.NutritionId,
            request.UserId);

        return nutrition.ToDto();
    }
}
