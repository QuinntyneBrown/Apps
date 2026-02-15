using Trips.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Trips.Api.Features;

public record UpdateTripCommand : IRequest<TripDto?>
{
    public Guid TripId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Destination { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, TripDto?>
{
    private readonly ITripsDbContext _context;

    public UpdateTripCommandHandler(ITripsDbContext context)
    {
        _context = context;
    }

    public async Task<TripDto?> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null) return null;

        trip.Name = request.Name;
        trip.Destination = request.Destination;
        trip.StartDate = request.StartDate;
        trip.EndDate = request.EndDate;

        await _context.SaveChangesAsync(cancellationToken);

        return new TripDto
        {
            TripId = trip.TripId,
            UserId = trip.UserId,
            Name = trip.Name,
            Destination = trip.Destination,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            CreatedAt = trip.CreatedAt
        };
    }
}
