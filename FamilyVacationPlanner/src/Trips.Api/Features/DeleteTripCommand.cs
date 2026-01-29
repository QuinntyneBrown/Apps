using Trips.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Trips.Api.Features;

public record DeleteTripCommand(Guid TripId) : IRequest<bool>;

public class DeleteTripCommandHandler : IRequestHandler<DeleteTripCommand, bool>
{
    private readonly ITripsDbContext _context;

    public DeleteTripCommandHandler(ITripsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null) return false;

        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
