using BucketListManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.Memories;

public record GetMemoriesQuery : IRequest<IEnumerable<MemoryDto>>
{
    public Guid? UserId { get; init; }
    public Guid? BucketListItemId { get; init; }
}

public class GetMemoriesQueryHandler : IRequestHandler<GetMemoriesQuery, IEnumerable<MemoryDto>>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<GetMemoriesQueryHandler> _logger;

    public GetMemoriesQueryHandler(
        IBucketListManagerContext context,
        ILogger<GetMemoriesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MemoryDto>> Handle(GetMemoriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting memories for user {UserId}", request.UserId);

        var query = _context.Memories.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(x => x.UserId == request.UserId.Value);
        }

        if (request.BucketListItemId.HasValue)
        {
            query = query.Where(x => x.BucketListItemId == request.BucketListItemId.Value);
        }

        var memories = await query
            .OrderByDescending(x => x.MemoryDate)
            .ToListAsync(cancellationToken);

        return memories.Select(x => x.ToDto());
    }
}
