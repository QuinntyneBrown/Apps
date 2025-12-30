using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Goals;

public record GetGoalsQuery : IRequest<IEnumerable<GoalDto>>
{
    public GoalType? GoalType { get; init; }
    public GoalStatus? Status { get; init; }
}

public class GetGoalsQueryHandler : IRequestHandler<GetGoalsQuery, IEnumerable<GoalDto>>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<GetGoalsQueryHandler> _logger;

    public GetGoalsQueryHandler(
        IFinancialGoalTrackerContext context,
        ILogger<GetGoalsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<GoalDto>> Handle(GetGoalsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting goals with filters - Type: {GoalType}, Status: {Status}",
            request.GoalType,
            request.Status);

        var query = _context.Goals.AsQueryable();

        if (request.GoalType.HasValue)
        {
            query = query.Where(g => g.GoalType == request.GoalType.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(g => g.Status == request.Status.Value);
        }

        var goals = await query.ToListAsync(cancellationToken);

        _logger.LogInformation("Found {Count} goals", goals.Count);

        return goals.Select(g => g.ToDto());
    }
}
