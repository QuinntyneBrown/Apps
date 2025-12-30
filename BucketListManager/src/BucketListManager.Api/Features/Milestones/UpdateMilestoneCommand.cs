using BucketListManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.Milestones;

public record UpdateMilestoneCommand : IRequest<MilestoneDto?>
{
    public Guid MilestoneId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? CompletedDate { get; init; }
}

public class UpdateMilestoneCommandHandler : IRequestHandler<UpdateMilestoneCommand, MilestoneDto?>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<UpdateMilestoneCommandHandler> _logger;

    public UpdateMilestoneCommandHandler(
        IBucketListManagerContext context,
        ILogger<UpdateMilestoneCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MilestoneDto?> Handle(UpdateMilestoneCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating milestone {MilestoneId}", request.MilestoneId);

        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(x => x.MilestoneId == request.MilestoneId, cancellationToken);

        if (milestone == null)
        {
            _logger.LogWarning("Milestone {MilestoneId} not found", request.MilestoneId);
            return null;
        }

        milestone.Title = request.Title;
        milestone.Description = request.Description;
        milestone.IsCompleted = request.IsCompleted;
        milestone.CompletedDate = request.CompletedDate;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated milestone {MilestoneId}", request.MilestoneId);

        return milestone.ToDto();
    }
}
