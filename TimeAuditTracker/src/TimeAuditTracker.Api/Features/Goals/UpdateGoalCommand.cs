using TimeAuditTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.Goals;

public record UpdateGoalCommand : IRequest<GoalDto?>
{
    public Guid GoalId { get; init; }
    public ActivityCategory Category { get; init; }
    public double TargetHoursPerWeek { get; init; }
    public double? MinimumHoursPerWeek { get; init; }
    public string Description { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public DateTime? EndDate { get; init; }
}

public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand, GoalDto?>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<UpdateGoalCommandHandler> _logger;

    public UpdateGoalCommandHandler(
        ITimeAuditTrackerContext context,
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

        goal.Category = request.Category;
        goal.TargetHoursPerWeek = request.TargetHoursPerWeek;
        goal.MinimumHoursPerWeek = request.MinimumHoursPerWeek;
        goal.Description = request.Description;
        goal.IsActive = request.IsActive;
        goal.EndDate = request.EndDate;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated goal {GoalId}", request.GoalId);

        return goal.ToDto();
    }
}
