namespace Warranties.Core.Models;

public class Warranty
{
    public Guid WarrantyId { get; set; }
    public Guid PurchaseId { get; set; }
    public Guid UserId { get; set; }
    public string WarrantyType { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string? Provider { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
