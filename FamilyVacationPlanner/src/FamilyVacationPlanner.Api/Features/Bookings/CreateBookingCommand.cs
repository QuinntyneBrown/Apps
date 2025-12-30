using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Bookings;

public record CreateBookingCommand : IRequest<BookingDto>
{
    public Guid TripId { get; init; }
    public string Type { get; init; } = string.Empty;
    public string? ConfirmationNumber { get; init; }
    public decimal? Cost { get; init; }
}

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<CreateBookingCommandHandler> _logger;

    public CreateBookingCommandHandler(
        IFamilyVacationPlannerContext context,
        ILogger<CreateBookingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating booking for trip {TripId}, type: {Type}",
            request.TripId,
            request.Type);

        var booking = new Booking
        {
            BookingId = Guid.NewGuid(),
            TripId = request.TripId,
            Type = request.Type,
            ConfirmationNumber = request.ConfirmationNumber,
            Cost = request.Cost,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created booking {BookingId} for trip {TripId}",
            booking.BookingId,
            request.TripId);

        return booking.ToDto();
    }
}
