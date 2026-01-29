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
            .Select(g => g.ToDto())
            .ToListAsync(cancellationToken);
    }
}
