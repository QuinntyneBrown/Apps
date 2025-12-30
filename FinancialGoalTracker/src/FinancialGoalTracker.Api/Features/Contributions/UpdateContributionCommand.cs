using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Contributions;

public record UpdateContributionCommand : IRequest<ContributionDto?>
{
    public Guid ContributionId { get; init; }
    public decimal Amount { get; init; }
    public DateTime ContributionDate { get; init; }
    public string? Notes { get; init; }
}

public class UpdateContributionCommandHandler : IRequestHandler<UpdateContributionCommand, ContributionDto?>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<UpdateContributionCommandHandler> _logger;

    public UpdateContributionCommandHandler(
        IFinancialGoalTrackerContext context,
        ILogger<UpdateContributionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContributionDto?> Handle(UpdateContributionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating contribution {ContributionId}", request.ContributionId);

        var contribution = await _context.Contributions
            .FirstOrDefaultAsync(c => c.ContributionId == request.ContributionId, cancellationToken);

        if (contribution == null)
        {
            _logger.LogWarning("Contribution {ContributionId} not found", request.ContributionId);
            return null;
        }

        var oldAmount = contribution.Amount;
        var amountDifference = request.Amount - oldAmount;

        contribution.Amount = request.Amount;
        contribution.ContributionDate = request.ContributionDate;
        contribution.Notes = request.Notes;

        // Update the goal's current amount
        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.GoalId == contribution.GoalId, cancellationToken);

        if (goal != null && amountDifference != 0)
        {
            goal.UpdateProgress(amountDifference);
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated contribution {ContributionId}", request.ContributionId);

        return contribution.ToDto();
    }
}
