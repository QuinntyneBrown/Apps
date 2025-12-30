using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.ShoppingLists;

public record UpdateShoppingListCommand : IRequest<ShoppingListDto?>
{
    public Guid ShoppingListId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Items { get; init; } = string.Empty;
    public bool IsCompleted { get; init; }
    public string? Notes { get; init; }
}

public class UpdateShoppingListCommandHandler : IRequestHandler<UpdateShoppingListCommand, ShoppingListDto?>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<UpdateShoppingListCommandHandler> _logger;

    public UpdateShoppingListCommandHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<UpdateShoppingListCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ShoppingListDto?> Handle(UpdateShoppingListCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating shopping list {ShoppingListId}", request.ShoppingListId);

        var shoppingList = await _context.ShoppingLists
            .FirstOrDefaultAsync(s => s.ShoppingListId == request.ShoppingListId, cancellationToken);

        if (shoppingList == null)
        {
            _logger.LogWarning("Shopping list {ShoppingListId} not found", request.ShoppingListId);
            return null;
        }

        shoppingList.Name = request.Name;
        shoppingList.Items = request.Items;
        shoppingList.Notes = request.Notes;

        if (request.IsCompleted && !shoppingList.IsCompleted)
        {
            shoppingList.Complete();
        }
        else if (!request.IsCompleted && shoppingList.IsCompleted)
        {
            shoppingList.IsCompleted = false;
            shoppingList.CompletedDate = null;
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated shopping list {ShoppingListId}", request.ShoppingListId);

        return shoppingList.ToDto();
    }
}
