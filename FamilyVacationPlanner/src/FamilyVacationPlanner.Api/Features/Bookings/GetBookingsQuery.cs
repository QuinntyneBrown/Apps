using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Bookings;

public record GetBookingsQuery : IRequest<IEnumerable<BookingDto>>
{
    public Guid? TripId { get; init; }
}

public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, IEnumerable<BookingDto>>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<GetBookingsQueryHandler> _logger;

    public GetBookingsQueryHandler(
        IFamilyVacationPlannerContext context,
        ILogger<GetBookingsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<BookingDto>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting bookings for trip {TripId}", request.TripId);

        var query = _context.Bookings.AsNoTracking();

        if (request.TripId.HasValue)
        {
            query = query.Where(b => b.TripId == request.TripId.Value);
        }

        var bookings = await query
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync(cancellationToken);

        return bookings.Select(b => b.ToDto());
    }
}
