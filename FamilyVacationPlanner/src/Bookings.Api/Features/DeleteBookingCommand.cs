using Bookings.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Api.Features;

public record DeleteBookingCommand(Guid BookingId) : IRequest<bool>;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, bool>
{
    private readonly IBookingsDbContext _context;

    public DeleteBookingCommandHandler(IBookingsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.BookingId == request.BookingId, cancellationToken);

        if (booking == null) return false;

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
