using TimeAuditTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.TimeBlocks;

public record GetTimeBlocksQuery : IRequest<IEnumerable<TimeBlockDto>>
{
    public Guid? UserId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public ActivityCategory? Category { get; init; }
    public bool? IsProductive { get; init; }
    public bool? IsActive { get; init; }
}

public class GetTimeBlocksQueryHandler : IRequestHandler<GetTimeBlocksQuery, IEnumerable<TimeBlockDto>>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<GetTimeBlocksQueryHandler> _logger;

    public GetTimeBlocksQueryHandler(
        ITimeAuditTrackerContext context,
        ILogger<GetTimeBlocksQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TimeBlockDto>> Handle(GetTimeBlocksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting time blocks for user {UserId}", request.UserId);

        var query = _context.TimeBlocks.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(t => t.StartTime >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(t => t.StartTime <= request.EndDate.Value);
        }

        if (request.Category.HasValue)
        {
            query = query.Where(t => t.Category == request.Category.Value);
        }

        if (request.IsProductive.HasValue)
        {
            query = query.Where(t => t.IsProductive == request.IsProductive.Value);
        }

        if (request.IsActive.HasValue)
        {
            if (request.IsActive.Value)
            {
                query = query.Where(t => t.EndTime == null);
            }
            else
            {
                query = query.Where(t => t.EndTime != null);
            }
        }

        var timeBlocks = await query
            .OrderByDescending(t => t.StartTime)
            .ToListAsync(cancellationToken);

        return timeBlocks.Select(t => t.ToDto());
    }
}
