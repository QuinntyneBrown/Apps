using BucketListManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.BucketListItems;

public record DeleteBucketListItemCommand : IRequest<bool>
{
    public Guid BucketListItemId { get; init; }
}

public class DeleteBucketListItemCommandHandler : IRequestHandler<DeleteBucketListItemCommand, bool>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<DeleteBucketListItemCommandHandler> _logger;

    public DeleteBucketListItemCommandHandler(
        IBucketListManagerContext context,
        ILogger<DeleteBucketListItemCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteBucketListItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting bucket list item {BucketListItemId}", request.BucketListItemId);

        var item = await _context.BucketListItems
            .FirstOrDefaultAsync(x => x.BucketListItemId == request.BucketListItemId, cancellationToken);

        if (item == null)
        {
            _logger.LogWarning("Bucket list item {BucketListItemId} not found", request.BucketListItemId);
            return false;
        }

        _context.BucketListItems.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted bucket list item {BucketListItemId}", request.BucketListItemId);

        return true;
    }
}
