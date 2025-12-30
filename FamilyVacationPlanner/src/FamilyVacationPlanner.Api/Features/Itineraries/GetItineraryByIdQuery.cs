using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Itineraries;

public record GetItineraryByIdQuery : IRequest<ItineraryDto?>
{
    public Guid ItineraryId { get; init; }
}

public class GetItineraryByIdQueryHandler : IRequestHandler<GetItineraryByIdQuery, ItineraryDto?>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<GetItineraryByIdQueryHandler> _logger;

    public GetItineraryByIdQueryHandler(
        IFamilyVacationPlannerContext context,
        ILogger<GetItineraryByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ItineraryDto?> Handle(GetItineraryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting itinerary {ItineraryId}", request.ItineraryId);

        var itinerary = await _context.Itineraries
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.ItineraryId == request.ItineraryId, cancellationToken);

        if (itinerary == null)
        {
            _logger.LogWarning("Itinerary {ItineraryId} not found", request.ItineraryId);
            return null;
        }

        return itinerary.ToDto();
    }
}
