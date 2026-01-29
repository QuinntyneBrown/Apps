namespace ServiceRecords.Core.Models;

public class ServiceRecord
{
    public Guid ServiceRecordId { get; set; }
    public Guid TenantId { get; set; }
    public Guid ApplianceId { get; set; }
    public DateTime ServiceDate { get; set; }
    public string? ServiceProvider { get; set; }
    public string? Description { get; set; }
    public decimal? Cost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
