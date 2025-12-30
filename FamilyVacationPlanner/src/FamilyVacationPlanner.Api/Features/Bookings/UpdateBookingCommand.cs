using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Bookings;

public record UpdateBookingCommand : IRequest<BookingDto?>
{
    public Guid BookingId { get; init; }
    public string Type { get; init; } = string.Empty;
    public string? ConfirmationNumber { get; init; }
    public decimal? Cost { get; init; }
}

public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, BookingDto?>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<UpdateBookingCommandHandler> _logger;

    public UpdateBookingCommandHandler(
        IFamilyVacationPlannerContext context,
        ILogger<UpdateBookingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BookingDto?> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating booking {BookingId}", request.BookingId);

        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.BookingId == request.BookingId, cancellationToken);

        if (booking == null)
        {
            _logger.LogWarning("Booking {BookingId} not found", request.BookingId);
            return null;
        }

        booking.Type = request.Type;
        booking.ConfirmationNumber = request.ConfirmationNumber;
        booking.Cost = request.Cost;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated booking {BookingId}", request.BookingId);

        return booking.ToDto();
    }
}
