using HomeInventoryManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeInventoryManager.Api.Features.Items;

public record GetItemsQuery : IRequest<IEnumerable<ItemDto>>
{
    public Guid? UserId { get; init; }
    public Category? Category { get; init; }
    public Room? Room { get; init; }
}

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IEnumerable<ItemDto>>
{
    private readonly IHomeInventoryManagerContext _context;
    private readonly ILogger<GetItemsQueryHandler> _logger;

    public GetItemsQueryHandler(
        IHomeInventoryManagerContext context,
        ILogger<GetItemsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting items for user {UserId}", request.UserId);

        var query = _context.Items.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(i => i.UserId == request.UserId.Value);
        }

        if (request.Category.HasValue)
        {
            query = query.Where(i => i.Category == request.Category.Value);
        }

        if (request.Room.HasValue)
        {
            query = query.Where(i => i.Room == request.Room.Value);
        }

        var items = await query
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync(cancellationToken);

        return items.Select(i => i.ToDto());
    }
}
