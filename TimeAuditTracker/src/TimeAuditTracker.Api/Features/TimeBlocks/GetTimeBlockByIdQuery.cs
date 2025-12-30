using TimeAuditTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.TimeBlocks;

public record GetTimeBlockByIdQuery : IRequest<TimeBlockDto?>
{
    public Guid TimeBlockId { get; init; }
}

public class GetTimeBlockByIdQueryHandler : IRequestHandler<GetTimeBlockByIdQuery, TimeBlockDto?>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<GetTimeBlockByIdQueryHandler> _logger;

    public GetTimeBlockByIdQueryHandler(
        ITimeAuditTrackerContext context,
        ILogger<GetTimeBlockByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TimeBlockDto?> Handle(GetTimeBlockByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting time block {TimeBlockId}", request.TimeBlockId);

        var timeBlock = await _context.TimeBlocks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TimeBlockId == request.TimeBlockId, cancellationToken);

        if (timeBlock == null)
        {
            _logger.LogWarning("Time block {TimeBlockId} not found", request.TimeBlockId);
            return null;
        }

        return timeBlock.ToDto();
    }
}
