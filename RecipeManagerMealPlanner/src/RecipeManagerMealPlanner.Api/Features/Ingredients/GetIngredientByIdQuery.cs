using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.Ingredients;

public record GetIngredientByIdQuery : IRequest<IngredientDto?>
{
    public Guid IngredientId { get; init; }
}

public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, IngredientDto?>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<GetIngredientByIdQueryHandler> _logger;

    public GetIngredientByIdQueryHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<GetIngredientByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IngredientDto?> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting ingredient {IngredientId}", request.IngredientId);

        var ingredient = await _context.Ingredients
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.IngredientId == request.IngredientId, cancellationToken);

        return ingredient?.ToDto();
    }
}
