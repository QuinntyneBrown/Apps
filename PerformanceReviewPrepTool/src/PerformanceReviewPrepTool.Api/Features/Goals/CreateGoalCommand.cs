using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Goals;

public record CreateGoalCommand : IRequest<GoalDto>
{
    public Guid UserId { get; init; }
    public Guid ReviewPeriodId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public GoalStatus Status { get; init; }
    public DateTime? TargetDate { get; init; }
    public int ProgressPercentage { get; init; }
    public string? SuccessMetrics { get; init; }
    public string? Notes { get; init; }
}

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, GoalDto>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<CreateGoalCommandHandler> _logger;

    public CreateGoalCommandHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<CreateGoalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GoalDto> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating goal for user {UserId}: {Title}",
            request.UserId,
            request.Title);

        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = request.UserId,
            ReviewPeriodId = request.ReviewPeriodId,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            TargetDate = request.TargetDate,
            ProgressPercentage = request.ProgressPercentage,
            SuccessMetrics = request.SuccessMetrics,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created goal {GoalId} for user {UserId}",
            goal.GoalId,
            request.UserId);

        return goal.ToDto();
    }
}
