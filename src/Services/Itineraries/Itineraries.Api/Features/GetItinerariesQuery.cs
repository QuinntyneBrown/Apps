using Itineraries.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itineraries.Api.Features;

public record GetItinerariesQuery : IRequest<IEnumerable<ItineraryDto>>;

public class GetItinerariesQueryHandler : IRequestHandler<GetItinerariesQuery, IEnumerable<ItineraryDto>>
{
    private readonly IItinerariesDbContext _context;

    public GetItinerariesQueryHandler(IItinerariesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ItineraryDto>> Handle(GetItinerariesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Itineraries
            .Select(i => new ItineraryDto
            {
                ItineraryId = i.ItineraryId,
                TripId = i.TripId,
                Date = i.Date,
                Activity = i.Activity,
                Location = i.Location,
                CreatedAt = i.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
