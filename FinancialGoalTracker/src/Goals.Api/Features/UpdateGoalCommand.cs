using Goals.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Goals.Api.Features;

public record UpdateGoalCommand : IRequest<GoalDto?>
{
    public Guid GoalId { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal? TargetAmount { get; init; }
    public DateTime? TargetDate { get; init; }
}

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

        goal.Update(request.Name, request.Description, request.TargetAmount, request.TargetDate);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Goal updated: {GoalId}", goal.GoalId);

        return new GoalDto
        {
            GoalId = goal.GoalId,
            Name = goal.Name,
            Description = goal.Description,
            TargetAmount = goal.TargetAmount,
            CurrentAmount = goal.CurrentAmount,
            TargetDate = goal.TargetDate,
            Status = goal.Status.ToString(),
            CreatedAt = goal.CreatedAt
        };
    }
}
