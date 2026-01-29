using Goals.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Goals.Api.Features;

public record GetGoalsQuery : IRequest<IEnumerable<GoalDto>>;

public class GetGoalsQueryHandler : IRequestHandler<GetGoalsQuery, IEnumerable<GoalDto>>
{
    private readonly IGoalsDbContext _context;

    public GetGoalsQueryHandler(IGoalsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GoalDto>> Handle(GetGoalsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Goals
            .AsNoTracking()
            .OrderByDescending(g => g.CreatedAt)
            .Select(g => new GoalDto
            {
                GoalId = g.GoalId,
                Name = g.Name,
                Description = g.Description,
                TargetAmount = g.TargetAmount,
                CurrentAmount = g.CurrentAmount,
                TargetDate = g.TargetDate,
                Status = g.Status.ToString(),
                CreatedAt = g.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
