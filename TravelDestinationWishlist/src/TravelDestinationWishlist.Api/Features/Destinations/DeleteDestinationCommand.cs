using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Destinations;

public record DeleteDestinationCommand : IRequest<bool>
{
    public Guid DestinationId { get; init; }
}

public class DeleteDestinationCommandHandler : IRequestHandler<DeleteDestinationCommand, bool>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<DeleteDestinationCommandHandler> _logger;

    public DeleteDestinationCommandHandler(
        ITravelDestinationWishlistContext context,
        ILogger<DeleteDestinationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteDestinationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting destination {DestinationId}", request.DestinationId);

        var destination = await _context.Destinations
            .FirstOrDefaultAsync(d => d.DestinationId == request.DestinationId, cancellationToken);

        if (destination == null)
        {
            _logger.LogWarning("Destination {DestinationId} not found", request.DestinationId);
            return false;
        }

        _context.Destinations.Remove(destination);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted destination {DestinationId}", request.DestinationId);

        return true;
    }
}
