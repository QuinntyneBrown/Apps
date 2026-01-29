namespace Letters.Core.Models;

public class Letter
{
    public Guid LetterId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime WrittenDate { get; set; } = DateTime.UtcNow;
    public DateTime ScheduledDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; } = DeliveryStatus.Pending;
    public bool HasBeenRead { get; set; }
    public DateTime? ReadDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public void MarkAsDelivered()
    {
        DeliveryStatus = DeliveryStatus.Delivered;
        ActualDeliveryDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsRead()
    {
        HasBeenRead = true;
        ReadDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsDueForDelivery()
    {
        return DeliveryStatus == DeliveryStatus.Pending && ScheduledDeliveryDate <= DateTime.UtcNow;
    }
}

public enum DeliveryStatus
{
    Pending,
    Delivered,
    Failed,
    Cancelled
}
