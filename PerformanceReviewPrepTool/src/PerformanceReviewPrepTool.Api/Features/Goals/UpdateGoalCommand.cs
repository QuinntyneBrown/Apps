using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Goals;

public record UpdateGoalCommand : IRequest<GoalDto?>
{
    public Guid GoalId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public GoalStatus Status { get; init; }
    public DateTime? TargetDate { get; init; }
    public int ProgressPercentage { get; init; }
    public string? SuccessMetrics { get; init; }
    public string? Notes { get; init; }
}

public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand, GoalDto?>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<UpdateGoalCommandHandler> _logger;

    public UpdateGoalCommandHandler(
        IPerformanceReviewPrepToolContext context,
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

        goal.Title = request.Title;
        goal.Description = request.Description;
        goal.TargetDate = request.TargetDate;
        goal.SuccessMetrics = request.SuccessMetrics;
        goal.Notes = request.Notes;

        // Use domain methods for status and progress updates
        if (request.Status != goal.Status)
        {
            goal.UpdateStatus(request.Status);
        }

        if (request.ProgressPercentage != goal.ProgressPercentage)
        {
            goal.UpdateProgress(request.ProgressPercentage);
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated goal {GoalId}", request.GoalId);

        return goal.ToDto();
    }
}
