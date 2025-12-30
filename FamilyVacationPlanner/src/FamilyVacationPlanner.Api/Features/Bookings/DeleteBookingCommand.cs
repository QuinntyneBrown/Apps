using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Bookings;

public record DeleteBookingCommand : IRequest<bool>
{
    public Guid BookingId { get; init; }
}

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, bool>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<DeleteBookingCommandHandler> _logger;

    public DeleteBookingCommandHandler(
        IFamilyVacationPlannerContext context,
        ILogger<DeleteBookingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting booking {BookingId}", request.BookingId);

        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.BookingId == request.BookingId, cancellationToken);

        if (booking == null)
        {
            _logger.LogWarning("Booking {BookingId} not found", request.BookingId);
            return false;
        }

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted booking {BookingId}", request.BookingId);

        return true;
    }
}
