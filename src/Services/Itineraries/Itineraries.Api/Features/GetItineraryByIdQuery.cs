using Itineraries.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itineraries.Api.Features;

public record GetItineraryByIdQuery(Guid ItineraryId) : IRequest<ItineraryDto?>;

public class GetItineraryByIdQueryHandler : IRequestHandler<GetItineraryByIdQuery, ItineraryDto?>
{
    private readonly IItinerariesDbContext _context;

    public GetItineraryByIdQueryHandler(IItinerariesDbContext context)
    {
        _context = context;
    }

    public async Task<ItineraryDto?> Handle(GetItineraryByIdQuery request, CancellationToken cancellationToken)
    {
        var itinerary = await _context.Itineraries
            .FirstOrDefaultAsync(i => i.ItineraryId == request.ItineraryId, cancellationToken);

        if (itinerary == null) return null;

        return new ItineraryDto
        {
            ItineraryId = itinerary.ItineraryId,
            TripId = itinerary.TripId,
            Date = itinerary.Date,
            Activity = itinerary.Activity,
            Location = itinerary.Location,
            CreatedAt = itinerary.CreatedAt
        };
    }
}
