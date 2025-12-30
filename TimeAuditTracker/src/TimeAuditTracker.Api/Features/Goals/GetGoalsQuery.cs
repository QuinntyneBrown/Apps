using TimeAuditTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.Goals;

public record GetGoalsQuery : IRequest<IEnumerable<GoalDto>>
{
    public Guid? UserId { get; init; }
    public ActivityCategory? Category { get; init; }
    public bool? IsActive { get; init; }
}

public class GetGoalsQueryHandler : IRequestHandler<GetGoalsQuery, IEnumerable<GoalDto>>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<GetGoalsQueryHandler> _logger;

    public GetGoalsQueryHandler(
        ITimeAuditTrackerContext context,
        ILogger<GetGoalsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<GoalDto>> Handle(GetGoalsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting goals for user {UserId}", request.UserId);

        var query = _context.Goals.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(g => g.UserId == request.UserId.Value);
        }

        if (request.Category.HasValue)
        {
            query = query.Where(g => g.Category == request.Category.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(g => g.IsActive == request.IsActive.Value);
        }

        var goals = await query
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync(cancellationToken);

        return goals.Select(g => g.ToDto());
    }
}
