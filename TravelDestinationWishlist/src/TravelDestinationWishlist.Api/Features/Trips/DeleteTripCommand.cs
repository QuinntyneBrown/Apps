using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Trips;

public record DeleteTripCommand : IRequest<bool>
{
    public Guid TripId { get; init; }
}

public class DeleteTripCommandHandler : IRequestHandler<DeleteTripCommand, bool>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<DeleteTripCommandHandler> _logger;

    public DeleteTripCommandHandler(
        ITravelDestinationWishlistContext context,
        ILogger<DeleteTripCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting trip {TripId}", request.TripId);

        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null)
        {
            _logger.LogWarning("Trip {TripId} not found", request.TripId);
            return false;
        }

        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted trip {TripId}", request.TripId);

        return true;
    }
}
