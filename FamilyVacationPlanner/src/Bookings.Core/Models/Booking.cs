namespace Bookings.Core.Models;

public class Booking
{
    public Guid BookingId { get; set; }
    public Guid TenantId { get; set; }
    public Guid TripId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? ConfirmationNumber { get; set; }
    public decimal? Cost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
