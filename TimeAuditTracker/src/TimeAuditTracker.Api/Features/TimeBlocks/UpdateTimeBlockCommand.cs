using TimeAuditTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.TimeBlocks;

public record UpdateTimeBlockCommand : IRequest<TimeBlockDto?>
{
    public Guid TimeBlockId { get; init; }
    public ActivityCategory Category { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public string? Notes { get; init; }
    public string? Tags { get; init; }
    public bool IsProductive { get; init; }
}

public class UpdateTimeBlockCommandHandler : IRequestHandler<UpdateTimeBlockCommand, TimeBlockDto?>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<UpdateTimeBlockCommandHandler> _logger;

    public UpdateTimeBlockCommandHandler(
        ITimeAuditTrackerContext context,
        ILogger<UpdateTimeBlockCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TimeBlockDto?> Handle(UpdateTimeBlockCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating time block {TimeBlockId}", request.TimeBlockId);

        var timeBlock = await _context.TimeBlocks
            .FirstOrDefaultAsync(t => t.TimeBlockId == request.TimeBlockId, cancellationToken);

        if (timeBlock == null)
        {
            _logger.LogWarning("Time block {TimeBlockId} not found", request.TimeBlockId);
            return null;
        }

        timeBlock.Category = request.Category;
        timeBlock.Description = request.Description;
        timeBlock.StartTime = request.StartTime;
        timeBlock.EndTime = request.EndTime;
        timeBlock.Notes = request.Notes;
        timeBlock.Tags = request.Tags;
        timeBlock.IsProductive = request.IsProductive;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated time block {TimeBlockId}", request.TimeBlockId);

        return timeBlock.ToDto();
    }
}
