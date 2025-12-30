using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Trips;

public record UpdateTripCommand : IRequest<TripDto?>
{
    public Guid TripId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal? TotalCost { get; init; }
    public string? Accommodation { get; init; }
    public string? Transportation { get; init; }
    public string? Notes { get; init; }
}

public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, TripDto?>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<UpdateTripCommandHandler> _logger;

    public UpdateTripCommandHandler(
        ITravelDestinationWishlistContext context,
        ILogger<UpdateTripCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TripDto?> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating trip {TripId}", request.TripId);

        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null)
        {
            _logger.LogWarning("Trip {TripId} not found", request.TripId);
            return null;
        }

        trip.StartDate = request.StartDate;
        trip.EndDate = request.EndDate;
        trip.TotalCost = request.TotalCost;
        trip.Accommodation = request.Accommodation;
        trip.Transportation = request.Transportation;
        trip.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated trip {TripId}", request.TripId);

        return trip.ToDto();
    }
}
