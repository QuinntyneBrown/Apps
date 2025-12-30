using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Goals;

public record CreateGoalCommand : IRequest<GoalDto>
{
    public Guid UserId { get; init; }
    public Guid? MissionStatementId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public GoalStatus Status { get; init; } = GoalStatus.NotStarted;
    public DateTime? TargetDate { get; init; }
}

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, GoalDto>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<CreateGoalCommandHandler> _logger;

    public CreateGoalCommandHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<CreateGoalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GoalDto> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating goal for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = request.UserId,
            MissionStatementId = request.MissionStatementId,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            TargetDate = request.TargetDate,
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
