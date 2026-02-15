using Trips.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Trips.Api.Features;

public record GetTripsQuery : IRequest<IEnumerable<TripDto>>;

public class GetTripsQueryHandler : IRequestHandler<GetTripsQuery, IEnumerable<TripDto>>
{
    private readonly ITripsDbContext _context;

    public GetTripsQueryHandler(ITripsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TripDto>> Handle(GetTripsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Trips
            .Select(t => new TripDto
            {
                TripId = t.TripId,
                UserId = t.UserId,
                Name = t.Name,
                Destination = t.Destination,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
