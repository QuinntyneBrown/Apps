using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.Trips;

public record GetTripByIdQuery : IRequest<TripDto?>
{
    public Guid TripId { get; init; }
}

public class GetTripByIdQueryHandler : IRequestHandler<GetTripByIdQuery, TripDto?>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<GetTripByIdQueryHandler> _logger;

    public GetTripByIdQueryHandler(
        ICampingTripPlannerContext context,
        ILogger<GetTripByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TripDto?> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting trip {TripId}", request.TripId);

        var trip = await _context.Trips
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null)
        {
            _logger.LogWarning("Trip {TripId} not found", request.TripId);
            return null;
        }

        return trip.ToDto();
    }
}
