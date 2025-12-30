using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.ShoppingLists;

public record GetShoppingListByIdQuery : IRequest<ShoppingListDto?>
{
    public Guid ShoppingListId { get; init; }
}

public class GetShoppingListByIdQueryHandler : IRequestHandler<GetShoppingListByIdQuery, ShoppingListDto?>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<GetShoppingListByIdQueryHandler> _logger;

    public GetShoppingListByIdQueryHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<GetShoppingListByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ShoppingListDto?> Handle(GetShoppingListByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting shopping list {ShoppingListId}", request.ShoppingListId);

        var shoppingList = await _context.ShoppingLists
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ShoppingListId == request.ShoppingListId, cancellationToken);

        return shoppingList?.ToDto();
    }
}
