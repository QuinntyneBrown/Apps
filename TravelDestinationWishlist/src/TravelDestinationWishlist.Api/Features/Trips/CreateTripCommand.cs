using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Trips;

public record CreateTripCommand : IRequest<TripDto>
{
    public Guid UserId { get; init; }
    public Guid DestinationId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal? TotalCost { get; init; }
    public string? Accommodation { get; init; }
    public string? Transportation { get; init; }
    public string? Notes { get; init; }
}

public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, TripDto>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<CreateTripCommandHandler> _logger;

    public CreateTripCommandHandler(
        ITravelDestinationWishlistContext context,
        ILogger<CreateTripCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TripDto> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating trip for user {UserId}, destination: {DestinationId}",
            request.UserId,
            request.DestinationId);

        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = request.UserId,
            DestinationId = request.DestinationId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalCost = request.TotalCost,
            Accommodation = request.Accommodation,
            Transportation = request.Transportation,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Trips.Add(trip);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created trip {TripId} for user {UserId}",
            trip.TripId,
            request.UserId);

        return trip.ToDto();
    }
}
