using HomeInventoryManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeInventoryManager.Api.Features.Items;

public record DeleteItemCommand : IRequest<bool>
{
    public Guid ItemId { get; init; }
}

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, bool>
{
    private readonly IHomeInventoryManagerContext _context;
    private readonly ILogger<DeleteItemCommandHandler> _logger;

    public DeleteItemCommandHandler(
        IHomeInventoryManagerContext context,
        ILogger<DeleteItemCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting item {ItemId}", request.ItemId);

        var item = await _context.Items
            .FirstOrDefaultAsync(i => i.ItemId == request.ItemId, cancellationToken);

        if (item == null)
        {
            _logger.LogWarning("Item {ItemId} not found", request.ItemId);
            return false;
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted item {ItemId}", request.ItemId);

        return true;
    }
}
