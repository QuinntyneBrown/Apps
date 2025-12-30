using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Goals;

public record GetGoalsQuery : IRequest<IEnumerable<GoalDto>>
{
    public Guid? UserId { get; init; }
    public Guid? ReviewPeriodId { get; init; }
    public GoalStatus? Status { get; init; }
}

public class GetGoalsQueryHandler : IRequestHandler<GetGoalsQuery, IEnumerable<GoalDto>>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<GetGoalsQueryHandler> _logger;

    public GetGoalsQueryHandler(
        IPerformanceReviewPrepToolContext context,
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

        if (request.ReviewPeriodId.HasValue)
        {
            query = query.Where(g => g.ReviewPeriodId == request.ReviewPeriodId.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(g => g.Status == request.Status.Value);
        }

        var goals = await query
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync(cancellationToken);

        return goals.Select(g => g.ToDto());
    }
}
