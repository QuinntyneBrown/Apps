using TimeAuditTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.TimeBlocks;

public record CreateTimeBlockCommand : IRequest<TimeBlockDto>
{
    public Guid UserId { get; init; }
    public ActivityCategory Category { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public string? Notes { get; init; }
    public string? Tags { get; init; }
    public bool IsProductive { get; init; }
}

public class CreateTimeBlockCommandHandler : IRequestHandler<CreateTimeBlockCommand, TimeBlockDto>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<CreateTimeBlockCommandHandler> _logger;

    public CreateTimeBlockCommandHandler(
        ITimeAuditTrackerContext context,
        ILogger<CreateTimeBlockCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TimeBlockDto> Handle(CreateTimeBlockCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating time block for user {UserId}, category: {Category}",
            request.UserId,
            request.Category);

        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = request.UserId,
            Category = request.Category,
            Description = request.Description,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Notes = request.Notes,
            Tags = request.Tags,
            IsProductive = request.IsProductive,
            CreatedAt = DateTime.UtcNow,
        };

        _context.TimeBlocks.Add(timeBlock);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created time block {TimeBlockId} for user {UserId}",
            timeBlock.TimeBlockId,
            request.UserId);

        return timeBlock.ToDto();
    }
}
