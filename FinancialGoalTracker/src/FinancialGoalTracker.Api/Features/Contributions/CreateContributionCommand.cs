using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Contributions;

public record CreateContributionCommand : IRequest<ContributionDto>
{
    public Guid GoalId { get; init; }
    public decimal Amount { get; init; }
    public DateTime ContributionDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateContributionCommandHandler : IRequestHandler<CreateContributionCommand, ContributionDto>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<CreateContributionCommandHandler> _logger;

    public CreateContributionCommandHandler(
        IFinancialGoalTrackerContext context,
        ILogger<CreateContributionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContributionDto> Handle(CreateContributionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating contribution for goal {GoalId}, amount: {Amount}",
            request.GoalId,
            request.Amount);

        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            GoalId = request.GoalId,
            Amount = request.Amount,
            ContributionDate = request.ContributionDate,
            Notes = request.Notes,
        };

        _context.Contributions.Add(contribution);

        // Update the goal's current amount
        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);

        if (goal != null)
        {
            goal.UpdateProgress(request.Amount);
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created contribution {ContributionId} for goal {GoalId}",
            contribution.ContributionId,
            request.GoalId);

        return contribution.ToDto();
    }
}
