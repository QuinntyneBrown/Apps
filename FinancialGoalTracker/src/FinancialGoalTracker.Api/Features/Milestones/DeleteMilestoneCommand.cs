using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Milestones;

public record DeleteMilestoneCommand : IRequest<bool>
{
    public Guid MilestoneId { get; init; }
}

public class DeleteMilestoneCommandHandler : IRequestHandler<DeleteMilestoneCommand, bool>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<DeleteMilestoneCommandHandler> _logger;

    public DeleteMilestoneCommandHandler(
        IFinancialGoalTrackerContext context,
        ILogger<DeleteMilestoneCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMilestoneCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting milestone {MilestoneId}", request.MilestoneId);

        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(m => m.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null)
        {
            _logger.LogWarning("Milestone {MilestoneId} not found", request.MilestoneId);
            return false;
        }

        _context.Milestones.Remove(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted milestone {MilestoneId}", request.MilestoneId);

        return true;
    }
}
