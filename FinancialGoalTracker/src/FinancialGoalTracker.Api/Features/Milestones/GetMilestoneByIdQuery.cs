using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Milestones;

public record GetMilestoneByIdQuery : IRequest<MilestoneDto?>
{
    public Guid MilestoneId { get; init; }
}

public class GetMilestoneByIdQueryHandler : IRequestHandler<GetMilestoneByIdQuery, MilestoneDto?>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<GetMilestoneByIdQueryHandler> _logger;

    public GetMilestoneByIdQueryHandler(
        IFinancialGoalTrackerContext context,
        ILogger<GetMilestoneByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MilestoneDto?> Handle(GetMilestoneByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting milestone {MilestoneId}", request.MilestoneId);

        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(m => m.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null)
        {
            _logger.LogWarning("Milestone {MilestoneId} not found", request.MilestoneId);
            return null;
        }

        return milestone.ToDto();
    }
}
