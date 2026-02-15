using Bookings.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Api.Features;

public record GetBookingsQuery : IRequest<IEnumerable<BookingDto>>;

public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, IEnumerable<BookingDto>>
{
    private readonly IBookingsDbContext _context;

    public GetBookingsQueryHandler(IBookingsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookingDto>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Bookings
            .Select(b => new BookingDto
            {
                BookingId = b.BookingId,
                TripId = b.TripId,
                Type = b.Type,
                ConfirmationNumber = b.ConfirmationNumber,
                Cost = b.Cost,
                CreatedAt = b.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
