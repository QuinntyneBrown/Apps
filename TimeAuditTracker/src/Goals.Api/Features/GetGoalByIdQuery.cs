using Goals.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Goals.Api.Features;

public record GetGoalByIdQuery(Guid GoalId) : IRequest<GoalDto?>;

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
            .FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);
        return goal?.ToDto();
    }
}
