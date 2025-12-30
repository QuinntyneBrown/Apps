using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Trips;

public record GetTripsQuery : IRequest<IEnumerable<TripDto>>
{
    public Guid? UserId { get; init; }
}

public class GetTripsQueryHandler : IRequestHandler<GetTripsQuery, IEnumerable<TripDto>>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<GetTripsQueryHandler> _logger;

    public GetTripsQueryHandler(
        IFamilyVacationPlannerContext context,
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

        var trips = await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return trips.Select(t => t.ToDto());
    }
}
