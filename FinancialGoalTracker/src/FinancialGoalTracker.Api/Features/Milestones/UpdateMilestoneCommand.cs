using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Milestones;

public record UpdateMilestoneCommand : IRequest<MilestoneDto?>
{
    public Guid MilestoneId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal TargetAmount { get; init; }
    public DateTime TargetDate { get; init; }
    public bool IsCompleted { get; init; }
    public string? Notes { get; init; }
}

public class UpdateMilestoneCommandHandler : IRequestHandler<UpdateMilestoneCommand, MilestoneDto?>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<UpdateMilestoneCommandHandler> _logger;

    public UpdateMilestoneCommandHandler(
        IFinancialGoalTrackerContext context,
        ILogger<UpdateMilestoneCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MilestoneDto?> Handle(UpdateMilestoneCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating milestone {MilestoneId}", request.MilestoneId);

        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(m => m.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null)
        {
            _logger.LogWarning("Milestone {MilestoneId} not found", request.MilestoneId);
            return null;
        }

        milestone.Name = request.Name;
        milestone.TargetAmount = request.TargetAmount;
        milestone.TargetDate = request.TargetDate;
        milestone.Notes = request.Notes;

        if (request.IsCompleted && !milestone.IsCompleted)
        {
            milestone.MarkAsCompleted();
        }
        else if (!request.IsCompleted && milestone.IsCompleted)
        {
            milestone.IsCompleted = false;
            milestone.CompletedDate = null;
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated milestone {MilestoneId}", request.MilestoneId);

        return milestone.ToDto();
    }
}
