using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.Ingredients;

public record CreateIngredientCommand : IRequest<IngredientDto>
{
    public Guid RecipeId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Quantity { get; init; } = string.Empty;
    public string? Unit { get; init; }
    public string? Notes { get; init; }
}

public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, IngredientDto>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<CreateIngredientCommandHandler> _logger;

    public CreateIngredientCommandHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<CreateIngredientCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IngredientDto> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating ingredient for recipe {RecipeId}, name: {Name}",
            request.RecipeId,
            request.Name);

        var ingredient = new Ingredient
        {
            IngredientId = Guid.NewGuid(),
            RecipeId = request.RecipeId,
            Name = request.Name,
            Quantity = request.Quantity,
            Unit = request.Unit,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created ingredient {IngredientId} for recipe {RecipeId}",
            ingredient.IngredientId,
            request.RecipeId);

        return ingredient.ToDto();
    }
}
