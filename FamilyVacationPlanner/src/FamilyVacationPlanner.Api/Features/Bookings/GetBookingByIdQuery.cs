using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Bookings;

public record GetBookingByIdQuery : IRequest<BookingDto?>
{
    public Guid BookingId { get; init; }
}

public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDto?>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<GetBookingByIdQueryHandler> _logger;

    public GetBookingByIdQueryHandler(
        IFamilyVacationPlannerContext context,
        ILogger<GetBookingByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BookingDto?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting booking {BookingId}", request.BookingId);

        var booking = await _context.Bookings
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.BookingId == request.BookingId, cancellationToken);

        if (booking == null)
        {
            _logger.LogWarning("Booking {BookingId} not found", request.BookingId);
            return null;
        }

        return booking.ToDto();
    }
}
