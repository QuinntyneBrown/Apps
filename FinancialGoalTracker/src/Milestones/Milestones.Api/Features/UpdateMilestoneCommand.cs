using Milestones.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Milestones.Api.Features;

public record UpdateMilestoneCommand : IRequest<MilestoneDto?>
{
    public Guid MilestoneId { get; init; }
    public string? Name { get; init; }
    public decimal? TargetAmount { get; init; }
}

public class UpdateMilestoneCommandHandler : IRequestHandler<UpdateMilestoneCommand, MilestoneDto?>
{
    private readonly IMilestonesDbContext _context;
    private readonly ILogger<UpdateMilestoneCommandHandler> _logger;

    public UpdateMilestoneCommandHandler(IMilestonesDbContext context, ILogger<UpdateMilestoneCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MilestoneDto?> Handle(UpdateMilestoneCommand request, CancellationToken cancellationToken)
    {
        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(m => m.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null) return null;

        milestone.Update(request.Name, request.TargetAmount);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Milestone updated: {MilestoneId}", milestone.MilestoneId);

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
