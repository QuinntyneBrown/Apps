namespace Appointments.Core.Models;

public class Appointment
{
    public Guid AppointmentId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string ProviderName { get; set; } = string.Empty;
    public string AppointmentType { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
