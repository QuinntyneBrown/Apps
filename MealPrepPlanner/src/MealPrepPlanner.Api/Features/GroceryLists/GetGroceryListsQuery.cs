using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.GroceryLists;

public record GetGroceryListsQuery : IRequest<IEnumerable<GroceryListDto>>
{
    public Guid? UserId { get; init; }
    public Guid? MealPlanId { get; init; }
    public bool? IsCompleted { get; init; }
}

public class GetGroceryListsQueryHandler : IRequestHandler<GetGroceryListsQuery, IEnumerable<GroceryListDto>>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<GetGroceryListsQueryHandler> _logger;

    public GetGroceryListsQueryHandler(
        IMealPrepPlannerContext context,
        ILogger<GetGroceryListsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<GroceryListDto>> Handle(GetGroceryListsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting grocery lists for user {UserId}", request.UserId);

        var query = _context.GroceryLists.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(g => g.UserId == request.UserId.Value);
        }

        if (request.MealPlanId.HasValue)
        {
            query = query.Where(g => g.MealPlanId == request.MealPlanId.Value);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(g => g.IsCompleted == request.IsCompleted.Value);
        }

        var groceryLists = await query
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync(cancellationToken);

        return groceryLists.Select(g => g.ToDto());
    }
}
