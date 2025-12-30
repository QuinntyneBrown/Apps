using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Goals;

public record UpdateGoalCommand : IRequest<GoalDto?>
{
    public Guid GoalId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public GoalType GoalType { get; init; }
    public decimal TargetAmount { get; init; }
    public DateTime TargetDate { get; init; }
    public GoalStatus Status { get; init; }
    public string? Notes { get; init; }
}

public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand, GoalDto?>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<UpdateGoalCommandHandler> _logger;

    public UpdateGoalCommandHandler(
        IFinancialGoalTrackerContext context,
        ILogger<UpdateGoalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GoalDto?> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating goal {GoalId}", request.GoalId);

        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);

        if (goal == null)
        {
            _logger.LogWarning("Goal {GoalId} not found", request.GoalId);
            return null;
        }

        goal.Name = request.Name;
        goal.Description = request.Description;
        goal.GoalType = request.GoalType;
        goal.TargetAmount = request.TargetAmount;
        goal.TargetDate = request.TargetDate;
        goal.Status = request.Status;
        goal.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated goal {GoalId}", request.GoalId);

        return goal.ToDto();
    }
}
