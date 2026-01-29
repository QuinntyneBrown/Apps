using Goals.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Goals.Api.Features;

public record UpdateGoalCommand(
    Guid GoalId,
    decimal DailyGoalMl,
    DateTime StartDate,
    bool IsActive,
    string? Notes) : IRequest<GoalDto?>;

public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand, GoalDto?>
{
    private readonly IGoalsDbContext _context;
    private readonly ILogger<UpdateGoalCommandHandler> _logger;

    public UpdateGoalCommandHandler(IGoalsDbContext context, ILogger<UpdateGoalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GoalDto?> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);

        if (goal == null) return null;

        goal.DailyGoalMl = request.DailyGoalMl;
        goal.StartDate = request.StartDate;
        goal.IsActive = request.IsActive;
        goal.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Goal updated: {GoalId}", goal.GoalId);

        return goal.ToDto();
    }
}
