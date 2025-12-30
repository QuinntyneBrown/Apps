using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.Ingredients;

public record UpdateIngredientCommand : IRequest<IngredientDto?>
{
    public Guid IngredientId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Quantity { get; init; } = string.Empty;
    public string? Unit { get; init; }
    public string? Notes { get; init; }
}

public class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand, IngredientDto?>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<UpdateIngredientCommandHandler> _logger;

    public UpdateIngredientCommandHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<UpdateIngredientCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IngredientDto?> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating ingredient {IngredientId}", request.IngredientId);

        var ingredient = await _context.Ingredients
            .FirstOrDefaultAsync(i => i.IngredientId == request.IngredientId, cancellationToken);

        if (ingredient == null)
        {
            _logger.LogWarning("Ingredient {IngredientId} not found", request.IngredientId);
            return null;
        }

        ingredient.Name = request.Name;
        ingredient.Quantity = request.Quantity;
        ingredient.Unit = request.Unit;
        ingredient.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated ingredient {IngredientId}", request.IngredientId);

        return ingredient.ToDto();
    }
}
