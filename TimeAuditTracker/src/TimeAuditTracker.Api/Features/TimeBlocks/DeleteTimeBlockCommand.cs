using TimeAuditTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.TimeBlocks;

public record DeleteTimeBlockCommand : IRequest<bool>
{
    public Guid TimeBlockId { get; init; }
}

public class DeleteTimeBlockCommandHandler : IRequestHandler<DeleteTimeBlockCommand, bool>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<DeleteTimeBlockCommandHandler> _logger;

    public DeleteTimeBlockCommandHandler(
        ITimeAuditTrackerContext context,
        ILogger<DeleteTimeBlockCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTimeBlockCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting time block {TimeBlockId}", request.TimeBlockId);

        var timeBlock = await _context.TimeBlocks
            .FirstOrDefaultAsync(t => t.TimeBlockId == request.TimeBlockId, cancellationToken);

        if (timeBlock == null)
        {
            _logger.LogWarning("Time block {TimeBlockId} not found", request.TimeBlockId);
            return false;
        }

        _context.TimeBlocks.Remove(timeBlock);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted time block {TimeBlockId}", request.TimeBlockId);

        return true;
    }
}
