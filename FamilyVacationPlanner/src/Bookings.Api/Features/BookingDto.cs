namespace Bookings.Api.Features;

public record BookingDto
{
    public Guid BookingId { get; init; }
    public Guid TripId { get; init; }
    public string Type { get; init; } = string.Empty;
    public string? ConfirmationNumber { get; init; }
    public decimal? Cost { get; init; }
    public DateTime CreatedAt { get; init; }
}
