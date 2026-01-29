namespace Carpools.Core.Models;

public class Carpool
{
    public Guid CarpoolId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid ScheduleId { get; set; }
    public string DriverName { get; set; } = string.Empty;
    public string? DriverPhone { get; set; }
    public int AvailableSeats { get; set; }
    public string? PickupLocation { get; set; }
    public TimeSpan? PickupTime { get; set; }
    public string? Notes { get; set; }
    public bool IsConfirmed { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
