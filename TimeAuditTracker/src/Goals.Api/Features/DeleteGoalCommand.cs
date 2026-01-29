using Goals.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Goals.Api.Features;

public record DeleteGoalCommand(Guid GoalId) : IRequest<bool>;

public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand, bool>
{
    private readonly IGoalsDbContext _context;

    public DeleteGoalCommandHandler(IGoalsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals.FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);
        if (goal == null) return false;

        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
