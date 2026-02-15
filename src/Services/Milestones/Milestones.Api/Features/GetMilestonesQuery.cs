using Milestones.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Milestones.Api.Features;

public record GetMilestonesQuery : IRequest<IEnumerable<MilestoneDto>>
{
    public Guid? GoalId { get; init; }
}

public class GetMilestonesQueryHandler : IRequestHandler<GetMilestonesQuery, IEnumerable<MilestoneDto>>
{
    private readonly IMilestonesDbContext _context;

    public GetMilestonesQueryHandler(IMilestonesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MilestoneDto>> Handle(GetMilestonesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Milestones.AsNoTracking();

        if (request.GoalId.HasValue)
        {
            query = query.Where(m => m.GoalId == request.GoalId.Value);
        }

        return await query
            .OrderBy(m => m.TargetAmount)
            .Select(m => new MilestoneDto
            {
                MilestoneId = m.MilestoneId,
                GoalId = m.GoalId,
                Name = m.Name,
                TargetAmount = m.TargetAmount,
                IsReached = m.IsReached,
                ReachedAt = m.ReachedAt,
                CreatedAt = m.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
