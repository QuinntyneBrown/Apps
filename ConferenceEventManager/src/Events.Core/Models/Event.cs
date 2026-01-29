namespace Events.Core.Models;

public class Event
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public EventType EventType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Location { get; set; }
    public bool IsVirtual { get; set; }
    public string? Website { get; set; }
    public decimal? RegistrationFee { get; set; }
    public bool IsRegistered { get; set; }
    public bool DidAttend { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid? TenantId { get; set; }
}
