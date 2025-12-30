using BucketListManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.Milestones;

public record DeleteMilestoneCommand : IRequest<bool>
{
    public Guid MilestoneId { get; init; }
}

public class DeleteMilestoneCommandHandler : IRequestHandler<DeleteMilestoneCommand, bool>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<DeleteMilestoneCommandHandler> _logger;

    public DeleteMilestoneCommandHandler(
        IBucketListManagerContext context,
        ILogger<DeleteMilestoneCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMilestoneCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting milestone {MilestoneId}", request.MilestoneId);

        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(x => x.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null)
        {
            _logger.LogWarning("Milestone {MilestoneId} not found", request.MilestoneId);
            return false;
        }

        _context.Milestones.Remove(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted milestone {MilestoneId}", request.MilestoneId);

        return true;
    }
}
