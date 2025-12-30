using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.GroceryLists;

public record DeleteGroceryListCommand : IRequest<bool>
{
    public Guid GroceryListId { get; init; }
}

public class DeleteGroceryListCommandHandler : IRequestHandler<DeleteGroceryListCommand, bool>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<DeleteGroceryListCommandHandler> _logger;

    public DeleteGroceryListCommandHandler(
        IMealPrepPlannerContext context,
        ILogger<DeleteGroceryListCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGroceryListCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting grocery list {GroceryListId}", request.GroceryListId);

        var groceryList = await _context.GroceryLists
            .FirstOrDefaultAsync(g => g.GroceryListId == request.GroceryListId, cancellationToken);

        if (groceryList == null)
        {
            _logger.LogWarning("Grocery list {GroceryListId} not found", request.GroceryListId);
            return false;
        }

        _context.GroceryLists.Remove(groceryList);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted grocery list {GroceryListId}", request.GroceryListId);

        return true;
    }
}
