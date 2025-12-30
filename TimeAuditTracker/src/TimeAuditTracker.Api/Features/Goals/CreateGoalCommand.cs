using TimeAuditTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.Goals;

public record CreateGoalCommand : IRequest<GoalDto>
{
    public Guid UserId { get; init; }
    public ActivityCategory Category { get; init; }
    public double TargetHoursPerWeek { get; init; }
    public double? MinimumHoursPerWeek { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
}

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, GoalDto>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<CreateGoalCommandHandler> _logger;

    public CreateGoalCommandHandler(
        ITimeAuditTrackerContext context,
        ILogger<CreateGoalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GoalDto> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating goal for user {UserId}, category: {Category}",
            request.UserId,
            request.Category);

        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = request.UserId,
            Category = request.Category,
            TargetHoursPerWeek = request.TargetHoursPerWeek,
            MinimumHoursPerWeek = request.MinimumHoursPerWeek,
            Description = request.Description,
            IsActive = true,
            StartDate = request.StartDate,
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
