using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Goals;

public record UpdateGoalCommand : IRequest<GoalDto>
{
    public Guid GoalId { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public GoalStatus? Status { get; init; }
    public DateTime? TargetDate { get; init; }
    public Guid? MissionStatementId { get; init; }
}

public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand, GoalDto>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<UpdateGoalCommandHandler> _logger;

    public UpdateGoalCommandHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<UpdateGoalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GoalDto> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating goal {GoalId}",
            request.GoalId);

        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);

        if (goal == null)
        {
            throw new InvalidOperationException($"Goal with ID {request.GoalId} not found");
        }

        if (!string.IsNullOrEmpty(request.Title))
        {
            goal.Title = request.Title;
            goal.UpdatedAt = DateTime.UtcNow;
        }

        if (request.Description != null)
        {
            goal.Description = request.Description;
            goal.UpdatedAt = DateTime.UtcNow;
        }

        if (request.Status.HasValue)
        {
            if (request.Status.Value == GoalStatus.Completed)
            {
                goal.MarkAsCompleted();
            }
            else
            {
                goal.UpdateStatus(request.Status.Value);
            }
        }

        if (request.TargetDate.HasValue)
        {
            goal.TargetDate = request.TargetDate.Value;
            goal.UpdatedAt = DateTime.UtcNow;
        }

        if (request.MissionStatementId.HasValue)
        {
            goal.MissionStatementId = request.MissionStatementId.Value;
            goal.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated goal {GoalId}",
            request.GoalId);

        return goal.ToDto();
    }
}
