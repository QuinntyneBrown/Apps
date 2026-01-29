using Trips.Core;
using Trips.Core.Models;
using MediatR;

namespace Trips.Api.Features;

public record CreateTripCommand : IRequest<TripDto>
{
    public Guid UserId { get; init; }
    public Guid TenantId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Destination { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, TripDto>
{
    private readonly ITripsDbContext _context;

    public CreateTripCommandHandler(ITripsDbContext context)
    {
        _context = context;
    }

    public async Task<TripDto> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Name = request.Name,
            Destination = request.Destination,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CreatedAt = DateTime.UtcNow
        };

        _context.Trips.Add(trip);
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
