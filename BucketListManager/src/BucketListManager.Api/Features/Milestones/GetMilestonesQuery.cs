using BucketListManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.Milestones;

public record GetMilestonesQuery : IRequest<IEnumerable<MilestoneDto>>
{
    public Guid? UserId { get; init; }
    public Guid? BucketListItemId { get; init; }
    public bool? IsCompleted { get; init; }
}

public class GetMilestonesQueryHandler : IRequestHandler<GetMilestonesQuery, IEnumerable<MilestoneDto>>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<GetMilestonesQueryHandler> _logger;

    public GetMilestonesQueryHandler(
        IBucketListManagerContext context,
        ILogger<GetMilestonesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MilestoneDto>> Handle(GetMilestonesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting milestones for user {UserId}", request.UserId);

        var query = _context.Milestones.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(x => x.UserId == request.UserId.Value);
        }

        if (request.BucketListItemId.HasValue)
        {
            query = query.Where(x => x.BucketListItemId == request.BucketListItemId.Value);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(x => x.IsCompleted == request.IsCompleted.Value);
        }

        var milestones = await query
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return milestones.Select(x => x.ToDto());
    }
}
