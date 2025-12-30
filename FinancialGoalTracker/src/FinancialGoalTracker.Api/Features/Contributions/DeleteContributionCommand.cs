using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Contributions;

public record DeleteContributionCommand : IRequest<bool>
{
    public Guid ContributionId { get; init; }
}

public class DeleteContributionCommandHandler : IRequestHandler<DeleteContributionCommand, bool>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<DeleteContributionCommandHandler> _logger;

    public DeleteContributionCommandHandler(
        IFinancialGoalTrackerContext context,
        ILogger<DeleteContributionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteContributionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting contribution {ContributionId}", request.ContributionId);

        var contribution = await _context.Contributions
            .FirstOrDefaultAsync(c => c.ContributionId == request.ContributionId, cancellationToken);

        if (contribution == null)
        {
            _logger.LogWarning("Contribution {ContributionId} not found", request.ContributionId);
            return false;
        }

        // Update the goal's current amount
        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.GoalId == contribution.GoalId, cancellationToken);

        if (goal != null)
        {
            goal.UpdateProgress(-contribution.Amount);
        }

        _context.Contributions.Remove(contribution);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted contribution {ContributionId}", request.ContributionId);

        return true;
    }
}
