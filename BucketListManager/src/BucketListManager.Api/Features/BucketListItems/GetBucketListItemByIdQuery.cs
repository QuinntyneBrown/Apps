using BucketListManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.BucketListItems;

public record GetBucketListItemByIdQuery : IRequest<BucketListItemDto?>
{
    public Guid BucketListItemId { get; init; }
}

public class GetBucketListItemByIdQueryHandler : IRequestHandler<GetBucketListItemByIdQuery, BucketListItemDto?>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<GetBucketListItemByIdQueryHandler> _logger;

    public GetBucketListItemByIdQueryHandler(
        IBucketListManagerContext context,
        ILogger<GetBucketListItemByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BucketListItemDto?> Handle(GetBucketListItemByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting bucket list item {BucketListItemId}", request.BucketListItemId);

        var item = await _context.BucketListItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BucketListItemId == request.BucketListItemId, cancellationToken);

        return item?.ToDto();
    }
}
