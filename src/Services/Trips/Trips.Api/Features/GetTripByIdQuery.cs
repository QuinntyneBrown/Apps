using Trips.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Trips.Api.Features;

public record GetTripByIdQuery(Guid TripId) : IRequest<TripDto?>;

public class GetTripByIdQueryHandler : IRequestHandler<GetTripByIdQuery, TripDto?>
{
    private readonly ITripsDbContext _context;

    public GetTripByIdQueryHandler(ITripsDbContext context)
    {
        _context = context;
    }

    public async Task<TripDto?> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null) return null;

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
