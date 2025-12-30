using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Itineraries;

public record GetItinerariesQuery : IRequest<IEnumerable<ItineraryDto>>
{
    public Guid? TripId { get; init; }
}

public class GetItinerariesQueryHandler : IRequestHandler<GetItinerariesQuery, IEnumerable<ItineraryDto>>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<GetItinerariesQueryHandler> _logger;

    public GetItinerariesQueryHandler(
        IFamilyVacationPlannerContext context,
        ILogger<GetItinerariesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ItineraryDto>> Handle(GetItinerariesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting itineraries for trip {TripId}", request.TripId);

        var query = _context.Itineraries.AsNoTracking();

        if (request.TripId.HasValue)
        {
            query = query.Where(i => i.TripId == request.TripId.Value);
        }

        var itineraries = await query
            .OrderBy(i => i.Date)
            .ToListAsync(cancellationToken);

        return itineraries.Select(i => i.ToDto());
    }
}
