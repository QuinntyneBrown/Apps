using FamilyVacationPlanner.Core;

namespace FamilyVacationPlanner.Api.Features.Bookings;

public record BookingDto
{
    public Guid BookingId { get; init; }
    public Guid TripId { get; init; }
    public string Type { get; init; } = string.Empty;
    public string? ConfirmationNumber { get; init; }
    public decimal? Cost { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class BookingExtensions
{
    public static BookingDto ToDto(this Booking booking)
    {
        return new BookingDto
        {
            BookingId = booking.BookingId,
            TripId = booking.TripId,
            Type = booking.Type,
            ConfirmationNumber = booking.ConfirmationNumber,
            Cost = booking.Cost,
            CreatedAt = booking.CreatedAt,
        };
    }
}
