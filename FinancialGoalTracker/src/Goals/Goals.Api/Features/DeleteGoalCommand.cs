using Goals.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Goals.Api.Features;

public record DeleteGoalCommand : IRequest<bool>
{
    public Guid GoalId { get; init; }
}

public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand, bool>
{
    private readonly IGoalsDbContext _context;
    private readonly ILogger<DeleteGoalCommandHandler> _logger;

    public DeleteGoalCommandHandler(IGoalsDbContext context, ILogger<DeleteGoalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);

        if (goal == null) return false;

        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Goal deleted: {GoalId}", request.GoalId);

        return true;
    }
}
