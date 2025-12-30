using BucketListManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.Milestones;

public record CreateMilestoneCommand : IRequest<MilestoneDto>
{
    public Guid UserId { get; init; }
    public Guid BucketListItemId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
}

public class CreateMilestoneCommandHandler : IRequestHandler<CreateMilestoneCommand, MilestoneDto>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<CreateMilestoneCommandHandler> _logger;

    public CreateMilestoneCommandHandler(
        IBucketListManagerContext context,
        ILogger<CreateMilestoneCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MilestoneDto> Handle(CreateMilestoneCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating milestone for bucket list item {BucketListItemId}, title: {Title}",
            request.BucketListItemId,
            request.Title);

        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            UserId = request.UserId,
            BucketListItemId = request.BucketListItemId,
            Title = request.Title,
            Description = request.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Milestones.Add(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created milestone {MilestoneId} for bucket list item {BucketListItemId}",
            milestone.MilestoneId,
            request.BucketListItemId);

        return milestone.ToDto();
    }
}
