using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.ShoppingLists;

public record DeleteShoppingListCommand : IRequest<bool>
{
    public Guid ShoppingListId { get; init; }
}

public class DeleteShoppingListCommandHandler : IRequestHandler<DeleteShoppingListCommand, bool>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<DeleteShoppingListCommandHandler> _logger;

    public DeleteShoppingListCommandHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<DeleteShoppingListCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteShoppingListCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting shopping list {ShoppingListId}", request.ShoppingListId);

        var shoppingList = await _context.ShoppingLists
            .FirstOrDefaultAsync(s => s.ShoppingListId == request.ShoppingListId, cancellationToken);

        if (shoppingList == null)
        {
            _logger.LogWarning("Shopping list {ShoppingListId} not found", request.ShoppingListId);
            return false;
        }

        _context.ShoppingLists.Remove(shoppingList);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted shopping list {ShoppingListId}", request.ShoppingListId);

        return true;
    }
}
