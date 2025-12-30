using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.Ingredients;

public record DeleteIngredientCommand : IRequest<bool>
{
    public Guid IngredientId { get; init; }
}

public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, bool>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<DeleteIngredientCommandHandler> _logger;

    public DeleteIngredientCommandHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<DeleteIngredientCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting ingredient {IngredientId}", request.IngredientId);

        var ingredient = await _context.Ingredients
            .FirstOrDefaultAsync(i => i.IngredientId == request.IngredientId, cancellationToken);

        if (ingredient == null)
        {
            _logger.LogWarning("Ingredient {IngredientId} not found", request.IngredientId);
            return false;
        }

        _context.Ingredients.Remove(ingredient);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted ingredient {IngredientId}", request.IngredientId);

        return true;
    }
}
