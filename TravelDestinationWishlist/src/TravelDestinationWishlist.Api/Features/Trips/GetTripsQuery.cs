using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Trips;

public record GetTripsQuery : IRequest<IEnumerable<TripDto>>
{
    public Guid? UserId { get; init; }
    public Guid? DestinationId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class GetTripsQueryHandler : IRequestHandler<GetTripsQuery, IEnumerable<TripDto>>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<GetTripsQueryHandler> _logger;

    public GetTripsQueryHandler(
        ITravelDestinationWishlistContext context,
        ILogger<GetTripsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TripDto>> Handle(GetTripsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting trips for user {UserId}", request.UserId);

        var query = _context.Trips.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        if (request.DestinationId.HasValue)
        {
            query = query.Where(t => t.DestinationId == request.DestinationId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(t => t.StartDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(t => t.EndDate <= request.EndDate.Value);
        }

        var trips = await query
            .OrderByDescending(t => t.StartDate)
            .ToListAsync(cancellationToken);

        return trips.Select(t => t.ToDto());
    }
}
