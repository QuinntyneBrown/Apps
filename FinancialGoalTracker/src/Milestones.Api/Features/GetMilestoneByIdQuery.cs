using Milestones.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Milestones.Api.Features;

public record GetMilestoneByIdQuery : IRequest<MilestoneDto?>
{
    public Guid MilestoneId { get; init; }
}

public class GetMilestoneByIdQueryHandler : IRequestHandler<GetMilestoneByIdQuery, MilestoneDto?>
{
    private readonly IMilestonesDbContext _context;

    public GetMilestoneByIdQueryHandler(IMilestonesDbContext context)
    {
        _context = context;
    }

    public async Task<MilestoneDto?> Handle(GetMilestoneByIdQuery request, CancellationToken cancellationToken)
    {
        var milestone = await _context.Milestones
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null) return null;

        return new MilestoneDto
        {
            MilestoneId = milestone.MilestoneId,
            GoalId = milestone.GoalId,
            Name = milestone.Name,
            TargetAmount = milestone.TargetAmount,
            IsReached = milestone.IsReached,
            ReachedAt = milestone.ReachedAt,
            CreatedAt = milestone.CreatedAt
        };
    }
}
