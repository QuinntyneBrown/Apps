using Milestones.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Milestones.Api.Features;

public record DeleteMilestoneCommand : IRequest<bool>
{
    public Guid MilestoneId { get; init; }
}

public class DeleteMilestoneCommandHandler : IRequestHandler<DeleteMilestoneCommand, bool>
{
    private readonly IMilestonesDbContext _context;
    private readonly ILogger<DeleteMilestoneCommandHandler> _logger;

    public DeleteMilestoneCommandHandler(IMilestonesDbContext context, ILogger<DeleteMilestoneCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMilestoneCommand request, CancellationToken cancellationToken)
    {
        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(m => m.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null) return false;

        _context.Milestones.Remove(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Milestone deleted: {MilestoneId}", request.MilestoneId);

        return true;
    }
}
