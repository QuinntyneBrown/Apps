using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Goals;

public record DeleteGoalCommand : IRequest<bool>
{
    public Guid GoalId { get; init; }
}

public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand, bool>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<DeleteGoalCommandHandler> _logger;

    public DeleteGoalCommandHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<DeleteGoalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting goal {GoalId}",
            request.GoalId);

        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);

        if (goal == null)
        {
            _logger.LogWarning(
                "Goal {GoalId} not found for deletion",
                request.GoalId);
            return false;
        }

        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted goal {GoalId}",
            request.GoalId);

        return true;
    }
}
