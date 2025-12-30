using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.ShoppingLists;

public record GetShoppingListsQuery : IRequest<IEnumerable<ShoppingListDto>>
{
    public Guid? UserId { get; init; }
    public bool? CompletedOnly { get; init; }
}

public class GetShoppingListsQueryHandler : IRequestHandler<GetShoppingListsQuery, IEnumerable<ShoppingListDto>>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<GetShoppingListsQueryHandler> _logger;

    public GetShoppingListsQueryHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<GetShoppingListsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ShoppingListDto>> Handle(GetShoppingListsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting shopping lists for user {UserId}", request.UserId);

        var query = _context.ShoppingLists.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(s => s.UserId == request.UserId.Value);
        }

        if (request.CompletedOnly == true)
        {
            query = query.Where(s => s.IsCompleted);
        }

        var shoppingLists = await query
            .OrderByDescending(s => s.CreatedDate)
            .ToListAsync(cancellationToken);

        return shoppingLists.Select(s => s.ToDto());
    }
}
