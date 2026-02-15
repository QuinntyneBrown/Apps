namespace DeliverySchedules.Core.Models;

public class DeliverySchedule
{
    public Guid DeliveryScheduleId { get; set; }
    public Guid TenantId { get; set; }
    public Guid LetterId { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public string DeliveryMethod { get; set; } = string.Empty;
    public string? RecipientContact { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
