using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Milestones;

public record GetMilestonesQuery : IRequest<IEnumerable<MilestoneDto>>
{
    public Guid? GoalId { get; init; }
    public bool? IsCompleted { get; init; }
}

public class GetMilestonesQueryHandler : IRequestHandler<GetMilestonesQuery, IEnumerable<MilestoneDto>>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<GetMilestonesQueryHandler> _logger;

    public GetMilestonesQueryHandler(
        IFinancialGoalTrackerContext context,
        ILogger<GetMilestonesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MilestoneDto>> Handle(GetMilestonesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting milestones with filters - GoalId: {GoalId}, IsCompleted: {IsCompleted}",
            request.GoalId,
            request.IsCompleted);

        var query = _context.Milestones.AsQueryable();

        if (request.GoalId.HasValue)
        {
            query = query.Where(m => m.GoalId == request.GoalId.Value);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(m => m.IsCompleted == request.IsCompleted.Value);
        }

        var milestones = await query
            .OrderBy(m => m.TargetDate)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Found {Count} milestones", milestones.Count);

        return milestones.Select(m => m.ToDto());
    }
}
