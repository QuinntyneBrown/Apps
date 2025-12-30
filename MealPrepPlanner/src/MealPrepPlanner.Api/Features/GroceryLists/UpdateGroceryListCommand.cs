using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.GroceryLists;

public record UpdateGroceryListCommand : IRequest<GroceryListDto?>
{
    public Guid GroceryListId { get; init; }
    public Guid? MealPlanId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Items { get; init; } = "[]";
    public DateTime? ShoppingDate { get; init; }
    public bool IsCompleted { get; init; }
    public decimal? EstimatedCost { get; init; }
    public string? Notes { get; init; }
}

public class UpdateGroceryListCommandHandler : IRequestHandler<UpdateGroceryListCommand, GroceryListDto?>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<UpdateGroceryListCommandHandler> _logger;

    public UpdateGroceryListCommandHandler(
        IMealPrepPlannerContext context,
        ILogger<UpdateGroceryListCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GroceryListDto?> Handle(UpdateGroceryListCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating grocery list {GroceryListId}", request.GroceryListId);

        var groceryList = await _context.GroceryLists
            .FirstOrDefaultAsync(g => g.GroceryListId == request.GroceryListId, cancellationToken);

        if (groceryList == null)
        {
            _logger.LogWarning("Grocery list {GroceryListId} not found", request.GroceryListId);
            return null;
        }

        groceryList.MealPlanId = request.MealPlanId;
        groceryList.Name = request.Name;
        groceryList.Items = request.Items;
        groceryList.ShoppingDate = request.ShoppingDate;
        groceryList.IsCompleted = request.IsCompleted;
        groceryList.EstimatedCost = request.EstimatedCost;
        groceryList.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated grocery list {GroceryListId}", request.GroceryListId);

        return groceryList.ToDto();
    }
}
