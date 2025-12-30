using BucketListManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.BucketListItems;

public record GetBucketListItemsQuery : IRequest<IEnumerable<BucketListItemDto>>
{
    public Guid? UserId { get; init; }
    public Category? Category { get; init; }
    public ItemStatus? Status { get; init; }
}

public class GetBucketListItemsQueryHandler : IRequestHandler<GetBucketListItemsQuery, IEnumerable<BucketListItemDto>>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<GetBucketListItemsQueryHandler> _logger;

    public GetBucketListItemsQueryHandler(
        IBucketListManagerContext context,
        ILogger<GetBucketListItemsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<BucketListItemDto>> Handle(GetBucketListItemsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting bucket list items for user {UserId}", request.UserId);

        var query = _context.BucketListItems.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(x => x.UserId == request.UserId.Value);
        }

        if (request.Category.HasValue)
        {
            query = query.Where(x => x.Category == request.Category.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(x => x.Status == request.Status.Value);
        }

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return items.Select(x => x.ToDto());
    }
}
