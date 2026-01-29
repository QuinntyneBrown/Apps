using Goals.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Goals.Api.Features;

public record UpdateGoalCommand(Guid GoalId, string? Category, int? TargetMinutesPerDay, bool? IsActive, string? Notes) : IRequest<GoalDto?>;

public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand, GoalDto?>
{
    private readonly IGoalsDbContext _context;

    public UpdateGoalCommandHandler(IGoalsDbContext context)
    {
        _context = context;
    }

    public async Task<GoalDto?> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals.FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);
        if (goal == null) return null;

        if (request.Category != null) goal.Category = request.Category;
        if (request.TargetMinutesPerDay.HasValue) goal.TargetMinutesPerDay = request.TargetMinutesPerDay.Value;
        if (request.IsActive.HasValue) goal.IsActive = request.IsActive.Value;
        if (request.Notes != null) goal.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);
        return goal.ToDto();
    }
}
