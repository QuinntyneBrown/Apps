using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Destinations;

public record MarkDestinationVisitedCommand : IRequest<DestinationDto?>
{
    public Guid DestinationId { get; init; }
    public DateTime? VisitedDate { get; init; }
}

public class MarkDestinationVisitedCommandHandler : IRequestHandler<MarkDestinationVisitedCommand, DestinationDto?>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<MarkDestinationVisitedCommandHandler> _logger;

    public MarkDestinationVisitedCommandHandler(
        ITravelDestinationWishlistContext context,
        ILogger<MarkDestinationVisitedCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DestinationDto?> Handle(MarkDestinationVisitedCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Marking destination {DestinationId} as visited", request.DestinationId);

        var destination = await _context.Destinations
            .FirstOrDefaultAsync(d => d.DestinationId == request.DestinationId, cancellationToken);

        if (destination == null)
        {
            _logger.LogWarning("Destination {DestinationId} not found", request.DestinationId);
            return null;
        }

        destination.IsVisited = true;
        destination.VisitedDate = request.VisitedDate ?? DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Marked destination {DestinationId} as visited", request.DestinationId);

        return destination.ToDto();
    }
}
