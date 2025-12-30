using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Milestones;

public record CreateMilestoneCommand : IRequest<MilestoneDto>
{
    public Guid GoalId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal TargetAmount { get; init; }
    public DateTime TargetDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateMilestoneCommandHandler : IRequestHandler<CreateMilestoneCommand, MilestoneDto>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<CreateMilestoneCommandHandler> _logger;

    public CreateMilestoneCommandHandler(
        IFinancialGoalTrackerContext context,
        ILogger<CreateMilestoneCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MilestoneDto> Handle(CreateMilestoneCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating milestone for goal {GoalId}: {Name}",
            request.GoalId,
            request.Name);

        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            GoalId = request.GoalId,
            Name = request.Name,
            TargetAmount = request.TargetAmount,
            TargetDate = request.TargetDate,
            IsCompleted = false,
            Notes = request.Notes,
        };

        _context.Milestones.Add(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created milestone {MilestoneId} for goal {GoalId}",
            milestone.MilestoneId,
            request.GoalId);

        return milestone.ToDto();
    }
}
