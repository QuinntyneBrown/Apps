using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.Recipes;

public record DeleteRecipeCommand : IRequest<bool>
{
    public Guid RecipeId { get; init; }
}

public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand, bool>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<DeleteRecipeCommandHandler> _logger;

    public DeleteRecipeCommandHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<DeleteRecipeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting recipe {RecipeId}", request.RecipeId);

        var recipe = await _context.Recipes
            .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);

        if (recipe == null)
        {
            _logger.LogWarning("Recipe {RecipeId} not found", request.RecipeId);
            return false;
        }

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted recipe {RecipeId}", request.RecipeId);

        return true;
    }
}
