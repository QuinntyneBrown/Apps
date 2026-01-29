namespace Warranties.Core.Models;

public class Warranty
{
    public Guid WarrantyId { get; set; }
    public Guid TenantId { get; set; }
    public Guid ApplianceId { get; set; }
    public string? Provider { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? CoverageDetails { get; set; }
    public string? DocumentUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
