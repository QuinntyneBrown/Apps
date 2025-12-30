using HomeInventoryManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeInventoryManager.Api.Features.Items;

public record GetItemByIdQuery : IRequest<ItemDto?>
{
    public Guid ItemId { get; init; }
}

public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemDto?>
{
    private readonly IHomeInventoryManagerContext _context;
    private readonly ILogger<GetItemByIdQueryHandler> _logger;

    public GetItemByIdQueryHandler(
        IHomeInventoryManagerContext context,
        ILogger<GetItemByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ItemDto?> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting item {ItemId}", request.ItemId);

        var item = await _context.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.ItemId == request.ItemId, cancellationToken);

        return item?.ToDto();
    }
}
