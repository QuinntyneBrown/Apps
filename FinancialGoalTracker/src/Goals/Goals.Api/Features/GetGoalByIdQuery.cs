using Goals.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Goals.Api.Features;

public record GetGoalByIdQuery : IRequest<GoalDto?>
{
    public Guid GoalId { get; init; }
}

public class GetGoalByIdQueryHandler : IRequestHandler<GetGoalByIdQuery, GoalDto?>
{
    private readonly IGoalsDbContext _context;

    public GetGoalByIdQueryHandler(IGoalsDbContext context)
    {
        _context = context;
    }

    public async Task<GoalDto?> Handle(GetGoalByIdQuery request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);

        if (goal == null) return null;

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
