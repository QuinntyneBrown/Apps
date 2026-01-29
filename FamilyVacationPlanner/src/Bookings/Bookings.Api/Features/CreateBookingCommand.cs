using Bookings.Core;
using Bookings.Core.Models;
using MediatR;

namespace Bookings.Api.Features;

public record CreateBookingCommand : IRequest<BookingDto>
{
    public Guid TenantId { get; init; }
    public Guid TripId { get; init; }
    public string Type { get; init; } = string.Empty;
    public string? ConfirmationNumber { get; init; }
    public decimal? Cost { get; init; }
}

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
{
    private readonly IBookingsDbContext _context;

    public CreateBookingCommandHandler(IBookingsDbContext context)
    {
        _context = context;
    }

    public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = new Booking
        {
            BookingId = Guid.NewGuid(),
            TenantId = request.TenantId,
            TripId = request.TripId,
            Type = request.Type,
            ConfirmationNumber = request.ConfirmationNumber,
            Cost = request.Cost,
            CreatedAt = DateTime.UtcNow
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync(cancellationToken);

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
