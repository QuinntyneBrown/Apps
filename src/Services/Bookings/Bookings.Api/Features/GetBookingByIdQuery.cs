using Bookings.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Api.Features;

public record GetBookingByIdQuery(Guid BookingId) : IRequest<BookingDto?>;

public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDto?>
{
    private readonly IBookingsDbContext _context;

    public GetBookingByIdQueryHandler(IBookingsDbContext context)
    {
        _context = context;
    }

    public async Task<BookingDto?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.BookingId == request.BookingId, cancellationToken);

        if (booking == null) return null;

        return new BookingDto
        {
            BookingId = booking.BookingId,
            TripId = booking.TripId,
            Type = booking.Type,
            ConfirmationNumber = booking.ConfirmationNumber,
            Cost = booking.Cost,
            CreatedAt = booking.CreatedAt
        };
    }
}
