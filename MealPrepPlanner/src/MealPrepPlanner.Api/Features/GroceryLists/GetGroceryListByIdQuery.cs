using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.GroceryLists;

public record GetGroceryListByIdQuery : IRequest<GroceryListDto?>
{
    public Guid GroceryListId { get; init; }
}

public class GetGroceryListByIdQueryHandler : IRequestHandler<GetGroceryListByIdQuery, GroceryListDto?>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<GetGroceryListByIdQueryHandler> _logger;

    public GetGroceryListByIdQueryHandler(
        IMealPrepPlannerContext context,
        ILogger<GetGroceryListByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GroceryListDto?> Handle(GetGroceryListByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting grocery list {GroceryListId}", request.GroceryListId);

        var groceryList = await _context.GroceryLists
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.GroceryListId == request.GroceryListId, cancellationToken);

        if (groceryList == null)
        {
            _logger.LogWarning("Grocery list {GroceryListId} not found", request.GroceryListId);
            return null;
        }

        return groceryList.ToDto();
    }
}
